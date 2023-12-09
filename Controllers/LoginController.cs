using Microsoft.AspNetCore.Mvc;
using Tranning.Models;
using Tranning.Queries;

namespace Tranning.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            LoginModel model = new LoginModel();
            return View(model);
        }
        [HttpPost]
        public IActionResult Index(LoginModel model)
        {
            model = new LoginQueries().CheckLoginUser(model.UserName, model.Password);
            if (string.IsNullOrEmpty(model.UserId) || string.IsNullOrEmpty(model.UserName))
            {
                //dang nhap linh tinh- khong dung tai khoan trong database
                ViewData["MessageLogin"] = " Account invalid";
                return View();

            }
            //luu thong tin vao session 
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("SessionUserID")))
            {
                HttpContext.Session.SetString("SessionUserID", model.UserId);
                HttpContext.Session.SetString("SessionRoleID", model.RoleId);
                HttpContext.Session.SetString("SessionUserName", model.UserName);
                HttpContext.Session.SetString("SessionEmail", model.EmailUser);
            }
            //cho chuyen vao trang home
            if (model.RoleId == "7")
            {
                return RedirectToAction(nameof(Users1Controller.Index), "Users1");
            }
            else
            {
                if (model.RoleId == "8")
                {
                    return RedirectToAction(nameof(AdminController.Index), "Admin");
                }
                else




                    return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
        [HttpPost]
        public IActionResult Logout()
        {
            if(!string.IsNullOrEmpty(HttpContext.Session.GetString("SessionUserID")))
            {
                // xoa cac session  da duoc tao
                HttpContext.Session.Remove("SessionUserID");
                HttpContext.Session.Remove("SessionRoleID");
                HttpContext.Session.Remove("SessionUserName");
                HttpContext.Session.Remove("SessionEmail");
            }
            return RedirectToAction(nameof(LoginController.Index), "Login");
        }
    }
}
