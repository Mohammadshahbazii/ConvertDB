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
    internal class AuthTypeRepository : IAuthTypeRepository
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public DataBaseAuthenticationType GetByID(int id)
        {
            DataBaseAuthenticationType authenticationType = new DataBaseAuthenticationType();
            try
            {
                var modelDB = AuthenticationType.GetByID(id);
                authenticationType.TypeID = Convert.ToInt32(modelDB["AuthTypeID"].ToString());
                authenticationType.Type = modelDB["Title"].ToString();
                return authenticationType;
            }
            catch (Exception)
            {
                return authenticationType;
            }
        }
    }
}
