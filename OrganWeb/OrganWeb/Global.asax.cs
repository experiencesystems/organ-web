using OrganWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace OrganWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
	        ModelBinders.Binders.Add(typeof(decimal?), new DecimalModelBinder());
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            Response.Clear();
            if (exception is HttpException httpException)
            {
                string action;

                switch (httpException.GetHttpCode())
                {
                    case 404:
                        // page not found
                        action = "NotFound";
                        break;
                    case 403:
                        // forbidden
                        //action = "Forbidden";
                        action = "NotFound";
                        break;
                    case 500:
                        // server error
                        //action = "HttpError500";
                        action = "NotFound";
                        break;
                    default:
                        //action = "Unknown";
                        action = "NotFound";
                        break;
                }
                // clear error on server
                Server.ClearError();

                Response.Redirect(String.Format("~/Error/{0}", action));
            }
            else
            {
                // this is my modification, which handles any type of an exception.
                Response.Redirect(String.Format("~/Error/NotFound"));
            }
        }
    }
}
