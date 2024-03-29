﻿namespace Joao.Ana.Modas.Dominio.Entidades
{
    public class ProdutoEstoque : EntidadeBase
    {
        public Produto Produto { get; private set; }
        public Guid ProdutoId { get; private set; }

        public Cor Cor { get; private set; }
        public Guid? CorId { get; private set; }

        public Tamanho Tamanho { get; private set; }
        public Guid? TamanhoId { get; private set; }

        public int Quantidade { get; private set; }

        public int? Check { get; private set; } = 0;

        public ProdutoEstoque(Guid produtoId, Guid? corId, Guid? tamanhoId, int quantidade)
        {
            ProdutoId = produtoId;
            CorId = corId;
            TamanhoId = tamanhoId;
            Quantidade = quantidade;
        }

        private ProdutoEstoque() { }

        public void AtualizarEstoque(int quantidade)
        {
            Quantidade += quantidade;
            Atualizar();
        }

        public void AtualizarChecking(int check, Guid? usuarioAlteracao = null)
        {
            Check = check;
            Atualizar(usuarioAlteracao);
        }
    }
}
