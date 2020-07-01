import { Component, OnInit } from "@angular/core";
import { UserService } from "../user.service";
import { ActivatedRoute } from "@angular/router";
import { GeneralUser } from "./user-details.dto";
import { RoleService } from "src/app/services/role.service";

@Component({
    selector: 'user-details',
    templateUrl: './user-details.component.html',
    providers: [UserService, RoleService]
})
export class UserDetailsComponent implements OnInit
{
    public readonly userId : number | undefined;
    public currentTab : UserPartToDisplay;
    public generalUser : GeneralUser;

    public user: UserDetailsComponent;

    constructor(private _userService : UserService,
        private _roleService : RoleService,
        private activatedRoute : ActivatedRoute) {
        this.userId = activatedRoute.snapshot.params['id'];
    }

    ngOnInit(){
        /*if(this.userId)
            this._userService.getById(this.userId).subscribe(item => this.item = item);
        else
            this.item = Item.empty();*/
        this.generalUser = new GeneralUser(this.userId, this._userService, this._roleService );
        this.currentTab = UserPartToDisplay.Common;
    }

    updateUser(){
        this.generalUser.rolePart.updateUserRoles();
    }
}


export enum UserPartToDisplay{
    Common,
    Role,
    //Profile
}