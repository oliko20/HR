using System;
using HR.UI.Contracts;

namespace HR.UI.Models.Employees
{
    public class DeleteEmployeeViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalId { get; set; }
        public Gender Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public string JobPosition { get; set; }
        public Status Status { get; set; }
    }
}
