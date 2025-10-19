import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-login',
  imports: [FormsModule, RouterLink],
  templateUrl: './login.html',
  styleUrl: './login.css'
})
export class Login {
  email: string = '';
  password: string = '';
  rememberMe: boolean = false;
  showPassword: boolean = false;

  constructor(private router: Router) {}

  onSubmit() {
    console.log('Login attempt:', { email: this.email, password: this.password, rememberMe: this.rememberMe });
    // Aqui você pode adicionar a lógica de autenticação
  }

  togglePasswordVisibility() {
    this.showPassword = !this.showPassword;
  }
}
