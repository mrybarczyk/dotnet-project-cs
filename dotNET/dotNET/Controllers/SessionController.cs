using dotNET.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace dotNET.Controllers
{
    public class SessionController : Controller
    {
        protected static bazaEntities db = new bazaEntities();

        protected void SessionSetter(int p) {
            Session["player"] = p;
        }

        protected int SessionGetter()
        {
            try
            {
                int a = (int)Session["player"];
                return a;
            } catch
            {
                return -1;
            }
        }

        public int checkIfAdmin()
        {
            int id = SessionGetter();
            if (id == -1) return -1;
            var p = (from players in db.Player where players.playerID == id select players).First();
            if (p.isAdmin == 0) return 0;
            if (p.isAdmin == 1) return 1;
            else return -1;
        }

        public ActionResult Form()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Authorize(Player p)
        {
            var pd = (from players in db.Player where players.email.ToLower() == p.email.ToLower() && players.psd == p.psd select players).FirstOrDefault();
            if (pd == null)
            {
                p.LoginErrorMessage = "Błąd logowania: użytkownik nie istnieje lub podano błędne hasło.";
                return View("Form", p);
            }
            else if (pd.isBanned == 1)
            {
                p.LoginErrorMessage = "Przykro nam, ale twoje konto zostało zablokowane. Skontaktuj się z administratorem w celu rozwiązania problemu.";
                return View("Form", p);
            }
            else
            {
                SessionSetter(pd.playerID);
                return RedirectToAction("Panel", "Home");
            }
        }

        public ActionResult LogOut()
        {
            System.Web.HttpContext.Current.Session.Remove("player");
            return RedirectToAction("Form", "Session");
        }
    }
}