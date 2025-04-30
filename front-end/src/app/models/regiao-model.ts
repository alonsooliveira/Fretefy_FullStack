import { CidadeLista } from "./regiao-lista";

export class RegiaoModel {
    id: string;
    nome: string;
    ativo: boolean;
    cidades: Array<string>;
}
