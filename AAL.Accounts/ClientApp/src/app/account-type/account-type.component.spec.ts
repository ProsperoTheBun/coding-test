import { async, ComponentFixture, TestBed } from "@angular/core/testing";
import { FormControl, NgForm, ReactiveFormsModule } from "@angular/forms";
import { By } from "@angular/platform-browser";
import { Observable, of, Subject, SubscriptionLike } from "rxjs";
import { AccountType } from "../model";
import { AccountTypeComponent } from "./account-type.component";

describe("AccountTypeComponent", () => {
    let component: AccountTypeComponent;
    let fixture: ComponentFixture<AccountTypeComponent>;
    //  let mockSelectionSubject: { next: jasmine.Spy };

    beforeEach(async () => {
        //
        TestBed.configureTestingModule({
            imports: [ReactiveFormsModule],
            declarations: [AccountTypeComponent],
        });
        await TestBed.compileComponents();
    });

    beforeEach(() => {
        fixture = TestBed.createComponent(AccountTypeComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it("should create", () => {
        expect(component).toBeTruthy();
    });

    it("should put account type onto Subject when changed", () => {
        // Arrange
        const accountType: AccountType = {
            Id: "xxx",
            Name: "TestType",
        };
        component.currentSelection$ = jasmine.createSpyObj("Subject<AccountType>", ["next"]);

        // Act
        component.selectedAccountType.setValue(accountType);
        component.onChange();

        // Assert
        expect(component.currentSelection$.next).toHaveBeenCalledWith(accountType);
    });
});
