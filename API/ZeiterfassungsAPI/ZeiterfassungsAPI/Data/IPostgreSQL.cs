using System.Data;

namespace ZeiterfassungsAPI.Data
{
    public interface IPostgreSQL
    {
        IDbConnection Connection { get; }
        bool IsConnected();
        void ReConnect();
    }
}
