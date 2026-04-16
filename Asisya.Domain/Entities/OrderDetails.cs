using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asisya.Domain.Entities
{
    public class OrderDetails
    {
        public Guid OrderId { get; set; }
        public Order Order { get; set; } = new Order();

        public Guid ProductId { get; set; }
        public Product Product { get; set; } = new Product();

        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
