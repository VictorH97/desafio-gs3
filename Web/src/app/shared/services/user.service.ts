import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { lastValueFrom } from 'rxjs';
import { LoginResponse } from '../models/response/login-response';
import { environment } from '../../../environments/environment';
import { Usuario } from '../models/usuario';
import { UserSessionService } from './user-session-service';

@Injectable({providedIn: 'root'})
export class UserService {
    private readonly _httpClient = inject(HttpClient);
    private readonly _userSessionService = inject(UserSessionService);

    private readonly _apiUrl = environment.apiUrl;

    constructor() { }

    async login(email: string, senha: string): Promise<void> {
        const response = await lastValueFrom(this._httpClient.post(`${this._apiUrl}/user/login`, { email, senha })) as LoginResponse;

        localStorage.setItem('access_token', response.token.token);
        localStorage.setItem('expiresIn', response.token.expiresIn.toString());

        this._userSessionService.setUsuarioLogado(response.user);
    }

    async getUsers(): Promise<Usuario[]> {
        return await lastValueFrom(this._httpClient.get(`${this._apiUrl}/user/all`)) as Usuario[];
    }

    async getUserById(id: string): Promise<Usuario | null> {
        return await lastValueFrom(this._httpClient.get(`${this._apiUrl}/user/${id}`)) as Usuario;
    }

    async createUser(user: Usuario): Promise<Usuario> {
        return await lastValueFrom(this._httpClient.post(`${this._apiUrl}/user`, user)) as Usuario;
    }

    async updateUser(user: Usuario): Promise<Usuario> {
        return await lastValueFrom(this._httpClient.put(`${this._apiUrl}/user`, user)) as Usuario;
    }

    async deleteUser(id: string): Promise<void> {
        await lastValueFrom(this._httpClient.delete(`${this._apiUrl}/user/${id}`));
    }

    async logout(): Promise<void> {
        localStorage.removeItem('access_token');
        localStorage.removeItem('expiresIn');
    }
}