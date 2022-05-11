import { async, ComponentFixture, TestBed } from "@angular/core/testing";
import { ReactiveFormsModule } from "@angular/forms";
import { Router } from "@angular/router";
import { ToastrService } from "ngx-toastr";
import { AccountService } from "../services/account/account.service";

import { CreateAccountComponent } from "./create-account.component";

describe("CreateAccountComponent", () => {
    let component: CreateAccountComponent;
    let fixture: ComponentFixture<CreateAccountComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            imports: [ReactiveFormsModule],
            providers: [
                {
                    provide: AccountService,
                    useValue: jasmine.createSpy("postAccount"),
                },
                {
                    provide: Router,
                    useValue: jasmine.createSpy("navigate"),
                },
                {
                    provide: ToastrService,
                    useValue: jasmine.createSpy("success"),
                },
            ],
            declarations: [CreateAccountComponent],
        }).compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(CreateAccountComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it("should create", () => {
        expect(component).toBeTruthy();
    });
});
