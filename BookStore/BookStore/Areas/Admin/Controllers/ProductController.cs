using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Antlr.Runtime;
using BookStore.Context;
using BookStore.Models;
namespace BookStore.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        bookstoreEntities obj = new bookstoreEntities();
        // GET: Admin/Product
        public ActionResult Index()
        {
            var listProduct = obj.Sach.ToList();
            return View(listProduct);
        }
        [HttpGet]
        public ActionResult Create()
        {
     
        IEnumerable<SelectListItem> maTheLoaiItems = obj.TheLoaiCon.Select(t => new SelectListItem
        {
            Value = t.maTheLoaiCon.ToString(), // Điền vào trường ID của mỗi mục
            Text = t.tenTheLoaiCon // Điền vào trường bạn muốn hiển thị trong dropdown
        })
        .ToList();
            ViewBag.MaTheLoaiItems = maTheLoaiItems;

            return View();
        }
        [HttpPost]
        public ActionResult Create(Sach sach)
        {
            try
            {
                if (sach.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(sach.ImageUpload.FileName);
                    string extension = Path.GetExtension(sach.ImageUpload.FileName);
                    string img = fileName +"_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss"))+extension;

                    sach.tenAnh = img;
                    sach.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items/"), img));
                }
                obj.Sach.Add(sach);
                obj.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception) {
             return RedirectToAction("Index");
            }
        }
        [HttpGet]
        public ActionResult Details(string id)
        {
            var objProduct=obj.Sach.Where(n=>n.maSach==id).FirstOrDefault();
            return View(objProduct);
        }
        [HttpGet]
        public ActionResult Delete(string id)
        {
            var objProduct = obj.Sach.Where(n => n.maSach == id).FirstOrDefault();
            return View(objProduct);
        }
        [HttpPost]
        public ActionResult Delete(Sach objPro)
        {
            var objProduct = obj.Sach.Where(n => n.maSach == objPro.maSach).FirstOrDefault();
            obj.Sach.Remove(objProduct);
            obj.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(string id)
        {
            IEnumerable<SelectListItem> maTheLoaiItems = obj.TheLoaiCon.Select(t => new SelectListItem
            {
                Value = t.maTheLoaiCon.ToString(), // Điền vào trường ID của mỗi mục
                Text = t.tenTheLoaiCon // Điền vào trường bạn muốn hiển thị trong dropdown
            })
        .ToList();
            ViewBag.MaTheLoaiItems = maTheLoaiItems;
            var objProduct = obj.Sach.Where(n => n.maSach == id).FirstOrDefault();
            return View(objProduct);
        }
        [HttpPost]
        public ActionResult Edit(Sach sach)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (sach.ImageUpload != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(sach.ImageUpload.FileName);
                        string extension = Path.GetExtension(sach.ImageUpload.FileName);
                        string img = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;

                        sach.tenAnh = img;
                        sach.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items/"), img));
                    }

                    obj.Entry(sach).State = EntityState.Modified;
                    obj.SaveChanges();

                    // Cập nhật ViewBag.MaTheLoaiItems
                    IEnumerable<SelectListItem> maTheLoaiItems = obj.TheLoaiCon.Select(t => new SelectListItem
                    {
                        Value = t.maTheLoaiCon.ToString(),
                        Text = t.tenTheLoaiCon
                    }).ToList();

                    ViewBag.MaTheLoaiItems = maTheLoaiItems;

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    // Xử lý lỗi nếu có
                    ModelState.AddModelError("", "Lỗi khi lưu dữ liệu: " + ex.Message);
                }
            }

            // Nếu ModelState không hợp lệ, cập nhật ViewBag và trả về view với dữ liệu và thông báo lỗi
            IEnumerable<SelectListItem> updatedMaTheLoaiItems = obj.TheLoaiCon.Select(t => new SelectListItem
            {
                Value = t.maTheLoaiCon.ToString(),
                Text = t.tenTheLoaiCon
            }).ToList();

            ViewBag.MaTheLoaiItems = updatedMaTheLoaiItems;

            return View(sach);
        }
       
    }

}
