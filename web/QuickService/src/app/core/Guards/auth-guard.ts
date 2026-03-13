import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Injectable } from '@angular/core';
import { AuthService } from '../../features/auth/services/auth.service';
@Injectable({ providedIn: 'root' })  

export class AuthGuard implements CanActivate {
  constructor(private auth: AuthService, private router: Router) {}
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    if (this.auth.isLoggin()) {
    
      return true;
    } else {
     
      this.router.navigate(['']);
      return false;
    }
  }
  
    
  }