import {BaseEntity} from "./base-entity";
import {Role} from "../enums/role";

export interface UserProfile extends BaseEntity {
  nickname: string;
  email: string;
  phone: string;
  password: string;
  image: string;
  role: Role;

  // orders: Order[];
}
