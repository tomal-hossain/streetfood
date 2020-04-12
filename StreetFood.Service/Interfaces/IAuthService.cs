using StreetFood.Domain.Models;

namespace StreetFood.Service.Interfaces
{
    public interface IAuthService
    {
        public int Login(string email, string passowrd);
        bool IsEmailExist(string email);
        bool IsVerifiedEmail(string email);
        public int SignUp(User user);
        bool ConfirmEmail(string email);
        bool ResetPassword(string email, string password);
    }
}
