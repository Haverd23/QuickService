import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  Validators,
  AbstractControl,
  ValidationErrors,
  ReactiveFormsModule
} from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { RegistrationService } from '../../../../core/workflows/registration/service/registration.service';
import { AuthService } from '../../services/auth.service';
import { AuthStoreService } from '../../services/auth.store.service';

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

  displayPhone: string = '';

  constructor(
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private registrationService: RegistrationService,
    private authService: AuthService,
    private router: Router,
    private authStore: AuthStoreService
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
    const formValue = this.form.value;

    if (type === 'register') {
      this.authService.checkEmailExists(formValue.email).subscribe({
        next: (response: any) => {
          if (response.exists) {
            this.apiError = 'Este e-mail já está registrado.';
            return;
          }

          this.registrationService.register(formValue).subscribe({
            next: () => {
              this.apiError = '';

              const loginData = {
                email: formValue.email,
                password: formValue.password
              };

              setTimeout(() => {
                this.authService.login(loginData).subscribe({
                  next: (loginResponse: any) => {
                    const token =
                      loginResponse?.token ??
                      loginResponse?.Token ??
                      loginResponse?.accessToken ??
                      loginResponse?.jwt;

                    if (!token) {
                      this.apiError = 'Login retornou sucesso, mas sem token.';
                      return;
                    }

                    this.authService.storeToken(token);
                    this.authStore.setIsLogged(true);
                    this.authStore.setEmail(formValue.email);
                    this.apiError = '';
                    this.router.navigate(['/explorar']);
                  },
                  error: (err) => {
                    this.apiError =
                      err?.error?.message ??
                      'Usuário criado com sucesso, mas houve erro no login automático.';
                  }
                });
              }, 1500);
            },
            error: (err) => {
              if (err.error?.message) {
                this.apiError = err.error.message;
              } else {
                this.apiError = 'Erro inesperado ao registrar usuário.';
              }
            }
          });
        },
        error: () => {
          this.apiError = 'Erro ao verificar e-mail.';
        }
      });
    } else {
      const loginData = {
        email: formValue.email,
        password: formValue.password
      };

      this.authService.login(loginData).subscribe({
        next: (response: any) => {
          const token =
            response?.token ??
            response?.Token ??
            response?.accessToken ??
            response?.jwt;

          if (!token) {
            this.apiError = 'Login retornou sucesso, mas sem token.';
            return;
          }

          this.authService.storeToken(token);
          this.authStore.setIsLogged(true);
          this.authStore.setEmail(formValue.email);
          this.apiError = '';
          this.router.navigate(['/explorar']);
        },
        error: (err) => {
          this.apiError = err?.error?.message ?? 'Erro ao fazer login.';
        }
      });
    }
  }

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