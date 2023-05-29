import { HttpClient, HttpParams } from '@angular/common/http';
import { JwtHelperService } from '@auth0/angular-jwt';
import {
  BehaviorSubject,
  Observable,
  take,
  switchMap,
  of,
  filter,
  catchError,
  delay,
  tap,
} from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { Auth, tokenStorageKey, ApiResponse } from '../models';
import { Router } from '@angular/router';

export class BaseService<T> {
  public readonly baseUrl = environment.apiUrl;

  private isRefreshing = false;
  private refreshTokenSubject: BehaviorSubject<any> = new BehaviorSubject<any>(
    null
  );

  constructor(
    protected http: HttpClient,
    protected jwtHelper: JwtHelperService,
    protected router: Router
  ) {}

  public get tokenData(): Auth {
    const json = localStorage.getItem(tokenStorageKey);
    return json ? JSON.parse(json) : null;
  }

  public get token(): string {
    return this.tokenData?.accessToken;
  }

  public refreshToken(): Observable<ApiResponse<Auth>> {
    return this.http.post<ApiResponse<Auth>>(
      `${environment.apiUrl}auth/refresh`,
      {}
    );
  }

  refresh(): Observable<string> {
    if (this.token && !this.jwtHelper.isTokenExpired(this.token)) {
      if (!this.isRefreshing) {
        this.isRefreshing = true;
        this.refreshTokenSubject.next(null);

        return this.refreshToken().pipe(
          take(1),
          switchMap((response) => {
            const body = response.data;
            if (!body.accessToken || !body.userId) {
              throw new Error('Refresh failed');
            }

            this.isRefreshing = false;
            localStorage.setItem(tokenStorageKey, JSON.stringify(body));
            this.refreshTokenSubject.next(true);
            return of(body.accessToken);
          }),
          catchError((err) => {
            localStorage.removeItem(tokenStorageKey);
            this.isRefreshing = false;
            this.router.navigate(['/sign-in']);
            throw err;
          })
        );
      } else {
        return this.refreshTokenSubject.pipe(
          filter((token) => token),
          take(1),
          switchMap(() => of(this.token))
        );
      }
    }
    return of(this.token);
  }

  public getAll(url: string, queryParam?: any): Observable<T[]> {
    let params = new HttpParams();
    if (queryParam) {
      Object.keys(queryParam).forEach((key) => {
        params = params.append(key, queryParam[key]);
      });
    }
    return this.http.get<T[]>(`${url}`, { params });
  }

  public getById(url: string, id: string): Observable<ApiResponse<T>> {
    return this.http.get<ApiResponse<T>>(`${url}/Games/${id}`);
  }

  public create(url: string, data: any = null): Observable<ApiResponse<T>> {
    return this.http.post<ApiResponse<T>>(`${url}`, data);
  }

  public update(url: string, id: string, item: T): Observable<T> {
    return this.http.put<T>(`${url}/${id}`, item);
  }

  public delete(url: string, id: string): Observable<any> {
    return this.http.delete(`${url}/${id}`);
  }
}
