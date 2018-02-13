using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using HonestProject.DataModels;
using HonestProject.ViewModels;
using Microsoft.Extensions.Configuration;

namespace HonestProject.Repositories
{

    public class TeamRepository : BasicRepository, ITeamRepository
    {
        HonestProjectContext context;
        IConfiguration configuration;

        public TeamRepository(HonestProjectContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        public ViewModels.Team Save(ViewModels.RegisterTeam newTeam, string userName)
        {
            bool result = ValidateTeam(newTeam, userName);
            return null;
        }

        private bool ValidateTeam(RegisterTeam newTeam, string userName)
        {
            
            return true;
        }
    }
}