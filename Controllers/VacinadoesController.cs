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
    public class VacinadoesController : Controller
    {
        private Contexto db = new Contexto();

        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            var vacinado = db.Vacinado.Include(x => x.Lote).Where(y => y.Disable ==0);

            if (TempData["msg"] != null)
            {
                ViewBag.Mensagem = TempData["msg"].ToString();
            }

            return View(vacinado.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vacinado vacinado = db.Vacinado.Find(id);
            if (vacinado == null)
            {
                return HttpNotFound();
            }
            return View(vacinado);
        }

        public ActionResult Create()
        {
            ViewBag.LoteId = new SelectList(db.Lote, "Id", "Id");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Cpf,Nome,DataNascimento,DataVacinado,Dose,Disable,LoteId")] Vacinado vacinado)
        {
            if (ModelState.IsValid)
            {
                vacinado.Disable = 0;
                vacinado.DataVacinado = DateTime.Now;
                db.Vacinado.Add(vacinado);
                db.SaveChanges();

                int user = Convert.ToInt32(User.Identity.Name.Split('|')[0]);
                Usuario usuario = db.Usuario.Find(user);
                Historico historico = new Historico();
                historico.Descricao = "Registrou um vacinado";
                historico.TabelaNome = "Vacinado";
                historico.ItemId = vacinado.Id;
                historico.Data = DateTime.Now;
                historico.UsuarioId = usuario.Id;
                db.Historico.Add(historico);
                db.SaveChanges();

                TempData["msg"] = "Vacinado criado com sucesso";

                return RedirectToAction("Index");
            }

            ViewBag.LoteId = new SelectList(db.Lote, "Id", "Id", vacinado.LoteId);
            return View(vacinado);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vacinado vacinado = db.Vacinado.Find(id);
            if (vacinado == null)
            {
                return HttpNotFound();
            }
            ViewBag.LoteId = new SelectList(db.Lote, "Id", "Id", vacinado.LoteId);
            return View(vacinado);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Cpf,Nome,DataNascimento,DataVacinado,Dose,Disable,LoteId")] Vacinado vacinado)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vacinado).State = EntityState.Modified;
                db.SaveChanges();

                int user = Convert.ToInt32(User.Identity.Name.Split('|')[0]);
                Usuario usuario = db.Usuario.Find(user);
                Historico historico = new Historico();
                historico.Descricao = "Editou um vacinado";
                historico.TabelaNome = "Vacinado";
                historico.ItemId = vacinado.Id;
                historico.Data = DateTime.Now;
                historico.UsuarioId = usuario.Id;
                db.Historico.Add(historico);
                db.SaveChanges();

                TempData["msg"] = "Vacinado editado com sucesso";

                return RedirectToAction("Index");
            }
            ViewBag.LoteId = new SelectList(db.Lote, "Id", "Id", vacinado.LoteId);
            return View(vacinado);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vacinado vacinado = db.Vacinado.Find(id);
            if (vacinado == null)
            {
                return HttpNotFound();
            }
            return View(vacinado);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vacinado vacinado = db.Vacinado.Find(id);
            vacinado.Disable = 1;
            db.Entry(vacinado).State = EntityState.Modified;
            db.SaveChanges();

            int user = Convert.ToInt32(User.Identity.Name.Split('|')[0]);
            Usuario usuario = db.Usuario.Find(user);
            Historico historico = new Historico();
            historico.Descricao = "Excluiu um Vacinado";
            historico.TabelaNome = "Vacinado";
            historico.ItemId = vacinado.Id;
            historico.Data = DateTime.Now;
            historico.UsuarioId = usuario.Id;
            db.Historico.Add(historico);
            db.SaveChanges();

            TempData["msg"] = "Vacinado deletado com sucesso";

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
