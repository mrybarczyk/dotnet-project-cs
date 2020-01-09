using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using dotNET.Models;
using System.Data.Entity.Core.Objects;

// https://www.codeproject.com/Articles/640302/CRUD-Operations-Using-Entity-Framework-Code-Fi
// <button onclick="location.href='@Url.Action("Index", "Users")';return false;">Cancel</button>

namespace dotNET.Controllers
{
    public class HomeController : SessionController
    {

        public ActionResult Index()
        {
            int id = SessionGetter();
            if (id == -1)  return RedirectToAction("Form", "Session");
            else
            {
                
                var c = (from characters in db.Characters where characters.playerID == id select characters).ToList();
                return View(c);                                                    
            }
        }

        public ActionResult Uhoh()
        {
            return View();
        }

        public ActionResult Details(int id = 0)
        {
            if (id == 0) return RedirectToAction("Index");
            try
            {
                int sesja = SessionGetter();
                int ifAdmin = checkIfAdmin();
                if (ifAdmin == 1)
                {
                    var check = (from characters in db.Characters where characters.characterID == id select characters).First();
                    return View(check);
                }
                else
                {
                    var check = (from characters in db.Characters where (characters.playerID == sesja) && (characters.characterID == id) select characters).First();
                    return View(check);
                }
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Exclude = "characterID")] Characters c)
        {
            c.playerID = SessionGetter();
            if (c.playerID == -1) return RedirectToAction("Form", "Session");
            else
            {
                try
                {
                    db.Characters.Add(c);
                    db.SaveChanges();
                    return RedirectToAction("Panel");
                }
                catch
                {
                    return View();
                }
            }
        }

        public ActionResult Edit(int id = 0)
        {
            if (id == 0) return RedirectToAction("Index");
            try
            {
                int sesja = SessionGetter();
                int ifAdmin = checkIfAdmin();
                if (ifAdmin == 1)
                {
                    var check = (from characters in db.Characters where characters.characterID == id select characters).First();
                    return View(check);
                }
                else
                {
                    var check = (from characters in db.Characters where (characters.playerID == sesja) && (characters.characterID == id) select characters).First();
                    return View(check);
                }
            } catch
            {
                return RedirectToAction("Panel", "Home");
            }
        }

        [HttpPost]
        public ActionResult Edit(Characters p)
        {
            try
            {
                Characters oc = (from characters in db.Characters where characters.characterID == p.characterID select characters).SingleOrDefault();
                int sesja = SessionGetter();
                int ifAdmin = checkIfAdmin();
                if (p.playerID == sesja || ifAdmin == 1)
                { 
                    p.playerID = oc.playerID;
                    p.characterID = oc.characterID;
                    db.Entry(oc).CurrentValues.SetValues(p);
                    db.SaveChanges();
                    return RedirectToAction("Panel");
                }
                else return RedirectToAction("Index", "Home");
            } catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Delete(int id = 0)
        {
            if (id == 0) return RedirectToAction("Index");
            try
            {
                int sesja = SessionGetter();
                int ifAdmin = checkIfAdmin();
                if (ifAdmin == 1)
                {
                    var check = (from characters in db.Characters where characters.characterID == id select characters).First();
                    return View(check);
                }
                else
                {
                    var check = (from characters in db.Characters where (characters.playerID == sesja) && (characters.characterID == id) select characters).First();
                    return View(check);
                }
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult Delete(FormCollection collection, int id = 0)
        {
            if (id == 0) return RedirectToAction("Index");
            try
            {
                int sesja = SessionGetter();
                int ifAdmin = checkIfAdmin();
                var p = (from characters in db.Characters where characters.characterID == id select characters).First();
                if (p.playerID == sesja || ifAdmin == 1)
                {
                    db.Characters.Remove(p);
                    db.SaveChanges();
                }
                return RedirectToAction("Panel");
            } catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Contact()
        {
            try
            {
                return View(db.Player.Where(x => x.isAdmin == 1).ToList());
            } catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Panel()
        {
            int id = SessionGetter();
            try
            {
                var p = (from players in db.Player where players.playerID == id select players).SingleOrDefault();
                if (p.isAdmin == 1)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch
            {
                return RedirectToAction("Form", "Session");
            }
        }

        public ActionResult UserExists()
        {
            return View();
        }
    }
}
