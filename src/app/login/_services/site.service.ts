import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/catch'

import { RegisterSite } from '../_models/registerSite.model'

@Injectable()
export class SiteService {

    constructor(private http: HttpClient) {
    }

    public RegisterNewSite(site: RegisterSite): Observable<boolean> {
        return this.http.post<RegisterSiteResponse>('api/site/', site)
            .map((response: RegisterSiteResponse) => {
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

export class RegisterSiteResponse {
    constructor(public id: string) { };
}