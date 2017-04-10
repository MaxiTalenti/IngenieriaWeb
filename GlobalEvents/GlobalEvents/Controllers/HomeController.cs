using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModels;
using WebMatrix.WebData;
using GlobalEvents.Filters;
using RepositorioClases;

namespace GlobalEvents.Controllers
{
    [MyAuthorize]
    [InitializeSimpleMembership]
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(String returnURL)
        {
            ViewBag.returnURL = returnURL;
            if (WebSecurity.IsAuthenticated) // Si ya esta autenticado y quiere ingresar igualmente a esta página, redirecionamos a index.
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new LoginModel();

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginModel model, String returnURL)
        {
            // Verifica que no este bloqueado ni eliminado.
            using (Modelo context = new Modelo())
            {
                Users users = context.Users.Where(u => u.Email == model.Email).FirstOrDefault();
                if (users.Estado == UserState.Bloqueado || users.Estado == UserState.Eliminado)
                {
                    return View();
                }
            }

                if (ModelState.IsValid && WebSecurity.Login(model.Email, model.Password, model.Recordarme))
            {
                if (Url.IsLocalUrl(returnURL))
                {
                    return Redirect(returnURL);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        [MyAuthorize]
        public ActionResult Logout(String returnURL)
        {
            WebSecurity.Logout();
            return RedirectToAction("Index", "Home");

            /* Ver como manejar si la página no esta autorizada luego de desloguear.
            if (Url.IsLocalUrl(returnURL))
            {
                return Redirect(returnURL);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            */
        }
    }
}