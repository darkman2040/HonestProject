import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../models/User'

@Injectable()
export class UserService {
    public userId: string;

    constructor(private http: HttpClient) {
    }

    public GetCurrentUser(): Observable<User> {
        var currentUser = JSON.parse(localStorage.getItem('currentUser'));
        this.userId = currentUser && currentUser.userId;
        return this.http.get<User>('api/user/' + this.userId); 
    }

    public GetUnassignedUsers(): Observable<User[]> {
        return this.http.get<User[]>('api/user/unassigned'); 
    }

    public GetTeamMembers(teamId: string): Observable<User[]> {
        return this.http.get<User[]>('api/user/GetTeamMembers/' + teamId);
    }
}