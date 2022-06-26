using Disparo.Plataforma.Domain.Classes;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Disparo.Plataforma.Api.Controllers
{
    /// <summary>Controller que provê endpoints relacionados a entidade classe.</summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ClasseController : Controller
    {
        /// <summary>Serviço que provê acesso aos dados e operações relaciondas as classes.</summary>
        private readonly ClasseService _classeService;

        /// <summary>Construtor com parâmetros para inicialização.</summary>
        /// <param name="classeService">Injeção de dependência do serviço que provê acesso aos dados e operações relacionadas as classes.</param>
        public ClasseController(ClasseService classeService)
        {
            _classeService = classeService;
        }

        /// <summary>
        /// Busca no repositório uma determinada classe com base em sua habilitação.
        /// </summary>
        /// <param name="habilitacao">Nome do curso, da mesma forma que está escrito no NSA.</param>
        /// <returns>Classe com as características descritas nos parâmetros supracitados.</returns>
        [HttpGet("{habilitacao}")]
        public async Task<ActionResult<Classe>> BuscarClasse(string habilitacao)
        {
            var classeRecuperada = await _classeService.RecuperarClassePorHabilitacaoAsync(habilitacao);
            
            return classeRecuperada == null? Json($"O classe buscada não existe na base de dados."): Json(classeRecuperada);
        }
        
        /// <summary>
        /// Cadastra uma classe no repositório.
        /// </summary>
        /// <param name="habilitacao">Nome do curso corforme está exemplificado no NSA.</param>
        /// <param name="anoOC">Ano em que o aluno foi matrículado.</param>
        /// <param name="moduloSerie">Semestre corrente, em relação ao ano vigente.</param>
        [HttpPost]
        public async Task<IActionResult> CadastrarClasse(string habilitacao, int anoOC, string moduloSerie)
        {
            var classerecuperada = await _classeService.RecuperarClassePorHabilitacaoAsync(habilitacao);
            
            if(classerecuperada != null)
                return Json($"Já existe um classe cadastrada com a habilitação {habilitacao} no sistema!");
            else
            {
                await _classeService.CadastrarClasseAsync(habilitacao, anoOC, moduloSerie);        
                return Json("Classe cadastrada com sucesso!");
            }
        }

        /// <summary>
        /// Edita no repositório uma classe com base em sua habilitação.
        /// </summary>
        /// <param name="habilitacao">Nome do curso corforme está exemplificado no NSA.</param>
        [HttpPut]
        public async Task<IActionResult> EditarClasse(string habilitacao)
        {
            var classeRecuperada = await _classeService.RecuperarClassePorHabilitacaoAsync(habilitacao);
            if (classeRecuperada == null)
                return Json($"Classe com o nome {habilitacao} não existe na base de dados.");
            else
            {
                await _classeService.EditarClasseAsync(habilitacao);
                return Json("Classe editada com sucesso!");
            }
        }

        /// <summary>
        /// Deleta uma classe no repositório com base em sua habilitação.
        /// </summary>
        /// <param name="habilitacao">Nome do curso corforme está exemplificado no NSA.</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeletarClasse(string habilitacao)
        {
            var classeRecuperada = await _classeService.RecuperarClassePorHabilitacaoAsync(habilitacao);
            if (classeRecuperada == null)
                return Json($"Classe com o nome {habilitacao} não existe na base de dados.");
            else
            {
                await _classeService.ExcluirClasseAsync(habilitacao);
                return Json("Classe excluída com sucesso!");
            }
        }
    }
}