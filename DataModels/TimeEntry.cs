using System.ComponentModel.DataAnnotations;

namespace HonestProject.DataModels
{
    public class TimeEntry
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public User User { get; set; }
        [Required]
        [StringLength(50)]
        public string TaskDescription { get; set; }
        [Required]
        [Range(0, 23)]
        public int Hours { get; set; }
        [Required]
        [Range(0, 59)]
        public int Minutes { get; set; }
    }
}