using System;
using System.Collections.Generic;
using System.Linq;

namespace HonestProject.ViewModels
{
  public class AuthenticateResponse {

      public Guid userId {get; set;}
      public string token {get; set;}
      public string refreshToken {get; set;}

  }
}