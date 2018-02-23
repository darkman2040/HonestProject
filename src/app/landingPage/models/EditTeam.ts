import {EditTeamMember} from './EditTeamMember'

export class EditTeam {
    public constructor(
        public id: string,
        public name: string,
        public description: string,
        public teamLeaderId: string,
        public teamManagerId: string,
    public teamMembers: EditTeamMember[]) { }
}