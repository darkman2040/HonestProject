export class User {
    constructor(public userId: string,
        public firstName: string,
        public lastName: string,
        public emailAddress: string,
        public isSiteAdmin: boolean,
        public isManager: boolean,
        public isTeamLeader: boolean) { };
} 