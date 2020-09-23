import {Injectable} from '@angular/core';
import {DataObject} from './data-object';
import { HttpClient } from '@angular/common/http';
import { RequestApiService } from 'src/app/services/api.request.service';

@Injectable()
export class DataObjectService extends RequestApiService{

  constructor(private _http: HttpClient) {
    super('tableObject')
  }

  getAll() {
    return this._http.get(this.request_url);
  }

  get(id: number) {
    return this._http.get(this.request_url + '/' + id);
  }

  put(dataObj: DataObject) {
    return this._http.put(this.request_url, dataObj);
  }

  post(dataObj: DataObject) {
    return this._http.post(this.request_url, dataObj);
  }

  delete(dataObj: DataObject) {
    return this._http.delete(this.request_url + '/' + dataObj.objectId);
  }

}
