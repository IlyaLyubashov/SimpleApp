import { Component, Input } from "@angular/core";

@Component({
    selector: "error-message",
    templateUrl: "./error.component.html"
})
export class ErrorComponent{
    @Input() public errorMessage : string;

}