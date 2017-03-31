using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RepositorioClases;
using Servicios;

namespace GlobalEvents.Controllers
{
    public class UsersController : Controller
    {
        private Modelo db = new Modelo();

        // GET: Users
        public ViewResult Index()
        {    
            return View(UserService.Get(null).Select(u => new ViewModels.ListUserViewModel()
            {
                Email = u.Email,
                Id = u.Id,
                Name = u.Name,
                Password = u.Password,
                IdRol = u.IdRol
            }).ToList());
        }

        //
        // GET: /User/Details/5

        public ViewResult Details(int id)
        {
            ViewModels.ListUserViewModel user = UserService.Get(id).Select(u => new ViewModels.ListUserViewModel()
            {
                Email = u.Email,
                Id = u.Id,
                Name = u.Name,
                Password = u.Password,
                IdRol = u.IdRol
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
            if (ModelState.IsValid)
            {
                UserService.Create(new Users()
                {
                    Email = user.Email,
                    Name = user.Name,
                    Password = user.Password,
                    IdRol = user.IdRol
                });
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
                Name = u.Name,
                Password = u.Password,
                IdRol = u.IdRol
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
                    Email = user.Email,
                    Id = user.Id,
                    Name = user.Name,
                    Password = user.Password,
                    IdRol = user.IdRol
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
                Name = u.Name,
                Password = u.Password,
                IdRol = u.IdRol
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
                Id = id,
                DeletedDate = DateTime.Now
            });

            return RedirectToAction("Index");
        }
    }
}
