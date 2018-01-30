using System.Net;
using System.Net.Http;
using System.Web.Http;
using CursoFeriasSMN.Repository.Repositories;

namespace CursoFeriasSMN.Api.Controllers
{
    [RoutePrefix("api/produto")]
    public class ProdutoController : ApiController
    {
        private readonly ProdutoRepository _produtoRepository = new ProdutoRepository();

        [HttpGet,Route("listaProdutos")]
        public IHttpActionResult GetProdutos()
        {
            try
            {
                return Ok(_produtoRepository.GetProdutos());
            }
            catch
            {
                return BadRequest("Erro ao listar produtos");
            }
            
        }
    }
}