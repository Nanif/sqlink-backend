using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sqlink_Server.GeneratedModels;
using Sqlink_Server.Handlers;
using Sqlink_Server.Models;

namespace Sqlink_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IJWTAuthenticationManager _jwtAuthenticationManager;
        private readonly MyDBContext _context;

        public AuthenticationController(IJWTAuthenticationManager jwtAuthenticationManager, MyDBContext context)
        {
            _jwtAuthenticationManager = jwtAuthenticationManager;
            _context = context;
        }

        [HttpPost]
        public ActionResult<LoginResponseModel?> Login([FromBody] LoginRequestModel model)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = _context.Users.Where(u => !string.IsNullOrEmpty(u.Email) && u.Email.Equals(model.Email) &&
                                                 !string.IsNullOrEmpty(u.Password) && u.Password.Equals(model.Password))
                                                .FirstOrDefault();
            if (user == null)
            {
                return NotFound();
            }
            var responseModel = _jwtAuthenticationManager.Authenticate(user);
            return responseModel;
        }
    }
}
