import { Component } from '@angular/core';
import { FormBuilder, Validators,  ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-new-service-page',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './new-service-page.component.html',
  styleUrl: './new-service-page.component.css'
})
export class NewServicePageComponent {
 categories = [
    'Mecânica',
    'Pintura',
    'Elétrica',
    'TI',
    'Hidráulica',
    'Limpeza'
  ];

  isSubmitting = false;
  form;

  constructor(
    private fb: FormBuilder,
    private router: Router
  ) {
    this.form = this.fb.group({
      title: ['', [Validators.required, Validators.minLength(4)]],
      category: ['', Validators.required],
      city: ['', [Validators.required, Validators.minLength(2)]],
      price: [''],
      phone: [''],
      description: ['', [Validators.required, Validators.minLength(10)]]
    });
  }

  submit(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.isSubmitting = true;

    const newService = {
      ...this.form.value,
      createdAt: 'Hoje'
    };

    console.log('Serviço criado:', newService);

    setTimeout(() => {
      this.isSubmitting = false;
      this.router.navigate(['/explorar']);
    }, 500);
  }

  get title() {
    return this.form.get('title');
  }

  get category() {
    return this.form.get('category');
  }

  get city() {
    return this.form.get('city');
  }

  get description() {
    return this.form.get('description');
  }
}