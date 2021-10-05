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
    public class LotesController : Controller
    {
        private Contexto db = new Contexto();

        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            var lote = db.Lote.Where(x => x.Disable == 0).Include(l => l.Local).Include(l => l.Vacina).ToList();

            if (TempData["msg"] != null)
            {
                ViewBag.Mensagem = TempData["msg"].ToString();
            }

            return View(lote);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lote lote = db.Lote.Find(id);
            if (lote == null)
            {
                return HttpNotFound();
            }
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            return View(lote);
        }

        public ActionResult Create()
        {
            ViewBag.LocalId = new SelectList(db.Local, "Id", "Cidade");
            ViewBag.VacinaId = new SelectList(db.Vacina, "Id", "Nome");
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Quantidade,Disable,VacinaId,LocalId")] Lote lote)
        {
            if (ModelState.IsValid)
            {
                lote.Disable = 0;
                db.Lote.Add(lote);
                db.SaveChanges();

                int user = Convert.ToInt32(User.Identity.Name.Split('|')[0]);
                Usuario usuario = db.Usuario.Find(user);
                Historico historico = new Historico();
                historico.Descricao = "Registrou um lote de vacinas";
                historico.TabelaNome = "Lote de vacinas";
                historico.ItemId = lote.Id;
                historico.Data = DateTime.Now;
                historico.UsuarioId = usuario.Id;
                db.Historico.Add(historico);
                db.SaveChanges();

                TempData["msg"] = "Lote de vacinas criado com sucesso";

                return RedirectToAction("Index");
            }

            ViewBag.LocalId = new SelectList(db.Local, "Id", "Cidade", lote.LocalId);
            ViewBag.VacinaId = new SelectList(db.Vacina, "Id", "Nome", lote.VacinaId);
            return View(lote);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lote lote = db.Lote.Find(id);
            if (lote == null)
            {
                return HttpNotFound();
            }
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            ViewBag.LocalId = new SelectList(db.Local, "Id", "Cidade", lote.LocalId);
            ViewBag.VacinaId = new SelectList(db.Vacina, "Id", "Nome", lote.VacinaId);
            return View(lote);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Quantidade,Disable,VacinaId,LocalId")] Lote lote)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lote).State = EntityState.Modified;
                db.SaveChanges();

                int user = Convert.ToInt32(User.Identity.Name.Split('|')[0]);
                Usuario usuario = db.Usuario.Find(user);
                Historico historico = new Historico();
                historico.Descricao = "Editou um lote de vacinas";
                historico.TabelaNome = "Lote de vacinas";
                historico.ItemId = lote.Id;
                historico.Data = DateTime.Now;
                historico.UsuarioId = usuario.Id;
                db.Historico.Add(historico);
                db.SaveChanges();

                TempData["msg"] = "Lote de vacinas editado com sucesso";

                return RedirectToAction("Index");
            }
            ViewBag.LocalId = new SelectList(db.Local, "Id", "Cidade", lote.LocalId);
            ViewBag.VacinaId = new SelectList(db.Vacina, "Id", "Nome", lote.VacinaId);
            return View(lote);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lote lote = db.Lote.Find(id);
            if (lote == null)
            {
                return HttpNotFound();
            }
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            return View(lote);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Lote lote = db.Lote.Find(id);
            lote.Disable = 1;
            db.Entry(lote).State = EntityState.Modified;
            db.SaveChanges();

            int user = Convert.ToInt32(User.Identity.Name.Split('|')[0]);
            Usuario usuario = db.Usuario.Find(user);
            Historico historico = new Historico();
            historico.Descricao = "Excluiu um lote de vacinas";
            historico.TabelaNome = "Lote de vacinas";
            historico.ItemId = lote.Id;
            historico.Data = DateTime.Now;
            historico.UsuarioId = usuario.Id;
            db.Historico.Add(historico);
            db.SaveChanges();

            TempData["msg"] = "Lote de vacinas deletado com sucesso";

            return RedirectToAction("Index");
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
