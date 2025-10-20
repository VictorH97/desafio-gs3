import { Injectable } from '@angular/core';
import { Usuario } from '../models/usuario';

@Injectable({providedIn: 'root'})
export class UserSessionService {
    private usuarioLogado: Usuario | null = null;

    constructor() { }

    setUsuarioLogado(usuario: Usuario): void {
        this.usuarioLogado = usuario;
    }

    getUsuarioLogado(): Usuario | null {
        return this.usuarioLogado;
    }

    limparSessao(): void {
        this.usuarioLogado = null;
    }
}