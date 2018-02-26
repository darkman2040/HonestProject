import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Project } from '../models/Project'
import { ProjectTemplateTopLevel } from '../models/ProjectTemplateTopLevel';

@Injectable()
export class ProjectService {

    constructor(private http: HttpClient) {
    }

    public GetProjects(): Observable<Project[]> {
        return this.http.get<Project[]>('api/project/getProjects'); 
    }

    public GetProjectTemplateTopLevel(): Observable<ProjectTemplateTopLevel[]> {
        return this.http.get<ProjectTemplateTopLevel[]>('api/project/GetProjectTemplatesTopLevel'); 
    }
}