export class RegisterUser {
    constructor(public siteId: string,
        public firstName: string,
        public lastName: string,
        public emailAddress: string,
        public password: string) { };
}