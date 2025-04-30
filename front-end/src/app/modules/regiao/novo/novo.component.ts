import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CidadeLista } from 'src/app/models/regiao-lista';
import { RegiaoModel } from 'src/app/models/regiao-model';
import { CidadeService } from 'src/app/services/cidade.service';
import { RegiaoService } from 'src/app/services/regiao.service';

@Component({
  selector: 'app-novo',
  templateUrl: './novo.component.html',
  styleUrls: ['./novo.component.scss']
})
export class NovoComponent implements OnInit {

  regiaoForm: FormGroup;
  public lista: Array<CidadeLista>;
  private paramId: string;

  get cidades(): FormArray {
    return this.regiaoForm.get("cidades") as FormArray;
  }

  constructor(
    private fb: FormBuilder,
    private regiaoService: RegiaoService,
    private cidadeService: CidadeService,
    public router: Router,
    private route: ActivatedRoute) {
    this.paramId = this.route.snapshot.paramMap.get('id');

    if (this.paramId) {
      this.buscarRegiaoPorId();
    }

    this.listarCidades();
    this.regiaoForm = this.fb.group({
      id: [null],
      nome: [null, [Validators.required]],
      cidades: this.fb.array([this.criarCidadeFormGroup()]),
    });
  }

  ngOnInit(): void {

  }

  buscarRegiaoPorId() {
    this.regiaoService.buscarPorId(this.paramId).subscribe((data) => {
      this.regiaoForm.patchValue(data);
    });
  }

  listarCidades() {
    this.cidadeService.listar().subscribe((data) => {
      this.lista = data;
    });
  }

  criarCidadeFormGroup() {
    return this.fb.group({
      id: ['', [Validators.required]],
      nome: [null],
    });
  }

  addCidadeFormGroup() {
    let ultimoIndex = this.cidades.length > 0 ? (this.cidades.length - 1) : 0;
    console.log(this.cidades.controls[0].get("id").value);

    if (this.cidades.controls[0].get("id").value == "") {
      console.log('não pode add');
      return;
    }

    this.cidades.push(this.criarCidadeFormGroup())
  }

  removerCidade(index: number) {
    this.cidades.removeAt(index);
  }

  cancelar() {
    this.router.navigate(["/regiao/"])
  }

  salvar() {
    if (this.regiaoForm.invalid) {
      console.log('invalid');
      return;
    }

    var regiaoModel = new RegiaoModel();
    regiaoModel.nome = this.regiaoForm.get("nome").value;
    regiaoModel.cidades = this.cidades.controls.map(p => p.get("id").value);

    this.regiaoService.salvar(regiaoModel).subscribe(() => {
      this.router.navigate(["/regiao/"])
    }, error => {
      
    });
  }

}
