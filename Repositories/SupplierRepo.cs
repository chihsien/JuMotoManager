using JuMotoManager.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using MySql.Data;

namespace JuMotoManager.Repositories
{
    internal class SupplierRepo : RepositoryBase, ISupplierRepo
    {
        public SupplierRepo(IDbTransaction transaction)
            : base(transaction)
        {
        }

        public IEnumerable<Supplier> All()
        {
            return Connection.Query<Supplier>(
                "SELECT * FROM supplier ORDER BY supplier_sno",
                transaction: Transaction
            ).ToList();
        }

        public Supplier Find(int id)
        {
            return Connection.Query<Supplier>(
                "SELECT * FROM supplier WHERE supplier_sno = @sno",
                param: new { sno = id },
                transaction: Transaction
            ).FirstOrDefault();
        }

        public void Add(Supplier entity)
        {       
            // "INSERT INTO Breed(Name) VALUES(@Name); SELECT SCOPE_IDENTITY()",         
            entity.supplier_sno  = Connection.ExecuteScalar<int>(
                "INSERT INTO supplier(supplier_name,phone,date_created) VALUES(@name,@phone,now())",
                param: new { name = entity.supplier_name,
                             phone = entity.phone         
                 },
                transaction: Transaction
            );
        }

        public void Update(Supplier entity)
        {
            Connection.Execute(
                "UPDATE supplier SET supplier_name = @name, phone=@phone WHERE supplier_sno = @sno",
                param: new { sno = entity.supplier_sno,
                name = entity.supplier_name, phone = entity.phone },
                transaction: Transaction
            );
        }

        public void Delete(int id)
        {
            Connection.Execute(
                "DELETE FROM supplier WHERE supplier_sno = @sno",
                param: new { sno = id },
                transaction: Transaction
            );
        }

        public void Delete(Supplier entity)
        {
            Delete(entity.supplier_sno);
        }

        public Supplier FindByName(string name)
        {
            return Connection.Query<Supplier>(
                "SELECT * FROM Supplier WHERE supplier_name = @Name",
                param: new { Name = name },
                transaction: Transaction
            ).FirstOrDefault();
        }
    }
}
