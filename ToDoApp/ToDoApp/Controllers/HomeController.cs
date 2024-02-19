using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ToDoApp.Models;

namespace ToDoApp.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Listele()
        {
            ToDoEntitiesConnectionStringDB db = new ToDoEntitiesConnectionStringDB();
            //ViewBag.isler = db.islers;
            ViewBag.sayfa = "acikIs"; 
            ViewBag.isler = db.islers.Where(g => g.durum == "1").ToList();

            return View();
        }
        [Authorize]
        public ActionResult TamamlanmisIsListele()
        {
            ToDoEntitiesConnectionStringDB db = new ToDoEntitiesConnectionStringDB();
            //ViewBag.isler = db.islers;
            ViewBag.sayfa = "kapaliIs";
            ViewBag.isler = db.islers.Where(g => g.durum == "0").ToList();

            return View();
        }
        [Authorize]
        public ActionResult Kaydet(string txtIsinAdi)
        {
            string is_adi = txtIsinAdi.ToString();
            DateTime olusturuldugu_tarih = DateTime.Now;
            string durum = "1";

            ToDoEntitiesConnectionStringDB db = new ToDoEntitiesConnectionStringDB();
            var yeniIs = new isler
            {
                is_adi = is_adi,
                durum = durum,
                olusturuldugu_tarih = olusturuldugu_tarih,
            };
            db.islers.Add(yeniIs);
            db.SaveChanges();

            return RedirectToAction("Listele");
        }
        [Authorize]
        public ActionResult Sil(int id)
        {
            ToDoEntitiesConnectionStringDB db = new ToDoEntitiesConnectionStringDB();
            isler silinecek = db.islers.FirstOrDefault(x => x.id == id);
            db.islers.Remove(silinecek);
            db.SaveChanges();
            return RedirectToAction("Listele");
        }
        [Authorize]
        public ActionResult IsKapat(int id)
        {
            ToDoEntitiesConnectionStringDB db = new ToDoEntitiesConnectionStringDB();
            isler kapatilacakIs = db.islers.FirstOrDefault(x => x.id == id);

            kapatilacakIs.tamamlandigi_tarih = DateTime.Now;
            kapatilacakIs.durum = "0";
            db.SaveChanges();

            return RedirectToAction("Listele");
        }
        [Authorize]
        public ActionResult TekrarAktif(int id)
        {
            ToDoEntitiesConnectionStringDB db = new ToDoEntitiesConnectionStringDB();
            isler tekrarAktif = db.islers.FirstOrDefault(x => x.id == id);
            tekrarAktif.tamamlandigi_tarih = null;
            tekrarAktif.durum = "1";
            db.SaveChanges();
            return RedirectToAction("Listele");
        }
        [Authorize]
        public ActionResult Duzenle(int id, string txtIsinAdi)
        {
            ToDoEntitiesConnectionStringDB db = new ToDoEntitiesConnectionStringDB();
            isler duzenlenecekIs = db.islers.FirstOrDefault(x => x.id == id);
            duzenlenecekIs.is_adi = txtIsinAdi;
            db.SaveChanges();
            return RedirectToAction("Listele");
        }
        public ActionResult Signout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }
    }
}