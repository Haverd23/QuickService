import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms'; 
import { CommonModule } from '@angular/common';
import { RegistrationService } from '../../../../core/workflows/registration/service/registration.service';

interface FormField {
  type: string;
  name: string;
  label: string;
}
@Component({
  selector: 'app-auth-pages',
   standalone: true, 
  imports: [ReactiveFormsModule, RouterLink, CommonModule],
  
  templateUrl: './auth-pages.component.html',
  styleUrl: './auth-pages.component.css'
})
export class AuthPageComponent implements OnInit {

  form!: FormGroup;

  heroHtml = '';
  heading = '';
  subtitle = '';
  buttonText = '';
  imageSrc = '';
  fields: FormField[] = [];
  linkText = '';
  linkButtonText = '';
  linkUrl = '';

  constructor(
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private registrationService: RegistrationService
  ) {}

  ngOnInit(): void {
    const type = this.route.snapshot.data['type'];

    if (type === 'login') this.setupLogin();
    else this.setupRegister();

    this.form = this.fb.group({});
    this.fields.forEach(field => {
      this.form.addControl(field.name, this.fb.control('', Validators.required));
    });
  }

  setupLogin() {
    this.heroHtml = 'Onde o <br><span>serviço</span> <br>te encontra.';
    this.heading = 'Entrar';
    this.subtitle = 'Use suas credenciais para acessar o painel.';
    this.buttonText = 'Continuar';
    this.imageSrc = 'assets/background.png';

    this.fields = [
      { type: 'email', name: 'email', label: 'E-mail' },
      { type: 'password', name: 'password', label: 'Senha' }
    ];

    this.linkText = 'Ainda não possui uma conta?';
    this.linkButtonText = 'Junte-se a nós';
    this.linkUrl = '/register';
  }

  setupRegister() {
    this.heroHtml = 'Comece sua <br><span>jornada</span> <br>hoje.';
    this.heading = 'Criar Conta';
    this.subtitle = 'Preencha os dados abaixo para começar.';
    this.buttonText = 'Criar conta';
    this.imageSrc = 'assets/background2.png';

    this.fields = [
      { type: 'text', name: 'name', label: 'Nome completo' },
      { type: 'email', name: 'email', label: 'E-mail' },
      { type: 'tel', name: 'phone', label: 'Telefone' },
      { type: 'password', name: 'password', label: 'Senha' },
      { type: 'password', name: 'confirmPassword', label: 'Confirmar senha' }
    ];

    this.linkText = 'Já possui uma conta?';
    this.linkButtonText = 'Entrar';
    this.linkUrl = '/login';
  }

  submit() {
  if (this.form.invalid) return;

  const type = this.route.snapshot.data['type'];

  if (type === 'register') {
    const formValue = this.form.value;

    this.registrationService.register(formValue).subscribe({
      next: (response) => {
        console.log('Usuário registrado com sucesso!', response);
      },
      error: (err) => {
        console.error('Erro ao registrar', err);
      }
    });
  } else {
    console.log('Login:', this.form.value);
  }
}
}