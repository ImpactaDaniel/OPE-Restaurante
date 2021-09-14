export class Employee {
  name: string;
  lastName: string;
  email: string;
  password: string;
  address: Address;
  phones: Phone[];
  accounts: Account[];
  banks: Bank[];
}

export class Account {
  branch: string;
  accountNumber: string;
  digit: string;
}

export class Bank {
  bankId: number;
  bankName: string;
}

export class Address {
  street: string;
  number: string;
  district: string;
  cep: string;
  city: string;
  state: string;
}

export class Phone {
  ddd: string;
  phoneNumber: string;
}
