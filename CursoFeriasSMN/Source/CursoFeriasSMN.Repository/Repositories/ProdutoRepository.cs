using System.Collections.Generic;
using CursoFeriasSMN.Domain.Entidades;
using CursoFeriasSMN.Repository.DataBase;

namespace CursoFeriasSMN.Repository.Repositories
{
    public class ProdutoRepository : Execucao
    {
        private static readonly Conexao Conexao = new Conexao();

        public ProdutoRepository() : base(Conexao)
        {
        }

        public IEnumerable<Produto> GetProdutos()
        {
            ExecuteProcedure("[dbo].[SP_SelProdutos]");

            var produtos = new List<Produto>();

            using (var reader = ExecuReader())
            {
                while (reader.Read())
                    produtos.Add(new Produto
                    {
                        CodigoProduto = reader.ReadAsInt("CodigoProduto"),
                        Nome = reader.ReadAsString("Nome"),
                        Preco = reader.ReadAsDecimal("Preco"),
                        Estoque = reader.ReadAsShort("Estoque")
                    });
            }

            return produtos;
        }

        public enum Procedures
        {
            SP_SelProdutos,
            SP_SelDadosProduto,
            SP_InsProduto,
            SP_UpdProduto,
            SP_DelProduto,
            TRG_HistoricoProduto
        }
    }
}
