using OrganWeb.Areas.Sistema.Models;
using OrganWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrganWeb.Areas.Sistema.Controllers
{
    public class PlantioController : Controller
    {
        private Plantio plantio = new Plantio();

        // GET: Sistema/Plantio
        public ActionResult Index()
        {
            var select = plantio.GetPlantios();
            return View(select);
        }
    }
}