using System;

namespace NHibernateDemo.Entities
{
    public class Order
    {
        public virtual Guid Id { get; set; }
        public virtual DateTime Ordered { get; set; }
        public virtual DateTime? Shipped { get; set; }
        public virtual Location ShipTo { get; set; }
        public virtual Customer Customer { get; set; }

        public override string ToString()
        {
            return $"OrderId: {Id}";
        }
    }
}