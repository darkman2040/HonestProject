using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HonestProject.DataModels {

    public class Role {
        [Required]
        public int ID {get; set;}
        [Required]
        [StringLength(50)]
        public string Description;
    }
}