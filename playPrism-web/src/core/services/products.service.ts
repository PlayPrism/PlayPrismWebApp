import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { ApiResponse, Product, SearchItem } from '../models';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment.development';
import { Observable, map, of } from 'rxjs';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class ProductsService extends BaseService<Product> {
  private readonly controller = 'products';

  constructor(http: HttpClient, jwtHelper: JwtHelperService, router: Router) {
    super(http, jwtHelper, router);
  }

  public getProducts(id: string): Observable<Product> {
    return this.getById(`${this.baseUrl}/${this.controller}`, id).pipe(
      map((res) => res.data)
    );
  }

  public getSearchableItems(keyword: string): Observable<SearchItem[]> {
    return this.http
      .get<ApiResponse<SearchItem[]>>(
        `${this.baseUrl}/${this.controller}/search?keyword=${keyword}`
      )
      .pipe(map((res) => res.data));
  }
}
