
using RepositorioClases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    /// <summary>
    /// Acciones comunes sobre usuarios.
    /// </summary>
    public static class UserService
    {
        /// <summary>
        /// Creación de usuario
        /// </summary>
        /// <param name="user"></param>
        public static void Create(Users user)
        {
            using (Modelo context = new Modelo())
            {
                context.Users.Add(new Users()
                {
                    Email = user.Email,
                    Usuario = user.Usuario,
                    Id = user.Id,
                    Estado = user.Estado
                });

                context.SaveChanges();
            }
        }

        /// <summary>
        /// Obtiene usuarios menos lo que tienen estado eliminados.
        /// </summary>
        /// <param name="id">Busca por id (opcional)</param>
        /// <returns>Lista de usuario/s</returns>
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

                //return context.Users.Where(u => !u.DeletedDate.HasValue && (id.HasValue ? id.Value == u.Id : true)).ToList();// Select(u => new Users()
                return context.Users.Where(u => id.HasValue ? id.Value == u.Id : true).Where(z => z.Estado != UserState.Eliminado).ToList();// Select(u => new Users()
                //{
                //    Email = u.Email,
                //    Id = u.Id,
                //    Name = u.Name,
                //    Password = u.Password
                //}).ToList();
            }
        }

        /// <summary>
        /// Editar usuario
        /// </summary>
        /// <param name="user">Usuario a editar</param>
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
                    users.Usuario = user.Usuario;
                    users.Apellido = user.Apellido;
                    users.Nombre = user.Nombre;
                    users.Estado = user.Estado;
                }

                // el objeto en memoria persiste los cambios en la base de datos cuando hago un save sobre el contexto.
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Eliminar usuario (Marca como eliminado).
        /// </summary>
        /// <param name="user">Usuario a eliminar</param>
        public static void Delete(Users user)
        {
            using (Modelo context = new Modelo())
            {
                Users users = context.Users.Where(u => u.Id == user.Id).FirstOrDefault();

                // FirstOrDefault va a intentar recuperar el registro que cumpla la condición
                // si no encuentra ninguno, devuelve NULL, de ahí el siguiente IF.
                if (users != null)
                    users.Estado = UserState.Eliminado;

                // el objeto en memoria persiste los cambios en la base de datos cuando hago un save sobre el contexto.
                context.SaveChanges();
            }
        }
    }
}
