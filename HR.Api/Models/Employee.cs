using System;
namespace HR.Api.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalId { get; set; }      
        public Gender Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public string JobPosition { get; set; }
        public Status Status { get; set; }
        public DateTime? FireDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
