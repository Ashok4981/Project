using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Online_Pizza_Ordering_System.Models
{
    public class Pizzas
    {
       
        [Key]
        public Guid Id { get; set; }

        
        public string Name { get; set; }

        
        public decimal Price { get; set; }

        
        public string Description { get; set; }

        public string? PizzaUrl { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }

        [ForeignKey("Stores")]
        public Guid StoreId { get; set; }

        public virtual Stores? store { get; set; }
    }
}
