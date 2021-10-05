import { Employee } from "../funcionario/employee";

export class Deliveryman extends Employee{
  motorcycle: Motorcycle;
}

export class Motorcycle {
  model: string;
  brand: string;
  year: number;
}
