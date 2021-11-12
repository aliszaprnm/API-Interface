using API.ViewModel;
using Client.Repositories.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class LoginController : Controller
    {
        private readonly LoginRepository repository;
        /*private readonly IJWTHandler jWTHandler;*/
        public LoginController(LoginRepository repository/*, IJWTHandler jWTHandler*/)
        {
            this.repository = repository;
            /*this.jwtHandler = jwtHandler;*/
        }
        public IActionResult Index()
        {
            // IF LOGGED IN
            /*if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("index", "dashboard");
            };*/
            return View();
        }

        //USING SAKURA SERVICE LOGIN
        /*[ValidateAntiForgeryToken]*/
        /*[HttpPost("Auth/")]*/
        public async Task<IActionResult> Auth(LoginVM login)
        {
            var jwtToken = await repository.Auth(login);
            var token = jwtToken.Token;
            var email = login.Email;
            var pwd = login.Password;

            if (token == null)
            {
                return RedirectToAction("InvalidLogin", "Home");
            }

            HttpContext.Session.SetString("JWToken", token);
            /*HttpContext.Session.SetString("Name", jwtHandler.GetName(token));*/
            HttpContext.Session.SetString("ProfilePicture", "assets/img/theme/user.png");

            return RedirectToAction("Dashboard", "Home");
        }

        [Authorize]
        /*[HttpGet("Logout/")]*/
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Home");
        }
    }
}
