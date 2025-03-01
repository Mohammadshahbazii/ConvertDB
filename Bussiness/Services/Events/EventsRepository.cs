using DataLayer;
using Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Bussiness
{
    public class EventsRepository : IEventsRepository
    {
        public bool Create(EventsItemModels eventItem)
        {
            try
            {
                return Events.Create(eventItem);
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
                var eventItem = Events.GetByID(id);
                int sourceDB_ID = Convert.ToInt32(eventItem["DBSourceID"]);
                int destinationDB_ID = Convert.ToInt32(eventItem["DBDestinationID"]);

                bool isSuccess = Events.Delete(id);
                bool isDeleteSourceDB = DataBase.Delete(sourceDB_ID);
                bool isDeleteDestinationDB = DataBase.Delete(destinationDB_ID);

                return (isSuccess && isDeleteSourceDB && isDeleteDestinationDB);
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

        public EventsItemModels GetByID(int id)
        {
            EventsItemModels model = new EventsItemModels();
            try
            {
                var modelDB = Events.GetByID(id);
                model.ID = Convert.ToInt32(modelDB["EventID"].ToString());
                model.Name = modelDB["Title"].ToString();
                model.DBSourceID = Convert.ToInt32(modelDB["DBSourceID"].ToString());
                model.DBDestinationID = Convert.ToInt32(modelDB["DBDestinationID"].ToString());

                return model;
            }
            catch (Exception)
            {
                return model;
            }
        }

        public List<EventsListModels> GetEvents()
        {
            List<EventsListModels> models = new List<EventsListModels>();
            try
            {
                var modelDB = Events.GetEventsData();
                foreach (DataRow item in modelDB.Rows) 
                {
                    models.Add(new EventsListModels() 
                    {
                        ID = Convert.ToInt32(item["EventID"].ToString()),
                        CreateDate = DateConvertor.ToShamsiDetailed(Convert.ToDateTime(item["CreateDate"].ToString())),
                        Name = item["Title"].ToString(),
                        Status = Helpers.GetEventType(Convert.ToInt32(item["Status"].ToString()))
                    });
                }
                return models;
            }
            catch (Exception)
            {
                return models;
            }
        }

        public bool Update(EventsItemModels model)
        {
            try
            {
                return Events.Update(model);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
