

namespace FoodDelivery.Domain.Entities
{
    public class Customer
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = null!;
        public string Phone { get; set; } = null!;
    }
}
