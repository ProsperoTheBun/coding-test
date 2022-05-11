import { Component, Input } from "@angular/core";
import { FormControl } from "@angular/forms";
import { Observable, Subject } from "rxjs";
import { AccountType } from "../model";

@Component({
    selector: "app-account-type",
    templateUrl: "./account-type.component.html",
})
export class AccountTypeComponent {
    public selectedAccountType = new FormControl();
    @Input() public accountTypes$: Observable<AccountType[]>;
    @Input() public currentSelection$: Subject<AccountType>;

    public onChange(): void {
        this.currentSelection$.next(this.selectedAccountType.value);
    }
}
