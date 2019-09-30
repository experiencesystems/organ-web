using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OrganWeb.Areas.Sistema.Models;
using OrganWeb.Areas.Sistema.Models.Funcionarios;
using OrganWeb.Models;

namespace OrganWeb.Areas.Sistema.Controllers
{
    public class FuncionarioController : Controller
    {
        /*private BancoContext db = new BancoContext();

        // GET: Sistema/Funcionario
        public ActionResult Index()
        {
            {
                var funcionarios = db.Funcionarios
                    .Include(f => f.Cargo)
                    .ToList();

                return View(funcionarios);
            }
        }

        // GET: Sistema/Funcionario/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Funcionario funcionario = db.Funcionarios.Find(id);
            if (funcionario == null)
            {
                return HttpNotFound();
            }
            return View(funcionario);
        }

        // GET: Sistema/Funcionario/Create
        public ActionResult Create()
        {
            ViewBag.IdCargo = new SelectList(db.Cargos, "Id", "Nome");
            ViewBag.CEP = new SelectList(db.Localizacaos, "CEP", "Endereco");
            ViewBag.IdUsuario = new SelectList(db.Users, "Id", "Email");
            return View();
        }

        // POST: Sistema/Funcionario/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nome,Sobrenome,CPF,RG,DataNascimento,Email,Salario,GrauInstrucao,DataContratacao,TipoContratacao,PeriodoContratacao,MesAno,IdCargo,CEP,Numero,IdUsuario")] Funcionario funcionario)
        {
            if (ModelState.IsValid)
            {
                db.Funcionarios.Add(funcionario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdCargo = new SelectList(db.Cargos, "Id", "Nome", funcionario.IdCargo);
            ViewBag.CEP = new SelectList(db.Localizacaos, "CEP", "Endereco", funcionario.CEP);
            ViewBag.IdUsuario = new SelectList(db.Users, "Id", "Email", funcionario.IdUsuario);
            return View(funcionario);
        }

        // GET: Sistema/Funcionario/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Funcionario funcionario = db.Funcionarios.Find(id);
            if (funcionario == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdCargo = new SelectList(db.Cargos, "Id", "Nome", funcionario.IdCargo);
            ViewBag.CEP = new SelectList(db.Localizacaos, "CEP", "Endereco", funcionario.CEP);
            ViewBag.IdUsuario = new SelectList(db.Users, "Id", "Email", funcionario.IdUsuario);
            return View(funcionario);
        }

        // POST: Sistema/Funcionario/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome,Sobrenome,CPF,RG,DataNascimento,Email,Salario,GrauInstrucao,DataContratacao,TipoContratacao,PeriodoContratacao,MesAno,IdCargo,CEP,Numero,IdUsuario")] Funcionario funcionario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(funcionario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdCargo = new SelectList(db.Cargos, "Id", "Nome", funcionario.IdCargo);
            ViewBag.CEP = new SelectList(db.Localizacaos, "CEP", "Endereco", funcionario.CEP);
            ViewBag.IdUsuario = new SelectList(db.Users, "Id", "Email", funcionario.IdUsuario);
            return View(funcionario);
        }

        // GET: Sistema/Funcionario/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Funcionario funcionario = db.Funcionarios.Find(id);
            if (funcionario == null)
            {
                return HttpNotFound();
            }
            return View(funcionario);
        }

        // POST: Sistema/Funcionario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Funcionario funcionario = db.Funcionarios.Find(id);
            db.Funcionarios.Remove(funcionario);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }*/
    }
}
