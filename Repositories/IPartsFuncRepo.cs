using System.Collections.Generic;
using JuMotoManager.Models;

namespace JuMotoManager.Repositories
{
    public interface IPartsFuncRepo
    {
        void Add(PartsFunc entity);
        IEnumerable<PartsFunc> All();
        void Delete(int id);
        void Delete(PartsFunc entity);
        PartsFunc Find(int id);
        PartsFunc FindByName(string name);
        void Update(PartsFunc entity);
    }
}