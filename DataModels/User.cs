using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HonestProject.DataModels {

    public class User {
        [Required]
        public int ID {get; set;}
        [Required]
        [StringLength(50)]
        public string FirstName {get; set;}
        [Required]
        [StringLength(50)]
        public string LastName {get; set;}
        [Required]
        [StringLength(100)]
        public string EmailAddress {get; set;}
        [Required]
        public string PasswordHash {get; set;}
        public DateTime CreatedDate {get; set;}
        public Team Team {get; set;}
        public Role Role {get; set;}
        [Required]
        public Site Site{get; set;}
        [Required]
        public Guid PublicIdentifier {get; set;}
        public string RefreshToken{get; set;}
        public DateTime RefreshTokenExpiration{get; set;}
    }
}