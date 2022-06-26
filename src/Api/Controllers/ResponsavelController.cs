using Disparo.Plataforma.Domain.Responsaveis;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Disparo.Plataforma.Api.Controllers
{
    /// <summary>Controller que provê endpoints relacionados a entidade responsável.</summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ResponsavelController : Controller
    {
        /// <summary>Serviço que provê acesso aos dados e operações relaciondas aos responsáveis.</summary>
        private readonly ResponsavelService _responsavelService;

        /// <summary>Construtor com parâmetros para inicialização.</summary>
        /// <param name="responsavelService">Injeção de dependência do serviço que provê acesso aos dados e operações relacionadas aos responsáveis.</param>
        public ResponsavelController(ResponsavelService responsavelService)
        {
            _responsavelService = responsavelService;
        }

        /// <summary>
        /// Busca no repositório responsável com base em CPF.
        /// </summary>
        /// <param name="cpf">Cadastro de Pessoa Física.</param>
        [HttpGet("{cpf}")]
        public async Task<ActionResult<Responsavel>> BuscarResponsavelCPF(string cpf)
        {
            var responsavelRecuperado = await _responsavelService.RecuperarResponsavelPorCPFAsync(cpf);
            if (responsavelRecuperado == null)
                return Json("Responsável com esse número  de CPF não existe na base de dados.");
            
            return Json(responsavelRecuperado);
        }

        /// <summary>
        /// Cadastra no repositório responsável com base em CPF.
        /// </summary>
        /// <param name="cpf">Cadastro de Pessoa Física.</param>
        /// <param name="nome">Nome do responsável.</param>
        /// <param name="enderecoEmail">Endereço de e-mail do responsável.</param>
        /// <param name="numerosTelefones">Forma de contato telefônico direto com o responsável.</param>
        [HttpPost]
        public async Task<IActionResult> CadastrarResponsavel(string cpf, string nome, string enderecoEmail, string numerosTelefones)
        {
            var responsavelRecuperado = await _responsavelService.RecuperarResponsavelPorCPFAsync(cpf);
            
            var numeros = new List<string>();
            numeros.Add(numerosTelefones);
            
            if(responsavelRecuperado != null)
                return Ok("Já existe um responsável cadastrado com esse CPF no sistema!");
            else
            {
                await _responsavelService.CadastrarResponsavelAsync(cpf, nome, enderecoEmail, numeros);        
                return Ok("Responsável cadastrado com sucesso!");
            }
        }

        /// <summary>
        /// Edita no repositório responsável com base em CPF.
        /// </summary>
        /// <param name="cpf">Cadastro de Pessoa Física.</param>
        [HttpPut]
        public async Task<IActionResult> EditarResponsavel(string cpf)
        {
            var responsavelRecuperado = await _responsavelService.RecuperarResponsavelPorCPFAsync(cpf);
            if (responsavelRecuperado == null)
                return Json("Responsável com esse número  de CPF não existe na base de dados.");
            else
            {
                await _responsavelService.EditarResponsavelAsync(cpf);
                return Ok("Responsável editado com sucesso!");
            }
        }

        /// <summary>
        /// Deleta no repositório responsável com base em CPF.
        /// </summary>
        /// <param name="cpf">Cadastro de Pessoa Física.</param>
        [HttpDelete]
        public async Task<IActionResult> DeleteResponsavel(string cpf)
        {
            var responsavelRecuperado = await _responsavelService.RecuperarResponsavelPorCPFAsync(cpf);
            if (responsavelRecuperado == null)
                return Json("Responsável com esse número  de CPF não existe na base de dados.");
            else
            {
                await _responsavelService.ExcluirResponsavelAsync(cpf);
                return Ok("Responsável excluído com sucesso!");
            }
        }
    }
}