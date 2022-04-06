using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserIdentity.Entities;
using UserIdentity.Models;

namespace UserIdentity.Controllers
{
    public class AccountController : Controller
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
        public async Task<IActionResult> Register(string returnUrl = null)
        {
            var model = new RegisterModel();
            model.ReturnUrl = returnUrl;
            model.ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            model.ReturnUrl ??= Url.Content("~/");
            model.ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            else
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Login", "Account");

                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //code = HttpUtility.HtmlEncode(code);
                    ////code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                        //var callbackUrl = Url.Action(
                        //    nameof(ConfirmEmail), "Account",
                        //     new { token = code, userId = user.Id },
                        //    protocol: Request.Scheme);

                        //if (_userManager.Options.SignIn.RequireConfirmedEmail)
                        //{
                        //    _emailService.SendEmail(model.Email, "Confirm your email",
                        //    $"Please confirm your account by <a href='{callbackUrl}'>link</a>.", null);

                        //    return RedirectToAction("Login", "Account", new { email = model.Email, returnUrl = model.ReturnUrl });
                        //}
                    //else
                    //{
                    //    await _signInManager.SignInAsync(user, isPersistent: false);
                    //    return RedirectToAction("Login", "Account");
                    //}
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
    }
}
