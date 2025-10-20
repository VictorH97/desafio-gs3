import { Usuario } from "../usuario";

export interface LoginResponse {
    token: Token;
    user: Usuario;
}

export interface Token {
    token: string;
    expiresIn: number;
}