﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RespositoryLayer.Entity
{
    public class ImageEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ImageId { get; set; }
        public string ImageName { get; set; }
        public string ImageUrl { get; set; }

        [ForeignKey("notes")]
        public int notesId { get; set; }
        [JsonIgnore]
        public virtual NotesEntity notes { get; set; }

    }
}
