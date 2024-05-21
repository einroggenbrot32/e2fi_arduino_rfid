using Npgsql;
using System.Data;

namespace ZeiterfassungsAPI.Data
{
    public class PostgreSQL : IPostgreSQL
    {
        private readonly IConfiguration _config;
        
        public IDbConnection Connection { get; }

        public PostgreSQL(IConfiguration config)
        {
            _config = config;
            try
            {
                Connection = new Npgsql.NpgsqlConnection(_config.GetConnectionString("Connection"));
                Connection.Open();
                
            }catch (Exception ex)
            {
                throw new Exception("Error establishing Db connection: " + ex);
            }
        }
        public bool IsConnected()
        {
            return Connection.State == ConnectionState.Open;
        }

        public void ReConnect()
        {
            try
            {
                Connection.Open();
            }
            catch (Exception ex)
            { 
                throw new Exception("Error reconnection to Postgres DB: \n" + ex.Message); 
            }
        }
    }
}
