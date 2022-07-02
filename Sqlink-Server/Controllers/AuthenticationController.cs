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

        public AuthenticationController(IJWTAuthenticationManager jwtAuthenticationManager)
        {
            _jwtAuthenticationManager = jwtAuthenticationManager;
        }
        [HttpPost]
        public ActionResult<LoginResponseModel> Login([FromBody] LoginRequestModel model)
        {

            var responseModel = _jwtAuthenticationManager.Authenticate(model.Email, model.Password);
            if (responseModel == null)
            {
                return NotFound();
            }

            return responseModel;
        }
    }
}
