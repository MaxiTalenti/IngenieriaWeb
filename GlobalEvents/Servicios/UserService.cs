﻿
using RepositorioClases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public static class UserService
    {
        public static void Create(Users user)
        {
            using (Modelo context = new Modelo())
            {
                context.Users.Add(new Users()
                {
                    IdRol = user.IdRol,
                    Email = user.Email,
                    Name = user.Name,
                    Password = user.Password
                });

                context.SaveChanges();
            }
        }

        public static List<Users> Get(int? id)
        {
            using (Modelo context = new Modelo())
            {
                // la propiedad HasValue de los objetos que son Nullables o permiten tomar valores nulos, equivale a hacer la pregunta if (valor != null)
                // la expresión id.HasValue ? id.Value == u.Id : true
                // equivale a (id.HasValue && id.Value == u.Id) || (!id.HasValue)
                // lo que hacen es filtrar solo cuando viene un valor en la variable, ahorrándonos hacer un if antes y repetir la consulta.


                //var detalle = (from d in context.Users
                //              select  new Users
                //{
                //    Email = d.Email,
                //    Id = d.Id,
                //    Name = d.Name,
                //    Password = d.Password
                //}).ToList();

                //return context.Users.ToList();

                //return new List<Users>();

                return context.Users.Where(u => !u.DeletedDate.HasValue && (id.HasValue ? id.Value == u.Id : true)).ToList();// Select(u => new Users()
                //{
                //    Email = u.Email,
                //    Id = u.Id,
                //    Name = u.Name,
                //    Password = u.Password
                //}).ToList();
            }
        }

        public static void Edit(Users user)
        {
            using (Modelo context = new Modelo())
            {
                Users users = context.Users.Where(u => u.Id == user.Id).FirstOrDefault();

                // FirstOrDefault va a intentar recuperar el registro que cumpla la condición
                // si no encuentra ninguno, devuelve NULL, de ahí el siguiente IF.
                if (users != null)
                {
                    users.Email = user.Email;
                    users.Name = user.Name;
                    users.Password = user.Password;
                }

                // el objeto en memoria persiste los cambios en la base de datos cuando hago un save sobre el contexto.
                context.SaveChanges();
            }
        }

        public static void Delete(Users user)
        {
            using (Modelo context = new Modelo())
            {
                Users users = context.Users.Where(u => u.Id == user.Id).FirstOrDefault();

                // FirstOrDefault va a intentar recuperar el registro que cumpla la condición
                // si no encuentra ninguno, devuelve NULL, de ahí el siguiente IF.
                if (users != null)
                    users.DeletedDate = DateTime.Now;

                // el objeto en memoria persiste los cambios en la base de datos cuando hago un save sobre el contexto.
                context.SaveChanges();
            }
        }

        public static List<Roles> GetRole(int? id)
        {
            using (Modelo context = new Modelo())
            {
                // la propiedad HasValue de los objetos que son Nullables o permiten tomar valores nulos, equivale a hacer la pregunta if (valor != null)
                // la expresión id.HasValue ? id.Value == u.Id : true
                // equivale a (id.HasValue && id.Value == u.Id) || (!id.HasValue)
                // lo que hacen es filtrar solo cuando viene un valor en la variable, ahorrándonos hacer un if antes y repetir la consulta.
                return context.Roles.Where(r => id.HasValue ? id.Value == r.Id : true).ToList();
               
            }
        }
    }
}