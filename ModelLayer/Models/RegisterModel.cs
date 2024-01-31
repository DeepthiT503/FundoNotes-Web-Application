using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage="FirstName is Required")]
        [RegularExpression(@"^[A-Z][a-z]{3,20}$")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName is Required")]
        [RegularExpression(@"^[A-Z][a-z]{3,20}$")]
        public string LastName { get; set; }

        [Required(ErrorMessage ="EmailId is Required")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")]
        [DataType(DataType.EmailAddress)]
        public string EmailId { get; set; }

        [Required(ErrorMessage = "Password  is Required")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
