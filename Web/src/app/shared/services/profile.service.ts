import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { lastValueFrom } from 'rxjs';
import { Perfil } from '../models/perfil';

@Injectable({providedIn: 'root'})
export class ProfileService {
    private readonly _httpClient = inject(HttpClient);

    private readonly _apiUrl = environment.apiUrl;

    constructor() { }

    async getProfiles(): Promise<Perfil[]> {
        return await lastValueFrom(this._httpClient.get<Perfil[]>(`${this._apiUrl}/profile/all`)) as Perfil[];
    }

    async getProfileById(id: string): Promise<Perfil | null> {
        return await lastValueFrom(this._httpClient.get<Perfil>(`${this._apiUrl}/profile/${id}`)) as Perfil;
    }

    async createProfile(profile: Perfil): Promise<Perfil> {
        return await lastValueFrom(this._httpClient.post<Perfil>(`${this._apiUrl}/profile`, profile)) as Perfil;
    }

    async updateProfile(profile: Perfil): Promise<Perfil> {
        return await lastValueFrom(this._httpClient.put<Perfil>(`${this._apiUrl}/profile`, profile)) as Perfil;
    }

    async deleteProfile(id: string): Promise<void> {
        await lastValueFrom(this._httpClient.delete<void>(`${this._apiUrl}/profile/${id}`));
    }
}