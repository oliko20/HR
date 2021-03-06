﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR.Api.Models
{
    public class User
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PersonalId { get; set; }

        public Gender Gender { get; set; }

        public string Email { get; set; }

        public DateTime? BirthDate { get; set; }

        public string Password { get; set; }
    }
}
