import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { Product } from '../models';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment.development';
import { Observable, of } from 'rxjs';
import { Platform } from '../enums';

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
      name: 'Example Game',
      rating: 4.5,
      price: 49.99,
      shortDescription: 'This is an example game description.',
      detailedDescription: 'This is an example game description.',
      releaseDate: new Date(),
      genres: ['Action', 'Adventure', 'Sport', 'RPG'],
      headerImage:
        'https://3dnews.ru/assets/external/illustrations/2023/02/06/1081456/Cyberpunk2077NG_Cover_art_RGB-en.jpg',
      platforms: [
        Platform.Steam,
        Platform.Xbox,
        Platform.PlayStation,
        Platform.EpicGames,
      ],
    };

    return of(product);
    return this.getById(`${this.baseUrl}/`, id.toString());
  }
}
