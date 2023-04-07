using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStock.Domain.Model;
public class CustomerModel : BaseEntityModel
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string CPF { get; set; } = null!;
    public DateOnly BirthDate { get; set; }
    public IEnumerable<AddressModel> Addresses { get; set; } = null!;
}
