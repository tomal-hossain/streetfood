using Microsoft.AspNetCore.Http;
using StreetFood.Domain.Models;

namespace StreetFood.Web.Services
{
    public interface IUserService
    {
        string GenerateToken(int id);
        string GenerateEmailToken(string email);
        string GetEmailFromToken(string token);
        int GetUserIdFromRequest(HttpRequest Request);
        void SendConfirmationEmail(User user, string token);
        void SendResetPasswordEmail(string email, string token);
    }
}
