using Sqlink_Server.GeneratedModels;
namespace Sqlink_Server.Models
{
    public class LoginResponseModel
    {
        public string Token { get; set; }
        public User PersonalDetails { get; set; }

        public LoginResponseModel(string token, User user)
        {
            this.Token = token;
            this.PersonalDetails = user;
        }
    }

}
