import { Injectable } from '@angular/core';
import { HttpClientModule, HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { RequestApiService } from 'src/app/services/api.request.service';
import { Observable } from 'rxjs';
 
@Injectable({
  providedIn: 'root'
})
export class AccountService extends RequestApiService {
  token : string;
  public static tokenKeyInStorage = 'auth_token';
 
  constructor(private http: HttpClient,
              private router: Router) {
    super("account");
   }


  logIn(username: string, password: string, 
      errorHanlder? : (error: any) => void) {

    this.http.post(this.request_url + '/login', {Username: username,Password: password})
      .subscribe(
        (resp: any) => {
          this.router.navigate(['']);
          localStorage.setItem(AccountService.tokenKeyInStorage, resp.token);
        },
        (error: any) => errorHanlder != null ? errorHanlder(error) : (error : any) => {} );
  }

  logOut(){
    localStorage.removeItem(AccountService.tokenKeyInStorage)
  }

  isLoggedIn(){
    return localStorage.getItem(AccountService.tokenKeyInStorage) != null;
  }
 
}