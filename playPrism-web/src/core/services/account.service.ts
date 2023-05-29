import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { Auth, RegistrationRequest } from '../models';
import { BaseService } from './base.service';

export const tokenStorageKey = 'auth';

@Injectable({
  providedIn: 'root',
})
export class AccountService extends BaseService<Auth> {
  private readonly baseUrl = environment.apiUrl + '/account';

  constructor(http: HttpClient) {
    super(http);
  }

  public register(body: RegistrationRequest): Observable<Auth> {
    return this.create(`${this.baseUrl}/register`, body).pipe(
      map((response) => response.data)
    );
  }

  public login(body: RegistrationRequest): Observable<Auth> {
    return this.create(`${this.baseUrl}/login`, body).pipe(
      map((response) => response.data)
    );
  }
}
