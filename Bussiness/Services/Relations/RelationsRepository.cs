using DataLayer;
using Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
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

        public bool Delete(int id)
        {
            try
            {
                return Relations.Delete(id);
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

        public RelationItemModels GetById(int id)
        {
            RelationItemModels model = new RelationItemModels();
            try
            {
                var modelDB = Relations.GetByID(id);
                model = new RelationItemModels()
                {
                    ID = Convert.ToInt32(modelDB["RelationID"]),
                    SourceTableName = modelDB["SourceTableName"].ToString(),
                    SourceColumnName = modelDB["SourceColumnName"].ToString(),
                    DestinationTableName = modelDB["DestinationTableName"].ToString(),
                    DestinationColumnName = modelDB["DestinationColumnName"].ToString(),
                    FilterCondition = modelDB.Table.Columns.Contains("FilterCondition") ? modelDB["FilterCondition"].ToString() : string.Empty,
                    EventID = Convert.ToInt32(modelDB["EventID"])
                };
                return model;
            }
            catch (Exception)
            {
                return model;
            }
        }

        public List<RelationItemModels> GetRelationsByEventID(int eventID)
        {
            List<RelationItemModels> relations = new List<RelationItemModels>();
            try
            {
                // Get the DataTable from the database
                DataTable dataTable = Relations.GetRelationsByEventID(eventID);

                // Convert the DataTable to a List<RelationItemModels>

                foreach (DataRow row in dataTable.Rows)
                {
                    relations.Add(new RelationItemModels
                    {
                        ID = Convert.ToInt32(row["RelationID"]),
                        SourceTableName = row["SourceTableName"].ToString(),
                        SourceColumnName = row["SourceColumnName"].ToString(),
                        DestinationTableName = row["DestinationTableName"].ToString(),
                        DestinationColumnName = row["DestinationColumnName"].ToString(),
                        FilterCondition = dataTable.Columns.Contains("FilterCondition") ? row["FilterCondition"].ToString() : string.Empty,
                        EventID = Convert.ToInt32(row["EventID"])
                    });
                }

                return relations;
            }
            catch (Exception)
            {
                return relations;
            }
        }

        public List<RelationItemModels> Search(string search)
        {
            List<RelationItemModels> relations = new List<RelationItemModels>();
            try
            {
                // Get the DataTable from the database
                DataTable dataTable = Relations.Search(search);

                // Convert the DataTable to a List<RelationItemModels>

                foreach (DataRow row in dataTable.Rows)
                {
                    relations.Add(new RelationItemModels
                    {
                        ID = Convert.ToInt32(row["RelationID"]),
                        SourceTableName = row["SourceTableName"].ToString(),
                        SourceColumnName = row["SourceColumnName"].ToString(),
                        DestinationTableName = row["DestinationTableName"].ToString(),
                        DestinationColumnName = row["DestinationColumnName"].ToString(),
                        FilterCondition = dataTable.Columns.Contains("FilterCondition") ? row["FilterCondition"].ToString() : string.Empty,
                        EventID = Convert.ToInt32(row["EventID"])
                    });
                }

                return relations;
            }
            catch (Exception)
            {
                return relations;
            }
        }

        public bool Update(RelationItemModels model)
        {
            try
            {
                return Relations.Update(model);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
