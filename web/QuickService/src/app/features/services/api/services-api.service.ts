import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CreateServiceInterface } from '../interfaces/createServiceInterface';
import { Observable } from 'rxjs';
import { ServiceResponse } from '../interfaces/serviceResponseInterface';
import { environment } from '../../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ServicesApiService {

  private url = `${environment.services}/service`;
  constructor(private http: HttpClient) { }

  create(dto: CreateServiceInterface): Observable<void> {
    return this.http.post<void>(this.url, dto);
}
  getPublicService(): Observable<ServiceResponse[]> {
  return this.http.get<ServiceResponse[]>(`${this.url}`);
}
  getPrivateService(): Observable<ServiceResponse[]>{
    return this.http.get<ServiceResponse[]>(`${this.url}/private`)
  }
}

