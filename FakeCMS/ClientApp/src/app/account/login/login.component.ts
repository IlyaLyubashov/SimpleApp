import { Component } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
 
import { AccountService } from '../account.service';
import { HttpErrorResponse } from '@angular/common/http';

 
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
})
export class LoginComponent  {
  username : string;
  password : string;
  loginErrorMessage : string;
   
  constructor(private accountService: AccountService) {
     
  }

  Login() {

    if(!this.username || !this.password){
      this.loginErrorMessage = 'Введите логин и пароль.';
      return;
    }

    this.accountService.logIn(this.username, this.password, 
      (errorResponse : any) => {
        let httpError = errorResponse as HttpErrorResponse;
        if(httpError){
          if( typeof httpError.error == 'string')
            this.loginErrorMessage = httpError.error;
        }
        else{
          this.loginErrorMessage = 'Ошибка входа. Повторите попытку позже.'
        }
      }
    );
  }
}