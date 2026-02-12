import { Component, Input, input } from '@angular/core';

@Component({
  selector: 'app-cards',
  standalone: true,
  imports: [],
  templateUrl: './cards.component.html',
  styleUrl: './cards.component.css'
})
export class CardsComponent {
  @Input({alias: 'cardTitle',required: true}) title: string = '';
  @Input({alias: 'cardDescription', required: true}) description: string = ''


}
