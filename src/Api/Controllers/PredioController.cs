using Disparo.Plataforma.Domain.Predios.Exceptions;
using Disparo.Plataforma.Domain.Predios;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Disparo.Plataforma.Api.Controllers
{
    /// <summary>Controller que provê endpoints relacionados a entidade prédio.</summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class PredioController : Controller
    {
        /// <summary>Serviço que provê acesso aos dados e operações relaciondas aos prédios.</summary>
        private readonly PredioService _predioService;

        /// <summary>Construtor com parâmetros para inicialização.</summary>
        /// <param name="predioService">Injeção de dependência do serviço que provê acesso aos dados e operações relacionadas aos prédios.</param>
        public PredioController(PredioService predioService)
        {
            _predioService = predioService;
        }

        /// <summary>
        /// Busca no repositório prédio com base em seu número
        /// </summary>
        /// <param name="numeroIdentificador">Número de identificação do prédio.</param>
        [HttpGet("{numero}")]
        public async Task<ActionResult<Predio>> BuscarPredioNumero (int numeroIdentificador)
        {
            var predioRecuperado = await _predioService.RecuperarPredioPorNumeroAsync(numeroIdentificador);
            
            return predioRecuperado == null? Json($"O prédio com o número {numeroIdentificador} não está cadastrado na base de dados!"): Json(predioRecuperado); 
        }

        /// <summary>
        /// Cadastra no repositório prédio com base em seu número
        /// </summary>
        /// <param name="numeroIdentificador">Número de identificação do prédio.</param>
        [HttpPost]
        public async Task<IActionResult> CadastrarPredio(int numeroIdentificador)
        {
            // Valida se prédio já existe.
            if(_predioService.ValidarPredioExiste(numeroIdentificador).Result)
                throw new IdentificationNumberInvalidException(numeroIdentificador);

            await _predioService.CadastrarPredioAsync(numeroIdentificador);         
            return Json("Prédio cadastrado com sucesso!");
        }

        /// <summary>
        /// Edita no repositório prédio com base em seu número
        /// </summary>
        /// <param name="numeroIdentificador">Número de identificação do prédio.</param>
        [HttpPut]
        public async Task<IActionResult> EditarPredio(int numeroIdentificador)
        { 
            // Validar se prédio já existi.
            if(_predioService.ValidarPredioExiste(numeroIdentificador).Result)
                await _predioService.EditarPredioAsync(numeroIdentificador);
            return NoContent();
        }

        /// <summary>
        /// Deleta no repositório prédio com base em seu número
        /// </summary>
        /// <param name="numeroIdentificador">Número de identificação do prédio.</param>
        [HttpDelete]
        public async Task<IActionResult> DeletarPredio(int numeroIdentificador)
        {
            // Validar se prédio já existi.
            if(_predioService.ValidarPredioExiste(numeroIdentificador).Result)
                await _predioService.ExcluirPredioAsync(numeroIdentificador);

            return Json($"O prédio de número {numeroIdentificador} foi excluído da base de dados!");
        }
    }
}