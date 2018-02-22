import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map'

@Injectable()
export class AuthenticationService {
    public token: string;
    refreshToken: string;

    constructor(private http: HttpClient) {
        // set token if saved in local storage
        var currentUser = JSON.parse(localStorage.getItem('currentUser'));
        this.token = currentUser && currentUser.token;
        this.refreshToken = currentUser && currentUser.refreshToken;
    }

    login(username: string, password: string, getRefreshToken: boolean): Observable<boolean> {
        return this.http.post<Response>('/api/authenticate', new Request(username, password, getRefreshToken))
            .map((response: Response) => {
                // login successful if there's a jwt token in the response
                let token = response.token;
                let userId = response.userId;
                let refreshToken = response.refreshToken;
                if (token) {
                    // set token property
                    this.token = token;
                    this.refreshToken = refreshToken;

                    // store username and jwt token in local storage to keep user logged in between page refreshes
                    localStorage.setItem('currentUser', JSON.stringify({ userId: userId, token: token, refreshToken: refreshToken }));
                    console.log(JSON.stringify({ userId: userId, token: token, refreshToken: refreshToken }))

                    // return true to indicate successful login
                    return true;
                } else {
                    // return false to indicate failed login
                    return false;
                }
            });
    }

    refreshAuthToken(): Observable<string> {
        return this.http.post<NewTokenResponse>(`/api/authenticate/tokens/${this.refreshToken}/refresh`, null)
        .map((response: NewTokenResponse) => {
            console.log('New Token: ' + JSON.stringify(response));
            if(response)
            {
                
                var currentUser = JSON.parse(localStorage.getItem('currentUser'));
                currentUser.token = response.newToken;
                this.token = response.newToken;
                localStorage.setItem('currentUser', JSON.stringify(currentUser));
                console.log(JSON.stringify(currentUser))
                return currentUser.token;
            }
            else
            {
                console.log('Error: ' + JSON.stringify(response));
                Observable.throw("Could not get auth token");
            }
        })
    }

    logout(): void {
        // clear token remove user from local storage to log user out
        this.token = null;
        localStorage.removeItem('currentUser');
    }
}

export class Request {
    constructor(public username: string,
        public password: string,
    public getRefreshToken: boolean) { };
}

export class Response {
    constructor(public userId: string,
        public token: string,
        public refreshToken: string) { };
}

export class NewTokenResponse {
    constructor(public newToken: string) {};
}
