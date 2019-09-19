using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OrganWeb
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)           
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
              name: "Doenças",
              url: "Doencas/",
              defaults: new { controller = "Doencas", action = "Index", id = UrlParameter.Optional },
              namespaces: new[]{"OrganWeb.Controllers"}
          );
            routes.MapRoute(
              name: "Equipes",
              url: "Equipes/",
              defaults: new { controller = "Equipes", action = "Index", id = UrlParameter.Optional },
              namespaces: new[] { "OrganWeb.Controllers" }
          );
            routes.MapRoute(
             name: "Controle",
             url: "Controle/",
             defaults: new { controller = "Controle", action = "Index", id = UrlParameter.Optional }
             ,
              namespaces: new[] { "OrganWeb.Controllers" }
         );
            routes.MapRoute(
             name: "Estoque",
             url: "Estoque/",
             defaults: new { controller = "Estoque", action = "Index", id = UrlParameter.Optional },
              namespaces: new[] { "OrganWeb.Controllers" }
         );
            routes.MapRoute(
             name: "Fazenda",
             url: "Fazenda/",
             defaults: new { controller = "Fazenda", action = "Index", id = UrlParameter.Optional },
              namespaces: new[] { "OrganWeb.Controllers" }
         );
            routes.MapRoute(
             name: "Fornecedor",
             url: "Fornecedor/",
             defaults: new { controller = "Fornecedor", action = "Index", id = UrlParameter.Optional },
              namespaces: new[] { "OrganWeb.Controllers" }
         );
            routes.MapRoute(
              name: "Funcionario",
              url: "Funcionario/",
              defaults: new { controller = "Funcionario", action = "Index", id = UrlParameter.Optional },
               namespaces: new[] { "OrganWeb.Controllers" }
          );
            routes.MapRoute(
             name: "Item",
             url: "Item/",
             defaults: new { controller = "Item", action = "Index", id = UrlParameter.Optional },
              namespaces: new[] { "OrganWeb.Controllers" }
         );
            routes.MapRoute(
            name: "Manutenção",
            url: "Manutencao/",
            defaults: new { controller = "Manutencao", action = "Index", id = UrlParameter.Optional },
             namespaces: new[] { "OrganWeb.Controllers" }
        );
            routes.MapRoute(
            name: "Maquina",
            url: "Maquina/",
            defaults: new { controller = "Maquina", action = "Index", id = UrlParameter.Optional },
             namespaces: new[] { "OrganWeb.Controllers" }
        );
            routes.MapRoute(
            name: "Monitoramento",
            url: "Monitoramento/",
            defaults: new { controller = "Monitoramento", action = "Index", id = UrlParameter.Optional },
             namespaces: new[] { "OrganWeb.Controllers" }
        );
            routes.MapRoute(
            name: "Praga",
            url: "Praga/",
            defaults: new { controller = "Praga", action = "Index", id = UrlParameter.Optional },
             namespaces: new[] { "OrganWeb.Controllers" }
        );
            routes.MapRoute(
            name: "Tarefa",
            url: "Tarefa/",
            defaults: new { controller = "Tarefa", action = "Index", id = UrlParameter.Optional },
             namespaces: new[] { "OrganWeb.Controllers" }
        );
            routes.MapRoute(
            name: "VwItems",
            url: "VwItems/",
            defaults: new { controller = "VwItems", action = "Index", id = UrlParameter.Optional },
             namespaces: new[] { "OrganWeb.Controllers" }
        );
            routes.MapRoute(
            name: "Login",
            url: "LoginRegistro/",
            defaults: new { controller = "Account", action = "LoginRegistro", id = UrlParameter.Optional },
             namespaces: new[] { "OrganWeb.Controllers" }
        );
        }
    }
}
