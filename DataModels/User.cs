using System;
using System.Collections.Generic;
using System.Linq;

namespace HonestProject.DataModels {

    public class User {
        public int ID {get; set;}
        public string FirstName {get; set;}
        public string LastName {get; set;}
        public string UserName {get; set;}
        public DateTime CreatedDate {get; set;}
    }
}