import { Model } from "../common/models/model";


export class Employee extends Model {
  name: string;
  lastName: string;
  email: string;
  password: string;
  address: Address;
  phones: Phone[] = [];
  account: Account;
  cpf: string;
  birthDate: Date;
}

export class Account extends Model {
  branch: string;
  accountNumber: string;
  digit: string;
  bank: Bank;
}

export class Bank extends Model {
  bankId: number;
  bankName: string;
}

export class Address extends Model {
  street: string;
  number: string;
  district: string;
  cep: string;
  city: string;
  state: string;
}

export class Phone extends Model {
  ddd: string;
  phoneNumber: string;
}
