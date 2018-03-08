using System;
using System.Collections.Generic;
using System.Linq;

namespace HonestProject.ViewModels
{
    public class RegisterProject
    {
        public string Name {get; set;}
        public string Description{get; set;}
        public DateTime StartDate{get;set;}
        public int PercentageEstimate{get; set;}
        public string Color{get;set;}
        public RegisterProjectWorkType[] WorkTypeItems {get; set;} 
    }
}
