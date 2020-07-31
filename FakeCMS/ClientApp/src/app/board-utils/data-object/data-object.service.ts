import {Injectable} from '@angular/core';
import {DataObject} from './data-object';
import { HttpClient } from '@angular/common/http';
import { RequestApiService } from 'src/app/services/api.request.service';

@Injectable()
export class DataObjectService extends RequestApiService{

  constructor(private _http: HttpClient) {
    super('dataObj')
  }

  getAll() {
    return this._http.get(this.request_url);
  }

  get(id: number) {
    return this._http.get(this.request_url + '/' + id);
  }

  put(dataObj: DataObject) {
    return this._http.put(this.request_url + '/' + dataObj.objectId, JSON.stringify(dataObj));
  }

  post(dataObj: DataObject) {
    return this._http.post(this.request_url, JSON.stringify(dataObj));
  }

  delete(dataObj: DataObject) {
    return this._http.delete(this.request_url + '/' + dataObj.objectId);
  }

}
