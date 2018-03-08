using System;
using System.ComponentModel.DataAnnotations;

namespace HonestProject.DataModels
{
    public class TimePercentageUserProjectWorkType
    {
        [Required]
        public int ID {get; set;}
        [Required]
        public ProjectWorkType ProjectWorkType {get; set;}
        [Required]
        public User User {get; set;}
        [Required]
        public int WorkPercentage {get; set;}
        [Required]
        public Guid PublicIdentifier {get; set;}
    }
}