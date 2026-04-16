using Asisya.Domain.Common;
using Asisya.Domain.Entities;

public class Order : BaseEntity
{
    public DateTime OrderDate { get; set; }

    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; } = new Customer();

    public Guid EmployeeId { get; set; }
    public Employees Employees { get; set; } = new Employees();

    public Guid ShipperId { get; set; }
    public Shipper Shipper { get; set; } = new Shipper();

    public ICollection<OrderDetails> OrderDetails { get; set; } = new List<OrderDetails>();
}