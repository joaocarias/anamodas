export class Mensagem {
    constructor(
        public id: string | null,
        public nome: string,
        public telefone: string,
        public mensagem: string,
        public email: string
    ) { }
  }