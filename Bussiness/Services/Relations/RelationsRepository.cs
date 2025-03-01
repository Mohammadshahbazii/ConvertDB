using DataLayer;
using Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness
{
    public class RelationsRepository : IRelationsRepository
    {
        public bool Create(RelationItemModels model)
        {
            try
            {
                return Relations.Create(model);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public List<RelationItemModels> GetRelationsByEventID(int eventID)
        {
            // Get the DataTable from the database
            DataTable dataTable = Relations.GetRelationsByEventID(eventID);

            // Convert the DataTable to a List<RelationItemModels>
            List<RelationItemModels> relations = new List<RelationItemModels>();

            foreach (DataRow row in dataTable.Rows)
            {
                relations.Add(new RelationItemModels
                {
                    ID = Convert.ToInt32(row["RelationID"]),
                    SourceTableName = row["SourceTableName"].ToString(),
                    SourceColumnName = row["SourceColumnName"].ToString(),
                    DestinationTableName = row["DestinationTableName"].ToString(),
                    DestinationColumnName = row["DestinationColumnName"].ToString(),
                    EventID = Convert.ToInt32(row["EventID"])
                });
            }

            return relations;
        }

    }
}
