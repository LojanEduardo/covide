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
    public class DiaVacinacaosController : Controller
    {
        private Contexto db = new Contexto();

        public ActionResult Index(int?id)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            var diaV = db.DiaVacinacao.Where(x => x.Disable == 0).ToList();

            if (TempData["msg"] != null)
            {
                ViewBag.Mensagem = TempData["msg"].ToString();
            }

            return View(diaV.ToList());
        }

        public ActionResult Create()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            ViewBag.LoteId = new SelectList(db.Lote, "Id", "Id");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DataVacinacao,Disable,LoteId")] DiaVacinacao diaVacinacao)
        {
            if (ModelState.IsValid)
            {
                diaVacinacao.Disable = 0;
                db.DiaVacinacao.Add(diaVacinacao);
                db.SaveChanges();

                int user = Convert.ToInt32(User.Identity.Name.Split('|')[0]);
                Usuario usuario = db.Usuario.Find(user);
                Historico historico = new Historico();
                historico.Descricao = "Registrou um Dia de vacinação";
                historico.TabelaNome = "Dia Vacinação";
                historico.ItemId = diaVacinacao.Id;
                historico.Data = DateTime.Now;
                historico.UsuarioId = usuario.Id;
                db.Historico.Add(historico);
                db.SaveChanges();

                TempData["msg"] = "Dia vacinação criado com sucesso";

                return RedirectToAction("Index");
            }

            return View(diaVacinacao);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiaVacinacao diaVacinacao = db.DiaVacinacao.Find(id);
            if (diaVacinacao == null)
            {
                return HttpNotFound();
            }
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            ViewBag.LoteId = new SelectList(db.Lote, "Id", "Id", diaVacinacao.LoteId);
            return View(diaVacinacao);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DataVacinacao,Disable,LoteId")] DiaVacinacao diaVacinacao)
        {
            if (ModelState.IsValid)
            {
                db.Entry(diaVacinacao).State = EntityState.Modified;
                db.SaveChanges();

                int user = Convert.ToInt32(User.Identity.Name.Split('|')[0]);
                Usuario usuario = db.Usuario.Find(user);
                Historico historico = new Historico();
                historico.Descricao = "Editou um Dia de Vacinação";
                historico.TabelaNome = "Dia Vacinação";
                historico.ItemId = diaVacinacao.Id;
                historico.Data = DateTime.Now;
                historico.UsuarioId = usuario.Id;
                db.Historico.Add(historico);
                db.SaveChanges();

                TempData["msg"] = "Dia da vacinação editado com sucesso";

                return RedirectToAction("Index");
            }
            ViewBag.LoteId = new SelectList(db.Lote, "Id", "Id", diaVacinacao.LoteId);
            return View(diaVacinacao);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiaVacinacao diaVacinacao = db.DiaVacinacao.Find(id);
            if (diaVacinacao == null)
            {
                return HttpNotFound();
            }

            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            return View(diaVacinacao);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DiaVacinacao diaVacinacao = db.DiaVacinacao.Find(id);
            diaVacinacao.Disable = 1;
            db.Entry(diaVacinacao).State = EntityState.Modified;
            db.SaveChanges();

            int user = Convert.ToInt32(User.Identity.Name.Split('|')[0]);
            Usuario usuario = db.Usuario.Find(user);
            Historico historico = new Historico();
            historico.Descricao = "Excluiu um dia da vacinação";
            historico.TabelaNome = "Dia vacinação";
            historico.ItemId = diaVacinacao.Id;
            historico.Data = DateTime.Now;
            historico.UsuarioId = usuario.Id;
            db.Historico.Add(historico);
            db.SaveChanges();

            TempData["msg"] = "Dia da vacinação deletado com sucesso";

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
