import { Role } from '../enums';

export class Auth {
  userId: string;
  email: string;
  role: Role;
  accessToken: string;
}

export interface RegistrationRequest {
  email: string;
  password: string;
}

export const tokenStorageKey: string = 'auth';
