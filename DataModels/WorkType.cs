using System;
using System.ComponentModel.DataAnnotations;

namespace HonestProject.DataModels
{
    public class WorkType
    {
        [Required]
        public int ID {get; set;}
        [Required]
        [StringLength(50)]
        public string Name {get; set;}
        [Required]
        public Guid PublicIdentifier {get; set;}
    }
}