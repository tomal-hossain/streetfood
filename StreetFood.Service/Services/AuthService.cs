using StreetFood.Domain;
using StreetFood.Domain.Models;
using StreetFood.Service.Interfaces;
using System;
using System.Linq;
using System.Text;


namespace StreetFood.Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly StreetFoodDbContext _context;
        
        public AuthService(StreetFoodDbContext context)
        {
            _context = context;
        }

        public int Login(string email, string passowrd)
        {
            string hashPassword = this.HashPassword(passowrd);
            User user = _context.User
                .Where(x => x.Email == email && x.Password == hashPassword)
                .FirstOrDefault();
            if(user != null)
            {
                if (user.IsConfirmed)
                {
                    return user.Id;
                }
                return 0;
            }
            return -1;
        }

        public bool IsEmailExist(string email)
        {
            User user = _context.User
                .Where(x => x.Email == email)
                .FirstOrDefault();
            if(user != null)
            {
                return true;
            }
            return false;
        }

        public bool IsVerifiedEmail(string email)
        {
            User user = _context.User
                .Where(x => x.Email == email && x.IsConfirmed == true)
                .FirstOrDefault();
            if (user != null)
            {
                return true;
            }
            return false;
        }

        public int SignUp(User user)
        {
            user.Password = this.HashPassword(user.Password);
            try
            {
                _context.User.Add(user);
                _context.SaveChanges();
                return user.Id;
            }
            catch
            {
                return -1;
            }
        }

        public bool ConfirmEmail(string email)
        {
            User user = _context.User
                .Where(x => x.Email == email)
                .FirstOrDefault();
            if (user != null)
            {
                user.IsConfirmed = true;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool ResetPassword(string email, string password)
        {
            User user = _context.User
                .Where(x => x.Email == email)
                .FirstOrDefault();
            if (user != null)
            {
                user.Password = this.HashPassword(password);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        private string HashPassword(string password)
        {
            var bytes = new UTF8Encoding().GetBytes(password);
            var hashBytes = System.Security.Cryptography.SHA256.Create().ComputeHash(bytes);
            return Convert.ToBase64String(hashBytes);
        }
    }
}
