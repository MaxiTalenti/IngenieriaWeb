using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Servicios;

namespace ViewModels
{
    public class UserViewModel
    {
        public long Id { get; set; }

        [Required]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Correo electrónico")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Rol")]
        public int IdRol { get; set; }

        public List<Rol> Roles
        {
            get
            {
                return UserService.GetRole(null).Select(r => new Rol()
                {
                    Description = r.Description,
                    Id = r.Id
                }).ToList();
            }

            set { }
        }
    }

    public class Rol
    {
        public int Id { get; set; }

        public string Description { get; set; }
    }

    public class ListUserViewModel
    {
        public long Id { get; set; }

        [Required]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Correo electrónico")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "IdRol")]
        public int IdRol { get; set; }

        [Required]
        [Display(Name = "Rol")]
        public string Rol { get { return UserService.GetRole(IdRol).FirstOrDefault().Description; } set { } }

        public List<Rol> Roles
        {
            get
            {
                return UserService.GetRole(null).Select(r => new Rol()
                {
                    Description = r.Description,
                    Id = r.Id
                }).ToList();
            }

            set { }
        }
    }
}