using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;
using TrainingManagementSystem.Models;

namespace TrainingManagementSystem.ViewModel
{
    public class InstructorFormViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string ImageURL { get; set; }
        [ForeignKey("Department")]
        public int DeptId { get; set; }
        public SelectList? Departments { get; set; }
    }
}
