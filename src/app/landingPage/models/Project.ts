import { ProjectWorkType } from "./ProjectWorkType";

export class Project{
    constructor(
        public id: string,
        public name: string,
        public description: string,
        public startDate: Date,
        public percentageEstimate: number,
        public color: string,
        public workTypes: ProjectWorkType[]
    ) {}
}