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
    public class EmpresasController : Controller
    {
        private Contexto db = new Contexto();

        // GET: Empresas
        public ActionResult Index()
        {        
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            var empresa = db.Empresa.Where(x => x.Disable == 0).ToList();

            if (TempData["msg"] != null)
            {
                ViewBag.Mensagem = TempData["msg"].ToString();
            }          

            return View(empresa.ToList());
        }

        public ActionResult Create()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nome,UsuarioId,Disable")] Empresa empresa)
        {
            if (ModelState.IsValid)
            {
                empresa.Disable = 0;
                db.Empresa.Add(empresa);
                db.SaveChanges();

                int user = Convert.ToInt32(User.Identity.Name.Split('|')[0]);
                Usuario usuario = db.Usuario.Find(user);
                Historico historico = new Historico();
                historico.Descricao = "Registrou uma Empresa";
                historico.TabelaNome = "Empresa";
                historico.ItemId = empresa.Id;
                historico.Data = DateTime.Now;
                historico.UsuarioId = usuario.Id;
                db.Historico.Add(historico);
                db.SaveChanges();

                TempData["msg"] = "Empresa criada com sucesso";

                return RedirectToAction("Index");
            }

            return View(empresa);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empresa empresa = db.Empresa.Find(id);
            if (empresa == null)
            {
                return HttpNotFound();
            }

            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            return View(empresa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome")] Empresa empresa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(empresa).State = EntityState.Modified;
                db.SaveChanges();

                int user = Convert.ToInt32(User.Identity.Name.Split('|')[0]);
                Usuario usuario = db.Usuario.Find(user);
                Historico historico = new Historico();
                historico.Descricao = "Editou uma Empresa";
                historico.TabelaNome = "Empresa";
                historico.ItemId = empresa.Id;
                historico.Data = DateTime.Now;
                historico.UsuarioId = usuario.Id;
                db.Historico.Add(historico);
                db.SaveChanges();

                TempData["msg"] = "Empresa editada com sucesso";

                return RedirectToAction("Index");
            }
            return View(empresa);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empresa empresa = db.Empresa.Find(id);
            if (empresa == null)
            {
                return HttpNotFound();
            }

            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            return View(empresa);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Empresa empresa = db.Empresa.Find(id);
            empresa.Disable = 1;
            db.Entry(empresa).State = EntityState.Modified;
            db.SaveChanges();

            int user = Convert.ToInt32(User.Identity.Name.Split('|')[0]);
            Usuario usuario = db.Usuario.Find(user);
            Historico historico = new Historico();
            historico.Descricao = "Excluir uma empresa";
            historico.TabelaNome = "Empresa";
            historico.Data = DateTime.Now;
            historico.ItemId = empresa.Id;
            historico.UsuarioId = usuario.Id;
            db.Historico.Add(historico);
            db.SaveChanges();

            TempData["msg"] = "Empresa deletada com sucesso";

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
