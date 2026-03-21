import { Component } from '@angular/core';
import { FormBuilder, Validators,  ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router, RouterLink } from '@angular/router';
import { ServicesApiService } from '../../api/services-api.service';

@Component({
  selector: 'app-new-service-page',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './new-service-page.component.html',
  styleUrl: './new-service-page.component.css'
})
export class NewServicePageComponent {
 categories = [
    'Mecânico',
    'Pintura',
    'Elétrica',
    'TI',
    'Hidráulica',
    'Limpeza'
  ];

  isSubmitting = false;
  apiError = '';

  form;

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private serviceApi: ServicesApiService
  ) {
    this.form = this.fb.nonNullable.group({
      title: ['', [Validators.required, Validators.minLength(4)]],
      category: ['', Validators.required],
      city: ['', [Validators.required, Validators.minLength(2)]],
      price: [0, [Validators.required, Validators.min(1)]],
      description: ['', [Validators.required, Validators.minLength(10)]]
    });
  }

  submit(): void {
  if (this.form.invalid) {
    this.form.markAllAsTouched();
    return;
  }

  this.isSubmitting = true;

  const formValue = this.form.getRawValue();

  console.log('Payload enviado:', formValue);

  this.serviceApi.create(formValue).subscribe({
    next: (response) => {
      console.log('Serviço criado com sucesso!', response);
      this.apiError = '';
      this.isSubmitting = false;
      this.router.navigate(['/explorar']);
    },
    error: (err) => {
  console.error('Erro ao criar serviço', err);
  this.isSubmitting = false;

  if (err.error?.errors) {
    const errors = err.error.errors;
    this.apiError = Object.values(errors).flat().join(' ');
  } else if (err.error?.message) {
    this.apiError = err.error.message;
  } else {
    this.apiError = 'Erro inesperado ao criar serviço.';
  }
}
  });
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

  get price() {
    return this.form.get('price');
  }

  get description() {
    return this.form.get('description');
  }
}