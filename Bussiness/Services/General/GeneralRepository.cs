using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness
{
    public class GeneralRepository : IGeneralRepository
    {
        public Tuple<List<string>,bool> CreateDataBase()
        {
            List<string> messages = new List<string>();
            try
            {
                return General.CreateDatabase();
            }
            catch (Exception)
            {
                return Tuple.Create(messages, false);
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
