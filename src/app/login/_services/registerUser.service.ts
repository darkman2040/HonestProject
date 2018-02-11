import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/catch'

import {RegisterUser} from '../_models/registerUser.model'

@Injectable()
export class RegisterUserService {

    constructor(private http: HttpClient) {
    }

    public CheckIfSingleSite(): Observable<boolean> {
        return this.http.get<boolean>('api/user/isSingleSiteConfig');
    }

    public RegisterNewUser(user: RegisterUser): Observable<boolean> {
        return this.http.post<RegisterUserResponse>('api/user/', user)
            .map((response: RegisterUserResponse) => {
                console.log(JSON.stringify(response))
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

export class RegisterUserResponse {
    constructor(public id: string) { };
}