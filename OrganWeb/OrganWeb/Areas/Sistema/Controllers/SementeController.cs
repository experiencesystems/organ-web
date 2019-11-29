using OrganWeb.Areas.Sistema.Models.API;
using OrganWeb.Areas.Sistema.Models.Armazenamento;
using OrganWeb.Areas.Sistema.Models.Safras;
using OrganWeb.Areas.Sistema.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OrganWeb.Areas.Sistema.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SementeController : Controller
    {
        private Semente semente = new Semente();
        private Estoque estoque = new Estoque();
        private ListarUnidades unmd = new ListarUnidades();
        private UnidadeCadastro uncd = new UnidadeCadastro();

        public ActionResult Sementes()
        {
            return RedirectToAction("Index", "Estoque");
        }

        public async Task<ActionResult> Create()
        {
            var responseModel = await unmd.GetListarUnidades();
            semente = new Semente()
            {
                Estoque = new Estoque
                {
                    Unidades = responseModel.UnidadeCadastros,
                    Fornecedores = await new Fornecedor().GetAll()
                }
            };
            return View(semente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Semente semente)
        {
            if (ModelState.IsValid)
            {
                var estoque = new Estoque()
                {
                    Qtd = semente.Estoque.Qtd,
                    UM = semente.Estoque.UM,
                    IdFornecedor = semente.Estoque.IdFornecedor
                };
                estoque.Add(estoque);
                await estoque.Save();
                estoque.Dispose();

                semente.IdEstoque = estoque.Id;
                semente.Estoque = null;
                estoque = null;
                semente.Add(semente);
                await semente.Save();
                semente.Dispose();

                return RedirectToAction("Index");
            }
            return View(semente);
        }

        public async Task<ActionResult> Detalhes(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            semente = await semente.GetByID(id);
            if (semente == null)
            {
                return HttpNotFound();
            }
            return View(semente);
        }

        public async Task<ActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            semente = await semente.GetByID(id);
            if (semente == null)
            {
                return HttpNotFound();
            }
            semente.Estoque.Fornecedores = await new Fornecedor().GetAll();
            return View(semente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar(Semente semente)
        {
            if (ModelState.IsValid)
            {
                semente.Update(semente);
                await semente.Save();
                estoque.Update(semente.Estoque);
                await estoque.Save();
                return RedirectToAction("Index", "Estoque");
            }
            semente.Estoque.Fornecedores = await new Fornecedor().GetAll();
            return View(semente);
        }

        public async Task<ActionResult> Excluir(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            semente = await semente.GetByID(id);
            if (semente == null)
            {
                return HttpNotFound();
            }
            return View(semente);
        }

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExcluirConfirmado(Semente semente)
        {
            semente.Delete(semente.IdEstoque);
            await semente.Save();

            return RedirectToAction("Index", "Estoque");
        }
    }
}