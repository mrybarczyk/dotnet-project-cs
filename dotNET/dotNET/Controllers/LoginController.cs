using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using dotNET.Models;

namespace dotNET.Controllers
{ 
    public class LoginController : SessionController
    {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Exclude = "playerID")] Player p)
        {
            var check = (from players in db.Player where players.email.ToLower() == p.email.ToLower() select players).Any();
            if (check) return RedirectToAction("UserExists", "Home");
            else
            {
                try
                {
                    p.isAdmin = 0;
                    db.Player.Add(p);
                    db.SaveChanges();
                    return RedirectToAction("Form", "Session");
                }
                catch
                {
                    return View();
                }
            }
        }
        public ActionResult Edit()
        {
            int id = SessionGetter();
            if (id == -1) return RedirectToAction("Form", "Session");
            else
            {
                var p = (from players in db.Player where players.playerID == id select players).SingleOrDefault();
                return View(p);
            }
        }

        [HttpPost]
        public ActionResult Edit(Player p)
        {
            try
            {
                Player op = (from players in db.Player where players.playerID == p.playerID select players).SingleOrDefault();
                p.isAdmin = op.isAdmin;
                p.isBanned = op.isBanned;
                p.email = p.email.ToLower();
                db.Entry(op).CurrentValues.SetValues(p);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View(p);
            }
        }

        public ActionResult Delete()
        {
            int id = SessionGetter();
            if (id == -1) return RedirectToAction("Form", "Session");
            else
            {
                Player p = db.Player.Find(id);
                return View(p);
            }
        }

        [HttpPost]
        public ActionResult Delete(FormCollection collection)
        {
            int id = SessionGetter();
            if (id == -1) return RedirectToAction("Form", "Session");
            else
            {
                var p = (from players in db.Player where players.playerID == id select players).SingleOrDefault();
                var c = (from characters in db.Characters where characters.playerID == id select characters).ToList();
                if (p != null)
                {
                    foreach (Characters item in c)
                    {
                        db.Characters.Remove(item);
                    }
                    db.Player.Remove(p);
                    db.SaveChanges();
                }
                return RedirectToAction("Logout", "Session");
            }
        }

    }
}