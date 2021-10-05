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
    public class LocalsController : Controller
    {
        private Contexto db = new Contexto();

        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            var local = db.Local.Where(x => x.Disable == 0).ToList();

            if (TempData["msg"] != null)
            {
                ViewBag.Mensagem = TempData["msg"].ToString();
            }

            return View(local.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Local local = db.Local.Find(id);
            if (local == null)
            {
                return HttpNotFound();
            }

            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            return View(local);
        }

        public ActionResult Create()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Cidade,Bairro,Rua,Numero,Disable")] Local local)
        {
            if (ModelState.IsValid)
            {
                db.Local.Add(local);
                db.SaveChanges();

                int user = Convert.ToInt32(User.Identity.Name.Split('|')[0]);
                Usuario usuario = db.Usuario.Find(user);
                Historico historico = new Historico();
                historico.Descricao = "Registrou um local de vacinação";
                historico.TabelaNome = "Local";
                historico.ItemId = local.Id;
                historico.Data = DateTime.Now;
                historico.UsuarioId = usuario.Id;
                db.Historico.Add(historico);
                db.SaveChanges();

                TempData["msg"] = "Localização criada com sucesso";

                return RedirectToAction("Index");
            }

            return View(local);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Local local = db.Local.Find(id);
            if (local == null)
            {
                return HttpNotFound();
            }

            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            return View(local);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Cidade,Bairro,Rua,Numero")] Local local)
        {
            if (ModelState.IsValid)
            {
                db.Entry(local).State = EntityState.Modified;
                db.SaveChanges();


                int user = Convert.ToInt32(User.Identity.Name.Split('|')[0]);
                Usuario usuario = db.Usuario.Find(user);
                Historico historico = new Historico();
                historico.Descricao = "Editou um local de vacinação";
                historico.TabelaNome = "Local";
                historico.ItemId = local.Id;
                historico.Data = DateTime.Now;
                historico.UsuarioId = usuario.Id;
                db.Historico.Add(historico);
                db.SaveChanges();

                TempData["msg"] = "Localização editada com sucesso";

                return RedirectToAction("Index");
            }
            return View(local);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Local local = db.Local.Find(id);
            if (local == null)
            {
                return HttpNotFound();
            }
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            return View(local);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Local local = db.Local.Find(id);
            local.Disable = 1;
            db.Entry(local).State = EntityState.Modified;
            db.SaveChanges();

            int user = Convert.ToInt32(User.Identity.Name.Split('|')[0]);
            Usuario usuario = db.Usuario.Find(user);
            Historico historico = new Historico();
            historico.Descricao = "Excluiu um local de vacinação";
            historico.TabelaNome = "Local";
            historico.ItemId = local.Id;
            historico.Data = DateTime.Now;
            historico.UsuarioId = usuario.Id;
            db.Historico.Add(historico);
            db.SaveChanges();

            TempData["msg"] = "Localização deletada com sucesso";

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
