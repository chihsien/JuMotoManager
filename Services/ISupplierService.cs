using System;
using System.Collections.Generic;
using JuMotoManager.Models;

namespace JuMotoManager.Services
{
    public interface ISupplierService
    {
        Supplier Get(int id);
        List<Supplier> Get();
        void Add(Supplier b);
        void Edit(Supplier b);
        void Delete(int id);
    }
}
