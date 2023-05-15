using Online_Pizza_Ordering_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Online_Pizza_Ordering_System.Repositories
{

    public interface IStoresRepositories
    {
        IEnumerable<Stores> Stores { get; }

        Stores GetById(Guid? id);
        Task<Stores> GetByIdAsync(Guid? id);

        bool Exists(Guid id);

        IEnumerable<Stores> GetAll();
        Task<IEnumerable<Stores>> GetAllAsync();

        void Add(Stores category);
        void Update(Stores category);
        void Remove(Stores category);

        void SaveChanges();
        Task SaveChangesAsync();
    }
}
