using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserIdentity.Entities;
using UserIdentity.Models.ApiModels;

namespace UserIdentity.Controllers.ApiController
{
    [Route("/api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<AccountController> _logger;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            model.ReturnUrl ??= Url.Content("~/");
            model.ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                _logger.LogInformation("User created a new account with password.");

                await _signInManager.SignInAsync(user, isPersistent: false);

                return Ok("Success");
            }

            return Ok("Failed");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model, string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            model.ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
                return Ok("Success");
            else
                return Ok("Invalid");


        }
    }
}
