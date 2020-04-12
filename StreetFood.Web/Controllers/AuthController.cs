using Microsoft.AspNetCore.Mvc;
using StreetFood.Domain.Models;
using StreetFood.Service.Interfaces;
using StreetFood.Web.Models;
using StreetFood.Web.Services;

namespace StreetFood.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        
        public AuthController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [HttpPost, Route("signup")]
        public ActionResult SignUp(User user)
        {
            bool isEmailExist = _authService.IsEmailExist(user.Email);
            if (isEmailExist)
            {
                return BadRequest("Email already exist!");
            }
            int userId = _authService.SignUp(user);
            if (userId > 0)
            {
                string token = _userService.GenerateEmailToken(user.Email);
                if(token != null)
                {
                    _userService.SendConfirmationEmail(user, token);
                    return Ok();
                }
            }
            return BadRequest("Something went wrong. Please try again.");
        }

        [HttpGet, Route("confirm/{token}")]
        public ActionResult ConfirmEmail(string token)
        {
            string email = _userService.GetEmailFromToken(token);
            if (email != null)
            {
                bool status =_authService.ConfirmEmail(email);
                if (status)
                {
                    return Redirect("/authentication/confirm-success");
                }
            }
            return BadRequest();
        }

        [HttpPost, Route("signin")]
        public ActionResult SignIn(SignInModel model)
        {
            int userId = _authService.Login(model.Email, model.Password);
            if (userId < 0)
            {
                return BadRequest("Wrong username or password!");
            }
            if(userId == 0)
            {
                return BadRequest("Please confirm your email!");
            }
            string token = _userService.GenerateToken(userId);
            return Ok(new { Token = token });
        }

        [HttpPost, Route("forgotpassword")]
        public ActionResult ForgotPassword(SignInModel model)
        {
            bool isExist = _authService.IsEmailExist(model.Email);
            if (isExist)
            {
                bool isVerified = _authService.IsVerifiedEmail(model.Email);
                if (!isVerified)
                {
                    return BadRequest("Please confirm your email first.");
                }
                string token = _userService.GenerateEmailToken(model.Email);
                if (token != null)
                {
                    _userService.SendResetPasswordEmail(model.Email, token);
                    return Ok();
                }
            }
            return BadRequest("Email not found!");
        }

        [HttpPost, Route("resetpassword/{token}")]
        public ActionResult ResettPassword(string token, SignInModel model)
        {
            string email = _userService.GetEmailFromToken(token);
            if (email != null)
            {
                bool status = _authService.ResetPassword(email, model.Password);
                if (status)
                {
                    return Ok();
                }
            }
            return BadRequest();
        }
    }
}