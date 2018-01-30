using System.Collections.Generic;
using System.Net.Http;
using CursoFeriasSMN.Application.Models;

namespace CursoFeriasSMN.Application.Application
{
    public class ProdutoApplication
    {
        private readonly string _enderecoApi = $"{ApiConfig.EnderecoApi}/produto";

        public Response<IEnumerable<ProdutoModel>> GetProdutos()
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync($"{_enderecoApi}/listaProduto").Result;
                return new Response<IEnumerable<ProdutoModel>>(response.Content.ReadAsStringAsync().Result, response.StatusCode);
            }
        }

    }
}
