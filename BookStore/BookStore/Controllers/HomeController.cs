using BookStore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.Models;
using System.Security.Cryptography;
using System.Text;

namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        bookstoreEntities objModel = new bookstoreEntities();
        public ActionResult Index()
        {
            HomeModel model = new HomeModel();
            model.ListTheLoaiGoc = objModel.TheLoaiGoc.ToList();
            model.ListTheLoai = objModel.TheLoaiCon.ToList();
            model.ListProduct = objModel.Sach.ToList();
            return View(model);
        }
        //GET: Register
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        //POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(KhachHang _user)
        {
            if (ModelState.IsValid)
            {
                
                var check = objModel.KhachHang.FirstOrDefault(s => s.email == _user.email);
                if (check == null)
                {
                    
                    _user.matKhau = GetMD5(_user.matKhau);
                    objModel.Configuration.ValidateOnSaveEnabled = false;
                    objModel.KhachHang.Add(_user);
                    objModel.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Email already exists";
                    return View();
                }


            }
            return View();


        }
        //create a string MD5
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(KhachHang model)
        {
            if (ModelState.IsValid)
            {
                var f_password = GetMD5(model.matKhau); // Assuming GetMD5 is a function to hash the password.

                // Check if there is a user with the provided email and hashed password.
                var data = objModel.KhachHang
                    .Where(s => s.email.Equals(model.email) && s.matKhau.Equals(f_password))
                    .ToList();

                if (data.Count() > 0)
                {
                    var user = data.FirstOrDefault();

                    // Check if the user is an admin
                    if (user.isAdmin == 1)
                    {
                        // Add user information to session
                        Session["FullName"] = user.tenKhachHang;
                        Session["Email"] = user.email;
                        Session["idUser"] = user.maKhachHang;

                        return RedirectToAction("Index", "Product", new { area = "Admin" }); // Replace "AdminDashboard" with your actual admin page
                    }
                    else
                    {
                        // Add user information to session
                        Session["FullName"] = user.tenKhachHang;
                        Session["Email"] = user.email;
                        Session["idUser"] = user.maKhachHang;

                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    ViewBag.error = "Login failed";
                    return RedirectToAction("Login");
                }
            }

            // If ModelState is not valid, return the view with errors.
            return View();
        }




        //Logout
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Login");
        }
        public ActionResult gioiThieu()
        {
            return View();
        }
    }
}