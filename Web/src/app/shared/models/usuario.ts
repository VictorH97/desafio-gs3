import { Perfil } from "./perfil";

export interface Usuario {
    id: string;
    nome: string;
    email: string;
    perfilId: number;
    idade: number;
    sexo: string;
    nacionalidade: string;
    estadoCivil: string;
    perfil: Perfil;
}