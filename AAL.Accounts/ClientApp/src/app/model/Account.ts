import { AccountType, Address } from ".";

export class Account {
    public Id?: string;
    public FirstName?: string;
    public LastName?: string;
    public Balance?: number;
    public AccountType?: AccountType;
    public Address?: Address;
}
