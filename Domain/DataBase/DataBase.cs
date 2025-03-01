using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class DataBaseAuthenticationType
    {
        public int TypeID { get; set; }
        public string Type { get; set; }
    }
    public class DataBaseInfo
    {
        public string ServerAddress { get; set; }
        public string DataBaseName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DataBaseAuthenticationType AuthenticationType { get; set; }
        public int TableCount { get; set; }
    }
}
