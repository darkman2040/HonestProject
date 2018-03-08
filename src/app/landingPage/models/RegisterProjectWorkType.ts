import { RegisterProjectTimePct } from "./RegisterProjectTimePct";

export class RegisterProjectWorkType {
    constructor(public name: string,
        public manHours: number,
        public timePctWorkItems: RegisterProjectTimePct[]) { }

}