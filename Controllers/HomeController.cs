using AdminClientMVC.Models;
using AdminClientMVC.Models.DBContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace AdminClientMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly AdminClientContext db;

        public HomeController(AdminClientContext context)
        {
            db = context;
        }

        public IActionResult Admin()
        {
            var qry = db.tbl_employees.Where(x => x.Id == Convert.ToInt32(Request.Cookies["UserId"])).FirstOrDefault();

            ViewBag.UserId = qry?.Id;
            ViewBag.UserTypeId = qry?.UserTypeId;
            ViewBag.FullName = qry?.FirstName + " " + qry?.LastName;
            ViewBag.Birthday = qry?.Birthday != null ? qry?.Birthday.Value.ToLongDateString() : null;
            ViewBag.PhoneNumber = qry?.PhoneNumber;
            ViewBag.EmailAddress = qry?.EmailAddress;
            ViewBag.Address = qry?.Address;
            ViewBag.Notes = qry?.Notes != null ? qry?.Notes : "N/A";

            var userType = db.tbl_user_types.OrderBy(x => x.Id).ToList();
            ViewBag.cmbUserType = new SelectList(userType, "Id", "Description");

            return View();
        }

        public IActionResult Client()
        {
            var qry = db.tbl_employees.Where(x => x.Id == Convert.ToInt32(Request.Cookies["UserId"])).FirstOrDefault();

            ViewBag.UserId = qry?.Id;
            ViewBag.UserTypeId = qry?.UserTypeId;
            ViewBag.FullName = qry?.FirstName + " " + qry?.LastName;
            ViewBag.Birthday = qry?.Birthday != null ? qry?.Birthday.Value.ToLongDateString() : null;
            ViewBag.PhoneNumber = qry?.PhoneNumber;
            ViewBag.EmailAddress = qry?.EmailAddress;
            ViewBag.Address = qry?.Address;
            ViewBag.Notes = qry?.Notes != null? qry?.Notes : "N/A";

            var userType = db.tbl_user_types.OrderBy(x => x.Id).ToList();
            ViewBag.cmbUserType = new SelectList(userType, "Id", "Description");

            return View();
        }

        public IActionResult LoadEmployees()
        {
            var list = db.tbl_employees.Where(x=>x.IsDeleted == false).ToList();
            List<object> data = new List<object>();
            foreach (var item in list)
            {
                var obj = new
                {
                    Id = item.Id,
                    UserType = db.tbl_user_types.Where(x => x.Id == item.UserTypeId).FirstOrDefault()?.Description,
                    FullName = item.FirstName + " " + item.LastName,
                    Birthday = item.Birthday != null ? item.Birthday.Value.ToShortDateString() : null,
                    PhoneNumber = item.PhoneNumber,
                    EmailAddress = item.EmailAddress,
                    Address = item.Address,
                    Notes = item.Notes,
                };
                data.Add(obj);
            }
            return Json(new { data = data });
        }

        [HttpPost]
        public IActionResult SaveEmployee(tbl_employees data)
        {
            try
            {
                if (data.Id != 0)
                {
                    var qry = db.tbl_employees.Where(x => x.Id == data.Id).SingleOrDefault();
                    qry.UserTypeId = data.UserTypeId;
                    qry.FirstName = data.FirstName;
                    qry.LastName = data.LastName;
                    qry.Birthday = data.Birthday;
                    qry.PhoneNumber = data.PhoneNumber;
                    qry.EmailAddress = data.EmailAddress;
                    qry.Address = data.Address;
                    qry.Notes = data.Notes;
                    qry.Password = data.Password;
                    db.SaveChanges();
                }
                else
                {
                    db.tbl_employees.Add(data);
                    db.SaveChanges();

                }
                return Json(new { success = true });

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }

        }

        public IActionResult UpdateEmployee(int Id)
        {
            try
            {
                var data = db.tbl_employees.Where(x => x.Id == Id).SingleOrDefault();
                return Json(new { data = data });
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message });
            }
        }

        public IActionResult RemoveEmployee(int Id)
        {
            try
            {
                var data = db.tbl_employees.Where(x => x.Id == Id).SingleOrDefault();
                //db.tbl_employees.Remove(data);
                data.IsDeleted = true;
                db.SaveChanges();
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

    }
}
