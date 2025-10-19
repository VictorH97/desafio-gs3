import { Routes } from '@angular/router';

export const routes: Routes = [
      {
        path: '',
        redirectTo: 'login',
        pathMatch: 'full'
    },
    {
        path: 'login',
        loadComponent: () => import('./pages/login/login').then(m => m.Login)
    },
    {
        path: 'usuarios',
        loadComponent: () => import('./pages/usuarios/usuarios').then(m => m.Usuarios)
    },
    {
        path: 'detalhe-usuario/:id',
        loadComponent: () => import('./pages/detalhe-usuario/detalhe-usuario').then(m => m.DetalheUsuario)
    },
    {
        path: 'perfis',
        loadComponent: () => import('./pages/perfis/perfis').then(m => m.Perfis)
    }
];
