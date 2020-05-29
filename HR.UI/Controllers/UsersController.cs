using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using HR.UI.Contracts;
using HR.UI.Models.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HR.UI.Controllers
{
    public class UsersController : Controller
    {
        private readonly HttpClientWrapper _clientWrapper;

        public UsersController(HttpClientWrapper clientWrapper)
        {
            _clientWrapper = clientWrapper;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserViewModel model)
        {
            var userToCreate = new CreateUserDto
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                BirthDate = model.BirthDate,
                Gender = model.Gender,
                PersonalId = model.PersonalId,
                Password = model.Password,
                Email = model.Email
            };
            var response = await _clientWrapper.PostAsync("api/Users", userToCreate);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", await response.Content.ReadAsStringAsync());
                return View();
            }

            await LoginAsync(model.Email);
            return RedirectToAction("Index", "Employees");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginViewModel model)
        {
            var result = await _clientWrapper.PostAsync<AuthenticateDto, bool>("api/Users/Authenticate", new AuthenticateDto
            {
                Email = model.Email,
                Password = model.Password
            });

            if (result)
            {
                await LoginAsync(model.Email);
                return RedirectToAction("Index", "Employees");
            }

            ModelState.AddModelError("", "Invalid username or password");
            return View();
        }

        private async Task LoginAsync(string email)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, email),
                new Claim(ClaimTypes.Name, email)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity),
                new AuthenticationProperties());
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Employees");
        }
    }
}