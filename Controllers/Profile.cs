using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Tranning.DataDBContext;
using Tranning.Models;

namespace Tranning.Controllers
{
    public class ProfileController : Controller
    {
        private readonly TranningDBContext _dbContext;

        public ProfileController(TranningDBContext context)
        {
            _dbContext = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        
    }
}