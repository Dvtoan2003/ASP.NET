using Microsoft.AspNetCore.Mvc;
using Training.DataDBContext;
using Tranning.DataDBContext;
using Tranning.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Tranning.Controllers
{
    public class RoleController : Controller
    {
        private readonly TranningDBContext _dbContext;

        public RoleController(TranningDBContext context)
        {
            _dbContext = context;
        }

        [HttpGet]
        public IActionResult Index(string searchString)
        {
            RoleModel roleModel = new RoleModel();
            roleModel.RoleDetailLists = new List<RoleDetail>();

            var data = from m in _dbContext.Roles
                       select m;

            foreach (var item in data)
            {
                roleModel.RoleDetailLists.Add(new RoleDetail
                {
                    id = item.id,
                    name = item.name,
                    description = item.description,
                    status = item.status,
                    created_at = item.created_at,
                    updated_at = item.updated_at // Make sure to include updated_at if it exists in your model
                });
            }

           
            return View(roleModel); // Fix this line to return roleModel instead of roleDetail
        }


        [HttpGet]
        public IActionResult Add()
        {
            RoleDetail role = new RoleDetail();
            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(RoleDetail role)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var roleData = new Role
                    {
                        name = role.name,
                        description = role.description,
                        status = role.status,
                        created_at = DateTime.Now
                    };

                    _dbContext.Roles.Add(roleData);
                    _dbContext.SaveChanges();
                    TempData["saveStatus"] = true;
                }
                catch
                {
                    TempData["saveStatus"] = false;
                }
                return RedirectToAction(nameof(RoleController.Index), "Role");
            }
            return View(role);
        }

        [HttpGet]
        public IActionResult Update(int id = 0)
        {
            RoleDetail role = new RoleDetail();
            var data = _dbContext.Roles.FirstOrDefault(m => m.id == id);

            if (data != null)
            {
                role.id = data.id;
                role.name = data.name;
                role.description = data.description;
                role.status = data.status;
            }

            return View(role);
        }

        [HttpPost]
        public IActionResult Update(RoleDetail role)
        {
            try
            {
                var data = _dbContext.Roles.FirstOrDefault(m => m.id == role.id);

                if (data != null)
                {
                    // Update properties based on the assumptions about the RoleDetail class
                    data.name = role.name;
                    data.description = role.description;
                    data.status = role.status;

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
            return RedirectToAction(nameof(RoleController.Index), "Role");
        }

        [HttpGet]
        public IActionResult Delete(int id = 0)
        {
            try
            {
                var data = _dbContext.Roles.FirstOrDefault(m => m.id == id);
                if (data != null)
                {
                    data.deleted_at = DateTime.Now;
                    _dbContext.SaveChanges();
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
            return RedirectToAction(nameof(RoleController.Index), "Role");
        }
    }
}
