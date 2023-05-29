import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { Auth, RegistrationRequest } from '../models';
import { BaseService } from './base.service';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AccountService extends BaseService<Auth> {
  private readonly controller = environment.apiUrl + 'account';

  constructor(http: HttpClient, jwtHelper: JwtHelperService, router: Router) {
    super(http, jwtHelper, router);
  }

  public register(body: RegistrationRequest): Observable<Auth> {
    return this.create(
      `${this.baseUrl}/${this.controller}/register`,
      body
    ).pipe(map((response) => response.data));
  }

  public login(body: RegistrationRequest): Observable<Auth> {
    return this.create(`${this.baseUrl}/login`, body).pipe(
      map((response) => response.data)
    );
  }
}
