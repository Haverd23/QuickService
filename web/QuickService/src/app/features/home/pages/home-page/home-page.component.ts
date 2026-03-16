import { Component, OnInit } from '@angular/core';
import { CardsComponent } from "../../components/cards/cards.component";
import { CategoryComponent } from "../../components/category/category.component";
import { CommonModule } from '@angular/common';
import { AuthService } from '../../../auth/services/auth.service';

@Component({
  selector: 'app-home-page',
  standalone: true,
  imports: [CommonModule, CardsComponent,
    CategoryComponent
  ],
  templateUrl: './home-page.component.html',
  styleUrl: './home-page.component.css'
})
export class HomePageComponent implements OnInit {
constructor(private authService : AuthService) { }
isLogged = false;
categories = [
  { icon: '🔧', title: 'Mecânica' },
  { icon: '🎨', title: 'Pintura' },
  { icon: '💡', title: 'Elétrica' },
  { icon: '💻', title: 'TI' },
  { icon: '🚿', title: 'Hidráulica' },
  { icon: '🧹', title: 'Limpeza' }
];
ngOnInit(): void {
      this.isLogged = this.authService.isLoggin();
    
}
}
