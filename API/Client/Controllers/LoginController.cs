using API.ViewModel;
using Client.Repositories.Data;
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
        /*private readonly EmployeeRepository repository;
        private readonly IJWTHandler jWTHandler;
        public LoginController(EmployeeRepository repository, IJWTHandler jWTHandler)
        {
            this.repository = repository;
            this.jwtHandler = jwtHandler;
        }
        public IActionResult Index()
        {
            // IF LOGGED IN
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("index", "dashboard");
            };
            return View();
        }

        //USING SAKURA SERVICE LOGIN
        [ValidateAntiForgeryToken]
        [HttpPost("Auth/")]
        public async Task<IActionResult> Auth(LoginVM login)
        {
            var jwtToken = await repository.Auth(login);
            var token = jwtToken.Token;

            if (token == null)
            {
                return RedirectToAction("index");
            }

            HttpContext.Session.SetString("JWToken", token);
            HttpContext.Session.SetString("Name", jwtHandler.GetName(token));
            HttpContext.Session.SetString("ProfilePicture", "assets/img/theme/user.png");

            return RedirectToAction("index", "dashboard");
        }*/
    }
}
