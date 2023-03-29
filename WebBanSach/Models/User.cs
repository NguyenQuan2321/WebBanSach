using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanSach.Models
{
    public class User
    {
        public int mahk { get; set; }
        public string hoten { get; set; }
        public string tendangnhap { get; set; }
        public string matkhau { get; set; }
        public string email { get; set; }
        public string diachi { get; set; }
        public string dienthoai { get; set; }
        public DateTime ngaysinh { get; set; }
    }
}