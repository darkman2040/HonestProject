using System;
using System.Collections.Generic;
using System.Linq;

namespace HonestProject.ViewModels
{
  public class AuthenticateRequest {

      public string username {get; set;}
      public string password {get; set;}
      public bool getRefreshToken {get; set;}

  }
}