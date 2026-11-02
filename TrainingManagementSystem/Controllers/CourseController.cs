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
        [HttpPost, ActionName("Create")]
        public IActionResult ConfirmCreate(CourseFormViewModel crsFromRequest)
        {
            if (crsFromRequest.Name != null && crsFromRequest.DeptId != 0 && crsFromRequest.InsId != 0)
            {
                Course newCourse = new Course()
                {
                    Id = crsFromRequest.Id,
                    Name = crsFromRequest.Name,
                    Description = crsFromRequest.Description,
                    Degree = crsFromRequest.Degree,
                    MinDegree = crsFromRequest.MinDegree,
                    Hours = crsFromRequest.Hours,
                    DeptId = crsFromRequest.DeptId,
                    InsId = crsFromRequest.InsId,
                };
                context.Courses.Add(newCourse);
                context.SaveChanges();
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
            Course crsModel = context.Courses.FirstOrDefault(c => c.Id == id);
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

        [HttpPost, ActionName("Edit")]
        public IActionResult ConfirmEdit(CourseFormViewModel crsFromRequest)
        {
            if (crsFromRequest.Name != null && crsFromRequest.InsId != 0 && crsFromRequest.DeptId != 0)
            {
                Course crsdb =
                    context.Courses.FirstOrDefault(c => c.Id == crsFromRequest.Id);
                
                crsdb.Name = crsFromRequest.Name;
                crsdb.Description = crsFromRequest.Description;
                crsdb.Degree = crsFromRequest.Degree;
                crsdb.MinDegree = crsFromRequest.MinDegree;
                crsdb.Hours = crsFromRequest.Hours;
                crsdb.DeptId = crsFromRequest.DeptId;
                crsdb.InsId = crsFromRequest.InsId;
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            crsFromRequest.Departments = new SelectList(context.Departments.ToList(), "Id", "Name");
            crsFromRequest.Instructors = new SelectList(context.Instructors.ToList(), "Id", "Name");
            return View("Edit", crsFromRequest);
        }
        #endregion

        #region Delete
        [HttpPost]
        public IActionResult Delete(int id)
        {
            Course crs = context.Courses.FirstOrDefault(c => c.Id == id);
            if (crs == null)
                return NotFound("Course Not Found");

            context.Courses.Remove(crs);
            context.SaveChanges();

            return RedirectToAction("Index");
        } 
        #endregion
    }
}
