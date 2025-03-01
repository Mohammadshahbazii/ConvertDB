using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness
{
    public interface IDataBaseRepository
    {
        List<string> GetTables(string connectionString);
        List<string> GetColumns(string connectionString, string tableName);
        DataBaseInfo GetByID(int id);
        int Create(DataBaseInfo dataBaseInfo);
        bool Update(DataBaseInfo dataBaseInfo);
        bool Delete(int id);
    }
}
