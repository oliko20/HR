using System;
using System.ComponentModel.DataAnnotations;
using HR.Api.Models;

namespace HR.Api.Contracts
{
    public class UpdateEmployeeDto
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

        [DataType(DataType.Date)]
        public DateTime? FireDate { get; set; }

        public bool IsDeleted { get; set; }
        public Employee GetEmployee(int id)
        {
            return new Employee
            {
                Id = id,
                FirstName = FirstName,
                LastName = LastName,
                PersonalId = PersonalId,
                Gender = Gender,
                BirthDate = BirthDate,
                Status = Status,
                FireDate = FireDate,
                JobPosition = JobPosition,
                IsDeleted = IsDeleted
            };
        }
    }
}
