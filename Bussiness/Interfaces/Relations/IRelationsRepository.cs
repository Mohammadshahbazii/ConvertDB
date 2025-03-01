using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness
{
    public interface IRelationsRepository : IDisposable
    {
        List<RelationItemModels> GetRelationsByEventID(int eventID);

        List<RelationItemModels> Search(string search);

        bool Create(RelationItemModels model);

        bool Update(RelationItemModels model);

        bool Delete(int id);

        RelationItemModels GetById(int id);
    }
}
