using System;
using System.ComponentModel.DataAnnotations;
using HR.UI.Contracts;

namespace HR.UI.Models.Employees
{
    public class CreateEmployeeViewModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string PersonalId { get; set; }
        public Gender Gender { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Required]
        public string JobPosition { get; set; }
        public Status Status { get; set; }
    }
}
