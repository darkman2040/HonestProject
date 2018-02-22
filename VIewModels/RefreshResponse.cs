using System;
using System.Collections.Generic;
using System.Linq;

namespace HonestProject.ViewModels
{
  public class RefreshResponse {
      public RefreshResponse(string token)
      {
          NewToken = token;
      }
      public string NewToken {get; set;}
  }
}