import { Model } from './models/model';
export class ChangePasswordModel extends Model {
  oldPassword: string;
  newPassword: string;
}
