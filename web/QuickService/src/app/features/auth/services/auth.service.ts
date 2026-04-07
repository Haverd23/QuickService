import { inject, Inject, Injectable, PLATFORM_ID } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { isPlatformBrowser } from '@angular/common';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { JwtHelperService } from '@auth0/angular-jwt';

import { LoginInterface } from '../interfaces/loginInterface';
import { LoginResponse } from '../interfaces/loginResponse';
import { environment } from '../../../../environments/environment.prod';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private url = `${environment.auth}/Auth`;
  private userPayload: any;
  private platformId = inject(PLATFORM_ID);

  constructor(
  private http: HttpClient,
  private router: Router,
) {}

  private isBrowser(): boolean {
    return isPlatformBrowser(this.platformId);
  }

  checkEmailExists(email: string): Observable<{ exists: boolean }> {
    return this.http.get<{ exists: boolean }>(`${this.url}/email-exists`, {
      params: { email }
    });
  }

  login(obj: LoginInterface): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(`${this.url}/login`, obj);
  }

  storeToken(tokenValue: string): void {
    if (!this.isBrowser()) return;

    localStorage.setItem('token', tokenValue);
    this.userPayload = this.decodeToken(tokenValue);
  }

  getToken(): string | null {
    if (!this.isBrowser()) return null;

    return localStorage.getItem('token');
  }

  isLoggin(): boolean {
    if (!this.isBrowser()) return false;

    return !!localStorage.getItem('token');
  }

  logout(): void {
    if (this.isBrowser()) {
      localStorage.clear();
    }

    this.userPayload = null;
    this.router.navigate(['/login']);
  }

  decodedToken() {
    const token = this.getToken();

    if (token) {
      const jwtHelper = new JwtHelperService();
      return jwtHelper.decodeToken(token);
    }

    return null;
  }

  decodeToken(token: string): any {
    const jwtHelper = new JwtHelperService();
    return jwtHelper.decodeToken(token);
  }
}