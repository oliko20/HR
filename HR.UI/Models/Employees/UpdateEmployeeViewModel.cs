using System;
using System.ComponentModel.DataAnnotations;
using HR.UI.Contracts;

namespace HR.UI.Models.Employees
{
    public class UpdateEmployeeViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalId { get; set; }
        public Gender Gender { get; set; }
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        public string JobPosition { get; set; }
        public Status Status { get; set; }
        [DataType(DataType.Date)]

        public DateTime? FireDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
