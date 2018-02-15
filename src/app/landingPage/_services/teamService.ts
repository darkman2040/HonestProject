import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Team } from '../models/Team'

@Injectable()
export class TeamService {
    public userId: string;

    constructor(private http: HttpClient) {
    }

    public GetManagedTeams(): Observable<Team[]> {
        return this.http.get<Team[]>('api/team/managedTeams'); 
    }
}