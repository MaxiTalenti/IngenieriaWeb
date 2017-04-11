using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading;
using System.Web.Mvc;
using WebMatrix.WebData;
using RepositorioClases;
using System.Web.Routing;

namespace GlobalEvents.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class InitializeSimpleMembershipAttribute : ActionFilterAttribute
    {
        private static SimpleMembershipInitializer _initializer;
        private static object _initializerLock = new object();
        private static bool _isInitialized;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Asegúrese de que ASP.NET Simple Membership se inicialice solo una vez por inicio de la aplicación
            LazyInitializer.EnsureInitialized(ref _initializer, ref _isInitialized, ref _initializerLock);
        }

        private class SimpleMembershipInitializer
        {
            public SimpleMembershipInitializer()
            {
                Database.SetInitializer<Modelo>(null);

                try
                {
                    using (var context = new Modelo())
                    {
                        if (!context.Database.Exists())
                        {
                            // Crear la base de datos SimpleMembership sin el esquema de migración de Entity Framework
                            ((IObjectContextAdapter)context).ObjectContext.CreateDatabase();
                        }
                    }

                    WebSecurity.InitializeDatabaseConnection("Modelo", "Users", "Id", "Usuario", true);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("No se pudo inicializar la base de datos de ASP.NET Simple Membership. Para obtener más información, consulte http://go.microsoft.com/fwlink/?LinkId=256588", ex);
                }
            }
        }
    }

    public class MyAuthorizeAttribute : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                // El usuario no esta autenticado.
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "Login", returnURL =  filterContext.HttpContext.Request.Url.AbsolutePath }));
                //base.HandleUnauthorizedRequest(filterContext);
            }
            else
            {
                // Esta autenticado.
                // Acà falta ver el tema de los roles.

                /*
                if (!this.Roles.Split(',').Any(filterContext.HttpContext.User.IsInRole))
                {
                    // The user is not in any of the listed roles => 
                    // show the unauthorized view
                    filterContext.Result = new ViewResult
                    {
                        ViewName = "~/Views/Shared/Unauthorized.cshtml"
                    };
                }
                else
                {
                    base.HandleUnauthorizedRequest(filterContext);
                }
                */
            }
        }
    }
}
