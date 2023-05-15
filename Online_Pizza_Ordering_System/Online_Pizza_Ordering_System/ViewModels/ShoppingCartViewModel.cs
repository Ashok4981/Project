using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Online_Pizza_Ordering_System.Models;

namespace Online_Pizza_Ordering_System.ViewModels
{
    public class ShoppingCartViewModel
    {
        public ShoppingCart ShoppingCart { get; set; }
        public decimal ShoppingCartTotal { get; set; }
    }
}
