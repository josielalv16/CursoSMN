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

        public string CadastraProduto(Produto produto)
        {
            ExecuteProcedure("[dbo].[SP_InsProduto]");
            AddParameter("@Nome", produto.Nome);
            AddParameter("@Preco", produto.Preco);
            AddParameter("@Estoque", produto.Estoque);

            var retorno = ExecuteNonQueryWithReturn();

            if (retorno == 1)
                return "Erro ao inserir o produto";

            return null;
        }

        public string DeletarProduto(int codigoProduto)
        {
            ExecuteProcedure("SP_DelProduto");
            AddParameter("@CodigoProduto", codigoProduto);

            var retorno = ExecuteNonQueryWithReturn();

            switch (retorno)
            {
                case 1: return "Exclusão não permitida, o produto esta vinculada a uma venda";
                case 2: return "Erro ao excluir o produto";
            }

            return null;
        }
    }
}
