using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TrainingManagementSystem.Models;
using TrainingManagementSystem.ViewModel;

namespace TrainingManagementSystem.Controllers
{
    public class CourseController : Controller
    {
        Context context = new Context();

        #region Read
        public IActionResult Index()
        {
            List<Course> coursesModel =
                context.Courses
                .Include(c => c.Instructor)
                .Include(c => c.Department)
                .ToList();

            return View(coursesModel);
        }
        #endregion

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            var vm = new CourseFormViewModel()
            {
                Instructors = new SelectList(context.Instructors.ToList(), "Id", "Name"),
                Departments = new SelectList(context.Departments.ToList(), "Id", "Name")
            };
            return View(vm);
        }
        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Create")]
        public IActionResult ConfirmCreate(CourseFormViewModel crsFromRequest)
        {
            bool existsDept =
                    context.Departments.Any(d => d.Id == crsFromRequest.DeptId);
            bool existsIns =
                context.Instructors.Any(i => i.Id == crsFromRequest.InsId);
            if (!existsDept)
                ModelState.AddModelError("DeptId", "Please select a valid department");
            if (!existsIns)
                ModelState.AddModelError("InsId", "Please select a valid instructor");
            if (ModelState.IsValid)
            {
                Course newCourse = new Course()
                {
                    Id = crsFromRequest.Id,
                    Name = crsFromRequest.Name.Trim(),
                    Description = crsFromRequest.Description.Trim(),
                    Degree = crsFromRequest.Degree,
                    MinDegree = crsFromRequest.MinDegree,
                    Hours = crsFromRequest.Hours,
                    DeptId = crsFromRequest.DeptId,
                    InsId = crsFromRequest.InsId,
                };
                context.Courses.Add(newCourse);
                context.SaveChanges();
                TempData["Success"] = "Course created successfully";
                return RedirectToAction("Index");
            }

            crsFromRequest.Departments = new SelectList(context.Departments.ToList(), "Id", "Name");
            crsFromRequest.Instructors = new SelectList(context.Instructors.ToList(), "Id", "Name");
            return View("Create", crsFromRequest);
        }
        #endregion

        #region Edit
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Course? crsModel = 
                context.Courses.FirstOrDefault(c => c.Id == id);
            if (crsModel == null)
                return NotFound("Course Not Found");

            var vm = new CourseFormViewModel()
            {
                Id = crsModel.Id,
                Name = crsModel.Name,
                Description = crsModel.Description,
                Degree = crsModel.Degree,
                MinDegree = crsModel.MinDegree,
                Hours = crsModel.Hours,
                DeptId = crsModel.DeptId,
                InsId = crsModel.InsId,
                Departments = new SelectList(context.Departments.ToList(), "Id", "Name"),
                Instructors = new SelectList(context.Instructors.ToList(), "Id", "Name")
            };

            return View(vm);
        }

        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Edit")]
        public IActionResult ConfirmEdit(CourseFormViewModel crsFromRequest)
        {
            bool existsDept =
                context.Departments.Any(d => d.Id == crsFromRequest.Id);
            bool existsIns =
                context.Instructors.Any(i => i.Id == crsFromRequest.Id);
            if (!existsDept)
                ModelState.AddModelError("DeptId", "Please select a valid department");
            if (!existsIns)
                ModelState.AddModelError("InsId", "Please select a valid instructor");

            if (ModelState.IsValid)
            {
                Course? crsdb =
                context.Courses.FirstOrDefault(c => c.Id == crsFromRequest.Id);

                crsdb.Name = crsFromRequest.Name.Trim();
                crsdb.Description = crsFromRequest.Description.Trim();
                crsdb.Degree = crsFromRequest.Degree;
                crsdb.MinDegree = crsFromRequest.MinDegree;
                crsdb.Hours = crsFromRequest.Hours;
                crsdb.DeptId = crsFromRequest.DeptId;
                crsdb.InsId = crsFromRequest.InsId;
                context.SaveChanges();
                TempData["Success"] = "Course edited successfully";
                return RedirectToAction("Index");
            }

            crsFromRequest.Departments = new SelectList(context.Departments.ToList(), "Id", "Name");
            crsFromRequest.Instructors = new SelectList(context.Instructors.ToList(), "Id", "Name");
            return View("Edit", crsFromRequest);
        }
        #endregion

        #region Delete
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Delete(int id)
        {
            Course? crs = context.Courses.FirstOrDefault(c => c.Id == id);
            if (crs == null)
                return NotFound("Course Not Found");

            bool hasEnrollments =
                context.CourseEnrollments.Any(e => e.CourseId == id);
            if (hasEnrollments)
            {
                TempData["Error"] = "Cannot delete: this course has enrollments";
                return RedirectToAction("Index");
            }

            context.Courses.Remove(crs);
            context.SaveChanges();
            TempData["Success"] = "Course deleted successfully";
            return RedirectToAction("Index");
        } 
        #endregion
    }
}
