using System.Web.Mvc;

namespace OrganWeb.Areas.Ecommerce
{
    public class EcommerceAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Ecommerce";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Ecommerce_default",
                "Ecommerce/{controller}/{action}/{id}",
                new { controller = "Loja", action = "Index", id = UrlParameter.Optional },
                new[] { "OrganWeb.Areas.Ecommerce.Controllers" }
            );
        }
    }
}