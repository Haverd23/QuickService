import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RegisterInterface } from '../interfaces/register-interface';
import { environment } from '../../../../../environments/environment.prod';

@Injectable({
  providedIn: 'root'
})
export class RegistrationService {

  private url = `${environment.workflow}/Registration`;
  constructor(private http: HttpClient) { }

  register(obj: RegisterInterface): Observable<RegisterInterface> {
    return this.http.post<RegisterInterface>(this.url, obj);
  }
}
