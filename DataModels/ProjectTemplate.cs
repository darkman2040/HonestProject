using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HonestProject.DataModels
{
    public class ProjectTemplate
    {
        [Required]
        public int ID { get; set; }
        [Required]
        [StringLength(50)]
        public string Name {get; set;}
        public Team OwningTeam {get; set;}
        [Required]
        public List<ProjectTemplateWorkType> WorkTypes {get; set;}
        [Required]
        public Guid PublicIdentifier {get; set;}
    }
}