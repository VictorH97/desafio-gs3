import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { UserService } from '../../shared/services/user.service';
import { UserSessionService } from '../../shared/services/user-session-service';
import { Usuario } from '../../shared/models/usuario';

@Component({
  selector: 'app-detalhe-usuario',
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './detalhe-usuario.html',
  styleUrl: './detalhe-usuario.css'
})
export class DetalheUsuario implements OnInit {
  usuarioId: string | null = null;
  isNewUser: boolean = false;
  isLoading: boolean = true;
  
  // Simulando usuário logado
  usuarioLogado!: Usuario;

  perfisDisponiveis = ['Administrador', 'Gerente', 'Usuário', 'Visitante'];
  sexosDisponiveis = ['Masculino', 'Feminino', 'Outro'];
  estadosCivisDisponiveis = ['Solteiro', 'Casado', 'Divorciado', 'Viúvo'];
  
  nacionalidadesDisponiveis = [
    'Brasileiro', 'Português', 'Argentino', 'Americano', 'Canadense',
    'Espanhol', 'Francês', 'Italiano', 'Alemão', 'Inglês', 'Outro'
  ];

  private readonly _userService = inject(UserService);
  private readonly _userSessionService = inject(UserSessionService);
  private readonly route = inject(ActivatedRoute);
  private readonly router = inject(Router);

  ngOnInit() {
    this.usuarioLogado = this._userSessionService.getUsuarioLogado()!;
    this.usuarioId = this.route.snapshot.paramMap.get('id');
    this.isNewUser = this.usuarioId === 'new';

    if (!this.isNewUser && this.usuarioId) {
      this.carregarUsuario(parseInt(this.usuarioId));
    } else {
      this.isLoading = false;
    }
  }

  carregarUsuario(id: number) {
    // Simulação de carregamento de dados
    setTimeout(() => {
      // Dados mockados - substituir por chamada de API
      const usuarios = [
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
          nacionalidade: 'Brasileiro',
          estadoCivil: 'Solteiro'
        }
      ];

      const usuarioEncontrado = usuarios.find(u => u.id === id);
      if (usuarioEncontrado) {
        this.usuario = { ...usuarioEncontrado };
      }
      this.isLoading = false;
    }, 500);
  }

  isAdmin(): boolean {
    return this.usuarioLogado.perfil === 'Administrador';
  }

  canEditPerfil(): boolean {
    return this.isAdmin();
  }

  salvarUsuario() {
    // Validações
    if (!this.usuario.nome || !this.usuario.email) {
      alert('Preencha todos os campos obrigatórios!');
      return;
    }

    if (this.usuario.idade < 18 || this.usuario.idade > 120) {
      alert('Idade deve estar entre 18 e 120 anos!');
      return;
    }

    if (!this.validateEmail(this.usuario.email)) {
      alert('Email inválido!');
      return;
    }

    // Simulação de salvamento
    console.log('Salvando usuário:', this.usuario);
    
    if (this.isNewUser) {
      alert('Usuário criado com sucesso!');
    } else {
      alert('Usuário atualizado com sucesso!');
    }

    // Redirecionar para lista de usuários
    this.router.navigate(['/usuarios']);
  }

  excluirUsuario() {
    if (confirm('Tem certeza que deseja excluir este usuário? Esta ação não pode ser desfeita.')) {
      console.log('Excluindo usuário:', this.usuario.id);
      alert('Usuário excluído com sucesso!');
      this.router.navigate(['/usuarios']);
    }
  }

  cancelar() {
    this.router.navigate(['/usuarios']);
  }

  validateEmail(email: string): boolean {
    const re = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return re.test(email);
  }

  getPerfilColor(perfil: string): string {
    const colors: { [key: string]: string } = {
      'Administrador': '#EF4444',
      'Gerente': '#3B82F6',
      'Usuário': '#10B981',
      'Visitante': '#6B7280'
    };
    return colors[perfil] || '#6B7280';
  }
}
