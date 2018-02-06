using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HonestProject.DataModels {

    public class User {
        [Required]
        public int ID {get; set;}
        [Required]
        [StringLength(50)]
        public string FirstName {get; set;}
        [Required]
        [StringLength(50)]
        public string LastName {get; set;}
        [Required]
        [StringLength(50)]
        public string UserName {get; set;}
        [Required]
        public string Password {get; set;}
        public DateTime CreatedDate {get; set;}
        public Team Team {get; set;}
        public Role Role {get; set;}
        [Required]
        public Site Site{get; set;}
    }
}