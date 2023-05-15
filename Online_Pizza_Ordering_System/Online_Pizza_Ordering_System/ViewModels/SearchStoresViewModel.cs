using Online_Pizza_Ordering_System.Models;

namespace Online_Pizza_Ordering_System.ViewModels
{
    public class SearchStoresViewModel
    {
        public string SearchText { get; set; }
        public IEnumerable<Stores> Storeslist { get; set; }
    }
}
