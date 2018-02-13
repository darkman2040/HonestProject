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
        public List<User> TeamMembers { get; set; }
        public int? TeamLeaderId {get; set;}
        public User TeamLeader {get; set;}

        public int? TeamManagerId {get; set;}
        public User TeamManager {get; set;}
        [Required]
        public Guid PublicIdentifier { get; set; }
    }
}