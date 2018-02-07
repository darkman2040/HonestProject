using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HonestProject.DataModels {

    public class Site {
        [Required]
        public int ID {get;set;}
        [Required]
        [StringLength(50)]
        public string Name {get; set;}
        [Required]
        public int HoursPerDay {get; set;}
        [Required]
        public bool IncludeWeekends{get; set;}
        public Guid PublicIdentifier{get; set;}
    }
}