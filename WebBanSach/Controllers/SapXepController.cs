using PagedList;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanSach.Models;

namespace WebBanSach.Controllers
{
    public class SapXepController : Controller
    {
        // GET: SapXep
        dbNhaSachDataContext db = new dbNhaSachDataContext();
        public ActionResult Index(int? page, string searchString )
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