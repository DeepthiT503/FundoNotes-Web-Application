using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Models
{
    public class ColloboratorModel
    {
        [Required(ErrorMessage="c_Email is Required")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")]
        public string c_Email {get; set; }

        [Required(ErrorMessage ="NotesId is Required")]
        public int notesId { get; set; }

        [Required(ErrorMessage="UserId is Required")]
        public int UserId { get; set; }

    }
}
