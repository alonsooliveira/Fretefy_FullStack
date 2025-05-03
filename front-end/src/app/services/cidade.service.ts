import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CidadeLista } from '../models/regiao-lista';

@Injectable({
  providedIn: 'root'
})
export class CidadeService {

  constructor(private http: HttpClient) { }
  
    listar(): Observable<Array<CidadeLista>> {
      return this.http
        .get<Array<CidadeLista>>('http://localhost:55700/api/cidade')
    }
}
