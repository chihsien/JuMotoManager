using JuMotoManager.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using MySql.Data;

namespace JuMotoManager.Repositories
{
    internal class PartsFuncRepo : RepositoryBase, IPartsFuncRepo
    {
        public PartsFuncRepo(IDbTransaction transaction)
            : base(transaction)
        {
        }

        public IEnumerable<PartsFunc> All()
        {
            return Connection.Query<PartsFunc>(
                "SELECT * FROM parts_function ORDER BY func_sno",
                transaction: Transaction
            ).ToList();
        }

        public PartsFunc Find(int id)
        {
            return Connection.Query<PartsFunc>(
                "SELECT * FROM parts_function WHERE func_sno = @sno",
                param: new { sno = id },
                transaction: Transaction
            ).FirstOrDefault();
        }

        public void Add(PartsFunc entity)
        {       
            // "INSERT INTO Breed(Name) VALUES(@Name); SELECT SCOPE_IDENTITY()",         
            entity.func_sno  = Connection.ExecuteScalar<int>(
                "INSERT INTO parts_function(func_name,date_created) VALUES(@name,now())",
                param: new { name = entity.func_sno},
                transaction: Transaction
            );
        }

        public void Update(PartsFunc entity)
        {
            Connection.Execute(
                "UPDATE parts_function SET func_name = @name WHERE func_sno = @sno",
                param: new { sno = entity.func_sno,
                             name = entity.func_name },
                transaction: Transaction
            );
        }

        public void Delete(int id)
        {
            Connection.Execute(
                "DELETE FROM parts_function WHERE func_sno = @sno",
                param: new { sno = id },
                transaction: Transaction
            );
        }

        public void Delete(PartsFunc entity)
        {
            Delete(entity.func_sno);
        }

        public PartsFunc FindByName(string name)
        {
            return Connection.Query<PartsFunc>(
                "SELECT * FROM parts_function WHERE func_sno = @Name",
                param: new { Name = name },
                transaction: Transaction
            ).FirstOrDefault();
        }
    }
}
