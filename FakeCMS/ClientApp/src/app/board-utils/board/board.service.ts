import {Injectable} from '@angular/core';
import {Observable, forkJoin} from 'rxjs';
import {Board} from '../board/board';
import {State} from '../state/state';
import {DataObject} from '../data-object/data-object';
import { HttpClient } from '@angular/common/http';
import { RequestApiService } from 'src/app/services/api.request.service';

@Injectable()
export class BoardService extends RequestApiService{
  boards: Board[] = [];

  constructor(private _http: HttpClient) {
    super('table');
  }

  getAll() {
    return this._http.get(this.request_url);
  }

  get(id: string) {
    return this._http.get(`${this.request_url}/${id}`);
  }

  getBoardWithColumnsAndCards(id: string){
    return forkJoin(this.get(id), this.getStates(id), this.getTableData(id));
  }

  getStates(id: string) {
    return this._http.get(`${this.request_url}/${id}/states`);
  }

  getTableData(id: string) {
    return this._http.get(`${this.request_url}/${id}/data`);
  }

  addStateToTable(state : State){
    return this._http.post<State>(`${this.request_url}/addState`, state);
  }
}
