import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { Product } from '../models';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment.development';
import { Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ProductsService extends BaseService<Product> {
  private readonly baseUrl = environment.apiUrl + '/products';

  constructor(http: HttpClient) {
    super(http);
  }

  public get(id: string): Observable<Product> {
    const product: Product = {
      id: 'cc170eb3-22d4-48e8-a190-9f39df127e03',
      title: 'Example Game',
      rating: 4.5,
      price: 49.99,
      description: 'This is an example game description.',
      genres: ['Action', 'Adventure'],
      image: 'https://example.com/game-image.jpg',
      platforms: ['PC', 'PlayStation 5', 'Xbox Series X'],
    };

    return of(product);
    return this.getById(`${this.baseUrl}/`, id.toString());
  }
}
