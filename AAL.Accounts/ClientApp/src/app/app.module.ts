import { HttpClientModule } from "@angular/common/http";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { BrowserModule } from "@angular/platform-browser";
import { RouterModule } from "@angular/router";

import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { ToastrModule } from "ngx-toastr";

import { AccountTypeComponent } from "./account-type/account-type.component";
import { AppComponent } from "./app.component";
import { CreateAccountComponent } from "./create-account/create-account.component";
import { HomeComponent } from "./home/home.component";
import { NavMenuComponent } from "./nav-menu/nav-menu.component";

@NgModule({
    declarations: [AppComponent, NavMenuComponent, HomeComponent, CreateAccountComponent, AccountTypeComponent],
    imports: [
        BrowserModule.withServerTransition({ appId: "ng-cli-universal" }),
        HttpClientModule,
        FormsModule,
        ReactiveFormsModule,
        RouterModule.forRoot([
            { path: "", component: HomeComponent, pathMatch: "full" },
            { path: "create-account", component: CreateAccountComponent },
        ]),
        BrowserAnimationsModule, // required animations module
        ToastrModule.forRoot(), // ToastrModule added
    ],
    providers: [],
    bootstrap: [AppComponent],
})
export class AppModule {}
