using Covid.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Covid.Controllers
{
    public class HomeController : Controller
    {
        private Contexto db = new Contexto();

        public ActionResult Index()
        {

            var ptv = db.Vacinado.Where(x => x.Disable == 0 && x.Lote.Vacina.QuantidadeDose == x.Dose).Count();
            ViewBag.PesTVascinado = ptv;

            var vacU = db.Vacinado.Where(x => x.Disable == 0).ToList().Count();
            ViewBag.VacinasU = vacU;

            var varD = db.Lote.Where(x => x.Disable == 0).Select(x => x.Quantidade).DefaultIfEmpty().Sum();
            ViewBag.VacinasD = varD;

            var vacT = db.Vacina.Where(x => x.Disable == 0).Count();
            ViewBag.VaciT = vacT;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Historico()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            return View(db.Historico.ToList());
        }

        public ActionResult ContadorVacinados()
        {
            var cont = db.Vacinado.Where(x => x.Disable == 0).ToList().GroupBy(x => x.Cpf).Count();
            ViewBag.ContadorVacinado = cont;

            return PartialView();
        }

        public ActionResult LocalVacinacao()
        {
            var diaV = db.DiaVacinacao.Where(x => x.Disable == 0 && x.DataVacinacao >= DateTime.Now).ToList();

            return View(diaV);
        }

    }
}