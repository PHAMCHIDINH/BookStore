using BookStore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        bookstoreEntities obj = new bookstoreEntities();
        // GET: Admin/Product
        public ActionResult KhachHang()
        {
            var ListKhachHang = obj.KhachHang.ToList();
            return View(ListKhachHang);
        }
        public ActionResult DonHang()
        {
            var ListDonHang = obj.Order.ToList();
            return View(ListDonHang);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objProduct = obj.KhachHang.Where(n => n.maKhachHang == id).FirstOrDefault();
            return View(objProduct);
        }
        [HttpPost]
        public ActionResult Delete(KhachHang objPro)
        {
            var objProduct = obj.KhachHang.Where(n => n.maKhachHang == objPro.maKhachHang).FirstOrDefault();
            var objOrderUser = obj.Order.Where(n => n.UserId == objPro.maKhachHang).FirstOrDefault();
            obj.Order.Remove(objOrderUser);
            obj.KhachHang.Remove(objProduct);
            obj.SaveChanges();
            return RedirectToAction("KhachHang");
        }
        public ActionResult Logout()
        {
            Session.Clear(); // Xóa session
            return RedirectToAction("Login", "Home", new { area = "" });
        }

    }
}
