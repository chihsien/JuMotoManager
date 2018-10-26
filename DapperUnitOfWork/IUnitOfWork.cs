using JuMotoManager.Repositories;
using System;

namespace JuMotoManager.DapperUnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IPartsFuncRepo PartsFuncRepo { get; }
        IPartsInfoRepo PartsInfoRepo { get; }
        ISupplierRepo SupplierRepo { get; }

        void Commit();
    }
}