import { AfterViewInit, Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { RegiaoLista } from 'src/app/models/regiao-lista';
import { RegiaoModel } from 'src/app/models/regiao-model';
import { RegiaoService } from 'src/app/services/regiao.service';

@Component({
  selector: 'app-listar',
  templateUrl: './listar.component.html',
  styleUrls: ['./listar.component.scss']
})
export class ListarComponent implements OnInit {

  regioes: any[] = [];

  constructor(
    private regiaoService: RegiaoService,
    private router: Router) {
    this.listar();
  }

  ngAfterViewInit(): void {

  }

  ngOnInit(): void {

  }

  listar() {
    this.regiaoService.listar().subscribe((data) => {
      this.regioes = data;
    });
  }

  ativar(regiao: any): void {

    this.regiaoService.ativar(regiao.id).subscribe({
      next(success) { },
      error(erro) { console.error(erro); },
      complete() { regiao.ativo = true; }
    });
  }

  desativar(regiao: any): void {
    this.regiaoService.desativar(regiao.id).subscribe({
      next(success) { },
      error(erro) { console.error(erro); },
      complete() { regiao.ativo = false; }
    });
  }

  goTo(): void {
    this.router.navigate(["/regiao/novo"])
  }

  editar(id: string) {
    this.router.navigate([`/regiao/novo/${id}`])
  }

  exportar() {
    this.regiaoService.exportar();
  }
}
