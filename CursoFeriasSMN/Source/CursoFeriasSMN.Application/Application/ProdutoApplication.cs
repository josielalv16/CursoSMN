using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
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
                var response = client.GetAsync($"{_enderecoApi}/listaProdutos").Result;
                return new Response<IEnumerable<ProdutoModel>>(response.Content.ReadAsStringAsync().Result, response.StatusCode);
            }
        }

        public Response<string> PostProduto(ProdutoModel produto)
        {
            using (var client = new HttpClient())
            {
                var response = client.PostAsync($"{_enderecoApi}/cadastraProduto", produto, new JsonMediaTypeFormatter()).Result;
                return new Response<string>(response.Content.ReadAsStringAsync().Result, response.StatusCode);
            }
        }

    }
}
