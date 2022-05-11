import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { ToastrService } from "ngx-toastr";
import { EMPTY, Observable } from "rxjs";
import { catchError } from "rxjs/operators";

import { api } from "../../../environments/environment";
import { Account, AccountType } from "../../model";

@Injectable({
    providedIn: "root",
})
export class AccountService {
    constructor(private httpClient: HttpClient, private toastr: ToastrService) {}

    public getAccounts(typeId: string): Observable<Account[]> {
        return this.httpClient
            .get<Account[]>(api.baseUrl + "accounts", {
                params: { accountTypeId: typeId },
            })
            .pipe(catchError((err) => this.displayError("Error getting accounts", err)));
    }

    public getAccountTypes(): Observable<AccountType[]> {
        return this.httpClient
            .get<AccountType[]>(api.baseUrl + "accounttypes")
            .pipe(catchError((err) => this.displayError("Error getting account types", err)));
    }

    public postAccount(account: Account): Observable<Account> {
        return this.httpClient
            .post<Account>(api.baseUrl + "accounts", account)
            .pipe(catchError((err) => this.displayError("Error creating account", err)));
    }

    private displayError(header: string, err: any) {
        this.toastr.error(`${err.error.title}: ${err.error.detail}`, header);
        return EMPTY;
    }
}
