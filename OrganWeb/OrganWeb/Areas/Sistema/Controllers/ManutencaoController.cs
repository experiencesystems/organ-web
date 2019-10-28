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
using System.Threading.Tasks;

namespace OrganWeb.Areas.Sistema.Controllers
{
    public class ManutencaoController : Controller
    {
        private MaquinaManutencao manumaq = new MaquinaManutencao();
        private Maquina maquina = new Maquina();
        private Manutencao manutencao = new Manutencao();
        private readonly BancoContext db = new BancoContext();
        
        public async Task<ActionResult> Index()
        {
            var select = new ViewManutencao
            {
                MaquinaManutencaos = await manumaq.GetAll()
            };
            return View(select);
        }

        public async Task<ActionResult> Detalhes(int? id, int? id2)
        {
            if (id == null || id2 == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            manumaq = await manumaq.GetByID(id, id2);
            if (manumaq == null)
            {
                return HttpNotFound();
            }
            return View(manumaq);
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            var manutencaom = new MaquinaManutencao
            {
                Maquinas = await maquina.GetAll()
            };
            return View(manutencaom);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(MaquinaManutencao manutencaom, int[] IdMaquina)
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
                await manutencao.Save(); //Precisa salvar antes pra gerar um Id e asim coloca-lo na tabela máquina-manutenção

                foreach (var item in IdMaquina)
                {
                    manutencaom.Add(new MaquinaManutencao { IdMaquina = item, IdManutencao = manutencao.Id });
                    await manutencaom.Save();
                }

                return RedirectToAction("Index");
            }
            //Enviando a dropdownlist caso o formulário não seja preenchido corretamente (NullReferenceException)
            manutencaom.Maquinas = await maquina.GetAll();
            return View(manutencaom);
        }

        public async Task<ActionResult> Editar(int? id, int? id2)
        {
            if (id == null || id2 == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            manumaq = await manumaq.GetByID(id, id2);
            if (manumaq == null)
            {
                return HttpNotFound();
            }
            return View(manumaq);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar(MaquinaManutencao manumaq)
        {
            if (ModelState.IsValid)
            {
                manutencao = new Manutencao
                {
                    Id = manumaq.IdManutencao,
                    Nome = manumaq.Manutencao.Nome,
                    Data = manumaq.Manutencao.Data,
                    Detalhes = manumaq.Manutencao.Detalhes,
                    ValorPago = manumaq.Manutencao.ValorPago
                };
                manutencao.Update(manutencao);
                await manutencao.Save();
                return RedirectToAction("Index");
            }
            return View(manumaq);
        }

        //http://www.macoratti.net/18/06/mvc5_vmodal2.htm
        //https://cursos.alura.com.br/forum/topico-implementacao-de-alteracao-de-produto-41440

        public async Task<ActionResult> Excluir(int? id, int? id2)
        {
            if (id == null || id2 == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            manumaq = await manumaq.GetByID(id, id2);
            if (manumaq == null)
            {
                return HttpNotFound();
            }
            return View(manumaq);
        }
        
        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExcluirConfirmado(MaquinaManutencao mm)
        {
            manutencao = await manutencao.GetByID(mm.IdManutencao);
            manumaq = await manumaq.GetByID(mm.IdManutencao, mm.IdMaquina);
            manumaq.Delete(mm.IdManutencao, mm.IdMaquina);
            await manumaq.Save();
            manutencao.Delete(mm.IdManutencao);
            await manutencao.Save();
            return RedirectToAction("Index");
        }
    }
}