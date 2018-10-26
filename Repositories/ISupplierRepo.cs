using System.Collections.Generic;
using JuMotoManager.Models;

namespace JuMotoManager.Repositories
{
    public interface ISupplierRepo
    {
        void Add(Supplier entity);
        IEnumerable<Supplier> All();
        void Delete(int id);
        void Delete(Supplier entity);
        Supplier Find(int id);
        Supplier FindByName(string name);
        void Update(Supplier entity);
    }
}