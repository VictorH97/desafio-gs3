import { Component, inject, signal } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { AuthService } from './shared/services/auth-service';
import { UserSessionService } from './shared/services/user-session-service';
import { lastValueFrom } from 'rxjs';
import { UserService } from './shared/services/user.service';
import { Usuario } from './shared/models/usuario';
import { JwtHelperService } from '@auth0/angular-jwt';

@Component({
    selector: 'app-root',
    imports: [RouterOutlet],
    templateUrl: './app.html',
    styleUrl: './app.css'
})
export class App {
    private readonly _router = inject(Router);
    private readonly _authService = inject(AuthService);
    private readonly _userService = inject(UserService);
    private readonly _userSessionService = inject(UserSessionService);
    private readonly _jwtHelper = inject(JwtHelperService);

    ngOnInit(): void {
       this.init();
    }

    async init() {
        if (await this._authService.isAuthenticated()) {
            const token = await localStorage.getItem('access_token');
            // Supondo que o token contenha o ID do usuário no payload
            const userId = this.parseUserIdFromToken(token!);

            const usuario = await this._userService.getUserById(userId!);
            if (usuario) {
                this._userSessionService.setUsuarioLogado(usuario);
            }

            this._router.navigate(['/usuarios']);
        } else {
            this._router.navigate(['/login']);
        }
    }

    parseUserIdFromToken(token: string): string | null {
        const decodedToken = this._jwtHelper.decodeToken(token);
        return decodedToken ? decodedToken.Id : null;
    }
}
