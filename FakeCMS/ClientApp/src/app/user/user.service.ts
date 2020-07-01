import { Injectable, Component } from '@angular/core';

import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { GridUser } from './dto/grid-user';
import { RequestApiService } from '../services/api.request.service';
import { Role } from './user-details/user-details.dto';
import { UpdateUserRolesDto } from './dto/update-user-roles';

@Injectable()
export class UserService extends RequestApiService{

    constructor(private _httpClient : HttpClient) {
         super("user");
    }

    list() : Observable<GridUser[]> {
        return this._httpClient.get<GridUser[]>(`${this.request_url}`);
    }

    getById(id : number) : Observable<GridUser>{
        return this._httpClient.get<GridUser>(`${this.request_url}/${id}`);
    }

    delete(id : number) : Observable<GridUser>{
        return this._httpClient.delete<GridUser>(`${this.request_url}/${id}`);
    }

    userRoles(userId : number) : Observable<Role[]>{
        return this._httpClient.get<Role[]>(`${this.request_url}/roles/${userId}`);
    }

    updateUserRoles( updateUserRolesDto : UpdateUserRolesDto ) : Observable<UpdateUserRolesDto>  {
        console.log(typeof(updateUserRolesDto.userId));
        this._httpClient.post<UpdateUserRolesDto>(`${this.request_url}/roles/update`, updateUserRolesDto)
            .subscribe( next => { }, err => { console.log(err)});
        return this._httpClient.post<UpdateUserRolesDto>(`${this.request_url}/roles/update`, updateUserRolesDto);
    }
}