export class Employee {
  name: string;
  lastName: string;
  email: string;
  password: string;
  address: Address;
  phones: Phone[];
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
