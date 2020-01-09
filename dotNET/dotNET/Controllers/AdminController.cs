using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using dotNET.Models;

namespace dotNET.Controllers
{
    public class AdminController : SessionController
    { 
        public ActionResult Index()
        {
            int test = checkIfAdmin();
            if (test == 1) return View(db.Player);
            if (test == 0) return RedirectToAction("Index", "Home");
            else return RedirectToAction("Form", "Session");
        }

        public ActionResult Details(int id = 0)
        {
            if (id == 0) return RedirectToAction("Index");
            int test = checkIfAdmin();
            if (test == 1)
            {
                try
                {
                    var p = (from players in db.Player where players.playerID == id select players).First();
                    return View(p.Characters);
                }
                catch
                {
                    return RedirectToAction("Index", "Admin");
                }
            }
            if (test == 0) return RedirectToAction("Index", "Home");
            else return RedirectToAction("Form", "Session");
        }

        public ActionResult Ban(int id = 0)
        {
            if (id == 0) return RedirectToAction("Index");
            int test = checkIfAdmin();
            if (test == 1)
            {
                try
                {
                    var op = (from players in db.Player where players.playerID == id select players).First();
                    var p = (from players in db.Player where players.playerID == id select players).First();
                    if (op.isBanned == 0)
                    {
                        if (op.isAdmin == 0) p.isBanned = 1;
                        if (op.isAdmin == 1) return RedirectToAction("ErrorNotAdmin");
                    }
                    else p.isBanned = 0;
                    db.Entry(op).CurrentValues.SetValues(p);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Admin");
                }
                catch
                {
                    return RedirectToAction("Index", "Admin");
                }
            }
            if (test == 0) return RedirectToAction("Index", "Home");
            else return RedirectToAction("Form", "Session");
        }

        public ActionResult ErrorNotAdmin()
        {
            int test = checkIfAdmin();
            if (test == 1) return View();
            if (test == 0) return RedirectToAction("Index", "Home");
            else return RedirectToAction("Form", "Session");
        }

        public ActionResult Reset(int id = 0)
        {
            if (id == 0) return RedirectToAction("Index");
            int test = checkIfAdmin();
            if (test == 1)
            {
                try
                {
                    var op = (from players in db.Player where players.playerID == id select players).First();
                    var p = (from players in db.Player where players.playerID == id select players).First();
                    const string chars1 = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    const string chars2 = "0123456789";
                    const string chars3 = "@$!%*?&";
                    const string chars4 = "abcdefghijklmnopqrstuvwxyz";
                    Random random = new Random();
                    var result1 = (Enumerable.Repeat(chars1, 2)
                      .Select(s => s[random.Next(s.Length)]).ToArray());
                    var result2 = (Enumerable.Repeat(chars2, 2)
                      .Select(s => s[random.Next(s.Length)]).ToArray());
                    var result3 = (Enumerable.Repeat(chars3, 2)
                      .Select(s => s[random.Next(s.Length)]).ToArray());
                    var result4 = (Enumerable.Repeat(chars4, 2)
                      .Select(s => s[random.Next(s.Length)]).ToArray());
                    var sb = new StringBuilder(8);
                    for (int i = 0; i < 2; i++)
                    {
                        sb.Append(result1[i]);
                        sb.Append(result2[i]);
                        sb.Append(result3[i]);
                        sb.Append(result4[i]);
                    }
                    var result = sb.ToString();
                    p.psd = result;
                    db.Entry(op).CurrentValues.SetValues(p);
                    db.SaveChanges();
                    StringModel t = new StringModel();
                    t.Text = result;
                    return RedirectToAction("AfterReset", "Admin", t);
                }
                catch
                {
                    return RedirectToAction("Index", "Admin");
                }
            }
            if (test == 0) return RedirectToAction("Index", "Home");
            else return RedirectToAction("Form", "Session");
        }
        
        public ActionResult AfterReset(StringModel t)
        {
            int test = checkIfAdmin();
            if (test == 1) return View(t);
            if (test == 0) return RedirectToAction("Index", "Home");
            else return RedirectToAction("Form", "Session");
        }

        public ActionResult Promote(int id = 0)
        {
            if (id == 0) return RedirectToAction("Index");
            int test = checkIfAdmin();
            if (test == 1)
            {
                try
                {
                    var op = (from players in db.Player where players.playerID == id select players).First();
                    var p = (from players in db.Player where players.playerID == id select players).First();
                    if (p.isAdmin == 0) p.isAdmin = 1;
                    else p.isAdmin = 0;
                    db.Entry(op).CurrentValues.SetValues(p);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Admin");
                }
                catch
                {
                    return RedirectToAction("Index", "Admin");
                }
            }
            if (test == 0) return RedirectToAction("Index", "Home");
            else return RedirectToAction("Form", "Session");
        }
    }
}