import { RequestApiService } from "./api.request.service";
import { Role } from "../user/user-details/user-details.dto";
import { Observable } from "rxjs";
import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";

@Injectable()
export class RoleService extends RequestApiService{
    
    constructor(private _httpClient : HttpClient) {
        super("role");
    }

    list() : Observable<Role[]> {
        return this._httpClient.get<Role[]>(`${this.request_url}`);
    }
}