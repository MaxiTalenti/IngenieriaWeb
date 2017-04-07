using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RepositorioClases;
using WebMatrix.WebData;
using Servicios;

namespace GlobalEvents.Controllers
{
    [OverrideAuthentication]
    public class UsersController : Controller
    {
        private Modelo db = new Modelo();

        // GET: Users
        [Authorize]
        public ViewResult Index()
        {
            return View(UserService.Get(null).Select(u => new ViewModels.ListUserViewModel()
            {
                Email = u.Email,
                Id = u.Id,
                Usuario = u.Usuario,
                Apellido = u.Apellido,
                Name = u.Nombre
            }).ToList());
        }

        //
        // GET: /User/Details/5

        public ViewResult Details(int id)
        {
            ViewModels.ListUserViewModel user = UserService.Get(id).Select(u => new ViewModels.ListUserViewModel()
            {
                Id = u.Id,
                Email = u.Email,
                Name = u.Nombre,
                Apellido = u.Apellido,
                Usuario = u.Usuario
            }).FirstOrDefault();

            return View(user);
        }

        //
        // GET: /User/Create

        public ActionResult Create()
        {
            return View(new ViewModels.UserViewModel());
        }

        //
        // POST: /User/Create

        [HttpPost]
        public ActionResult Create(ViewModels.UserViewModel user)
        {
            if (!WebSecurity.Initialized)
            {
                WebSecurity.InitializeDatabaseConnection("Modelo", "Users", "Id", "Usuario", true);
            }
            if (ModelState.IsValid)
            {
                // El usuario va a ser siempre el email.
                
                WebSecurity.CreateUserAndAccount(user.Email, user.Password, new { Email = user.Email}, false);
                int id = WebSecurity.GetUserId(user.Email);
                /*UserService.Create(new Users()
                {
                    Id = id,
                    Email = user.Email,
                    Usuario = user.Email // Inicialmente se le setea el mismo valor, luego se podrá cambiar.
                });*/
                return RedirectToAction("Index");
            }

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
                Usuario = u.Usuario
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
                UserService.Edit(new RepositorioClases.Users()
                {
                    Id = user.Id,
                    Email = user.Email,
                    Nombre = user.Nombre,
                    Apellido = user.Apellido,
                    Usuario = user.Usuario
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
                Usuario = u.Usuario
            }).FirstOrDefault();

            return View(user);
        }

        //
        // POST: /User/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            UserService.Delete(new RepositorioClases.Users()
            {
                Id = id
                //DeletedDate = DateTime.Now Ejecutar método de seguridad.
            });

            return RedirectToAction("Index");
        }
    }
}
