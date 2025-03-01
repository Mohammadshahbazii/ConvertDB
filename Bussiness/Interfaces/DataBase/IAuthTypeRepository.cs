using Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness
{
    public interface IAuthTypeRepository : IDisposable
    {
        DataBaseAuthenticationType GetByID(int id);
    }
}
