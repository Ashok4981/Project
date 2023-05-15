using System.ComponentModel.DataAnnotations;

namespace Online_Pizza_Ordering_System.Models
{
    public class ShoppingCartItem
    {
        [Key] 
        public int ShoppingCartItemId { get; set; }
        public Pizzas Pizza { get; set; }
        public int Amount { get; set; }
        public string ShoppingCartId { get; set; }
    }
}
