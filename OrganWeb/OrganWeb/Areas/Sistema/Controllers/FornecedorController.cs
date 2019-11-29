using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OrganWeb.Areas.Sistema.Models.ViewModels;
using OrganWeb.Areas.Sistema.Models.Armazenamento;
using OrganWeb.Areas.Sistema.Models.ViewsBanco.Pessoa;
using System.Threading.Tasks;
using OrganWeb.Areas.Ecommerce.Models.Endereco;
using OrganWeb.Areas.Sistema.Models.Telefone;

namespace OrganWeb.Areas.Sistema.Controllers
{
    [Authorize(Roles = "Admin")]
    public class FornecedorController : Controller
    {
        private TipoTel tipotel = new TipoTel();
        private Telefone telefone = new Telefone();
        private TelForn telForn = new TelForn();
        private Fornecedor fornecedor = new Fornecedor();
        private DDD ddd = new DDD();
        private VwFornecedor vwfornecedor = new VwFornecedor();

        public ActionResult Index()
        {
            return RedirectToAction("Index", "Estoque");
        }
        
        public async Task<ActionResult> Create()
        {
            return View(new Fornecedor
            {
                Telefone = new Telefone
                {
                    DDDs = await ddd.GetAll()
                }
            });
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Fornecedor model)
        {
            if (ModelState.IsValid)
            {
                tipotel = new TipoTel
                {
                    Tipo = model.Telefone.TipoTel.Tipo
                };
                tipotel.Add(tipotel);
                await tipotel.Save();

                telefone = new Telefone
                {
                    Numero = model.Telefone.Numero,
                    IdDDD = model.Telefone.IdDDD,
                    IdTipo = tipotel.Id
                };
                telefone.Add(telefone);
                await telefone.Save();

                model.Status = true;
                model.Add(model);
                await model.Save();

                telForn = new TelForn
                {
                    IdForn = model.Id,
                    IdTelefone = telefone.Id
                };
                telForn.Add(telForn);
                await telForn.Save();

                return RedirectToAction("Index");
            }
            fornecedor.Telefone.DDDs = await ddd.GetAll();
            return View(fornecedor);
        }
        
        public async Task<ActionResult> Excluir(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vwfornecedor = await vwfornecedor.GetByID(id);
            if (vwfornecedor == null)
            {
                return HttpNotFound();
            }
            return View(vwfornecedor);
        }

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExcluirConfirmado(VwFornecedor vwfornecedor)
        {
            fornecedor = await fornecedor.GetByID(vwfornecedor.Id);
            fornecedor.Status = false;
            fornecedor.Update(fornecedor);
            await fornecedor.Save();
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            fornecedor = await fornecedor.GetByID(id);
            if (fornecedor == null)
            {
                return HttpNotFound();
            }
            return View(fornecedor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar(Fornecedor fornecedor)
        {
            if (ModelState.IsValid)
            {
                fornecedor.Update(fornecedor);
                await fornecedor.Save();
                return RedirectToAction("Index");
            }
            return View(fornecedor);
        }

        public async Task<ActionResult> Detalhes(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vwfornecedor = await vwfornecedor.GetByID(id);
            if (vwfornecedor == null)
            {
                return HttpNotFound();
            }
            return View(vwfornecedor);
        }
    }
}
