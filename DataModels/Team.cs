using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HonestProject.DataModels
{

    public class Team
    {
        [Required]
        public int ID { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public ICollection<User> TeamMembers { get; set; }
        [Required]
        public Guid PublicIdentifier { get; set; }
    }
}