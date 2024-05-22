using Microsoft.AspNetCore.Mvc;
using ZeiterfassungsAPI.Models;
using ZeiterfassungsAPI.Data;
using ZeiterfassungsAPI.Services;

namespace ZeiterfassungsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {

        private readonly ISessionService _sessionService;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _contextAccessor;

        public UserController(IUserService userService, IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _contextAccessor = httpContextAccessor;
        }

        #region Endpunkte

        [HttpGet]
        [Route("/zeiterfassung/users/getall")]
        public ActionResult<IEnumerable<Session>> GetAll()
        {
            return Ok(_userService.GetAllUsers()); 
        }

        [HttpGet]
        [Route("/zeiterfassung/users/getUserNameById")]
        public ActionResult<string> GetUserNameById(int id)
        {
            var name = _userService.GetUserName(id);

            if (name != null)
            {
                return Ok(name);
            }
            else
            {
                return BadRequest();
            }
        }

    
        #endregion
    }
}