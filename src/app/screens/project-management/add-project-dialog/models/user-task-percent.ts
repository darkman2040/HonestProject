import { User } from "../../../../landingPage/models/User";

export class UserTaskPercent {
    constructor(public user: User,
      public pct: number) { }
  }