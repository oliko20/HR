﻿using System;
using System.ComponentModel.DataAnnotations;

namespace HR.UI.Contracts
{
    public class EmployeeDto
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
        public DateTime? FireDate { get; set; }
    }
}
