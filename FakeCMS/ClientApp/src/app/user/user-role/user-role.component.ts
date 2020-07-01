import { Component, Input, OnInit } from "@angular/core";
import { UserRolePart, Role } from "../user-details/user-details.dto";
import { faArrowCircleLeft, faArrowCircleRight } from '@fortawesome/free-solid-svg-icons';

@Component({
    selector: "user-role-details",
    templateUrl: "user-role.component.html",
    styleUrls: ["./../../styles.custom-table.css"]
})
export class UserRoleDetailsComponent implements OnInit{
    @Input()
    userRolePart : UserRolePart;
    public userRoles : Role[];
    public availableRoles: Role[];

    public arrowAddUserRole = faArrowCircleRight;
    public arrowRemoveUserRole = faArrowCircleLeft;
    public selectedRole : Role;

    ngOnInit(): void {
        this.emptySelectedRole();

        this.userRolePart.registerOnRolesChange( (allRoles : Role[], onlyUserRoles : Role[]) => {
            this.userRoles = onlyUserRoles;
            this.availableRoles = allRoles;
        })
    }

    public selectRole(userRole : Role)
    {
        this.selectedRole = userRole;
    }

    private emptySelectedRole(){
        this.selectedRole = new Role();
        this.selectedRole.id = -1;
    }

    onAddUserRoleClick(){
        this.userRolePart.userAddRole(this.selectedRole.id);
        this.emptySelectedRole();
    }

    onRemoveUserRoleClick(){
        this.userRolePart.userRemoveRole(this.selectedRole.id)
        this.emptySelectedRole();
    }
}