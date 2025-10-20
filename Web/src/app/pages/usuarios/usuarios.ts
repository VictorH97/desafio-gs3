import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from "@angular/router";
import { UserService } from '../../shared/services/user.service';
import { Usuario } from '../../shared/models/usuario';
import { ProfileService } from '../../shared/services/profile.service';
import { Perfil } from '../../shared/models/perfil';
import { EnumPerfil } from '../../shared/models/enum-perfil';

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
  selectedPerfil: number = 0;

  perfis: Perfil[] = [];
  EnumPerfil = EnumPerfil;

  private readonly _userService = inject(UserService);
  private readonly _profileService = inject(ProfileService);
  private readonly _router = inject(Router);

  ngOnInit() {
    this.obterUsuarios();
  }

  async obterUsuarios() {
    this.usuarios = await this._userService.getUsers();
    this.perfis = await this._profileService.getProfiles();

    this.usuariosFiltrados = [...this.usuarios];
  }

  filtrarUsuarios() {
    this.usuariosFiltrados = this.usuarios.filter(usuario => {
      const matchSearch = usuario.nome.toLowerCase().includes(this.searchTerm.toLowerCase()) ||
                         usuario.email.toLowerCase().includes(this.searchTerm.toLowerCase());
      const matchPerfil = Number(this.selectedPerfil) === 0 || usuario.perfilId === Number(this.selectedPerfil);
      
      return matchSearch && matchPerfil;
    });
  }

  getPerfilClass(perfilId: number): string {
    const perfil = this.perfis.find(p => p.id === perfilId);

    const classes: { [key: string]: string } = {
      1: 'bg-red-100 text-red-800',
      2: 'bg-green-100 text-green-800',
      3: 'bg-blue-100 text-blue-800',
    };
    return classes[perfilId] || 'bg-gray-100 text-gray-800';
  }

  editarUsuario(id: string) {
    this._router.navigate(['/detalhe-usuario', id]);
  }

  excluirUsuario(id: string) {
    if (confirm('Tem certeza que deseja excluir este usuário?')) {
      this._userService.deleteUser(id).then(() => {
        this.obterUsuarios();
      });
    }
  }

  adicionarUsuario() {
    this._router.navigate(['/detalhe-usuario', 'new']);
  }
}
