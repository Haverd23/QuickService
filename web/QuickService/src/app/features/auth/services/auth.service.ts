import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private url = "http://localhost:5298/Auth"
  constructor(private http: HttpClient) { }

  checkEmailExists(email: string): Observable<{ exists: boolean }> {
    return this.http.get<{ exists: boolean }>(`${this.url}/email-exists`, {
      params: { email }
    });
  }

}
  

  

