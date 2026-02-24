import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RegisterInterface } from '../interfaces/register-interface';

@Injectable({
  providedIn: 'root'
})
export class RegistrationService {

  private url = "http://localhost:5241/Registration";
  constructor(private http: HttpClient) { }

  register(obj: RegisterInterface): Observable<RegisterInterface> {
    return this.http.post<RegisterInterface>(this.url, obj);
  }
}
