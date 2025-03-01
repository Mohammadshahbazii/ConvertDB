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
        DataBaseInfo GetByID(int id);
        int Create(DataBaseInfo dataBaseInfo);
        bool Delete(int id);
    }
}
