import { Component, OnInit, Input } from "@angular/core";

@Component({
    selector: "user-common-details",
    template: `
        <div>Place common information here!</div>
    `
})
export class UserCommonDetailsComponent implements OnInit {
    @Input()
    public userId : number | undefined;

    ngOnInit(): void {
        
    }
    
}