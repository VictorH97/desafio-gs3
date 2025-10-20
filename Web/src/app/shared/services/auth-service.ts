import { inject, Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({providedIn: 'root'})
export class AuthService {
    private readonly jwtHelper = inject(JwtHelperService);

    async isAuthenticated(): Promise<boolean> {
        const token = await localStorage.getItem('access_token');

        return !this.jwtHelper.isTokenExpired(token);
    }
}