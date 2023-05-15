using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Identity;
using Online_Pizza_Ordering_System.ViewModels;

namespace Online_Pizza_Ordering_System.Models
{
    public class CardPayment
    {
        [Key]
        public Guid Id { get; set; }

        public string username { get; set; }

        public string CardNumber { get; set; }

        public int CVC { get; set; }
        [NotMapped]
        public DateOnly ExpiryDate { get; set; }

        public decimal? OrderTotal { get; set; }

        public decimal? Balance { get; set; }

        public string UserId { get; set; }

        [AllowNull]
        public IdentityUser? User { get; set; }
    }
}
