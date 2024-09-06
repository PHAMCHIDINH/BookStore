using BookStore.Context;
using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Controllers
{
    public class PaymentController : Controller
    {
        // GET: Payment
        public ActionResult Index()
        {
            bookstoreEntities obj= new bookstoreEntities();
            if (Session["idUser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                // Lấy thông tin giỏ hàng từ biến session
                var lstCart = (List<CartModel>)Session["cart"];
                // Gán cho Order
                var objOrder = new Order();
                objOrder.UserId = int.Parse(Session["idUser"].ToString());
                objOrder.Name = "DonHang-" + DateTime.Now.ToString("yyyyMMddHHmmss");
                objOrder.CreatedOnUtc = DateTime.Now;
                objOrder.Status = 1;

                obj.Order.Add(objOrder);
                obj.SaveChanges();

                // Lấy thông tin và mỗi tác dụ vào bảng OrderDetail
                int idOrderDetail = objOrder.Id;
                List<OrderDetail> lstOrderDetail = new List<OrderDetail>();

                foreach (var item in lstCart)
                {
                    OrderDetail objj = new OrderDetail();
                    objj.Quantity = item.soLuong;
                    objj.OrderId = idOrderDetail;
                    objj.ProductID = item.Sach.maSach;

                    obj.OrderDetail.AddRange(lstOrderDetail);
                    obj.SaveChanges();
                }

                return View();
            }

        }
    }
}