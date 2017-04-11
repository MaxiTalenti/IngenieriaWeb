using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Security;
using System.Web.Mvc;
using RepositorioClases;
using ViewModels;
using WebMatrix.WebData;
using Servicios;
using GlobalEvents.Filters;


namespace GlobalEvents.Controllers
{
    [MyAuthorize]
    [InitializeSimpleMembership]
    public class UsersController : Controller
    {
        private Modelo db = new Modelo();

        // GET: Users
        [MyAuthorize]
        public ViewResult Index()
        {
            return View(UserService.Get(null).Select(u => new ViewModels.ListUserViewModel()
            {
                Email = u.Email,
                Id = u.Id,
                Usuario = u.Usuario,
                Apellido = u.Apellido,
                Name = u.Nombre,
                Estado = u.Estado
            }).ToList());
        }

        //
        // GET: /User/Details/5

        public ViewResult Details(int id)
        {
            ListUserViewModel user = UserService.Get(id).Select(u => new ViewModels.ListUserViewModel()
            {
                Id = u.Id,
                Email = u.Email,
                Name = u.Nombre,
                Apellido = u.Apellido,
                Usuario = u.Usuario,
                Estado = u.Estado
            }).FirstOrDefault();

            return View(user);
        }

        //
        // GET: /User/Create
        [AllowAnonymous]
        public ActionResult Create()
        {
            return View(new ViewModels.UserViewModel());
        }

        //
        // POST: /User/Create

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Create(ViewModels.UserViewModel user)
        {
            if (ModelState.IsValid)
            {
                if (WebSecurity.UserExists(user.Email))
                {
                    ModelState.AddModelError("", "El usuario que intenta registrar ya existe.");
                    return View(user);
                }

                // El usuario va a ser siempre el email, mientras no lo cambie.
                //WebSecurity.CreateUserAndAccount(user.Email, user.Password, new { Email = user.Email}, false);
                WebSecurity.CreateUserAndAccount(user.Email, user.Password, new { Email = user.Email, Estado = 1 }, false);
                Roles.AddUserToRole(user.Email, "Usuario");
                /*UserService.Create(new Users()
                {
                    Id = id,
                    Email = user.Email,
                    Usuario = user.Email // Inicialmente se le setea el mismo valor, luego se podrá cambiar.
                });*/
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "El nombre de usuario o la contraseña son incorrectos.");
            user.Password = ""; // Se blanquea la pass.
            return View(user);
        }

        //
        // GET: /User/Edit/5

        public ActionResult Edit(int id)
        {
            Users user = UserService.Get(id).Select(u => new Users()
            {
                Email = u.Email,
                Id = u.Id,
                Nombre = u.Nombre,
                Apellido = u.Apellido,
                Usuario = u.Usuario,
                Estado = u.Estado
            }).FirstOrDefault();

            return View(user);
        }

        //
        // POST: /User/Edit/5

        [HttpPost]
        public ActionResult Edit(Users user)
        {
            if (ModelState.IsValid)
            {
                UserService.Edit(new Users()
                {
                    Id = user.Id,
                    Email = user.Email,
                    Nombre = user.Nombre,
                    Apellido = user.Apellido,
                    Usuario = user.Usuario,
                    Estado = user.Estado
                });

                return RedirectToAction("Index");
            }
            return View(user);
        }

        //
        // GET: /User/Delete/5

        public ActionResult Delete(int id)
        {
            Users user = UserService.Get(id).Select(u => new Users()
            {
                Email = u.Email,
                Id = u.Id,
                Nombre = u.Nombre,
                Usuario = u.Usuario,
                Apellido = u.Apellido
            }).FirstOrDefault();

            return View(user);
        }

        //
        // POST: /User/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            UserService.Delete(new Users()
            {
                Id = id
            });

            return RedirectToAction("Index");
        }

        public ActionResult ChangePassword(int id)
        {
            EditPasswordModel user = UserService.Get(id).Select(u => new EditPasswordModel()
            {
                Usuario = u.Usuario
            }).FirstOrDefault();

            return View(user);
        }

        [HttpPost]
        public ActionResult ChangePassword(EditPasswordModel editModel)
        {
            Users user = UserService.Get(WebSecurity.GetUserId(editModel.Usuario)).Select(u => new Users()
            {
                Email = u.Email,
                Id = u.Id,
            }).FirstOrDefault();

            if (!WebSecurity.ChangePassword(user.Email, editModel.actualPassword, editModel.newPassword))
            {
                ModelState.AddModelError("", "La contraseña ingresada es incorrecta.");
                return View(editModel);
            }
            return RedirectToAction("Index");
        }
    }
}
