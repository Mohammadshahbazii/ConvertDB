using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bussiness
{
    public interface IGeneralRepository : IDisposable
    {
        bool ValidateMappings(List<RelationItemModels> mappings, List<ColumnInfo> destinationColumns);
        List<ColumnInfo> GetColumnInfo(string connectionString, string tableName);
        Task TransferDataAsync(IProgress<int> progress, List<RelationItemModels> mappList, string oldDbConnectionString, string newDbConnectionString );

        Tuple<List<string>, bool> CreateDataBase();
    }
}
