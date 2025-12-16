using Oracle.ManagedDataAccess.Client;
using System.Data;
using TMB_TEST_WEBAPI.Interface;

namespace TMB_TEST_WEBAPI.Service
{

    public class DbContext : IDBContext
    {
        private readonly IConfiguration _configuration;
        public DbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public int ExecuteNonQuery(string query, Dictionary<string, object> parameters)
        {
            string connectionString = _configuration.GetConnectionString("TMB_TEST");

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                connection.Open();

                using (OracleCommand command = new OracleCommand(query, connection))
                {
                    foreach (var param in parameters)
                    {
                        command.Parameters.Add(new OracleParameter(param.Key, param.Value));
                    }

                    return command.ExecuteNonQuery();
                }
            }
        }

        public DataTable DbQueryExe(string query)
        {
            string connectionString = _configuration.GetConnectionString("TMB_TEST");
            DataTable dataTable = new DataTable();

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                connection.Open();
                using (OracleCommand command = new OracleCommand(query, connection))
                {
                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        dataTable.Load(reader);
                    }
                }
            }

            
            return dataTable;
        }



    }
}
