using DataLayer;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness
{
    public class DataBaseRepository : IDataBaseRepository
    {
        public int Create(DataBaseInfo dataBaseInfo)
        {
			try
			{
                return DataBase.Create(dataBaseInfo);
            }
			catch (Exception)
			{
                return 0;
			}
        }

        public bool Delete(int id)
        {
            try
            {
                return DataBase.Delete(id);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public DataBaseInfo GetByID(int id)
        {
            
            DataBaseInfo dataBaseInfo = new DataBaseInfo();
            try
            {
                using (IAuthTypeRepository authTypeRepository = new AuthTypeRepository())
                {
                    var modelDB = DataBase.GetByID(id);
                    dataBaseInfo = new DataBaseInfo()
                    {
                        ServerAddress = modelDB["ServerName"].ToString(),
                        DataBaseName = modelDB["DBName"].ToString(),
                        Password = modelDB["Password"].ToString(),
                        Username = modelDB["Username"].ToString(),
                        AuthenticationType = authTypeRepository.GetByID(Convert.ToInt32(modelDB["AuthTypeID"]))
                    };
                    return dataBaseInfo;
                }
                
            }
            catch (Exception)
            {
                return dataBaseInfo;
            }
        }
    }
}
