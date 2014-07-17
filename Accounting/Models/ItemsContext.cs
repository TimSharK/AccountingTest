using System.Collections.Generic;
using System.Data;
using System.Data.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using Newtonsoft.Json;

namespace Accounting.Models
{
    public partial class ItemsDb
    {
        public string GetAccountingReportJson(long[] parameterIds)
        {
            var dt = new DataTable();
            dt.Columns.Add("ParameterId", typeof (long));

            foreach (var parameterId in parameterIds)
                dt.Rows.Add(parameterId);

            var connectionString = ((EntityConnection)Connection).StoreConnection.ConnectionString;
            var connection = new SqlConnection(connectionString);

            connection.Open();

            using (connection)
            {
                var getAccountingReportCommand = new SqlCommand("dbo.sp_GetAccountingReport", connection);
                getAccountingReportCommand.CommandType = CommandType.StoredProcedure;

                var sqlParameter = new SqlParameter("ParameterIds", dt);
                getAccountingReportCommand.Parameters.Add(sqlParameter);

                var sqlDataReader = getAccountingReportCommand.ExecuteReader();

                var rawObject = Serialize(sqlDataReader);
                var json = JsonConvert.SerializeObject(rawObject, Formatting.None);

                return json;
            }
        }

        public IEnumerable<Dictionary<string, object>> Serialize(SqlDataReader reader)
        {
            var results = new List<Dictionary<string, object>>();
            var cols = new List<string>();
            for (var i = 0; i < reader.FieldCount; i++)
                cols.Add(reader.GetName(i));

            while (reader.Read())
                results.Add(SerializeRow(cols, reader));

            return results;
        }

        private Dictionary<string, object> SerializeRow(IEnumerable<string> cols, SqlDataReader reader)
        {
            return cols.ToDictionary(col => col, col => reader[col]);
        }
    }
}