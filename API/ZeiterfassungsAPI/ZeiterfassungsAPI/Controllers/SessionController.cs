using Microsoft.AspNetCore.Mvc;
using ZeiterfassungsAPI.Models;
using ZeiterfassungsAPI.Data;

namespace ZeiterfassungsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SessionController : ControllerBase
    {

        private readonly ILogger<SessionController> _logger;

        public SessionController(ILogger<SessionController> logger)
        {
            _logger = logger;
        }

        #region Endpunkte

        [HttpGet]
        [Route("/zeiterfassung/sessions/getall")]
        public IEnumerable<Session> GetAll()
        {

            return null;
        }

        [HttpGet]
        [Route("/zeiterfassung/sessions/getallactive")]
        public IEnumerable<Session> GetAllActive()
        {
            return null;
        }

        #endregion
    }
}