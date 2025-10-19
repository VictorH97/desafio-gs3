import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

interface Perfil {
  id: number;
  nome: string;
  descricao: string;
  usuariosVinculados: number;
  permissoes: string[];
  cor: string;
  dataCriacao: Date;
  ativo: boolean;
}

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
    cor: '#3B82F6',
    permissoes: [] as string[]
  };

  permissoesDisponiveis = [
    'Criar usuários',
    'Editar usuários',
    'Excluir usuários',
    'Visualizar relatórios',
    'Gerenciar perfis',
    'Configurações do sistema',
    'Backup de dados',
    'Auditoria'
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

  ngOnInit() {
    this.carregarPerfis();
  }

  carregarPerfis() {
    // Dados mockados
    this.perfis = [
      {
        id: 1,
        nome: 'Administrador',
        descricao: 'Acesso total ao sistema',
        usuariosVinculados: 5,
        permissoes: ['Criar usuários', 'Editar usuários', 'Excluir usuários', 'Visualizar relatórios', 'Gerenciar perfis', 'Configurações do sistema', 'Backup de dados', 'Auditoria'],
        cor: '#EF4444',
        dataCriacao: new Date('2024-01-15'),
        ativo: true
      },
      {
        id: 2,
        nome: 'Gerente',
        descricao: 'Gerenciamento de equipes e relatórios',
        usuariosVinculados: 12,
        permissoes: ['Criar usuários', 'Editar usuários', 'Visualizar relatórios'],
        cor: '#3B82F6',
        dataCriacao: new Date('2024-02-20'),
        ativo: true
      },
      {
        id: 3,
        nome: 'Usuário',
        descricao: 'Acesso básico ao sistema',
        usuariosVinculados: 45,
        permissoes: ['Visualizar relatórios'],
        cor: '#10B981',
        dataCriacao: new Date('2024-03-10'),
        ativo: true
      },
      {
        id: 4,
        nome: 'Visitante',
        descricao: 'Apenas visualização',
        usuariosVinculados: 8,
        permissoes: [],
        cor: '#6B7280',
        dataCriacao: new Date('2024-04-05'),
        ativo: true
      }
    ];
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
      cor: '#3B82F6',
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

  salvarPerfil() {
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
          cor: this.novoPerfil.cor,
          permissoes: [...this.novoPerfil.permissoes]
        };
      }
    } else {
      // Adicionar novo perfil
      const novoPerfil: Perfil = {
        id: Math.max(...this.perfis.map(p => p.id)) + 1,
        nome: this.novoPerfil.nome,
        descricao: this.novoPerfil.descricao,
        usuariosVinculados: 0,
        permissoes: [...this.novoPerfil.permissoes],
        cor: this.novoPerfil.cor,
        dataCriacao: new Date(),
        ativo: true
      };
      this.perfis.push(novoPerfil);
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
      cor: perfil.cor,
      permissoes: [...perfil.permissoes]
    };
    this.showForm = true;
  }

  excluirPerfil(id: number) {
    if (confirm('Tem certeza que deseja excluir este perfil?')) {
      this.perfis = this.perfis.filter(p => p.id !== id);
      this.perfisFiltrados = [...this.perfis];
    }
  }

  toggleAtivo(perfil: Perfil) {
    perfil.ativo = !perfil.ativo;
  }

  getCorClass(cor: string): string {
    return `bg-[${cor}]`;
  }
}
