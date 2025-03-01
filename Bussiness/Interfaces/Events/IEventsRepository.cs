using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness
{
    public interface IEventsRepository : IDisposable
    {
        List<EventsListModels> GetEvents();

        EventsItemModels GetByID(int id);

        bool Delete(int id);

        bool Update(EventsItemModels model);

        bool Create(EventsItemModels model);
    }
}
