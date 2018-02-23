import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Team } from '../models/Team'
import {RegisterTeam} from '../models/RegisterTeam'
import { EditTeam } from '../models/EditTeam';

@Injectable()
export class TeamService {
    public userId: string;

    constructor(private http: HttpClient) {
    }

    public GetManagedTeams(): Observable<Team[]> {
        return this.http.get<Team[]>('api/team/managedTeams'); 
    }

    public RegisterNewTeam(team: RegisterTeam): Observable<boolean> {
        return this.http.post<RegisterTeamResponse>('api/team/', team)
            .map((response: RegisterTeamResponse) => {
                if (response && response.id) {
                    return true;
                }
                else {
                    return false;
                }
            })
            .catch((e: any) => Observable.throw(this.errorHandler(e)));
            
    }

    public EditTeam(team: EditTeam): Observable<boolean> {
        return this.http.post<RegisterTeamResponse>('api/team/update', team)
            .map((response: RegisterTeamResponse) => {
                if (response && response.id) {
                    return true;
                }
                else {
                    return false;
                }
            })
            .catch((e: any) => Observable.throw(this.errorHandler(e)));
            
    }

    errorHandler(error: any): void {
        console.log(error)
      }
}

export class RegisterTeamResponse {
    constructor(public id: string) { };
}