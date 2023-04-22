import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.development';


export class BaseService<T> {
  private readonly apiUrl: string = environment.apiUrl;

  constructor(
    protected http: HttpClient,
  ) { }

  public getAll(queryParam?: any): Observable<T[]> {
    let params = new HttpParams();
    if (queryParam) {
      Object.keys(queryParam).forEach(key => {
        params = params.append(key, queryParam[key]);
      });
    }
    return this.http.get<T[]>(`${this.apiUrl}/${this.apiUrl}`, { params });
  }

  public getById(id: string): Observable<T> {
    return this.http.get<T>(`${this.apiUrl}/${this.apiUrl}/${id}`);
  }

  public create(item: T): Observable<T> {
    return this.http.post<T>(`${this.apiUrl}/${this.apiUrl}`, item);
  }

  public update(id: string, item: T): Observable<T> {
    return this.http.put<T>(`${this.apiUrl}/${this.apiUrl}/${id}`, item);
  }

  public delete(id: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${this.apiUrl}/${id}`);
  }
}
