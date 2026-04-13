import { Component } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../../../features/auth/services/auth.service';
import { CommonModule  } from '@angular/common';
import { AuthStoreService } from '../../../features/auth/services/auth.store.service';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [RouterModule, CommonModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent {

  constructor(private authService: AuthService,
    private router: Router,
    private authStore: AuthStoreService
  ) { }
  isLogged = false;

   ngOnInit(): void {
    this.authStore.getIsLogged().subscribe(value => {
      this.isLogged = value;
    });
  }

  logout(): void {
    this.authService.logout();
    this.authStore.clear();
    this.router.navigate(['/login']);
  }
}