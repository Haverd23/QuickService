import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { LoginInterface } from '../interfaces/loginInterface';
import { LoginResponse } from '../interfaces/loginResponse';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Router } from '@angular/router';



@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private url = "http://localhost:5298/Auth"
  private userPayload: any;

  constructor(private http: HttpClient,
    private router: Router
  ) { }

  checkEmailExists(email: string): Observable<{ exists: boolean }> {
    return this.http.get<{ exists: boolean }>(`${this.url}/email-exists`, {
      params: { email }
    });}

    login(obj: LoginInterface): Observable<LoginResponse> {
      return this.http.post<LoginResponse>(`${this.url}/login`, obj);
    }

  
  storeToken(tokenValue: string): void{
    localStorage.setItem('token', tokenValue);
    this.userPayload = this.decodeToken(tokenValue);
    
  }


  getToken(): string|null{
    return localStorage.getItem('token');
  }


  isLoggin(): boolean{
    return  localStorage.getItem('token') ? true :  false;
  }

  logout(): void{
    localStorage.clear();
    this.userPayload = null;
    this.router.navigate(['/login'])
    
  }

  decodedToken(){
    const token = this.getToken();
    if(token){
      const jwtHelper = new JwtHelperService();
      return jwtHelper.decodeToken(token);
    }
    return null;
  }

  decodeToken(token:string): any{
    const jwtHelper = new JwtHelperService();
    return jwtHelper.decodeToken(token);

  }
}

  

  

