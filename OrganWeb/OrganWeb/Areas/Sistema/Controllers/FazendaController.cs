using OrganWeb.Areas.Sistema.Models.Administrativo;
using OrganWeb.Areas.Sistema.Models.ViewModels;
using OrganWeb.Areas.Sistema.Models.ViewsBanco.Pessoa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OrganWeb.Areas.Sistema.Controllers
{
    public class FazendaController : Controller
    {   //TODO: mais de uma colheita
        //TODO: plantios incompletos + com mais de uma colheita
        private Solo solo = new Solo();
        private ViewFazenda vwFazenda = new ViewFazenda();
        private VwFuncionario vwFuncionario = new VwFuncionario();
        private Area area = new Area();

        public async Task<ActionResult> Index()
        {
            vwFazenda = new ViewFazenda()
            {
                Areas = await area.GetAll(),
                VwFuncionarios = await vwFuncionario.GetAll(),
                Solos = await solo.GetAll()
            };
            return View(vwFazenda);
        }
    }
}