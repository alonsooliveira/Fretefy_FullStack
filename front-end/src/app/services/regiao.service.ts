import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { RegiaoLista } from '../models/regiao-lista';
import { Observable } from 'rxjs';
import { RegiaoModel } from '../models/regiao-model';

@Injectable({
  providedIn: 'root'
})
export class RegiaoService {

  private apiUrl = 'http://localhost:55700/api/regiao';

  constructor(private http: HttpClient) { }

  listar(): Observable<RegiaoLista[]> {
    return this.http
      .get<RegiaoLista[]>(this.apiUrl);
  }

  buscarPorId(id: string): Observable<RegiaoLista> {
    return this.http
      .get<RegiaoLista>(`${this.apiUrl}/${id}`)
  }

  salvar(regiao: RegiaoModel): Observable<boolean> {
    return this.http
      .post<boolean>(this.apiUrl, regiao)
  }

  ativar(id: string): Observable<boolean> {
    return this.http
      .post<boolean>(`${this.apiUrl}/ativar`, { id: id})
  }

  desativar(id: string): Observable<boolean> {
    return this.http
    .post<boolean>(`${this.apiUrl}/desativar`, { id: id})
  }
}
