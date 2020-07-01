import { Component, OnInit} from '@angular/core';
import { UserService } from '../user.service';
import { RoleService } from 'src/app/services/role.service';
import { CssSelector } from '@angular/compiler';
import { UpdateUserRolesDto } from '../dto/update-user-roles';

export class GeneralUser{
    public rolePart : UserRolePart;
    public commonPart : UserCommonPart;

    constructor(userId : number,
            userService : UserService,
            roleService : RoleService) {
        this.rolePart = new UserRolePart(userId, userService, roleService);
    }
}

export abstract class UserPart {
    protected readonly userId : number;
    
    constructor(userId : number) {
        this.userId = userId;
    }
}

export class UserRolePart extends UserPart{
    private onRolesChange : Array<(allRoles : Role[], userRoles : Role[]) => void>;
    private _rolesUserHas : Role[];
    private _rolesAvailable : Role[];
    
    constructor(userId : number,
                private _userService : UserService,
                private _roleService : RoleService) {
        super(userId);
        this.initRoleFields();
        this.onRolesChange = new Array<(allRoles : Role[], userRoles : Role[]) => void>();
    }

    
    userAddRole(roleId : number){
        let roleToAdd = this._rolesAvailable.find(r => r.id == roleId);
        if(!roleToAdd)return;

        this._rolesUserHas.push(roleToAdd);
        this._rolesAvailable = this._rolesAvailable.filter(r => r.id != roleId);
        this.onRolesChangeTrigger();
    }

    userRemoveRole(roleId : number){
        let roleToRemove = this._rolesUserHas.find(r => r.id == roleId);
        if(!roleToRemove)return;

        this._rolesUserHas = this._rolesUserHas.filter(r => r.id != roleId);
        this._rolesAvailable.push(roleToRemove);
        this.onRolesChangeTrigger();
    }
    
    private initRoleFields(){
        this._roleService.list().subscribe(roles => {
            this._rolesAvailable = roles;
            this._rolesUserHas = new Array<Role>();
            if(this.userId){
                this._userService.userRoles(this.userId).subscribe(userRoles => {
                    this._rolesUserHas = userRoles;
                    this._rolesAvailable = this._rolesAvailable.filter(availableRole => 
                        this._rolesUserHas.findIndex(userRole => availableRole.id == userRole.id) < 0);
                    this.onRolesChangeTrigger();
                });
            }
        });
    }

    onRolesChangeTrigger(){
        this.onRolesChange.forEach(callback => 
            callback(this._rolesAvailable, this._rolesUserHas));
    }

    registerOnRolesChange( operation: (availableRoles : Role[], userRoles: Role[]) => void){
        this.onRolesChange.push(operation);
    }

    updateUserRoles(){
        let dto = new UpdateUserRolesDto();
        dto.userId = +this.userId;
        dto.roleIds = this._rolesUserHas.map(r => r.id);
        this._userService.updateUserRoles(dto);
    }

}

export class UserCommonPart extends UserPart{
    
}

export class Role{
    public id: number;
    public name: string;
}


