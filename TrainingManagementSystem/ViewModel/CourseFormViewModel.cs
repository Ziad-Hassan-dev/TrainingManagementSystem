using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;
using TrainingManagementSystem.Models;

namespace TrainingManagementSystem.ViewModel
{
    public class CourseFormViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Degree { get; set; }
        public int MinDegree { get; set; }
        public int Hours { get; set; }
        [ForeignKey(nameof(Departments))]
        public int DeptId { get; set; }
        [ForeignKey(nameof(Instructors))]
        public int? InsId { get; set; }
        public SelectList? Instructors { get; set; }
        public SelectList? Departments { get; set; }
    }
}
