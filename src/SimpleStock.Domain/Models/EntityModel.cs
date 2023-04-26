namespace SimpleStock.Domain.Models;
public abstract class EntityModel
{
    protected EntityModel()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; private set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
}
