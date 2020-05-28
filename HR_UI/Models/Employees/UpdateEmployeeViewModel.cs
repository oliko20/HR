using System;
using HR.Api.Models;

namespace HR_UI.Models.Employees
{
    public class UpdateEmployeeViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalId { get; set; }
        public Gender Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public string JobPosition { get; set; }
        public Status Status { get; set; }
        public DateTime? FireDate { get; set; }
    }
}
