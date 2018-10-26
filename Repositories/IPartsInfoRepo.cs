using System.Collections.Generic;
using JuMotoManager.Models;

namespace JuMotoManager.Repositories
{
    public interface IPartsInfoRepo
    {
        void Add(PartsInfo entity);
        IEnumerable<PartsInfo> All();
        void Delete(int id);
        void Delete(PartsInfo entity);
        PartsInfo Find(int id);
        PartsInfo FindByName(string name);
        void Update(PartsInfo entity);
    }
}