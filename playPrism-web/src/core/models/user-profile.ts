import { BaseEntity } from './base-entity';
import { Role } from '../enums/role';

export interface UserProfile extends BaseEntity {
  nickname: string;
  firstName: string;
  lastName: string;
  email: string;
  phone: string;
  image: string;
  role: Role;
  country: string;
  city: string;
  // orders: Order[];
}
