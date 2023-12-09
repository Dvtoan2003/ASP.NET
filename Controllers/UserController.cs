using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;
using System.Net;
using System.Reflection;
using Training.DataDBContext;
using Tranning.DataDBContext;
using Tranning.Models;

namespace Tranning.Controllers
{
    public class UserController : Controller
    {
        private readonly TranningDBContext _dbContext;
        public UserController(TranningDBContext context)
        {
            _dbContext = context;
        }

        [HttpGet]
        public IActionResult Index(string SearchString)
        {

            //check dang nhap
            //if (string.IsNullOrEmpty(HttpContext.Session.GetString("SessionUsername")))
            //{
            //    return RedirectToAction(nameof(LoginController.Index), "Login");
            //}

            UserModel userModel = new UserModel();
            userModel.UserDetailLists = new List<UserDetail>();

            var data = from m in _dbContext.Users
                       select m;

            data = data.Where(m => m.deleted_at == null);
            if (!string.IsNullOrEmpty(SearchString))
            {
                data = data.Where(m => m.username.Contains(SearchString) || m.email.Contains(SearchString));
            }
            data.ToList();

            foreach (var item in data)
            {
                userModel.UserDetailLists.Add(new UserDetail
                {
                    id = item.id,
                    username = item.username,
                    password = item.password,
                    email = item.email,
                    role_id = item.role_id,
                    phone = item.phone,
                    address = item.address,
                    extra_code = item.extra_code,
                    gender = item.gender,
                    birthday = item.birthday,
                    avatar = item.avatar,
                    full_name = item.full_name,
                    education = item.education,
                    programming_language = item.programming_language,
                    toeic_score = item.toeic_score,
                    experience = item.experience,
                    department = item.department,
                    status = item.status,
                    created_at = item.created_at,
                    updated_at = item.updated_at
             

        });
            }
            ViewData["CurrentFilter"] = SearchString;
            return View(userModel);
        }
        
              

        [HttpGet]
        public IActionResult Add()
        {
            // check dang nhap
            //if (string.IsNullOrEmpty(HttpContext.Session.GetString("SessionUsername")))
            //{
            //    return RedirectToAction(nameof(LoginController.Index), "Login");
            //}

            UserDetail user = new UserDetail();
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(UserDetail user, IFormFile Photo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string uniqueFileName = UploadFile(Photo);
                    var userData = new User()
                    {
                        username = user.username,
                        password = user.password,
                        email = user.email,
                        role_id = user.role_id,
                        phone = user.phone,
                        address = user.address,
                        extra_code = user.extra_code,
                        gender = user.gender,
                        birthday = user.birthday,
                        avatar = uniqueFileName,
                        full_name = user.full_name,
                       
                        status = user.status,
                        created_at = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                    };
                    _dbContext.Users.Add(userData);
                    _dbContext.SaveChanges(true);
                    TempData["saveStatus"] = true;
                }
                catch
                {
                    TempData["saveStatus"] = false;
                }
                return RedirectToAction(nameof(UserController.Index), "User");
            }
            return View(user);
        }

        [HttpGet]
        public IActionResult Update(int id = 0)
        {
            UserDetail user = new UserDetail();
            var data = _dbContext.Users.Where(m => m.id == id).FirstOrDefault();
            if (data != null)
            {
                user.id = data.id;
                user.username = data.username;
                user.password = data.password;
                user.email = data.email;
                user.role_id = data.role_id;
                user.phone = data.phone;
                user.address = data.address;
                user.extra_code = data.extra_code;
                user.gender = data.gender;
                user.birthday = data.birthday;
                user.avatar = data.avatar;
                user.full_name = data.full_name;
                user.status = data.status;
            }

            return View(user);
        }

        [HttpPost]
        public IActionResult Update(UserDetail user, IFormFile file)
        {
            try
            {
                var data = _dbContext.Users.FirstOrDefault(m => m.id == user.id);
                if (data != null)
                {
                    // Update properties based on the assumptions about the UserDetail class
                    data.username = user.username;
                    data.email = user.email;
                    data.role_id = user.role_id;
                    data.phone = user.phone;
                    data.address = user.address;
                    data.extra_code = user.extra_code;
                    data.gender = user.gender;
                    data.full_name = user.full_name;
                    data.password = user.password;
                    data.status = user.status;

                    // Check if the uploaded file is not null before updating the avatar
                    if (file != null)
                    {
                        string uniqueIconAvatar = UploadFile(file);
                        data.avatar = uniqueIconAvatar;
                    }

                    _dbContext.SaveChanges();
                    TempData["UpdateStatus"] = true;
                }
                else
                {
                    TempData["UpdateStatus"] = false;
                }
            }
            catch (Exception ex)
            {
                TempData["UpdateStatus"] = false;
                // Log the exception or handle it appropriately
            }
            return RedirectToAction(nameof(UserController.Index), "User");
        }

        [HttpGet]
        public IActionResult Delete(int id = 0)
        {
            try
            {
                var data = _dbContext.Users.Where(m => m.id == id).FirstOrDefault();
                if (data != null)
                {
                    data.deleted_at = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    _dbContext.SaveChanges(true);
                    TempData["DeleteStatus"] = true;
                }
                else
                {
                    TempData["DeleteStatus"] = false;
                }
            }
            catch
            {
                TempData["DeleteStatus"] = false;
            }
            return RedirectToAction(nameof(UserController.Index), "user");
        }

        private string UploadFile(IFormFile file)
        {
            string uniqueFileName;
            try
            {
                string pathUploadServer = "wwwroot\\uploads\\images";

                string fileName = file.FileName;
                fileName = Path.GetFileName(fileName);
                string uniqueStr = Guid.NewGuid().ToString(); // random tao ra cac ky tu khong trung lap
                // tao ra ten fil ko trung nhau
                fileName = uniqueStr + "-" + fileName;
                string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), pathUploadServer, fileName);
                var stream = new FileStream(uploadPath, FileMode.Create);
                file.CopyToAsync(stream);
                // lay lai ten anh de luu database sau nay
                uniqueFileName = fileName;
            }
            catch (Exception ex)
            {
                uniqueFileName = ex.Message.ToString();
            }
            return uniqueFileName;
        }
    }
}








