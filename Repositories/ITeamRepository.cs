using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HonestProject.Repositories {

    public interface ITeamRepository : IBasicRepository
    {
        ViewModels.Team[] GetManagedTeams(string userName);
        ViewModels.Team Save(ViewModels.RegisterTeam newTeam, string userName);
        ViewModels.Team Update(ViewModels.EditTeam editTeam, string userName);
    }
}