using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HonestProject.DataModels
{
    public class Project
    {
        [Required]
        public int ID { get; set; }
        [Required]
        [StringLength(50)]
        public string Name {get; set;}
        [Required]
        [StringLength(200)]
        public string Description{get; set;}
        public DateTime StartDate {get; set;}
        public int PercentageEstimate {get; set;}
        [Required]
        public Team OwningTeam {get; set;} 
        [Required]
        public List<ProjectWorkType> WorkTypeItems {get; set;}
        [Required]
        public string Color{get; set;}
        [Required]
        public Guid PublicIdentifier {get; set;}
    }
}