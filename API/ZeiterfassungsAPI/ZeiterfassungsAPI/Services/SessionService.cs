using System.Collections;
using ZeiterfassungsAPI.Data;
using ZeiterfassungsAPI.Models;
using System.Linq;

namespace ZeiterfassungsAPI.Services
{
    public class SessionService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;
        private readonly ILogger _logger;

        public SessionService(
        AppDbContext context,
        ILogger logger,
        IConfiguration config)
        {
            _context = context;
            _config = config;   
            _logger = logger;   
        }

        /// <summary>
        ///  Gibt alle Sessions / alle aktiven Sessions zurück.
        /// </summary>
        /// <param name="onlyActive"></param>
        /// <returns></returns>
        public IEnumerable<Session> GetSessions(bool onlyActive)
        {
            if (onlyActive)
            {
                return _context.Sessions.ToList();
            }
            else
            {
                return _context.Sessions.Where(s => s.Endzeit != null).ToList();
            }
        }

        public Session GetSession(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            else
            {
                if (_context.Sessions.Any(s => s.UserId == user.Id && s.Endzeit != null) )
                {
                    return _context.Sessions.Where(s => s.UserId == user.Id && s.Endzeit != null).FirstOrDefault();
                }
                else 
                {
                    var session = new Session
                    {
                        UserId = user.Id,
                        Startzeit = DateTime.Now,
                        Endzeit = null
                    };

                    _context.Sessions.Add(session);
                    _context.SaveChanges();

                    return session;
                    
                }
            }
        }


        public bool EndSession(Session session)
        {
            if (session == null)
            {
                return false;
            }
            else
            { 
                session.Endzeit = DateTime.Now;
                if (SaveChanges())
                {
                    return true;
                }
                else { return false; }
            }
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }


    }
}
