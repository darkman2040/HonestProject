using System.ComponentModel.DataAnnotations;

namespace HonestProject.DataModels
{
    public class TimePercentageUserProjectWorkType
    {
        [Required]
        public int ID {get; set;}
        [Required]
        public User TeamMember {get; set;}
        [Required]
        public ProjectWorkType ProjectWorkType {get; set;}
        [Required]
        public int WorkPercentage {get; set;}
    }
}