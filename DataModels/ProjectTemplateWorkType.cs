using System;
using System.ComponentModel.DataAnnotations;

namespace HonestProject.DataModels
{
    public class ProjectTemplateWorkType
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public WorkType WorkType {get; set;}      
        [Required]
        public Guid PublicIdentifier {get; set;}
    }
}