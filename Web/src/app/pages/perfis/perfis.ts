import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Perfil } from '../../shared/models/perfil';
import { ProfileService } from '../../shared/services/profile.service';

@Component({
  selector: 'app-perfis',
  imports: [CommonModule, FormsModule],
  templateUrl: './perfis.html',
  styleUrl: './perfis.css'
})
export class Perfis implements OnInit {
  perfis: Perfil[] = [];
  perfisFiltrados: Perfil[] = [];
  searchTerm: string = '';
  
  // Formulário de novo perfil
  showForm: boolean = false;
  editingId: number | null = null;
  novoPerfil = {
    nome: '',
    descricao: '',
    permissoes: [] as string[]
  };

  permissoesDisponiveis = [
    'Criar',
    'Ler',
    'Atualizar',
    'Deletar'
  ];

  coresDisponiveis = [
    { nome: 'Azul', valor: '#3B82F6' },
    { nome: 'Verde', valor: '#10B981' },
    { nome: 'Vermelho', valor: '#EF4444' },
    { nome: 'Roxo', valor: '#8B5CF6' },
    { nome: 'Amarelo', valor: '#F59E0B' },
    { nome: 'Rosa', valor: '#EC4899' },
    { nome: 'Cinza', valor: '#6B7280' }
  ];

  private readonly _profileService = inject(ProfileService);

  ngOnInit() {
    this.carregarPerfis();
  }

  async carregarPerfis() {
    // Dados mockados
    this.perfis = await this._profileService.getProfiles();

    this.perfisFiltrados = [...this.perfis];
  }

  filtrarPerfis() {
    this.perfisFiltrados = this.perfis.filter(perfil =>
      perfil.nome.toLowerCase().includes(this.searchTerm.toLowerCase()) ||
      perfil.descricao.toLowerCase().includes(this.searchTerm.toLowerCase())
    );
  }

  toggleForm() {
    this.showForm = !this.showForm;
    if (!this.showForm) {
      this.limparFormulario();
    }
  }

  limparFormulario() {
    this.novoPerfil = {
      nome: '',
      descricao: '',
      permissoes: []
    };
    this.editingId = null;
  }

  togglePermissao(permissao: string) {
    const index = this.novoPerfil.permissoes.indexOf(permissao);
    if (index > -1) {
      this.novoPerfil.permissoes.splice(index, 1);
    } else {
      this.novoPerfil.permissoes.push(permissao);
    }
  }

  temPermissao(permissao: string): boolean {
    return this.novoPerfil.permissoes.includes(permissao);
  }

  async salvarPerfil() {
    if (!this.novoPerfil.nome || !this.novoPerfil.descricao) {
      alert('Preencha todos os campos obrigatórios!');
      return;
    }

    if (this.editingId) {
      // Editar perfil existente
      const index = this.perfis.findIndex(p => p.id === this.editingId);
      if (index > -1) {
        this.perfis[index] = {
          ...this.perfis[index],
          nome: this.novoPerfil.nome,
          descricao: this.novoPerfil.descricao,
          permissoes: this.novoPerfil.permissoes.join(',')
        };

        await this._profileService.updateProfile(this.perfis[index]);
      }
    } else {
      // Adicionar novo perfil
      const novoPerfil: Perfil = {
        id: Math.max(...this.perfis.map(p => p.id)) + 1,
        nome: this.novoPerfil.nome,
        descricao: this.novoPerfil.descricao,
        permissoes: this.novoPerfil.permissoes.join(',')
      };

      this.perfis.push(novoPerfil);

      await this._profileService.createProfile(novoPerfil);
    }

    this.perfisFiltrados = [...this.perfis];
    this.toggleForm();
    this.limparFormulario();
  }

  editarPerfil(perfil: Perfil) {
    this.editingId = perfil.id;
    this.novoPerfil = {
      nome: perfil.nome,
      descricao: perfil.descricao,
      permissoes: perfil.permissoes.split(',')
    };
    this.showForm = true;
  }

  async excluirPerfil(id: number) {
    if (confirm('Tem certeza que deseja excluir este perfil?')) {
      this.perfis = this.perfis.filter(p => p.id !== id);
      this.perfisFiltrados = [...this.perfis];

      await this._profileService.deleteProfile(id);
    }
  }

  getCorClass(cor: string): string {
    return `bg-[${cor}]`;
  }
}
