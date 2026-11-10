using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TrainingManagementSystem.Models;
using TrainingManagementSystem.Validations;

namespace TrainingManagementSystem.ViewModel
{
    public class CourseFormViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage="Course name is required")]
        [StringLength(100,MinimumLength =2,ErrorMessage ="Course name must be between 2 and 100 characters")]
        [UniqueNameCourse(ErrorMessage ="A course with this name already exists in this department")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Description is required")]
        [StringLength(500,ErrorMessage ="Description must not exceed 500 characters")]
        public string Description { get; set; }
        [Required]
        [Range(0, 1000,ErrorMessage ="Degree must be between 1 and 500")]
        public int Degree { get; set; }
        [Required]
        [Range(0,1000,ErrorMessage ="Minimum degree must be between 0 and 1000")]
        [LessThanOrEqualToDegree(ErrorMessage ="Minimum degree must be less than or equal to the course degree")]
        public int MinDegree { get; set; }
        [Required]
        [Range(0,500,ErrorMessage ="Hours must be between 1 and 500")]
        public int Hours { get; set; }
        [ForeignKey(nameof(Departments))]
        [Required(ErrorMessage ="Please select a department")]
        public int DeptId { get; set; }
        [ForeignKey(nameof(Instructors))]
        [Required(ErrorMessage ="Please select a instructor")]
        public int? InsId { get; set; }
        public SelectList? Instructors { get; set; }
        public SelectList? Departments { get; set; }
    }
}
