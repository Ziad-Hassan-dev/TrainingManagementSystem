using Microsoft.AspNetCore.Mvc;
using TrainingManagementSystem.Models;
using TrainingManagementSystem.ViewModel;

namespace TrainingManagementSystem.Controllers
{
    public class TraineeController : Controller
    {
        Context context = new Context();

        #region Read
        public IActionResult Index()
        {
            List<Trainee> traineesListModel = context.Trainees.ToList();
            if (traineesListModel != null)
                return View(traineesListModel);

            return NotFound("Trainee Not Found");
        }
        #endregion

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            return View("Create");
        }

        [HttpPost,ActionName("Create")]
        public IActionResult ConfirmCreate(TraineeFormViewModel traineeFromRequest)
        {
            if (ModelState.IsValid)
            {
                Trainee trainee = new Trainee();
                trainee.Name = traineeFromRequest.Name.Trim();
                trainee.Email = traineeFromRequest.Email.Trim();
                trainee.Phone = traineeFromRequest.Phone;
                trainee.Address = traineeFromRequest.Address;
                trainee.ImageURL = traineeFromRequest.ImageURL?.Trim() ?? "";
                context.Trainees.Add(trainee);
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("Create");
        }
        #endregion

        #region Edit
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Trainee? trainee = context.Trainees.FirstOrDefault(t => t.Id == id);

            if (trainee == null)
                return NotFound("Trainee Not Found");

            var vm = new TraineeFormViewModel()
            {
                Id = trainee.Id,
                Name = trainee.Name,
                Email = trainee.Email,
                Phone = trainee.Phone,
                Address = trainee.Address,
                ImageURL = trainee.ImageURL
            };


            return View(vm);
        }

        [HttpPost,ActionName("Edit")]
        public IActionResult ConfirmEdit(TraineeFormViewModel traineeFromRequest)
        {
            if(ModelState.IsValid)
            {
                Trainee? traineedb = 
                    context.Trainees.FirstOrDefault(t => t.Id == traineeFromRequest.Id);

                traineedb.Name = traineeFromRequest.Name.Trim();
                traineedb.Phone = traineeFromRequest.Phone;
                traineedb.Email = traineeFromRequest.Email.Trim();
                traineedb.Address = traineeFromRequest.Address;
                traineedb.ImageURL = traineeFromRequest.ImageURL?.Trim() ?? "";

                context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("Edit", traineeFromRequest);
        }
        #endregion

        #region Delete
        [HttpPost]
        public IActionResult Delete(int id)
        {
            Trainee trainee = context.Trainees.FirstOrDefault(t => t.Id == id);
            if (trainee == null)
                return NotFound("Trainee Not Found");

            context.Trainees.Remove(trainee);
            context.SaveChanges();
            return RedirectToAction("Index");
        } 
        #endregion

    }
}
