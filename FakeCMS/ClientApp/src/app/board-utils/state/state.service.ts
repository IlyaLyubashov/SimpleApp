import {Injectable} from '@angular/core';
import {State} from './state';
import {DataObject} from '../data-object/data-object';
import { Data } from '@angular/router';
import { RequestApiService } from 'src/app/services/api.request.service';
import { HttpClient } from '@angular/common/http';


@Injectable()
export class StateService extends RequestApiService{

  constructor(private _http: HttpClient) {
    super('tableState');
  }

  getAll() {
    return this._http.get<State>(this.request_url);
  }

  get(id: number) {
    return this._http.get<State>(this.request_url + '/' + id);
  }

  getCards(id: number) {
    return this._http.get<State>(this.request_url + '/' + id + '/cards');
  }

  put(state: State) {
    return this._http
      .put(this.request_url, state);
  }

  post(state: State) {;
    return this._http.post<State>(this.request_url, state);
  }

  delete(state: State) {
    return this._http.delete(this.request_url + '/' + state.id);

  }

}
