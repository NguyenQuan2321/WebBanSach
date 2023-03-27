using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using WebBanSach.Models;

namespace WebBanSach.Controllers
{
    public class SachController : Controller
    {
        // GET: Sach
        dbNhaSachDataContext db = new dbNhaSachDataContext();
        public ActionResult Index(int? page, string searchString)
        {
            ViewBag.Keyword = searchString;

            if (page == null) page = 1;
            var all_book = (from Sach in db.Saches select Sach).OrderBy(m => m.masach);
            if (!string.IsNullOrEmpty(searchString))
                all_book = (IOrderedQueryable<Sach>)all_book.Where(a => a.tensach.Contains(searchString));
            int pageSize = 3;
            int pageNum = page ?? 1;
            return View(all_book.ToPagedList(pageNum, pageSize));
        }

        public ActionResult ChiTiet(int id)
        {
            var D_Sach = db.Saches.Where(m => m.masach == id).First();
            return View(D_Sach);    
        }


        public ActionResult TaoMoi()
        {
            return View();
        }

        [HttpPost]
        public ActionResult TaoMoi(FormCollection collection, Sach s)
        {
            var E_tensach = collection["tensach"];
            var E_hinh = collection["hinh"];
            var E_giaban = Convert.ToDecimal(collection["giaban"]);
            var E_ngaycapnhat = Convert.ToDateTime(collection["ngaycapnhat"]);
            var E_soluongton = Convert.ToInt32(collection["soluongton"]);
            if (string.IsNullOrEmpty(E_tensach))
            {
                ViewData["Error"] = "Vui lòng điền đầy đủ thông tin";
            }
            else
            {
                s.tensach = E_tensach.ToString();
                s.hinh = E_hinh.ToString();
                s.giaban = E_giaban;
                s.ngaycapnhat = E_ngaycapnhat;
                s.soluongton = E_soluongton;
                db.Saches.InsertOnSubmit(s);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.TaoMoi();
        }

        public ActionResult TuyChinh(int id)
        {
            var E_sach = db.Saches.First(m => m.masach == id);
            return View(E_sach);
        }
        [HttpPost]
        public ActionResult TuyChinh(int id, FormCollection collection)
        {
            var E_sach = db.Saches.First(m=>m.masach==id);
            var E_tensach = collection["tensach"];
            var E_hinh = collection["hinh"];
            var E_giaban = Convert.ToDecimal(collection["giaban"]);
            var E_ngaycapnhat = Convert.ToDateTime(collection["ngaycapnhat"]);
            var E_soluongton = Convert.ToInt32(collection["soluongton"]);
            E_sach.masach = id;
            if (string.IsNullOrEmpty(E_tensach))
            {
                ViewData["Error"] = "Vui lòng điền đầy đủ thông tin";
            }
            else
            {
                E_sach.tensach = E_tensach;
                E_sach.hinh = E_hinh;
                E_sach.giaban = E_giaban;
                E_sach.ngaycapnhat = E_ngaycapnhat;
                E_sach.soluongton = E_soluongton;
                UpdateModel(E_sach);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.TuyChinh(id);
        }

        public ActionResult XoaSach(int id)
        {
            var D_Sach = db.Saches.First(m => m.masach == id);
            return View(D_Sach);
        }
        [HttpPost]
        public ActionResult XoaSach(int id, FormCollection collection)
        {
            var D_Sach = db.Saches.Where(m=>m.masach == id).First();
            db.Saches.DeleteOnSubmit(D_Sach);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }

        public string ProcessUpload(HttpPostedFileBase file)
        {
            if (file == null)
            {
                return "";
            }
            file.SaveAs(Server.MapPath("~/Content/images/" + file.FileName));
            return "/Content/images/" + file.FileName;
        }

    }
}