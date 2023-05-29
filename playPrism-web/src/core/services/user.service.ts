import { Injectable } from '@angular/core';
import { ApiResponse, UserProfile } from '../models';
import { BaseService } from './base.service';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, catchError, map, of, tap } from 'rxjs';
import { Router } from '@angular/router';
import { HistoryItem } from '../models/history-item';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root',
})
export class UserService extends BaseService<UserProfile> {
  private readonly userSubject$ = new BehaviorSubject<UserProfile | null>(null);
  private readonly userHistory$ = new BehaviorSubject<HistoryItem | null>(null);
  public user$ = this.userSubject$.asObservable();

  private readonly controller = 'user';

  constructor(http: HttpClient, jwtHelper: JwtHelperService, router: Router) {
    super(http, jwtHelper, router);
  }

  public getUser(): Observable<UserProfile> {
    const userId = this.tokenData.userId;
    return this.http.get<ApiResponse<UserProfile>>(this.baseUrl).pipe(
      map((response) => response.data),
      tap((user) => this.userSubject$.next(user)),
      tap((user) => {
        if (!user) {
          localStorage.clear();
          this.clearUser();
          this.router.navigate(['/sign-in']);
        }
      })
    );
  }

  // public getUserHistory(): Observable<HistoryItem> {
  //   return this.http.get<HistoryItem>(this.baseUrl + '/history').pipe(
  //     map((response) => response),
  //     tap((item) => this.userHistory$.next(item)),
  //   );
  // }

  public getUserHistory(): Observable<HistoryItem[]> {
    return this.http
      .get<HistoryItem[]>(this.baseUrl + '/history')
      .pipe(map((response: any) => response.data));
  }

  public clearUser(): void {
    this.userSubject$.next(null);
  }
}
