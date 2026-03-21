import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CreateServiceInterface } from '../interfaces/createServiceInterface';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ServicesApiService {

  private url = "http://localhost:5161";
  constructor(private http: HttpClient) { }

  create(dto: CreateServiceInterface): Observable<void> {
    return this.http.post<void>(`${this.url}/service`, dto);
}
}
