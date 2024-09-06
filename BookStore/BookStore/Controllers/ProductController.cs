using BookStore.Context;
using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Controllers
{
    public class ProductController : Controller
    {
        bookstoreEntities obj = new bookstoreEntities();
        // GET: Product
        public ActionResult Index(String id)
        {
            HomeModel model = new HomeModel();
            model.ListProduct = obj.Sach.Where(n => n.maSach == id).ToList();
            model.ListNoiDung = obj.NoiDungSach.Where(n => n.maSach == id).ToList();
            return View(model);
        }
    }
}