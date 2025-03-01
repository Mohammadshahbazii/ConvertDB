using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
	public class EventsListModels
	{
        public int ID { get; set; }
        public string Name { get; set; }
        public string CreateDate { get; set; }
        public string Status { get; set; }
    }

    public class EventsItemModels
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public int Status { get; set; }
        public int DBSourceID { get; set; }
        public int DBDestinationID { get; set; }
    }
}
