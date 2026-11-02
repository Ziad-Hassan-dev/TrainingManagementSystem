using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TrainingManagementSystem.Models;
using TrainingManagementSystem.ViewModel;

namespace TrainingManagementSystem.Controllers
{
    public class CourseEnrollmentController : Controller
    {
        Context context = new Context();
        #region Read
        public IActionResult Index()
        {
            List<CourseEnrollment> courseEnrollmentsModel =
                context.CourseEnrollments
                .Include(ce => ce.Trainee)
                .Include(ce => ce.Course)
                .ToList();
            return View(courseEnrollmentsModel);
        }
        #endregion

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            var vm = new CourseEnrollmentFormViewModel()
            {
                Course = new SelectList(context.Courses.ToList(), "Id", "Name"),
                Trainee = new SelectList(context.Trainees.ToList(), "Id", "Name")
            };

            return View(vm);
        }
        [HttpPost, ActionName("Create")]
        public IActionResult ConfirmCreate(CourseEnrollmentFormViewModel enrollmentFromRequest)
        {
            if ((int)enrollmentFromRequest.Status != -1 && enrollmentFromRequest.CourseId != -1 && enrollmentFromRequest.TraineeId != -1)
            {
                CourseEnrollment enrollment = new CourseEnrollment();
                enrollment.EnrollmentDate = enrollmentFromRequest.EnrollmentDate;
                enrollment.Status = enrollmentFromRequest.Status;
                enrollment.CourseId = enrollmentFromRequest.CourseId;
                enrollment.TraineeId = enrollmentFromRequest.TraineeId;
                context.CourseEnrollments.Add(enrollment);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            enrollmentFromRequest.Course = new SelectList(context.Courses.ToList(), "Id", "Name");
            enrollmentFromRequest.Trainee = new SelectList(context.Trainees.ToList(), "Id", "Name");
            return View("Create", enrollmentFromRequest);
        }
        #endregion

        #region Edit
        [HttpGet]
        public IActionResult Edit(int id)
        {
            CourseEnrollment courseEnrollment = context.CourseEnrollments.FirstOrDefault(ce => ce.Id == id);
            if (courseEnrollment == null)
                return NotFound("CourseEnrollments Not Found");

            var vm = new CourseEnrollmentFormViewModel()
            {
                Id = courseEnrollment.Id,
                EnrollmentDate = courseEnrollment.EnrollmentDate,
                Status = courseEnrollment.Status,
                TraineeId = courseEnrollment.TraineeId,
                CourseId = courseEnrollment.CourseId,
                Course = new SelectList(context.Courses.ToList(), "Id", "Name"),
                Trainee = new SelectList(context.Trainees.ToList(), "Id", "Name")
            };

            return View(vm);
        }
        [HttpPost, ActionName("Edit")]
        public IActionResult ConfirmEdit(CourseEnrollmentFormViewModel enrollmentFromRequest)
        {

            if ((int)enrollmentFromRequest.Status != -1 && enrollmentFromRequest.CourseId != -1 && enrollmentFromRequest.TraineeId != -1)
            {
                CourseEnrollment enrollment = context.CourseEnrollments.FirstOrDefault(ce => ce.Id == enrollmentFromRequest.Id);
                
                enrollment.EnrollmentDate = enrollmentFromRequest.EnrollmentDate;
                enrollment.Status = enrollmentFromRequest.Status;
                enrollment.CourseId = enrollmentFromRequest.CourseId;
                enrollment.TraineeId = enrollmentFromRequest.TraineeId;
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            enrollmentFromRequest.Course = new SelectList(context.Courses.ToList(), "Id", "Name");
            enrollmentFromRequest.Trainee = new SelectList(context.Trainees.ToList(), "Id", "Name");
            return View("Edit", enrollmentFromRequest);
        }
        #endregion

        [HttpPost]
        public IActionResult Delete(int id)
        {
            CourseEnrollment enrollment = context.CourseEnrollments.FirstOrDefault(ce => ce.Id == id);
            if (enrollment == null)
                return NotFound("CourseEnrollment Not Found");

            context.CourseEnrollments.Remove(enrollment);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
