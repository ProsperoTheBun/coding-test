Based on the specification doc: [Coding Exercise.docx](/Coding Exercise.docx)

The VS Solution is `AAL.Accounts.sln`.

The code to fulfil the specification provided is in `AAL.Accounts` folder, with the Angular 8 frontend in the `ClientApp` subfolder.

Unit and Integration tests are in AAL.Accounts.Test project.

I have created an external class library "Account.Core" for the refactored BalanceChecker.cs. Unfortunately,
I have not had time to integrate the BalanceChecker into the main solution but I would have registered it 
and its dependencies in `Startup.cs` and injected it into the `AccountsService` constructor to be used in the
`CreateAsync` method.
