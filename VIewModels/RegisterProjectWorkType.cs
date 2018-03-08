using System;
using System.Collections.Generic;
using System.Linq;

namespace HonestProject.ViewModels
{
    public class RegisterProjectWorkType
    {
        public string Name {get; set;}
        public int ManHours {get; set;}
        public RegisterProjectTimePct[] TimePctWorkItems {get; set;}
    }
}