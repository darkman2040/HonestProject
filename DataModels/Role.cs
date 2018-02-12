using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HonestProject.DataModels
{

    public class Role
    {
        [Required]
        public int ID { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(300)]
        public string Description { get; set; }
        [Required]
        public Guid PublicIdentifier { get; set; }
    }
}