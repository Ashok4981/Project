using Online_Pizza_Ordering_System.Models;
using System.ComponentModel;

namespace Online_Pizza_Ordering_System.ViewModel
{
    public class SearchPizzasViewModel
    {
        public string SearchText { get; set; }

        //public IEnumerable<string> SearchListExamples { get; set; }

        public IEnumerable<Pizzas> PizzaList { get; set; }
    }
}
