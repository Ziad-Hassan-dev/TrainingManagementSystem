using Microsoft.AspNetCore.Mvc;
using TrainingManagementSystem.Models;
using TrainingManagementSystem.ViewModel;

namespace TrainingManagementSystem.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly Context context = new Context();
        #region Read
        public IActionResult Index()
        {
            List<Department> deptModels = context.Departments.ToList();
            return View("Index", deptModels);
        }
        #endregion

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            return View("Create");
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult SaveDept(DepartmentFormViewModel deptFromRequest)
        {
            if (ModelState.IsValid)
            {
                Department dept = new Department();
                dept.Name = deptFromRequest.Name.Trim();
                dept.Description = deptFromRequest.Description.Trim();
                context.Departments.Add(dept);
                context.SaveChanges();
                TempData["Success"] = "Department created successfully";
                return RedirectToAction("Index");
            }
            else
            {
                return View("Create", deptFromRequest);
            }
        }
        #endregion

        #region Edit
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Department? deptModel =
                context.Departments.FirstOrDefault(d => d.Id == id);

            var vm = new DepartmentFormViewModel()
            {
                Id = deptModel.Id,
                Name = deptModel.Name,
                Description = deptModel.Description
            };

            return View(vm);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult SaveEdit(DepartmentFormViewModel deptFromRequest, int id)
        {
            if (ModelState.IsValid)
            {
                Department? deptFromDB =
                    context.Departments.FirstOrDefault(d => d.Id == id);

                if (deptFromDB == null)
                    return NotFound("Department Not Found");

                deptFromDB.Name = deptFromRequest.Name.Trim();
                deptFromDB.Description = deptFromRequest.Description.Trim();
                context.SaveChanges();
                TempData["Success"] = "Deparment edited successfully";
                return RedirectToAction("Index");
            }
            return View("Edit", deptFromRequest);
        }
        #endregion

        #region Delete
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Department? deptModel = 
                context.Departments.FirstOrDefault(d => d.Id == id);
            if (deptModel != null)
            {
                return View("Delete", deptModel);
            }
            return NotFound("Department Not Found");
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult ConfirmDelete(int id)
        {
            Department? deptFromDB = 
                context.Departments.FirstOrDefault(d => d.Id == id);
            if (deptFromDB == null)
                return NotFound("Department Not Found");

            bool hasInstructor = 
                context.Instructors.Any(i => i.DeptId == id);
            bool hasCourse = 
                context.Courses.Any(c => c.DeptId == id);

            if(hasInstructor || hasCourse)
            {
                TempData["Error"] = "Cannot delete: this department has instructors or courses";
                return RedirectToAction("Index");
            }

            context.Departments.Remove(deptFromDB);
            context.SaveChanges();
            TempData["Success"] = "Department deleted successfully";
            return RedirectToAction("Index");
        } 
        #endregion
    }
}
