using AdminClientMVC.Models.DBContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AdminClientMVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly AdminClientContext db;

        public LoginController(AdminClientContext context)
        {
            db = context;
        }

        public IActionResult Login()
        {
            ViewBag.DateNow = DateTime.Now;
            return View();
        }

        public IActionResult SignIn(string EmailAddress, string Password)
        {
            if (EmailAddress != null || Password != null)
            {
                var qry = db.tbl_employees.Where(x => x.EmailAddress.ToLower() == EmailAddress.ToLower() && x.Password == Password && x.IsDeleted == false).FirstOrDefault();

                if (qry != null)
                {
                    Response.Cookies.Append("UserId", qry.Id.ToString(), new CookieOptions
                    {
                        Expires = DateTimeOffset.UtcNow.AddDays(1),
                        HttpOnly = true,
                        Secure = true
                    });

                    if (qry.UserTypeId == 1)
                    {
                        return RedirectToAction("Admin", "Home");
                    }
                    else
                    {
                        return RedirectToAction("Client", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View("Login");
                }
            }
            else
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View("Login");
            }

        }
    }
}
