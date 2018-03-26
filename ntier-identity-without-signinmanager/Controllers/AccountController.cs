using dto;
using ntier_identity_without_signinmanager.Models;
using ntier_identity_without_signinmanager.Services;
using System.Web.Mvc;

namespace ntier_identity_without_signinmanager.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IIdentityAuthService _identityAuthService;

        public AccountController(IIdentityAuthService identityAuthService)
        {
            this._identityAuthService = identityAuthService;
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = _identityAuthService.SignIn(model.Email, model.Password, isPersistent: true, shouldLockOut: false);

            if (result == CustomerLoginResults.Successful)
            {
                return RedirectToLocal(returnUrl);
            }

            ModelState.AddModelError("", "Invalid login attempt.");
            return View(model);            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            _identityAuthService.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var account = new Account { Name = model.Name, Email = model.Email };
                var result = _identityAuthService.Create(account, model.Password);
                if (result)
                {
                    _identityAuthService.SignIn(account.Email, model.Password, isPersistent: true, shouldLockOut: true);

                    return RedirectToAction("Index", "Home");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}