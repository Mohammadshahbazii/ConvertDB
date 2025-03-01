using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class RelationItemModels
    {
        public int ID { get; set; }
        public string SourceTableName { get; set; }
        public string SourceColumnName { get; set; }
        public string DestinationTableName { get; set; }
        public string DestinationColumnName { get; set; }
        public int EventID { get; set; }

    }
}
