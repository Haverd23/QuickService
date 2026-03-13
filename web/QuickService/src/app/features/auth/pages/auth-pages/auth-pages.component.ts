import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, AbstractControl, ValidationErrors } from '@angular/forms';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { RegistrationService } from '../../../../core/workflows/registration/service/registration.service';
import { AuthService } from '../../services/auth.service';

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

  apiError: string = '';
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

  displayPhone: string = ''; // apenas para exibição no input

  constructor(
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private registrationService: RegistrationService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    const type = this.route.snapshot.data['type'];

    if (type === 'login') this.setupLogin();
    else this.setupRegister();

    this.form = this.fb.group({}, { validators: this.passwordMatchValidator });

    this.fields.forEach(field => {
      this.form.addControl(
        field.name,
        this.fb.control('', this.getValidators(field.name))
      );
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
  if (this.form.invalid) {
    this.form.markAllAsTouched();
    return;
  }

  const type = this.route.snapshot.data['type'];

  if (type === 'register') {
    const formValue = this.form.value;

    this.authService.checkEmailExists(formValue.email).subscribe({
      next: (response: any) => {

        if (response.exists) {
          this.apiError = 'Este e-mail já está registrado.';
          return;
        }

        this.registrationService.register(formValue).subscribe({
          next: (response) => {
            console.log('Usuário registrado com sucesso!', response);
            this.apiError = '';
          },
          error: (err) => {
            console.error('Erro ao registrar', err);

            if (err.error?.message) {
              this.apiError = err.error.message;
            } else {
              this.apiError = 'Erro inesperado ao registrar usuário.';
            }
          }
        });

      },
      error: (err) => {
        console.error('Erro ao verificar email', err);
        this.apiError = 'Erro ao verificar e-mail.';
      }
    });
  }
  else{
    const formValue = this.form.value;

    this.authService.login(formValue).subscribe({
      next: (response) => {
        this.authService.storeToken(response.token);
        console.log('Login bem-sucedido!', response);
        this.apiError = '';
      },
      error: (err) => {
        console.error('Erro ao fazer login', err);
        this.apiError = 'Erro ao fazer login.';
      }
    });

  }
}


  // -------------------------
  // VALIDADOR POR CAMPO
  // -------------------------
  getValidators(field: string) {
    switch (field) {
      case 'name':
        return [Validators.required, Validators.minLength(3)];
      case 'email':
        return [Validators.required, Validators.email];
      case 'phone':
        return [Validators.required, Validators.pattern(/^(\d{10,11})$/)];
      case 'password':
        return [
          Validators.required,
          Validators.minLength(6),
          Validators.pattern(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$/)
        ];
      case 'confirmPassword':
        return [Validators.required];
      default:
        return [Validators.required];
    }
  }

  // -------------------------
  // VALIDADOR DE CONFIRMAÇÃO DE SENHA
  // -------------------------
  passwordMatchValidator(form: AbstractControl): ValidationErrors | null {
    const password = form.get('password')?.value;
    const confirm = form.get('confirmPassword')?.value;
    if (!password || !confirm) return null;

    if (password !== confirm) {
      form.get('confirmPassword')?.setErrors({ passwordMismatch: true });
      return { passwordMismatch: true };
    }
    return null;
  }

  // -------------------------
  // FORMATAÇÃO DO TELEFONE PARA EXIBIÇÃO
  // -------------------------
  onPhoneInput(event: any) {
    let rawValue = event.target.value.replace(/\D/g, '');
    if (rawValue.length > 11) rawValue = rawValue.slice(0, 11);

    let display = rawValue;
    if (rawValue.length > 10) {
      display = rawValue.replace(/^(\d{2})(\d{5})(\d{4})$/, '($1) $2-$3');
    } else if (rawValue.length > 9) {
      display = rawValue.replace(/^(\d{2})(\d{4})(\d{4})$/, '($1) $2-$3');
    } else if (rawValue.length > 2) {
      display = rawValue.replace(/^(\d{2})(\d+)/, '($1) $2');
    }

    this.displayPhone = display;
    this.form.get('phone')?.setValue(rawValue, { emitEvent: false });
  }
}