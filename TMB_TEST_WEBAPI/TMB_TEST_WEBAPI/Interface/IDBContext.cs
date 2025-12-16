using System.Data;

namespace TMB_TEST_WEBAPI.Interface
{
    public interface IDBContext
    {
        public DataTable DbQueryExe(string query);
        public int ExecuteNonQuery(string query, Dictionary<string, object> parameters);
    }
}
