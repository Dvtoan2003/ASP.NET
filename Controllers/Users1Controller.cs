using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Tranning.Models;
using Training.DataDBContext;
using Tranning.DataDBContext;
namespace Tranning.Controllers
{
    public class Users1Controller : Controller
    {
        private readonly TranningDBContext _dbContext;
        public Users1Controller(TranningDBContext context)
        {
            _dbContext = context;
        }




        public IActionResult Index(String SearchString)
        {
            if(string.IsNullOrEmpty(HttpContext.Session.GetString("SessionUserName")))
            {
                return RedirectToAction(nameof(LoginController.Index), "Login");
            }
            CategoryModel categoryModel = new CategoryModel();
            categoryModel.CategoryDetailLists = new List<CategoryDetail>();
            var data = from m in _dbContext.Categories select m;
            data = data.Where(m => m.deleted_at == null);
            if (!string.IsNullOrEmpty(SearchString))
            {
                data = data.Where(m => m.name.Contains(SearchString) || m.description.Contains(SearchString));
            }
            data.ToList();
            foreach (var item in data)
            {
                categoryModel.CategoryDetailLists.Add(new CategoryDetail
                {
                    id = item.id,
                    name = item.name,
                    description = item.description,
                    icon = item.icon,
                    status = item.status,
                    created_at = item.created_at
                });
            }
            ViewData["CurrentFilter"] = SearchString;
            // file index - default file(root file)
            // file mac dinh se chay o trong 1 controller
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}