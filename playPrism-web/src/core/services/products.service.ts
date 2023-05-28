import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { ApiResponse, Product, SearchItem } from '../models';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment.development';
import { Observable, map, of } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ProductsService extends BaseService<Product> {
  private readonly baseUrl = environment.apiUrl + '/products';

  constructor(http: HttpClient) {
    super(http);
  }

  public getProducts(id: string): Observable<Product> {
    return this.getById(`${this.baseUrl}`, id).pipe(map((res) => res.data));
  }

  public getSearchableItems(keyword: string): Observable<SearchItem[]> {
    return this.http
      .get<ApiResponse<SearchItem[]>>(
        `${this.baseUrl}/search?keyword=${keyword}`
      )
      .pipe(map((res) => res.data));
  }
}
