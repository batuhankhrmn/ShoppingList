﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ToDoApp.Models;

namespace ToDoApp.Controllers
{
    public class LoginController : Controller
    {
        ShoppingListEntities db = new ShoppingListEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login(string kullaniciAdi, string sifre)
        {
            var kullanici = db.kullanicilar.FirstOrDefault(x => x.kullanici_adi == kullaniciAdi && x.sifre == sifre);
            if (kullanici != null)
            {
                FormsAuthentication.SetAuthCookie(kullaniciAdi, false);
                return RedirectToAction("Listele", "Home");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
    }
}