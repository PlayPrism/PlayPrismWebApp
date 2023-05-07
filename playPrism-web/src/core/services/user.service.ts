import { Injectable } from '@angular/core';
import { Product, UserProfile } from '../models';
import { BaseService } from './base.service';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, catchError, map, of, tap } from 'rxjs';
import { Router } from '@angular/router';
import { Role } from '../enums/role';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root',
})
export class UserService extends BaseService<UserProfile> {
  private readonly userSubject$ = new BehaviorSubject<UserProfile | null>(null);
  public user$ = this.userSubject$.asObservable();

  private readonly baseUrl = environment.apiUrl + 'user';

  constructor(http: HttpClient, private readonly router: Router) {
    super(http);
  }

  public getUser(): Observable<UserProfile> {
    const user: UserProfile = {
      id: 'sldfjsldfjsldfjlsdjf',
      nickname: 'user1',
      firstName: 'David',
      lastName: 'Dzabakhidze',
      email: 'test@gmail.com',
      phone: '+380238402384',
      image:
        'https://ih1.redbubble.net/image.4267646666.3242/mo,small,flatlay,product_square,600x600.jpg',
      role: Role.User,
      country: 'Ukraine',
      city: 'Kiev',
    };
    this.userSubject$.next(user);
    return of(user);
    //TODO: Create get user profile from BE
    return this.http.get<UserProfile>(this.baseUrl).pipe(
      //TODO: Create base response entity for BE and recieve them in FE
      // map((response) => response.data),
      tap((user) => this.userSubject$.next(user)),
      //  catchError(() => of(null)),
      tap((user) => {
        if (!user) {
          localStorage.clear();
          this.clearUser();
          this.router.navigate(['/sign-in']);
        }
      })
    );
  }

  public clearUser(): void {
    this.userSubject$.next(null);
  }
}
