import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { UserService } from '../../shared/services/user.service';

@Component({
  selector: 'app-login',
  imports: [FormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css'
})
export class Login {
  email: string = '';
  password: string = '';
  rememberMe: boolean = false;
  showPassword: boolean = false;

  private readonly _router = inject(Router);
  private readonly _userService = inject(UserService);

  onSubmit() {
    this._userService.login(this.email, this.password).then(() => {
      this._router.navigate(['/usuarios']);
    }).catch(error => {
      alert('Falha no login. Verifique suas credenciais e tente novamente.');
    });
  }

  togglePasswordVisibility() {
    this.showPassword = !this.showPassword;
  }
}
