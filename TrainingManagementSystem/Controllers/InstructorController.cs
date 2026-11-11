using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TrainingManagementSystem.Models;
using TrainingManagementSystem.ViewModel;

namespace TrainingManagementSystem.Controllers
{
    public class InstructorController : Controller
    {
        Context context = new Context();

        #region Read
        public IActionResult Index()
        {
            List<Instructor> instructorsModel = context.Instructors
                .Include(i => i.Department).ToList();

            return View(instructorsModel);
        } 
        #endregion

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            var vm = new InstructorFormViewModel()
            {
                Departments = new SelectList(context.Departments.ToList(), "Id", "Name")
            };
            return View(vm);
        }


        [HttpPost, ActionName("Create")]
        public IActionResult ConfirmCreate(InstructorFormViewModel insFromRequest)
        {
            if (ModelState.IsValid)
            {
                bool exists = 
                    context.Departments.Any(d => d.Id == insFromRequest.DeptId);
                if (exists)
                {
                    Instructor insdb = new Instructor();

                    insdb.Name = insFromRequest.Name;
                    insdb.Email = insFromRequest.Email;
                    insdb.Address = insFromRequest.Address;
                    insdb.DeptId = insFromRequest.DeptId;
                    insdb.Phone = insFromRequest.Phone;
                    insdb.ImageURL = insFromRequest.ImageURL?.Trim() ?? "";
                    context.Instructors.Add(insdb);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("DeptId", "Please select a valid department");
                }
            }

            insFromRequest.Departments =
                new SelectList(context.Departments.ToList(), "Id", "Name");
            return View("Create", insFromRequest);
        }

        #endregion

        #region Edit
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Instructor? insModel = context.Instructors.FirstOrDefault(i => i.Id == id);
            
            if (insModel != null)
            {
                var vm = new InstructorFormViewModel()
                {
                    Id = insModel.Id,
                    Name = insModel.Name,
                    Address = insModel.Address,
                    Email = insModel.Email,
                    Phone = insModel.Phone,
                    ImageURL = insModel.ImageURL,
                    DeptId = insModel.DeptId,
                    Departments = new SelectList(context.Departments.ToList(), "Id", "Name")
                };
                return View(vm);
            }

            return NotFound();
        }
        [HttpPost,ActionName("Edit")]
        public IActionResult ConfirmEdit(InstructorFormViewModel insFromRequest)
        {
            if (ModelState.IsValid)
            {
                bool exists =
                    context.Departments.Any(d => d.Id == insFromRequest.DeptId);
                if (exists)
                {
                    Instructor? insdb =
                    context.Instructors.FirstOrDefault(i => i.Id == insFromRequest.Id);

                    insdb.Name = insFromRequest.Name;
                    insdb.Address = insFromRequest.Address;
                    insdb.Phone = insFromRequest.Phone;
                    insdb.Email = insFromRequest.Email;
                    insdb.ImageURL = insFromRequest.ImageURL?.Trim() ?? "";
                    insdb.DeptId = insFromRequest.DeptId;
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("DeptId", "Please select a valid department");
                }
            }

            insFromRequest.Departments = 
                new SelectList(context.Departments.ToList(), "Id", "Name");
            return View("Edit", insFromRequest);
        }

        #endregion

        #region Delete
        [HttpPost]
        public IActionResult Delete(int id)
        {
            Instructor? insdb = 
                context.Instructors.FirstOrDefault(i => i.Id == id);
            if (insdb == null)
                return NotFound("Instructor Not Found");

            bool teachCourses =
                context.Courses.Any(c => c.InsId == id);
            if(teachCourses)
            {
                TempData["Error"] = "Cannot delete: this instructor teaches courses";
                return RedirectToAction("Index");
            }

            context.Instructors.Remove(insdb);
            context.SaveChanges();
            return RedirectToAction("Index");
        }


        #endregion
    }
}
