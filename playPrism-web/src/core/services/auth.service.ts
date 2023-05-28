import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import {
  Product,
  SearchItem,
  ApiResponse,
  Auth,
  RegistrationRequest,
} from '../models';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root',
})
export class AuthService extends BaseService<Auth> {
  private readonly baseUrl = environment.apiUrl + '/account';

  constructor(http: HttpClient) {
    super(http);
  }

  public register(body: RegistrationRequest): Observable<Auth> {
    return this.http.post<Auth>(`${this.baseUrl}/register`, body);
  }
}
