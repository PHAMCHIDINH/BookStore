using BookStore.Context;
using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Controllers
{
    public class CategoryController : Controller
    {
        bookstoreEntities objModel = new bookstoreEntities();
        // GET: Category
        public ActionResult Index()
        {
            HomeModel model = new HomeModel();
            model.ListTheLoaiGoc = objModel.TheLoaiGoc.ToList();
            model.ListTheLoai = objModel.TheLoaiCon.ToList();
            return View(model);
        }
        public ActionResult ProductCategory(String id)
        {
            HomeModel model = new HomeModel();
            model.ListProduct = objModel.Sach.Where(n => n.maTheLoai == id).ToList();
            model.ListTheLoai = objModel.TheLoaiCon.Where(n => n.maTheLoaiGoc == id).ToList();
            if (model.ListProduct.Count==0)
            {
                model.ListProduct = new List<Sach>();
            foreach (var item in model.ListTheLoai)
            {
                model.ListProduct.AddRange(objModel.Sach.Where(n => n.maTheLoai == item.maTheLoaiCon).ToList());
            }

            }
                return View(model);
            
        }
        public ActionResult Listing()
        {
            HomeModel model = new HomeModel();
            model.ListProduct = objModel.Sach.ToList();
            return View(model);
        }

    }
}