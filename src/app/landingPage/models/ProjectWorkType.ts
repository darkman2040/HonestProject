import { TimePercentageUserProjectWorkType } from "./TimePercentageUserProjectWorkType";

export class ProjectWorkType {
    constructor(public id: string,
      public name: string,
      public manHours: number,
      public userWorkList: TimePercentageUserProjectWorkType[])
      {}
  }