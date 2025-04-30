import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RegiaoComponent } from './regiao.component';
import { RegiaoRoutingModule } from './regiao.routing';
import { ListarComponent } from './listar/listar.component';
import { NovoComponent } from './novo/novo.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
  imports: [
    CommonModule,
    RegiaoRoutingModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  declarations: [RegiaoComponent, ListarComponent, NovoComponent],
  exports: [RegiaoComponent, ListarComponent, NovoComponent],
})
export class RegiaoModule { }
