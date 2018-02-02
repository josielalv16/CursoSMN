using System.Net;
using System.Web.Mvc;
using CursoFeriasSMN.Application.Application;
using CursoFeriasSMN.Application.Models;

namespace CursoFeriasSMN.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly ProdutoApplication _produtoApplication = new ProdutoApplication();

        public ActionResult ListarProdutos()
        {
            var response = _produtoApplication.GetProdutos();

            if (response.Status != HttpStatusCode.OK)
            {
                Response.StatusCode = (int) HttpStatusCode.BadRequest;
                Response.TrySkipIisCustomErrors = true;
                return Content(response.ContentAsString);
            }

            return View("GridProdutos", response.Content);
        }

        //public ActionResult CadastrarProduto()
        //{
        //    return View("");
        //}

        public ActionResult CadastrarProduto(ProdutoModel produto)
        {
            var response = _produtoApplication.PostProduto(produto);

            if (response.Status != HttpStatusCode.OK)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                Response.TrySkipIisCustomErrors = true;
                ViewBag.Erro = response.Content;
                return View("../Home/Index");
            }

            ViewBag.Resultado = response.Content;
            return View("../Home/Index");
        }

        public ActionResult DeletaProduto(int codigoProduto)
        {
            var response = _produtoApplication.DeletaProduto(codigoProduto);
            if (response.Status != HttpStatusCode.OK)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                Response.TrySkipIisCustomErrors = true;
                ViewBag.Erro = response.Content;
                return View("../Home/Index");
            }

            ViewBag.Resultado = response.Content;
            return View("../Home/Index");
        }
    }
}