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
            bool existsTrainee =
                context.Trainees.Any(t => t.Id == enrollmentFromRequest.TraineeId);
            bool existsCourse =
                context.Courses.Any(c => c.Id == enrollmentFromRequest.CourseId);
            bool alreadyEnrolled =
                context.CourseEnrollments.Any(e =>
                e.TraineeId == enrollmentFromRequest.TraineeId &&
                e.CourseId == enrollmentFromRequest.CourseId &&
                e.Status != EnrollmentStatus.Cancelled &&
                e.Id != enrollmentFromRequest.Id
                );

            if (!existsCourse)
                ModelState.AddModelError("CourseId", "Please select a valid Course");
            if (!existsTrainee)
                ModelState.AddModelError("TraineeId", "Please select a valid Trainee");
            if (alreadyEnrolled)
                ModelState.AddModelError("", "This trainee is already enrolled in this course");

            if (ModelState.IsValid) 
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
            CourseEnrollment? courseEnrollment = context.CourseEnrollments.FirstOrDefault(ce => ce.Id == id);
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

            bool existsTrainee =
                context.Trainees.Any(t => t.Id == enrollmentFromRequest.TraineeId);
            bool existsCourse =
                context.Courses.Any(c => c.Id == enrollmentFromRequest.CourseId);
            bool alreadyEnrolled =
                context.CourseEnrollments.Any(e =>
                e.TraineeId == enrollmentFromRequest.TraineeId &&
                e.CourseId == enrollmentFromRequest.CourseId &&
                e.Status != EnrollmentStatus.Cancelled &&
                e.Id != enrollmentFromRequest.Id
                );

            if (!existsCourse)
                ModelState.AddModelError("CourseId", "Please select a valid Course");
            if (!existsTrainee)
                ModelState.AddModelError("TraineeId", "Please select a valid Trainee");
            if (alreadyEnrolled)
                ModelState.AddModelError("", "This trainee is already enrolled in this course");

            if (ModelState.IsValid)
            {
                    CourseEnrollment? enrollment =
                        context.CourseEnrollments.FirstOrDefault(ce => ce.Id == enrollmentFromRequest.Id);

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
            CourseEnrollment? enrollment = 
                context.CourseEnrollments.FirstOrDefault(ce => ce.Id == id);
            if (enrollment == null)
                return NotFound("CourseEnrollment Not Found");

            bool hasResult =
                context.CourseResults.Any(r => r.EnrollmentId == id);
            if (hasResult)
            {
                TempData["Error"] = "Cannot delete: this enrollment has a result. Delete the result first";
                return RedirectToAction("Index");
            }


            context.CourseEnrollments.Remove(enrollment);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
