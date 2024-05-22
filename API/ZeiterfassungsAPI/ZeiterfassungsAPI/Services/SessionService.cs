using System.Collections;
using ZeiterfassungsAPI.Data;
using ZeiterfassungsAPI.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ZeiterfassungsAPI.Services
{
    public class SessionService : ISessionService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public SessionService(
        AppDbContext context,
        IConfiguration config,
        IHttpContextAccessor httpContextAccessor,
        IServiceScopeFactory serviceScopeFactory
        )
        {
            _context = context;
            _config = config;
            _httpContextAccessor = httpContextAccessor;    
            _serviceScopeFactory = serviceScopeFactory;
        }

        public IEnumerable<Session> GetSessions(bool onlyActive)
        {
            if  (!onlyActive)
            {
                return _context.Sessions.ToList();
            }
            else
            {
                return _context.Sessions.Where(s => s.Endzeit == null).ToList();
            }
        }

        public Session GetSessionExistingOrNew(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            else
            {
                if (hasOpenSession(user))
                {
                    return _context.Sessions.Where(s => s.UserId == user.Id && s.Endzeit == null).FirstOrDefault();
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


        public Session EndSession(Session session)
        {
            if (session == null)
            {
                throw new ArgumentException("session existiert nicht");
            }
            else
            { 
                session.Endzeit = DateTime.Now;
                if (SaveChanges())
                {
                    return session;
                }
                else 
                { 
                    throw new Exception("Fehler beim speichern");  
                }
            }
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }

        public bool hasOpenSession(User user)
        {
            return _context.Sessions.Any(s => s.UserId == user.Id && s.Endzeit == null);
        }

        public bool ChipExists(string rfid)
        {
            return _context.RfidChip.Any(r => r.Rfid == rfid);
        }
    }
}
