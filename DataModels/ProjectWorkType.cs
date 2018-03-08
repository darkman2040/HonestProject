using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HonestProject.DataModels
{
    public class ProjectWorkType
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public Project Project {get; set;}
        [Required]
        [StringLength(50)]
        public string Name {get; set;}
        [Required]
        public int ManHours {get; set;}   
        [Required]
        public List<TimePercentageUserProjectWorkType> TimePctWorkItems {get; set;}      
        [Required]
        public Guid PublicIdentifier {get; set;}

    }
}