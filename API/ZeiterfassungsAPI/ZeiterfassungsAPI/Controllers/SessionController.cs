using Microsoft.AspNetCore.Mvc;
using ZeiterfassungsAPI.Models;
using ZeiterfassungsAPI.Data;
using ZeiterfassungsAPI.Services;

namespace ZeiterfassungsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SessionController : ControllerBase
    {

        private readonly ISessionService _sessionService;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _contextAccessor;

        public SessionController(ISessionService sessionService, IUserService userService, IHttpContextAccessor httpContextAccessor)
        {
            _sessionService = sessionService;
            _userService = userService;
            _contextAccessor = httpContextAccessor;
        }

        #region Endpunkte

        [HttpGet]
        [Route("/zeiterfassung/sessions/getall")]
        public ActionResult<IEnumerable<Session>> GetAll()
        {
            return Ok(_sessionService.GetSessions(false)); 
        }

        [HttpGet]
        [Route("/zeiterfassung/sessions/getallactive")]
        public ActionResult<IEnumerable<Session>> GetAllActive()
        {
            return Ok(_sessionService.GetSessions(true));
        }

        [HttpPost]
        [Route("/zeiterfassung/sessions/update")]
        public ActionResult<Session> UpdateSession(string rfid)
        {
            if (_sessionService.ChipExists(rfid))
            {
                var user = _userService.GetUserByRFID(rfid);

                if (user != null)
                {
                    var hasOpenSession = _sessionService.hasOpenSession(_userService.GetUserByRFID(rfid));

                    if (hasOpenSession)
                    {
                        var session = _sessionService.GetSessionExistingOrNew(user);
                        return Ok(_sessionService.EndSession(session));
                    }
                    else
                    {
                        return Ok(_sessionService.GetSessionExistingOrNew(user));
                    }
                }
                else
                {
                    return BadRequest();
                }

            }
            else
            {
                return BadRequest();
            }
        }
        #endregion
    }
}