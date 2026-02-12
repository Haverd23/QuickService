import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-category',
  standalone: true,
  imports: [],
  templateUrl: './category.component.html',
  styleUrl: './category.component.css'
})
export class CategoryComponent {
  @Input({alias: 'icon'}) icon: string = '';
  @Input({alias: 'title'}) title: string = '';

}
