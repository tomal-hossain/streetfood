using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SendGrid;
using SendGrid.Helpers.Mail;
using StreetFood.Domain.Models;
using StreetFood.Web.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace StreetFood.Web.Services
{
    public class UserService : IUserService
    {
        private readonly IDataProtector _protector;
        private readonly IConfiguration _config;
        public UserService(IDataProtectionProvider provider, IConfiguration config)
        {
            _config = config;
            _protector = provider.CreateProtector(_config.GetValue<string>("Key:encryptionKey"));
        }
        public string GenerateToken(int id)
        {
            string secretKey = _config.GetValue<string>("key:secretKey");
            var byteKey =  new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(byteKey, SecurityAlgorithms.HmacSha256);
            var claim = new[]
            {
                new Claim("userid", id.ToString()),
                new Claim(ClaimTypes.Name, Guid.NewGuid().ToString())
            };
            var jwtToken = new JwtSecurityToken(
                claims: claim,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials
            );
            string token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return token;
        }

        public string GenerateEmailToken(string email)
        {
            string token = _protector.Protect(email);
            return token;
        }

        public int GetUserIdFromRequest(HttpRequest Request)
        {
            try
            {
                string header = Request.Headers["Authorization"];
                string[] tokenArray = header.Split(' ');
                string accessToken = tokenArray[1];
                var jwtToken = new JwtSecurityToken(accessToken);
                var claim = jwtToken.Claims.FirstOrDefault(x => x.Type.Equals("userid", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(claim.Value);
                return userId;

            }
            catch
            {
                return -1;
            }
        }

        public string GetEmailFromToken(string token)
        {
            try
            {
                string email = _protector.Unprotect(token);
                return email;
            }
            catch
            {
                return null;
            }
        }

        public async void SendConfirmationEmail(User user, string token)
        {
            string href = _config.GetValue<string>("apiEndPoint") + "/api/auth/confirm/" + token;
            string content = $@" <p>
                  Hello {user.Name}, <br> Thank you for joining Street Food. <br>
                  Please <a href='{href}'> click here </a> to confirm your email.
                  </p>";
            SendGridModel model = new SendGridModel
            {
                Subject = "Email Confirmation",
                To = user.Email,
                PlainText = "",
                HtmlContent = content
            };
            await this.SendEmail(model);
        }

        public async void SendResetPasswordEmail(string email, string token)
        {
            string href = _config.GetValue<string>("apiEndPoint") + "/authentication/reset-password/" + token;
            string content = $@" <p>
                  Please <a href='{href}'> click here </a> to reset your password.
                  </p>";
            SendGridModel model = new SendGridModel
            {
                Subject = "Reset Password",
                To = email,
                PlainText = "",
                HtmlContent = content
            };
            await this.SendEmail(model);
        }

        private async Task SendEmail(SendGridModel model)
        {
            var apiKey = _config.GetValue<string>("sendGrid:apiKey");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(_config.GetValue<string>("sendGrid:from"), _config.GetValue<string>("sendGrid:name"));
            var to = new EmailAddress(model.To);
            var msg = MailHelper.CreateSingleEmail(from, to, model.Subject, model.PlainText, model.HtmlContent);
            await client.SendEmailAsync(msg);
        }
    }
}
