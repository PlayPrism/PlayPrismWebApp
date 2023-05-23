import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { Product } from '../models';
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

  public get(id: string): Observable<Product> {
    return this.getById(`${this.baseUrl}`, id).pipe(map((res) => res.data));
  }
}
