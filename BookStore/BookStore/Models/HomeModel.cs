using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Context;
namespace BookStore.Models
{
    public class HomeModel
    {
        public List<Sach> ListProduct { get; set; }
        public List<TheLoaiGoc> ListTheLoaiGoc { get; set; }
        public List<TheLoaiCon> ListTheLoai { get; set; }
        public List<NoiDungSach> ListNoiDung { get; set; }
        public List<KhachHang>  ListKhachHang { get; set; }
    }
}