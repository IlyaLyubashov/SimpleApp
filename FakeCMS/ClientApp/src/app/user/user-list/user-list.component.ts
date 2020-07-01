import { Component, OnInit } from '@angular/core';
import {Router} from '@angular/router'

import { faMinusCircle } from '@fortawesome/free-solid-svg-icons';
import { GridUser } from '../dto/grid-user';
import { UserService } from '../user.service';


@Component({
  selector: 'user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['../../styles.custom-table.css'],
  providers: [UserService]
})
export class UserListComponent implements OnInit{
  faMinusCircle = faMinusCircle;

  public gridUsers: GridUser[];
  public deleteMode : boolean;
  

  constructor( private _userService : UserService, 
              private _router: Router) {
    
  }


  ngOnInit(){
    this.updateUsers();
  }
  
  toogleDeleteMode(){
    this.deleteMode = !this.deleteMode;
  }

  selectUser(gridUser : GridUser){
    this._router.navigate(['user-details', gridUser.id]);
  }
  
  async deleteUser(gridUser : GridUser){
    await this._userService.delete(gridUser.id).toPromise();
    this.updateUsers();
  }

  private  updateUsers(){
    this._userService.list()
        .subscribe(users => this.gridUsers = users);
  }

  
}



