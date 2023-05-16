using AutoMapper;
using SimpleStock.Application.Interfaces;
using SimpleStock.Data.Interfaces;
using SimpleStock.Domain.DTOs.Customer;
using SimpleStock.Domain.Models;
using SimpleStock.Exception;

namespace SimpleStock.Application.Services;
public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public CustomerService(
        ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<ICollection<CustomerResponseDto>> GetAll()
    {
        var customers = await _customerRepository.GetAll();
        return customers
            .Select(p => _mapper.Map<CustomerResponseDto>(p))
            .OrderBy(p => p.Name)
            .ToList();
    }

    public async Task<CustomerResponseDto?> GetById(Guid id)
    {
        var customer = await _customerRepository.GetById(id);
        if (customer == null) ThrowNotFound();

        return _mapper.Map<CustomerResponseDto>(customer);
    }

    public async Task<ICollection<CustomerModel>> GetByName(string name)
    {
        var customer = await _customerRepository.GetByName(name);
        if (customer == null) ThrowNotFound();

        return customer!;
    }

    public async Task<CustomerResponseDto?> AddCustomer(CustomerCreateRequestDto request)
    {
        await CheckIfExistsSameName(request.Name);

        var customerToPersist = _mapper.Map<CustomerModel>(request);
        await _customerRepository.Add(customerToPersist);

        var customer = _mapper.Map<CustomerResponseDto>(customerToPersist);

        return customer;
    }

    public async Task<CustomerResponseDto?> UpdateCustomer(Guid id, CustomerUpdateRequestDto request)
    {

        var customer = await _customerRepository.GetById(id);
        if (customer == null) ThrowNotFound();

        await CheckIfExistsSameName(request.Name);

        _mapper.Map(request, customer);
        customer!.Addresses.ToList().ForEach(a => a.CustomerId = customer.Id);

        await _customerRepository.Update(customer!);

        var responseCustomer = _mapper.Map<CustomerResponseDto>(customer);

        return responseCustomer;
    }

    public async Task<bool> DeleteCustomer(Guid id)
    {
        var customer = await _customerRepository.GetById(id);
        if (customer == null)
        {
            var message = "Não há cliente cadastrado com o ID informado.";
            throw new NotFoundException(message);
        }

        return await _customerRepository.Delete(customer);
    }

    private static void ThrowNotFound()
    {
        var message = "Não há cliente cadastrado com o ID informado.";
        throw new NotFoundException(message);
    }

    private async Task CheckIfExistsSameName(string name)
    {
        var findExistsWithName = await _customerRepository.GetByName(name);
        var checkExistsWithName = findExistsWithName
            .Where(c => c.Name == name)
            .ToList();

        if (checkExistsWithName.Count > 0)
        {
            var message = "Já existe um ítem de cliente com o nome fornecido";
            throw new AlreadyExistsException(message);
        }
    }

}