using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RespositoryLayer.Entity
{
    public class Colloboratory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int c_id { get; set; }
        public string c_Email { get; set; }

        [ForeignKey("notes")]
        public int notesId { get; set; }

        [ForeignKey("Users")]
        public int UserId { get; set; }

        [JsonIgnore]
        public virtual UserEntity Users { get; set; }

        [JsonIgnore]
        public virtual NotesEntity notes { get; set; }



    }
}
