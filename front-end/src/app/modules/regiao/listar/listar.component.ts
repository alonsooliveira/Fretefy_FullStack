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
      console.log(this.regioes);
    });
  }

  ativar(regiao: any): void {
    
    this.regiaoService.ativar(regiao.id).subscribe({
      next(success) { console.log('success'); },
      error(erro) { console.error(erro); },
      complete() { regiao.ativo = true; }
    });
  }

  desativar(regiao: any): void {
    this.regiaoService.desativar(regiao.id).subscribe({
      next(success) { console.log('success'); },
      error(erro) { console.error(erro); },
      complete() { regiao.ativo = false; }
    });
  }

  goTo(): void {
    this.router.navigate(["/regiao/novo"])
  }

  editar(id: string) {
    console.log(id);
    this.router.navigate([`/regiao/editar/${id}`])
  }
}
