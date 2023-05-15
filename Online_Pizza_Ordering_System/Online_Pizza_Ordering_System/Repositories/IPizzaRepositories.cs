using Online_Pizza_Ordering_System.Models;

namespace Online_Pizza_Ordering_System.Repositories
{
    public interface IPizzaRepositories
    {
        IEnumerable<Pizzas> Pizzas { get; }
        //IEnumerable<Pizzas> PizzasOfTheWeek { get; }

        Pizzas GetById(Guid? id);
        Task<Pizzas> GetByIdAsync(Guid? id);

        Pizzas GetByIdIncluded(Guid? id);
        Task<Pizzas> GetByIdIncludedAsync(Guid? id);

        bool Exists(Guid id);

        IEnumerable<Pizzas> GetAll();
        Task<IEnumerable<Pizzas>> GetAllAsync();

        IEnumerable<Pizzas> GetAllIncluded();
        Task<IEnumerable<Pizzas>> GetAllIncludedAsync();

        void Add(Pizzas pizza);
        void Update(Pizzas pizza);
        void Remove(Pizzas pizza);

        void SaveChanges();
        Task SaveChangesAsync();
    }
}
