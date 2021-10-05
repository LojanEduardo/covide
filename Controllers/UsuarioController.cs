using Covid.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using static Covid.Models.Funcoes;

namespace Covid.Controllers
{
    public class UsuarioController : Controller
    {
       private Contexto db = new Contexto();


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastro(Cadastro cad, string email)
        {
            cad.Email = email;
            if (ModelState.IsValid)
            {
                if (db.Usuario.Where(x => x.Email == cad.Email).ToList().Count > 0)
                {
                    ModelState.AddModelError("Email", "E-mail já cadastrado");
                    return View();
                }
                else
                {
                    Usuario usu = new Usuario();
                    usu.Email = cad.Email;
                    usu.Senha = Funcoes.HashTexto(cad.Senha, "SHA512");

                    db.Usuario.Add(usu);
                    db.SaveChanges();
                    TempData["message"] = "success|Conta cadastrada com sucesso.";
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login log)
        {
            if (ModelState.IsValid)
            {
                string senhacrip = Funcoes.HashTexto(log.Senha, "SHA512");

                Usuario usu = db.Usuario.Where(x => x.Email == log.Email && x.Senha == senhacrip).ToList().FirstOrDefault();

                if (usu == null)
                {
                    TempData["message"] = "danger|Usuário ou senha incorretos.";
                    return View();
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(usu.Id + "|" + usu.Email, false);
                    TempData["message"] = "success|Login realizado com sucesso.";
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(); ;
        }

        public ActionResult Sair()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index","Home");
        }
    }
}