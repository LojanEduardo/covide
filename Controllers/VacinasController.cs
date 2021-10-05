using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Covid.Models;

namespace Covid.Controllers
{
    public class VacinasController : Controller
    {
        private Contexto db = new Contexto();

        public ActionResult Index(int? id)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            if (id == null)
                return RedirectToAction("Index", "Home");
            Empresa empresa = db.Empresa.Find(id);       
            if (empresa == null)
                return RedirectToAction("Index", "Home");

            ViewBag.NomeEmpresa = empresa.Nome;
            ViewBag.IdEmpresa = empresa.Id;

            var vacina = db.Vacina.Where(x => x.EmpresaId == id && x.Disable == 0).ToList();

            if (TempData["msg"] != null)
            {
                ViewBag.Mensagem = TempData["msg"].ToString();
            }

            return View(vacina);
        }


        public ActionResult Create(int id)
        {
            if(id == null)
                return RedirectToAction("Index", "Home");

            Vacina vacina = new Vacina();
            vacina.EmpresaId = id;

            return View(vacina);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nome,QuantidadeDose,EmpresaId,Disable")] Vacina vacina)
        {
            if (ModelState.IsValid)
            {
                vacina.Disable = 0;
                db.Vacina.Add(vacina);
                db.SaveChanges();

                int user = Convert.ToInt32(User.Identity.Name.Split('|')[0]);
                Usuario usuario = db.Usuario.Find(user);
                Historico historico = new Historico();
                historico.Descricao = "Registrou uma Vacina";
                historico.TabelaNome = "Vacina";
                historico.ItemId = vacina.Id;
                historico.Data = DateTime.Now;
                historico.UsuarioId = usuario.Id;
                db.Historico.Add(historico);
                db.SaveChanges();

                TempData["msg"] = "Vacina criada com sucesso";

                return RedirectToAction("Index", "Vacinas", new { id = vacina.EmpresaId });
            }

            return View(vacina);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vacina vacina = db.Vacina.Find(id);
            if (vacina == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmpresaId = new SelectList(db.Empresa, "Id", "Nome", vacina.EmpresaId);
            return View(vacina);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome,QuantidadeDose,EmpresaId")] Vacina vacina)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vacina).State = EntityState.Modified;
                db.SaveChanges();

                int user = Convert.ToInt32(User.Identity.Name.Split('|')[0]);
                Usuario usuario = db.Usuario.Find(user);
                Historico historico = new Historico();
                historico.Descricao = "Editou uma Vacina";
                historico.TabelaNome = "Vacina";
                historico.ItemId = vacina.Id;
                historico.Data = DateTime.Now;
                historico.UsuarioId = usuario.Id;
                db.Historico.Add(historico);
                db.SaveChanges();

                TempData["msg"] = "Vacina editada com sucesso";

                return RedirectToAction("Index", "Vacinas", new { id = vacina.EmpresaId });
            }
            return View(vacina);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vacina vacina = db.Vacina.Find(id);
            if (vacina == null)
            {
                return HttpNotFound();
            }
            return View(vacina);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vacina vacina = db.Vacina.Find(id);
            vacina.Disable = 1;
            db.Entry(vacina).State = EntityState.Modified;
            db.SaveChanges();

            int user = Convert.ToInt32(User.Identity.Name.Split('|')[0]);
            Usuario usuario = db.Usuario.Find(user);
            Historico historico = new Historico();
            historico.Descricao = "Excluiu uma Vacina";
            historico.TabelaNome = "Vacina";
            historico.ItemId = vacina.Id;
            historico.Data = DateTime.Now;
            historico.UsuarioId = usuario.Id;
            db.Historico.Add(historico);
            db.SaveChanges();

            TempData["msg"] = "Vacina deletada com sucesso";

            return RedirectToAction("Index", "Vacinas", new { id = vacina.EmpresaId });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
