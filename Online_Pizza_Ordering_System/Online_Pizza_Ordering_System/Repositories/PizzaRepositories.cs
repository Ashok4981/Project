using Microsoft.EntityFrameworkCore;
using Online_Pizza_Ordering_System.Data;
using Online_Pizza_Ordering_System.Models;

namespace Online_Pizza_Ordering_System.Repositories
{
    public class PizzaRepositories : IPizzaRepositories
    {
        private readonly AppDbContext _context;

        public PizzaRepositories(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Pizzas> Pizzas => _context.Pizzas.Include(p => p.store); //include here

        //public IEnumerable<Pizzas> PizzasOfTheWeek => _context.Pizzas.Where(p => p.IsPizzaOfTheWeek).Include(p => p.Category);

        public void Add(Pizzas pizza)
        {
            _context.Add(pizza);
        }

        public IEnumerable<Pizzas> GetAll()
        {
            return _context.Pizzas.ToList();
        }

        public async Task<IEnumerable<Pizzas>> GetAllAsync()
        {
            return await _context.Pizzas.ToListAsync();
        }

        public async Task<IEnumerable<Pizzas>> GetAllIncludedAsync()
        {
            return await _context.Pizzas.Include(p => p.store).ToListAsync();
        }

        public IEnumerable<Pizzas> GetAllIncluded()
        {
            return _context.Pizzas.Include(p => p.store).ToList();
        }

        public Pizzas GetById(Guid? id)
        {
            return _context.Pizzas.FirstOrDefault(p => p.Id == id);
        }

        public async Task<Pizzas> GetByIdAsync(Guid? id)
        {
            return await _context.Pizzas.FirstOrDefaultAsync(p => p.Id == id);
        }

        public Pizzas GetByIdIncluded(Guid? id)
        {
            return _context.Pizzas.Include(p => p.store).FirstOrDefault(p => p.Id == id);
        }

        public async Task<Pizzas> GetByIdIncludedAsync(Guid? id)
        {
            return await _context.Pizzas.Include(p => p.store).FirstOrDefaultAsync(p => p.Id == id);
        }

        public bool Exists(Guid id)
        {
            return _context.Pizzas.Any(p => p.Id == id);
        }

        public void Remove(Pizzas pizza)
        {
            _context.Remove(pizza);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Update(Pizzas pizza)
        {
            _context.Update(pizza);
        }

    }
}
