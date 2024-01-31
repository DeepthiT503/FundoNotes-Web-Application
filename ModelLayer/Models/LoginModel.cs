using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "EmailId is Required")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")]
        [DataType(DataType.EmailAddress)]

        public string EmailId { get; set; }

        [Required(ErrorMessage = "Password  is Required")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$")]
        [DataType(DataType.EmailAddress)]

        public string Password { get; set; }
    }
}
