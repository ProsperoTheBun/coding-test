import { Component, OnInit } from "@angular/core";
import { Observable, Subject } from "rxjs";

import { Account, AccountType } from "../model";
import { AccountService } from "../services/account/account.service";

@Component({
    selector: "app-home",
    templateUrl: "./home.component.html",
})
export class HomeComponent implements OnInit {
    public accounts$: Observable<Account[]>;
    public accountTypes$: Observable<AccountType[]>;
    public selectedAccountTypeFilter$ = new Subject<AccountType>();

    constructor(private accountService: AccountService) {}

    public ngOnInit(): void {
        this.accounts$ = this.accountService.getAccounts(null);

        this.accountTypes$ = this.accountService.getAccountTypes();

        this.selectedAccountTypeFilter$.subscribe((t) => {
            this.accounts$ = this.accountService.getAccounts(t && t.Id);
        });
    }
}
