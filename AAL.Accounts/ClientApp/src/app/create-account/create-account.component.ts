import { Component } from "@angular/core";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { Router } from "@angular/router";
import { ToastrService } from "ngx-toastr";

import { Account } from "../model";
import { AccountService } from "../services/account/account.service";

@Component({
    selector: "app-create-account",
    templateUrl: "./create-account.component.html",
})
export class CreateAccountComponent {
    public accountForm = new FormGroup({
        firstName: new FormControl("", Validators.required),
        lastName: new FormControl("", Validators.required),
        balance: new FormControl("0"),
    });
    public model: Account = new Account();
    constructor(private accountService: AccountService, private router: Router, private toastr: ToastrService) {}

    public onSubmit() {
        const accountPayload: Account = {
            FirstName: this.accountForm.value.firstName,
            LastName: this.accountForm.value.lastName,
            Balance: parseInt(this.accountForm.value.balance, 10),
        };

        this.accountService.postAccount(accountPayload).subscribe((newAccount: Account) => {
            this.toastr.success(`New account created for ${newAccount.FirstName} ${newAccount.LastName}`);
            this.router.navigate(["/"]);
        });
    }
}
