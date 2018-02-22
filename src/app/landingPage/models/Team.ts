import { TeamMember } from "./TeamMember";

export class Team {
    constructor(public id: string,
        public name: string,
        public description: string,
        public teamLeaderId: string,
    public teamMembers: TeamMember[]) { }
}