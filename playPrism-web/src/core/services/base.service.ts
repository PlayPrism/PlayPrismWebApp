import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiResponse } from '../models';

export class BaseService<T> {
  constructor(protected http: HttpClient) {}

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
