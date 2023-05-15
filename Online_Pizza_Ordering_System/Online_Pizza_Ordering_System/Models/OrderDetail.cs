using System.ComponentModel.DataAnnotations;

namespace Online_Pizza_Ordering_System.Models
{
    public class OrderDetail
    {
        [Key]
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public Guid PizzaId { get; set; }
        public int Amount { get; set; }
 
        public decimal Price { get; set; }
        public virtual Pizzas Pizza { get; set; }
        public virtual Order Order { get; set; }
    }
}
