﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MvcTIC_IT.Models;
using MvcTIC_IT.Libraries;
using MvcTIC_IT.ViewModel;

namespace MvcTIC_IT.Controllers
{

    [Authorize]
    public class AccountController : BaseController
    {

        //
        // GET: /Account/Login

        [AllowAnonymous]
        public ActionResult Login()
        {
            return ContextDependentView();
        }

        //
        // POST: /Account/JsonLogin

        [AllowAnonymous]
        [HttpPost]
        public JsonResult JsonLogin(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    return Json(new { success = true, redirect = returnUrl });
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            // If we got this far, something failed
            return Json(new { errors = GetErrorsFromModelState() });
        }

        //
        // POST: /Account/Login

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register

        [AllowAnonymous]
        public ActionResult Register()
        {
            return ContextDependentView();
        }

        //
        // POST: /Account/JsonRegister

        [AllowAnonymous]
        [HttpPost]
        public ActionResult JsonRegister(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus;
                Membership.CreateUser(model.UserName, model.Password, model.Email, passwordQuestion: null, passwordAnswer: null, isApproved: true, providerUserKey: null, status: out createStatus);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    System.Web.Security.Roles.AddUserToRole(model.UserName, "user");
 

                    FormsAuthentication.SetAuthCookie(model.UserName, createPersistentCookie: false);

                    string body = "Registou-se : " + model.UserName + " na data : " + DateTime.Now;

                    Utils.sendSimpleEmail("pedro@dinispt.com", "TIC-IT", "pedro@dinispt.com", "TIC-IT -- Registo Utilizador", body);

                  
                    return Json(new { success = true });
                }
                else
                {
                    ModelState.AddModelError("", ErrorCodeToString(createStatus));
                }
            }

            // If we got this far, something failed
            return Json(new { errors = GetErrorsFromModelState() });
        }

        //
        // POST: /Account/Register

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus;
                Membership.CreateUser(model.UserName, model.Password, model.Email, passwordQuestion: null, passwordAnswer: null, isApproved: true, providerUserKey: null, status: out createStatus);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    //adicionar membro
                    System.Web.Security.Roles.AddUserToRole(model.UserName, "user");

                    FormsAuthentication.SetAuthCookie(model.UserName, createPersistentCookie: false);

                    string body = "Registou-se : " + model.UserName + " na data : " + DateTime.Now;

                    Utils.sendSimpleEmail("pedro@dinispt.com", "TIC-IT", "pedro@dinispt.com", "TIC-IT -- Registo Utilizador", body);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", ErrorCodeToString(createStatus));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePassword

        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Account/ChangePassword

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {

                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                bool changePasswordSucceeded;
                try
                {
                    MembershipUser currentUser = Membership.GetUser(User.Identity.Name, userIsOnline: true);
                    changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePasswordSuccess

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        private ActionResult ContextDependentView()
        {
            string actionName = ControllerContext.RouteData.GetRequiredString("action");
            if (Request.QueryString["content"] != null)
            {
                ViewBag.FormAction = "Json" + actionName;
                return PartialView();
            }
            else
            {
                ViewBag.FormAction = actionName;
                return View();
            }
        }

        private IEnumerable<string> GetErrorsFromModelState()
        {
            return ModelState.SelectMany(x => x.Value.Errors.Select(error => error.ErrorMessage));
        }

        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion


        [AllowAnonymous]
        public ActionResult RecuperarEmail()
        {
            return View("RecuperarEmailPassword");
        }
       
        [AllowAnonymous]
        [HttpPost]
        public ActionResult RecuperarEmail(EmailViewmodel m)
        {

            try
            {
                //string username = Membership.GetUserNameByEmail(m.email);
                var users = Membership.FindUsersByEmail(m.email);
                //if (username == null)
                //{
                //    return View("Error");

                //}
                int suc=0;
                foreach (var username in users)
                {
                    string password = Membership.Provider.ResetPassword(username.ToString(), null);
            

                string body = "";
                body = ViewRes.Account.username +" : "+username+" ---- "+ViewRes.Account.password+ " : " + password;
                if (!Utils.sendSimpleEmail(m.email, username.ToString(), "pedro@dinispt.com", "MVCSPECIALJOBS -- " + ViewRes.Account.recuperarpass, body))
                    //ViewData["erroemail"] = "rgewrgtwhwryjwkjteu" + ViewRes.Account.erroaoenviarpassword;
                    //return RedirectToAction("ResetPasswordSendError");
                    suc = -1;
                else
                    //ViewData["sucesso"] = ViewRes.Account.sucessoaoenviarapassword;
                    //return RedirectToAction("ResetPasswordSuccess");
                    suc = 1;

                }
                if (suc == 1)
                {

                    return RedirectToAction("ResetPasswordSuccess");

                }
                else if (suc == -1)
                {
                    return RedirectToAction("ResetPasswordSendError");
                
                }

            }
            catch (System.Configuration.Provider.ProviderException e)
            {
                ViewData.ModelState.AddModelError(m.email, ViewRes.Account.erroaorecuperapassword);
                return RedirectToAction("ResetPasswordError");
                //ViewData["erro"] = ViewRes.Account.erroaorecuperapassword;
            }


            return View("RecuperarEmailPassword", m);


        }
               [AllowAnonymous]
        public ActionResult ResetPasswordError()
        {
            return View();
        }
               [AllowAnonymous]
        public ActionResult ResetPasswordSendError()
        {
            return View();
        }
               [AllowAnonymous]
        public ActionResult ResetPasswordSuccess()
        {
            return View();
        }
    }
}
