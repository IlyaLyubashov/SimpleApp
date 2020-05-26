import { Injectable, Component } from '@angular/core';

import { Item } from './item';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class ItemService{
    private readonly entity_api_endpoint : string = window.location.origin + "/api/item";
    private items : Item[];
    private readonly _httpClient : HttpClient;

    constructor(httpClient : HttpClient) {
        this._httpClient = httpClient;
    }

    list() : Observable<Item[]> {
        let items : Item[];
        return this._httpClient.get<Item[]>(`${this.entity_api_endpoint}`);

    }

    create(item : Item) : Observable<Item>{
        return this._httpClient.post<Item>(`${this.entity_api_endpoint}`, item);
    }

    update(item : Item) : Observable<Item>{
        return this._httpClient.put<Item>(`${this.entity_api_endpoint}`, item);
    }

    getById(id : number) : Observable<Item>{
        return this._httpClient.get<Item>(`${this.entity_api_endpoint}/${id}`);
    }

    delete(id : number) : Observable<Item>{
        return this._httpClient.delete<Item>(`${this.entity_api_endpoint}/${id}`);
    }
}