using HR.Api.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace HR.Api.Contracts
{
    public class CreateEmployeeDto
    {
        [Required]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        public Gender Gender { get; set; }

        [Required]
        public string PersonalId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string JobPosition { get; set; }

        public Status Status { get; set; }

        public Employee GetEmployee()
        {
            return new Employee
            {
                FirstName = FirstName,
                LastName = LastName,
                PersonalId = PersonalId,
                Gender = Gender,
                BirthDate = BirthDate,
                Status = Status,
                JobPosition = JobPosition,
            };
        }
    }
}
