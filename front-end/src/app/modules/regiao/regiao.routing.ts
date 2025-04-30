import { RouterModule, Routes } from '@angular/router';
import { RegiaoComponent } from './regiao.component';
import { NovoComponent } from './novo/novo.component';
import { ListarComponent } from './listar/listar.component';

const routes: Routes = [
  { 
    path: '',
    component: ListarComponent
  },
  { path: 'novo', component: NovoComponent },
  { path: 'editar/:id', component: NovoComponent },
  
];

export const  RegiaoRoutingModule = RouterModule.forChild(routes);