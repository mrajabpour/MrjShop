﻿using System.Threading.Tasks;
using System.Web.Mvc;
using MRJ.DomainClasses;
using MRJ.ServiceLayer.Contracts;
using MRJ.Web.ViewModels.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace MRJ.Web.Controllers
{
    [RoutePrefix("Account")]
    public partial class AccountController : Controller
    {
        private readonly IApplicationUserManager _userManager;
        private readonly IApplicationSignInManager _signInManager;
        private readonly IAuthenticationManager _authenticationManager;

        public AccountController(IApplicationUserManager userManager, IApplicationSignInManager applicationSignInManager,
            IAuthenticationManager authenticationManager)
        {
            _userManager = userManager;
            _signInManager = applicationSignInManager;
            _authenticationManager = authenticationManager;
        }

        [Route("Login")]
        [AllowAnonymous]
        public virtual ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [Route("Login")]
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = SignInStatus.Failure;

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, shouldLockout: true);
            }

            switch (result)
            {
                case SignInStatus.Success:
                    return redirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl });
                default:
                    ModelState.AddModelError("", "ورود به سیستم موفقیت آمیز نبود");
                    return View(model);
            }
        }

        [Route("LogOff")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> LogOff()
        {
            var userId = User.Identity.GetUserId<int>();
            _authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            await _userManager.UpdateSecurityStampAsync(userId);

            return RedirectToAction((string) MVC.Account.ActionNames.Login, (string) MVC.Account.Name);
        }

        [Route("ResetPassword")]
        [AllowAnonymous]
        public virtual ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        [Route("ResetPassword")]
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await _userManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            addErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public virtual ActionResult ResetPasswordConfirmation()
        {
            return View();
        }


        [Route("ForgotPassword")]
        [AllowAnonymous]
        public virtual ActionResult ForgotPassword()
        {
            return View();
        }


        [Route("ForgotPassword")]
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                var code = await _userManager.GeneratePasswordResetTokenAsync(user.Id);
                var callbackUrl = Url.Action("ResetPassword", "Account",
                    new { userId = user.Id, code }, protocol: Request.Url.Scheme);
                await _userManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking here: <a href=\"" + callbackUrl + "\">link</a>");
                ViewBag.Link = callbackUrl;
                return View("ForgotPasswordConfirmation");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public virtual ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [Route("ChangePassword")]
        public virtual ActionResult ChangePassword()
        {
            return View();
        }

        [Route("ChangePassword")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await _userManager.ChangePasswordAsync(_userManager.GetCurrentUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await _userManager.GetCurrentUserAsync();
                if (user != null)
                {
                    await signInAsync(user, isPersistent: false);
                }
                TempData["message"] = "کلمه عبور با موفقیت ویرایش شد";
            }
            addErrors(result);
            return View(model);
        }


        private void addErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult redirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction(MVC.AdminPanel.Dashboard.ActionNames.Index, MVC.AdminPanel.Dashboard.Name, new { area = MVC.AdminPanel.Name });
        }

        private async Task signInAsync(ApplicationUser user, bool isPersistent)
        {
            _authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie, DefaultAuthenticationTypes.TwoFactorCookie);
            _authenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent },
                await _userManager.GenerateUserIdentityAsync(user));
        }


    }
}