using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class RegisterUserRequest
    {
        [Required(ErrorMessage = "First name is required.")]
        public required string Name { get; set; }
        public required string Username { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9._%+-]{1,64}@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}",
            ErrorMessage = "Your email is not valid. Pleases enter a valid email. ")]
        public string Email { get; set; }


        [Required(ErrorMessage = "password in Required")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*])(?=.{8,16}).*$",
            ErrorMessage = "Password must contain atleast 8 charactes and no more that 16 characters with One Uppercase, one lowercase, one digit and one special character")]
        public required string Password { get; set; }

        [Required(ErrorMessage = "Phone number is necessary")]
        public string PhoneNumber { get; set; }
    }
}
