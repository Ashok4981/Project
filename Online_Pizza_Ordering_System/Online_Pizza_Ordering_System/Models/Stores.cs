using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Online_Pizza_Ordering_System.Models
{
    public class Stores
    {

        public Stores()
        {
            Pizzas = new HashSet<Pizzas>();
        }
        [Key]
        public Guid StoreId { get; set; }

        public string StoreName { get; set;}

        public string AddressLine1 { get; set;}

        public string AddressLine2 { get; set;}

        public string ZipCode { get; set; }

        public string City { get; set; }

        public string State { get; set; }
       
        public string? StoreUrl { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }
        public string PhoneNumber { get; set; }

        public virtual ICollection<Pizzas> Pizzas { get; set; }
    }
}
