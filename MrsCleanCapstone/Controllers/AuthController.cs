using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MrsCleanCapstone.Data;
using MrsCleanCapstone.DTOs;
using MrsCleanCapstone.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrsCleanCapstone.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<Employee> _userManager;
        private readonly SignInManager<Employee> _signInManager;
        public AuthController(
            SignInManager<Employee> signInManager,
            UserManager<Employee> userManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }

        //[HttpPost("{controller}/{action}")]
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.UserName);
            if (user != null)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(user, loginDto.Password, false, false);
                // sign in
                if (signInResult.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("UnsuccessfulLogin", "Home");
            }

                return RedirectToAction("UnsuccessfulLogin","Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Register(RegisterDto registerDto)
        {

            var user = new Employee
            {
                UserName = registerDto.UserName
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (result.Succeeded)
            {
                // sign user here
                var signInResult = await _signInManager.PasswordSignInAsync(user, registerDto.Password, false, false);
                // sign in
                if (signInResult.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("UnsuccessfulLogin", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
