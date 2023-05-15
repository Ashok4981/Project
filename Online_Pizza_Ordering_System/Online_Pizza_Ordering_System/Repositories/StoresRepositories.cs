using Microsoft.EntityFrameworkCore;
using Online_Pizza_Ordering_System.Data;
using Online_Pizza_Ordering_System.Models;

namespace Online_Pizza_Ordering_System.Repositories
{
    public class StoresRepositories : IStoresRepositories
    {
        private readonly AppDbContext _context;

        public StoresRepositories(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Stores> Stores => _context.Stores.Include(x => x.Pizzas); //include here

        public void Add(Stores store)
        {
            _context.Add(store);
        }

        public IEnumerable<Stores> GetAll()
        {
           return _context.Stores.ToList();
        }

        public async Task<IEnumerable<Stores>> GetAllAsync()
        {
            return await _context.Stores.ToListAsync();
        }

        public Stores GetById(Guid? id)
        {
            return _context.Stores.FirstOrDefault(p => p.StoreId == id);
        }

        public async Task<Stores> GetByIdAsync(Guid? id)
        {
            return await _context.Stores.FirstOrDefaultAsync(p => p.StoreId == id);
        }

        public bool Exists(Guid id)
        {
            return _context.Stores.Any(p => p.StoreId == id);
        }

        public void Remove(Stores store)
        {
            _context.Remove(store);
        }

        public void SaveChanges ()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(Stores store)
        {
            _context.Update(store);
        }
    }
}
