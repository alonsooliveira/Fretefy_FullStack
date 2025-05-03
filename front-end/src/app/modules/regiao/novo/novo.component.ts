import { AfterViewInit, Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CidadeLista, RegiaoLista } from 'src/app/models/regiao-lista';
import { RegiaoModel } from 'src/app/models/regiao-model';
import { CidadeService } from 'src/app/services/cidade.service';
import { RegiaoService } from 'src/app/services/regiao.service';

@Component({
  selector: 'app-novo',
  templateUrl: './novo.component.html',
  styleUrls: ['./novo.component.scss']
})
export class NovoComponent implements OnInit, AfterViewInit {

  regiaoForm: FormGroup;
  private paramId: string;
  errosAoSalvar: Array<string> = [];
  erroAoAddCidade: string;

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
    
    this.regiaoForm = this.fb.group({
      id: [null],
      nome: [null, [Validators.required]],
      cidades: this.fb.array([this.criarCidadeFormGroup(new CidadeLista())]),
    });
  }
  ngAfterViewInit(): void {
    if (this.paramId) {
      this.buscarRegiaoPorId();
    }
  }

  ngOnInit(): void {
    
  }

  buscarRegiaoPorId() {
    var regiao: RegiaoLista;
    this.regiaoService.buscarPorId(this.paramId).subscribe((data) => {
      this.cidades.clear();
      data.cidades.forEach(cidade => {
        this.cidades.push(this.criarCidadeFormGroup(cidade))        
      });
      this.regiaoForm.patchValue(data);
      
    });
  }
  criarCidadeFormGroup(cidade: CidadeLista) {
    return this.fb.group({
      id: [cidade.id, [Validators.required]],
      nome: [cidade.nome],
    });
  }

  addCidadeFormGroup() {
    this.erroAoAddCidade = "";
    let ultimoIndex = this.cidades.length > 0 ? (this.cidades.length - 1) : 0;

    if (this.cidades.controls[0].get("id").value == "") {
      this.erroAoAddCidade = "Selecione uma cidade antes de adicionar";
      return;
    }

    this.cidades.push(this.criarCidadeFormGroup(new CidadeLista()))
  }

  removerCidade(index: number) {
    this.cidades.removeAt(index);
  }

  cancelar() {
    this.router.navigate(["/regiao/"])
  }

  salvar() {
    this.errosAoSalvar = [];
    if (this.regiaoForm.invalid) {
      var totalCidades = this.cidades.controls.filter(p => p.get("id").value != "").length;
      if (totalCidades == 0) {
        this.errosAoSalvar = ["É obrigatório, selecionar ao menos uma cidade a região"];
        return;
      }

      this.errosAoSalvar = ["Campo obrigatórios não preenchidos"];
      return;
    }

    var regiaoModel = new RegiaoModel();
    regiaoModel.nome = this.regiaoForm.get("nome").value;
    regiaoModel.cidades = this.cidades.controls.map(p => p.get("id").value);

    if (!this.paramId) {
      this.regiaoService.salvar(regiaoModel).subscribe(() => {
        this.router.navigate(["/regiao/"])
      }, error => {
        error.error.validacoes.forEach((e: string) => {
          this.errosAoSalvar.push(e);
        })
      });
    } else {
      regiaoModel.id = this.paramId;
      this.regiaoService.atualizar(regiaoModel).subscribe(() => {
        this.router.navigate(["/regiao/"])
      }, error => {
        error.error.validacoes.forEach((e: string) => {
          this.errosAoSalvar.push(e);
        })
      });
    }
  }

}
