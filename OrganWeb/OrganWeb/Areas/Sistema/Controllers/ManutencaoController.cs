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
        public ActionResult Create()
        {
            var manutencaom = new MaquinaManutencao
            {
                Maquinas = maquina.GetAll()
            };
            ViewBag.Maquinas = new MultiSelectList(manutencaom.Maquinas, "IdEstoque", "Nome");
            return View(manutencaom);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MaquinaManutencao manutencaom, int[] IdMaquina)
        {
            if (ModelState.IsValid)
            {
                manutencao = new Manutencao
                {
                    Nome = manutencaom.Manutencao.Nome,
                    Data = manutencaom.Manutencao.Data,
                    Detalhes = manutencaom.Manutencao.Detalhes,
                    ValorPago = manutencaom.Manutencao.ValorPago
                };
                manutencao.Add(manutencao);
                manutencao.Save(); //Precisa salvar antes pra gerar um Id e asim coloca-lo na tabela máquina-manutenção

                foreach (var item in IdMaquina)
                {
                    manutencaom.Add(new MaquinaManutencao { IdMaquina = item, IdManutencao = manutencao.Id });
                    manutencaom.Save();
                }

                return RedirectToAction("Index");
            }
            //Enviando a dropdownlist caso o formulário não seja preenchido corretamente (NullReferenceException)
            manutencaom.Maquinas = maquina.GetAll();
            ViewBag.Maquinas = new MultiSelectList(manutencaom.Maquinas, "IdEstoque", "Nome");
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
                var manutencao = new Manutencao
                {
                    Id = manumaq.IdManutencao,
                    Nome = manumaq.Manutencao.Nome,
                    Data = manumaq.Manutencao.Data,
                    Detalhes = manumaq.Manutencao.Detalhes,
                    ValorPago = manumaq.Manutencao.ValorPago
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
            manumaq = manumaq.GetByID(id, id2);
            if (manumaq == null)
            {
                return HttpNotFound();
            }
            return View(manumaq);
        }
        
        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public ActionResult ExcluirConfirmado(MaquinaManutencao mm)
        {
            manutencao = manutencao.GetByID(mm.IdManutencao);
            manumaq = manumaq.GetByID(mm.IdManutencao, mm.IdMaquina);
            manumaq.Delete(mm.IdManutencao, mm.IdMaquina);
            manumaq.Save();
            manutencao.Delete(mm.IdManutencao);
            manutencao.Save();
            return RedirectToAction("Index");
        }
    }
}