using OrganWeb.Areas.Sistema.Models;
using OrganWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OrganWeb.Areas.Sistema.Models.Ferramentas;
using OrganWeb.Areas.Sistema.Models.ViewModels;
using System.Net;
using System.Data.Entity;
using OrganWeb.Models.Banco;

namespace OrganWeb.Areas.Sistema.Controllers
{
    public class ManutencaoController : Controller
    {
        private MaquinaManutencao manumaq = new MaquinaManutencao();
        private Maquina maquina = new Maquina();
        private Manutencao manutencao = new Manutencao();
        private readonly BancoContext db = new BancoContext();

        // GET: Sistema/Manutencao
        public ActionResult Index()
        {
            var select = new ViewManutencao
            {
                MaquinaManutencaos = manumaq.GetFew()
            };
            return View(select);
        }

        public ActionResult Detalhes(int? id, int? id2)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            manumaq = manumaq.GetByID(id, id2);
            if (manumaq == null)
            {
                return HttpNotFound();
            }
            return View(manumaq);
        }

        [HttpGet]
        public ActionResult _NovaManutencao()
        {
            var manutencaom = new MaquinaManutencao
            {
                Maquinas = maquina.GetAll()
            };

            return View(manutencaom);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _NovaManutencao(MaquinaManutencao manutencaom)
        {
            if (ModelState.IsValid)
            {
                manutencao = new Manutencao
                {
                    Nome = manutencaom.Manutencao.Nome,
                    Data = manutencaom.Manutencao.Data,
                    Detalhes = manutencaom.Manutencao.Detalhes
                };
                manutencao.Add(manutencao);
                manutencaom.IdManutencao = manutencao.Id;
                manutencaom.Add(manutencaom);
                manutencaom.Save();
                return RedirectToAction("Index");
            }
            manutencaom.Maquinas = maquina.GetAll();
            return View(manutencaom);
        }

        public ActionResult Editar(int? id, int? id2)
        {
            if (id == null || id2 == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            manumaq = manumaq.GetByID(id, id2);
            if (manumaq == null)
            {
                return HttpNotFound();
            }
            return View(manumaq);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(MaquinaManutencao manumaq)
        {
            if (ModelState.IsValid)
            {
                manutencao = new Manutencao
                {
                    Nome = manumaq.Manutencao.Nome,
                    Data = manumaq.Manutencao.Data,
                    Detalhes = manumaq.Manutencao.Detalhes
                };
                manutencao.Update(manutencao);
                manutencao.Save();
                return RedirectToAction("Index");
            }
            return View(manumaq);
        }

        //http://www.macoratti.net/18/06/mvc5_vmodal2.htm
        //https://cursos.alura.com.br/forum/topico-implementacao-de-alteracao-de-produto-41440

        public ActionResult Excluir(int? id, int? id2)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            manutencao = manutencao.GetByID(id);
            if (manutencao == null)
            {
                return HttpNotFound();
            }
            return View(manutencao);
        }
        
        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public ActionResult ExcluirConfirmado(int id, int id2)
        {
            manutencao = manutencao.GetByID(id);
            manumaq = manumaq.GetByID(id, id2);
            manutencao.Delete(id);
            manumaq.Delete(id, id2);
            manutencao.Save();
            return RedirectToAction("Index");
        }
    }
}