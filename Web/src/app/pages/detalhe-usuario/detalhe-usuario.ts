import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { UserService } from '../../shared/services/user.service';
import { UserSessionService } from '../../shared/services/user-session-service';
import { Usuario } from '../../shared/models/usuario';
import { ProfileService } from '../../shared/services/profile.service';
import { lastValueFrom } from 'rxjs';
import { Perfil } from '../../shared/models/perfil';
import { EnumPerfil } from '../../shared/models/enum-perfil';

@Component({
    selector: 'app-detalhe-usuario',
    imports: [CommonModule, FormsModule, RouterLink],
    templateUrl: './detalhe-usuario.html',
    styleUrl: './detalhe-usuario.css'
})
export class DetalheUsuario implements OnInit {
    usuario!: Usuario;
    usuarioId: string | null = null;
    isNewUser: boolean = false;
    isLoading: boolean = true;

    usuarioLogado!: Usuario;

    perfisDisponiveis: Perfil[] = [];
    sexosDisponiveis = ['Masculino', 'Feminino'];
    estadosCivisDisponiveis = ['Solteiro', 'Casado', 'Viúvo'];

    nacionalidadesDisponiveis = [
        'Brasileiro', 'Português', 'Argentino', 'Americano', 'Canadense',
        'Espanhol', 'Francês', 'Italiano', 'Alemão', 'Inglês', 'Outro'
    ];

    private readonly _userService = inject(UserService);
    private readonly _perfilService = inject(ProfileService);
    private readonly _userSessionService = inject(UserSessionService);
    private readonly route = inject(ActivatedRoute);
    private readonly router = inject(Router);

    ngOnInit() {
        this.init();
    }

    async init() {
        this.usuarioLogado = this._userSessionService.getUsuarioLogado()!;
        this.perfisDisponiveis = await this._perfilService.getProfiles();
        this.usuarioId = this.route.snapshot.paramMap.get('id');
        this.isNewUser = this.usuarioId === 'new';

        if (!this.isNewUser && this.usuarioId) {
            this.carregarUsuario(this.usuarioId);
        } else {
            this.usuario = {
                id: '',
                nome: '',
                email: '',
                idade: 18,
                sexo: '',
                estadoCivil: '',
                nacionalidade: '',
                perfilId: EnumPerfil.USUARIO,
                perfil: this.perfisDisponiveis.find(p => p.id === EnumPerfil.USUARIO)!
            };

            this.isLoading = false;
        }
    }

    async carregarUsuario(id: string) {
        this.usuario = (await this._userService.getUserById(id))!;

        this.isLoading = false;
    }

    isAdmin(): boolean {
        return this.usuarioLogado.perfilId === EnumPerfil.ADMINISTRADOR;
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

        if (this.usuario.idade < 18 || this.usuario.idade > 80) {
            alert('Idade deve estar entre 18 e 80 anos!');
            return;
        }

        if (!this.validateEmail(this.usuario.email)) {
            alert('Email inválido!');
            return;
        }

        if (this.isNewUser) {
            this._userService.createUser(this.usuario)
                .then(() =>{
                    alert('Usuário criado com sucesso!');
                })
                .catch(() => {
                    alert('Erro ao criar usuário.');
                });
        } else {
            this._userService.updateUser(this.usuario)
                .then(() =>{
                    alert('Usuário atualizado com sucesso!');
                })
                .catch(() => {
                    alert('Erro ao atualizar usuário.');
                });
        }

        // Redirecionar para lista de usuários
        this.router.navigate(['/usuarios']);
    }

    excluirUsuario() {
        if (confirm('Tem certeza que deseja excluir este usuário? Esta ação não pode ser desfeita.')) {
            this._userService.deleteUser(this.usuario.id)
                .then(() =>{
                    alert('Usuário excluído com sucesso!');
                    this.router.navigate(['/usuarios']);
                })
                .catch(() => {
                    alert('Erro ao excluir usuário.');
                });
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
