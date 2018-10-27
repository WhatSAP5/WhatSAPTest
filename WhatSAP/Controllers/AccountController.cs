using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WhatSAP.Models;
using WhatSAP.Models.Account;

namespace WhatSAP.Controllers
{
    [Route("account")]
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signinManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly WhatSAPContext _context;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signinManager, WhatSAPContext context)
        {
            _userManager = userManager;
            _signinManager = signinManager;
            _context = context;
        }

        [HttpGet, Route("login")]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost, Route("login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel login, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = await _signinManager.PasswordSignInAsync(
                login.Email, login.Password, login.RememberMe, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Login Error!");
                return View();
            }

            if (string.IsNullOrWhiteSpace(returnUrl))
            {
                return RedirectToAction("Index", "Home");
            }

            return Redirect(returnUrl);
        }

        [HttpPost]
        public async Task<IActionResult> Logout(string returnUrl = null)
        {
            await _signinManager.SignOutAsync();

            if (string.IsNullOrWhiteSpace(returnUrl))
                return RedirectToAction("Index", "Home");

            return Redirect(returnUrl);
        }

        [HttpGet]
        public IActionResult ChooseStatus()
        {
            return View();
        }

        [HttpGet, Route("customer/register")]
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost, Route("customer/register")]
        public async Task<IActionResult> Register(RegisterViewModel registration)
        {
            if (!ModelState.IsValid)
                return View(registration);

            var nCustomer = new Customer();
            nCustomer.FirstName = registration.FirstName;
            nCustomer.LastName = registration.LastName;
            nCustomer.DateOfBirth = registration.DateOfBirth;
            nCustomer.Email = registration.EmailAddress;
            nCustomer.Password = registration.Password;
            nCustomer.Phone = registration.Phone;

            var newUser = new ApplicationUser
            {
                Email = registration.EmailAddress,
                UserName = registration.EmailAddress,
            };

            var result = await _userManager.CreateAsync(newUser, registration.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors.Select(x => x.Description))
                {
                    ModelState.AddModelError("", error);
                }

                return View();
            }

            _context.Customer.Add(nCustomer);
            _context.SaveChanges();

            return RedirectToAction("Login");
        }

        [HttpGet, Route("client/register")]
        public IActionResult ClientRegister()
        {
            return View(new ClientRegisterViewModel());
        }

        [HttpPost, Route("client/register")]
        public async Task<IActionResult> ClientRegister(ClientRegisterViewModel registration)
        {
            if (!ModelState.IsValid)
                return View(registration);

            var newUser = new ApplicationUser
            {
                Email = registration.EmailAddress,
                UserName = registration.EmailAddress,
            };

            var result = await _userManager.CreateAsync(newUser, registration.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors.Select(x => x.Description))
                {
                    ModelState.AddModelError("", error);
                }

                return View();
            }

            return RedirectToAction("Login");
        }
    }
}