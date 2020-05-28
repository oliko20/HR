using System;
using System.ComponentModel.DataAnnotations;
using HR.Api.Models;

namespace HR.Api.Contracts
{
    public class CreateUserDto
    {
        [Required]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [Required]
        public string PersonalId { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public User GetUser(string password)
        {
            return new User
            {
                FirstName = FirstName,
                LastName = LastName,
                PersonalId = PersonalId,
                Gender = Gender,
                BirthDate = BirthDate,
                Email = Email,
                Password = password
            };
        }
    }
}
