import { Model } from "./models/model";

export class TokenData extends Model {
  name: string;
  role: Roles;
  firstAccess: string;
}

export enum Roles {
  MANAGER,
  EMPLOYEE,
  DELIVERYMAN
}
