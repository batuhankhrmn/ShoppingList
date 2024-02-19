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
        ShoppingListEntities db = new ShoppingListEntities();
        [Authorize]
        public ActionResult Listele()
        {
            ViewBag.sayfa = "acikIs";
            ViewBag.urunler = db.urunler.Where(g => g.durum == "1").ToList();

            return View();
        }
        [Authorize]
        public ActionResult TamamlanmisIsListele()
        {
            //ViewBag.isler = db.islers;
            ViewBag.sayfa = "kapaliIs";
            ViewBag.urunler = db.urunler.Where(g => g.durum == "0").ToList();

            return View();
        }
        [Authorize]
        public ActionResult Kaydet(string txtUrunAdi, string txtUrunAdet)
        {
            string urun_adi = txtUrunAdi.ToString();
            string urun_adet = txtUrunAdet.ToString();
            DateTime olusturuldugu_tarih = DateTime.Now;
            string durum = "1";

            var yeniUrun = new urunler
            {
                urun_adi = urun_adi,
                urun_adet = urun_adet,
                durum = durum,
                olusturuldugu_tarih = olusturuldugu_tarih,
            };
            db.urunler.Add(yeniUrun);
            db.SaveChanges();

            return RedirectToAction("Listele");
        }
        [Authorize]
        public ActionResult Sil(int id)
        {
            urunler silinecek = db.urunler.FirstOrDefault(x => x.urun_id == id);
            db.urunler.Remove(silinecek);
            db.SaveChanges();
            return RedirectToAction("Listele");
        }
        [Authorize]
        public ActionResult UrunKapat(int id)
        {
            urunler kapatilacakUrun = db.urunler.FirstOrDefault(x => x.urun_id == id);

            kapatilacakUrun.tamamlandigi_tarih = DateTime.Now;
            kapatilacakUrun.durum = "0";
            db.SaveChanges();

            return RedirectToAction("Listele");
        }
        [Authorize]
        public ActionResult TekrarAktif(int id)
        {
            urunler tekrarAktif = db.urunler.FirstOrDefault(x => x.urun_id == id);
            tekrarAktif.tamamlandigi_tarih = null;
            tekrarAktif.durum = "1";
            db.SaveChanges();
            return RedirectToAction("Listele");
        }
        [Authorize]
        public ActionResult Duzenle(int id, string txtUrunAdi)
        {
            urunler duzenlenecekUrun = db.urunler.FirstOrDefault(x => x.urun_id == id);
            duzenlenecekUrun.urun_adi = txtUrunAdi;
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