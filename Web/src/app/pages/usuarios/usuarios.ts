import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink } from "@angular/router";

interface Usuario {
  id: number;
  nome: string;
  perfil: string;
  email: string;
  idade: number;
  sexo: string;
  nacionalidade: string;
  estadoCivil: string;
}

@Component({
  selector: 'app-usuarios',
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './usuarios.html',
  styleUrl: './usuarios.css'
})
export class Usuarios implements OnInit {
  usuarios: Usuario[] = [];
  usuariosFiltrados: Usuario[] = [];
  searchTerm: string = '';
  selectedPerfil: string = 'todos';

  perfis = ['Administrador', 'Gerente', 'Usuário', 'Visitante'];

  ngOnInit() {
    // Dados mockados para demonstração
    this.usuarios = [
      {
        id: 1,
        nome: 'João Silva',
        perfil: 'Administrador',
        email: 'joao.silva@email.com',
        idade: 32,
        sexo: 'Masculino',
        nacionalidade: 'Brasileiro',
        estadoCivil: 'Casado'
      },
      {
        id: 2,
        nome: 'Maria Santos',
        perfil: 'Gerente',
        email: 'maria.santos@email.com',
        idade: 28,
        sexo: 'Feminino',
        nacionalidade: 'Brasileira',
        estadoCivil: 'Solteira'
      },
      {
        id: 3,
        nome: 'Pedro Costa',
        perfil: 'Usuário',
        email: 'pedro.costa@email.com',
        idade: 45,
        sexo: 'Masculino',
        nacionalidade: 'Português',
        estadoCivil: 'Casado'
      },
      {
        id: 4,
        nome: 'Ana Oliveira',
        perfil: 'Gerente',
        email: 'ana.oliveira@email.com',
        idade: 35,
        sexo: 'Feminino',
        nacionalidade: 'Brasileira',
        estadoCivil: 'Divorciada'
      },
      {
        id: 5,
        nome: 'Carlos Ferreira',
        perfil: 'Usuário',
        email: 'carlos.ferreira@email.com',
        idade: 29,
        sexo: 'Masculino',
        nacionalidade: 'Brasileiro',
        estadoCivil: 'Solteiro'
      },
      {
        id: 6,
        nome: 'Juliana Lima',
        perfil: 'Usuário',
        email: 'juliana.lima@email.com',
        idade: 24,
        sexo: 'Feminino',
        nacionalidade: 'Brasileira',
        estadoCivil: 'Solteira'
      },
      {
        id: 7,
        nome: 'Roberto Alves',
        perfil: 'Usuário',
        email: 'roberto.alves@email.com',
        idade: 41,
        sexo: 'Masculino',
        nacionalidade: 'Argentino',
        estadoCivil: 'Casado'
      },
      {
        id: 8,
        nome: 'Fernanda Souza',
        perfil: 'Gerente',
        email: 'fernanda.souza@email.com',
        idade: 38,
        sexo: 'Feminino',
        nacionalidade: 'Brasileira',
        estadoCivil: 'Casada'
      }
    ];
    this.usuariosFiltrados = [...this.usuarios];
  }

  filtrarUsuarios() {
    this.usuariosFiltrados = this.usuarios.filter(usuario => {
      const matchSearch = usuario.nome.toLowerCase().includes(this.searchTerm.toLowerCase()) ||
                         usuario.email.toLowerCase().includes(this.searchTerm.toLowerCase());
      const matchPerfil = this.selectedPerfil === 'todos' || usuario.perfil === this.selectedPerfil;
      return matchSearch && matchPerfil;
    });
  }

  getPerfilClass(perfil: string): string {
    const classes: { [key: string]: string } = {
      'Administrador': 'bg-red-100 text-red-800',
      'Gerente': 'bg-blue-100 text-blue-800',
      'Usuário': 'bg-green-100 text-green-800'
    };
    return classes[perfil] || 'bg-gray-100 text-gray-800';
  }

  editarUsuario(id: number) {
    console.log('Editar usuário:', id);
    // Implementar navegação para edição
  }

  excluirUsuario(id: number) {
    console.log('Excluir usuário:', id);
    // Implementar lógica de exclusão
  }

  adicionarUsuario() {
    console.log('Adicionar novo usuário');
    // Implementar navegação para criação
  }
}
