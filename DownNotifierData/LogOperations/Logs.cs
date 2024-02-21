using Dapper;
using DownNotifierEntities.Context;
using DownNotifierEntities.Entities;
using Newtonsoft.Json.Linq;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DownNotifierData
{
    /// <summary>
    /// Allows log records to be saved in the database using dapper
    /// </summary>
    public class Logs
    {
        public static string connectionstring;

        public static IEnumerable<dynamic> Query(string sql)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionstring))
            {
                sqlConnection.Open();
                return sqlConnection.Query(sql);
            }
        }

        public static void LogTut(string className = "", string fonkName = "", string ErrorText = "", string RequestModal = "")
        {
            Log log = new Log
            {
                ErrorText = ErrorText,
                Fonksiyon = fonkName,
                RequestModel = RequestModal,
                Url = className + "/" + fonkName
            };

            using (IDbConnection db = new SqlConnection(connectionstring))
            {
                string sqlQuery = "Insert Into TblLog (ErrorText, Fonksiyon, RequestModel, Url, CreatedOn) Values(@ErrorText, @Fonksiyon, @RequestModel, @Url, Getdate())";
                int rowsAffected = db.Execute(sqlQuery, log);
            }

        }

    }
}
