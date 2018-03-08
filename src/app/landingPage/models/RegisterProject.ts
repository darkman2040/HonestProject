import { RegisterProjectWorkType } from "./RegisterProjectWorkType";

export class RegisterProject {
    constructor(public name: string,
        public description: string,
        public startDate: Date,
        public percentageEstimate: number,
        public color: string,
        public workTypeItems: RegisterProjectWorkType[]) { }
}