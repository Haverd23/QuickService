import { Component } from '@angular/core';
import { CardsComponent } from "../../components/cards/cards.component";
import { CategoryComponent } from "../../components/category/category.component";

@Component({
  selector: 'app-home-page',
  standalone: true,
  imports: [CardsComponent, CategoryComponent],
  templateUrl: './home-page.component.html',
  styleUrl: './home-page.component.css'
})
export class HomePageComponent {

}
