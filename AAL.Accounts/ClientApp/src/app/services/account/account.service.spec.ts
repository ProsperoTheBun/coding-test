import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { TestBed } from "@angular/core/testing";
import { ToastrService } from "ngx-toastr";
import { of } from "rxjs";
import { Account } from "src/app/model";

import { AccountService } from "./account.service";

describe("AccountService", () => {
    let mockToastr: { error: jasmine.Spy };
    let mockHttpClient: { get: jasmine.Spy };

    beforeEach(async () => {
        mockHttpClient = jasmine.createSpyObj("HttpClient", ["get"]);
        mockToastr = jasmine.createSpyObj("ToastrService", ["error"]);
        TestBed.configureTestingModule({
            providers: [
                { provide: HttpClient, useValue: mockHttpClient },
                { provide: ToastrService, useValue: mockToastr },
            ],
        });
        await TestBed.compileComponents();
    });

    it("should be created", () => {
        const service: AccountService = TestBed.get(AccountService);
        expect(service).toBeTruthy();
    });

    describe("getAccounts", () => {
        it("should call get endpoint", () => {
            const expectedAccounts: Account[] = [{ Id: "1" }, { Id: "2" }, { Id: "3" }];
            mockHttpClient.get.and.returnValue(of(expectedAccounts));

            const service: AccountService = TestBed.get(AccountService);
            service.getAccounts(null).subscribe((data) => {
                expect(data).toEqual(expectedAccounts, "expected Accounts");
                expect(data.length).toBe(3, "number of accounts");
            });

            expect(mockHttpClient.get.calls.count()).toBe(1, "once only");
        });

        it("should catch and display error", () => {
            const errorResponse = new HttpErrorResponse({
                error: "test 500 error",
                status: 500,
                statusText: "There was a server error",
            });
            mockHttpClient.get.and.returnValue(of(errorResponse));
            mockToastr.error();
            const service: AccountService = TestBed.get(AccountService);
            service.getAccounts(null).subscribe((result: any) => {
                expect(result).toEqual(errorResponse, "got error");
                expect(mockToastr.error).toHaveBeenCalled();
            });
        });
    });
});
