import { Component, inject, signal } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { AuthService } from './shared/services/auth-service';

@Component({
    selector: 'app-root',
    imports: [RouterOutlet],
    templateUrl: './app.html',
    styleUrl: './app.css'
})
export class App {
    private readonly _router = inject(Router);
    private readonly _authService = inject(AuthService);

    ngOnInit(): void {
       this.init();
    }

    async init() {
        if (await this._authService.isAuthenticated()) {
            this._router.navigate(['/usuarios']);
        } else {
            this._router.navigate(['/login']);
        }
    }
}
