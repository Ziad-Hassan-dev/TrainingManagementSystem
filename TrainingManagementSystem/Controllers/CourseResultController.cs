using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TrainingManagementSystem.Models;
using TrainingManagementSystem.ViewModel;

namespace TrainingManagementSystem.Controllers
{
    public class CourseResultController : Controller
    {
        Context context = new Context();
        #region Read
        public IActionResult Index()
        {
            List<CourseResult> courseResultsModel =
                context.CourseResults
                .Include(r => r.CourseEnrollment)
                .ThenInclude(cr => cr.Trainee)
                .Include(r => r.CourseEnrollment)
                .ThenInclude(cr => cr.Course)
                .ToList();
            if (courseResultsModel == null)
            {
                return NotFound("CourseResult Not Found");
            }
            
            return View(courseResultsModel);
        }
        #endregion

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            var enrollment = context.CourseEnrollments
                .Select(e => new
                {
                    Id = e.Id,
                    Text = e.Trainee.Name + " " + e.Course.Name
                });

            var vm = new CourseResultFormViewModel()
            {
                Enrollments = new SelectList(enrollment, "Id", "Text")
            };
            return View("Create", vm);
        }
        [HttpPost, ActionName("Create")]
        public IActionResult ConfirmCreate(CourseResultFormViewModel resultFromRequest)
        {
            if (resultFromRequest.EnrollmentId != -1)
            {
                CourseResult courseResult = new CourseResult();
                courseResult.EnrollmentId = resultFromRequest.EnrollmentId;
                courseResult.Degree = resultFromRequest.Degree;
                courseResult.ResultDate = resultFromRequest.ResultDate;
                context.CourseResults.Add(courseResult);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            var enrollments = context.CourseEnrollments.Select(e => new { Id = e.Id, Text = e.Trainee.Name + " " + e.Course.Name });
            resultFromRequest.Enrollments = new SelectList(enrollments, "Id", "Text");
            return View("Create", resultFromRequest);
        }
        #endregion

        #region Edit
        [HttpGet]
        public IActionResult Edit(int id)
        {
            CourseResult courseResult = context.CourseResults.FirstOrDefault(cr => cr.EnrollmentId == id);
            if (courseResult == null)
                return NotFound("CourseResult Not Found");

            var enrollments = context.CourseEnrollments
                .Select(e => new { Id = e.Id, Text = e.Trainee.Name + " " + e.Course.Name });

            var vm = new CourseResultFormViewModel()
            {
                EnrollmentId = courseResult.EnrollmentId,
                Degree = courseResult.Degree,
                ResultDate = courseResult.ResultDate,
                Enrollments = new SelectList(enrollments, "Id", "Text")
            };
            return View(vm);
        }
        [HttpPost, ActionName("Edit")]
        public IActionResult ConfirmEdit(CourseResultFormViewModel resultFromRequest)
        {
            if (resultFromRequest.EnrollmentId != -1)
            {
                CourseResult courseResult =
                    context.CourseResults
                    .FirstOrDefault
                    (cr => cr.EnrollmentId == resultFromRequest.EnrollmentId);

                courseResult.EnrollmentId = resultFromRequest.EnrollmentId;
                courseResult.Degree = resultFromRequest.Degree;
                courseResult.ResultDate = resultFromRequest.ResultDate;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            var enrollments = context.CourseEnrollments
                .Select(e => new { Id = e.Id, Text = e.Trainee.Name + " " + e.Course.Name });

            var vm = new CourseResultFormViewModel()
            {
                Enrollments = new SelectList(enrollments, "Id", "Text")
            };
            return View("Edit", vm);
        }

        #endregion

        #region Delete
        [HttpPost]
        public IActionResult Delete(int id)
        {
            CourseResult courseResult = context.CourseResults.FirstOrDefault(cr => cr.EnrollmentId == id);
            if (courseResult == null)
                return NotFound("Course Result Not Found");

            context.CourseResults.Remove(courseResult);
            context.SaveChanges();
            return RedirectToAction("Index");
        } 
        #endregion
    }
}
