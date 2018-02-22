export class RegisterSite {
    constructor(public name: string,
        public hoursPerDay: number,
        public includeWeekends: boolean,
        public uniqueSiteId: string) { }
}