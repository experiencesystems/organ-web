using OrganWeb.Areas.Sistema.Models.Administrativo;
using OrganWeb.Areas.Sistema.Models.Funcionarios;
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
    [Authorize]
    public class FazendaController : Controller
    {   
        private Solo solo = new Solo();
        private ViewFazenda vwFazenda = new ViewFazenda();
        private VwFuncionario funcionario = new VwFuncionario();
        private Area area = new Area();

        public async Task<ActionResult> Index()
        {
            vwFazenda = new ViewFazenda()
            {
                Areas = await area.GetAll(),
                Funcionarios = await funcionario.GetAll(),
                Solos = await solo.GetAll()
            };
            return View(vwFazenda);
        }
    }
}