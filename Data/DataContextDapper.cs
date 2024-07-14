using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;

namespace HelloWorld.data
{
    public class DataContextDapper
    {
        private string _connectionString = "Server=localhost;Database=AtakanDatabase;TrustServerCertificate=true;Trusted_Connection=true";

        public IEnumerable<T> LoadData<T>(string sql)
        {
            IDbConnection dbConnection = new SqlConnection(_connectionString);
            return dbConnection.Query<T>(sql);
        }

        public T LoadDataSingle<T>(string sql)
        {
            IDbConnection dbConnection = new SqlConnection(_connectionString);
            return dbConnection.QuerySingle<T>(sql);
        }
        public bool ExecuteSql(string sql, object? parameters = null)
        {
            IDbConnection dbConnection = new SqlConnection(_connectionString);
            int rowsAffected = dbConnection.Execute(sql, parameters);
            return rowsAffected > 0;
        }
        public int ExecuteSqlWithRowCount(string sql)
        {
            IDbConnection dbConnection = new SqlConnection(_connectionString);
            return dbConnection.Execute(sql);
        }



    }
}