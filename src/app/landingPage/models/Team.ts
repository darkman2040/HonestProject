import { TeamMember } from "./TeamMember";

export class Team {
    constructor(public name: string,
        public description: string,
        public teamLeaderId: string,
    public teamMembers: TeamMember[]) { }
}