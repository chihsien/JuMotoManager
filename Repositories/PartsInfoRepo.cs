using JuMotoManager.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using MySql.Data;

namespace JuMotoManager.Repositories
{
    internal class PartsInfoRepo : RepositoryBase, IPartsInfoRepo
    {
        public PartsInfoRepo(IDbTransaction transaction)
            : base(transaction)
        {
        }

        public IEnumerable<PartsInfo> All()
        {
            return Connection.Query<PartsInfo>(
                "SELECT * FROM parts_info ORDER BY part_sno",
                transaction: Transaction
            ).ToList();
        }

        public PartsInfo Find(int id)
        {
            return Connection.Query<PartsInfo>(
                "SELECT * FROM parts_info WHERE part_sno = @sno",
                param: new { sno = id },
                transaction: Transaction
            ).FirstOrDefault();
        }

        public void Add(PartsInfo entity)
        {       
            // "INSERT INTO Breed(Name) VALUES(@Name); SELECT SCOPE_IDENTITY()",         
            entity.func_sno  = Connection.ExecuteScalar<int>(
                "INSERT INTO parts_info(part_number,supplier_sno,func_sno, part_name,list_price,street_price,cost_price,date_created,date_updated) VALUES(part_number=@part_number,supplier_sno=@supplier_sno,func_sno=@func_sno,part_name=@part_name,list_price=@list_price,street_price=@street_price,cost_price=@cost_price,now(),now())",
                param: new { name = entity.func_sno,
                            part_number= entity.part_number,
                            supplier_sno= entity.supplier_sno,
                            func_sno=entity.func_sno,
                            part_name=entity.part_name,
                            list_price=entity.list_price,
                            street_price=entity.street_price,
                            cost_price=entity.cost_price    
                },
                transaction: Transaction
            );

            

        }

        public void Update(PartsInfo entity)
        {
            Connection.Execute(
                "UPDATE parts_info SET part_number= @part_number,supplier_sno= @supplier_sno,func_sno=@func_sno,part_name=@part_name,list_price=@list_price,street_price=@street_price,cost_price=@cost_price WHERE part_sno = @sno",
                param: new { sno = entity.func_sno,
                            part_number= entity.part_number,
                            supplier_sno= entity.supplier_sno,
                            func_sno=entity.func_sno,
                            part_name=entity.part_name,
                            list_price=entity.list_price,
                            street_price=entity.street_price,
                            cost_price=entity.cost_price    
                },
                transaction: Transaction
            );
        }

                            

        public void Delete(int id)
        {
            Connection.Execute(
                "DELETE FROM parts_info WHERE part_sno = @sno",
                param: new { sno = id },
                transaction: Transaction
            );
        }

        public void Delete(PartsInfo entity)
        {
            Delete(entity.func_sno);
        }

        public PartsInfo FindByName(string name)
        {
            return Connection.Query<PartsInfo>(
                "SELECT * FROM parts_info WHERE part_sno = @Name",
                param: new { Name = name },
                transaction: Transaction
            ).FirstOrDefault();
        }
    }
}
