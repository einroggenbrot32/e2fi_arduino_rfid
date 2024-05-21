using ZeiterfassungsAPI.Data;
using ZeiterfassungsAPI.Models;

namespace ZeiterfassungsAPI.Services
{
    public interface ISessionService
    {

        public IEnumerable<Session> GetSessions(bool onlyActive);
        public Session GetSessionExistingOrNew(User user);

        public Session EndSession(Session session);

        public bool SaveChanges();

        public bool hasOpenSession(User user);
    }

}
