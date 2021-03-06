using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [Required]
        [StringLength(200)]
        public string Description {get; set;}
        public List<User> TeamMembers { get; set; }
        public User TeamLeader { get; set; }
        public User TeamManager { get; set; }

        [Required]
        public Site Site { get; set; }
        [Required]
        public Guid PublicIdentifier { get; set; }
    }
}