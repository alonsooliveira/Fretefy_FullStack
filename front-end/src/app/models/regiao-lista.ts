export class RegiaoLista {
    id: string;
    nome: string;
    ativo: boolean;
    cidades: Array<CidadeLista>;
}

export class CidadeLista {
    id: string;
    nome: string;
}
