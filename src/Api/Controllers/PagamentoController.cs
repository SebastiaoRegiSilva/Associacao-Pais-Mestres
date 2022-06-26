using System;
using System.Threading.Tasks;
using Disparo.Plataforma.Domain.Alunos;
using Disparo.Plataforma.Domain.Pagamentos;
using Microsoft.AspNetCore.Mvc;

namespace Disparo.Plataforma.Api.Controllers
{
    /// <summary>Controller que provê endpoints relacionados a entidade pagamento.</summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class PagamentoController : Controller
    {
        /// <summary>Serviço que provê acesso aos dados e operações relaciondas as pagamentos.</summary>
        private readonly PagamentoService _pagamentoService;
        
        /// <summary>Serviço que provê acesso aos dados e operações relaciondas aos alunos.</summary>
        private readonly AlunoService _alunoService;

        /// <summary>Construtor com parâmetros para inicialização.</summary>
        /// <param name="pagamentoService">Injeção de dependência do serviço que provê acesso aos dados e operações relacionadas as pagamentos.</param>
        /// <param name="alunoService">Injeção de dependência do serviço que provê acesso aos dados e operações relacionadas aos alunos.</param>
        public PagamentoController(PagamentoService pagamentoService, AlunoService alunoService)
        {
            _pagamentoService = pagamentoService;
            _alunoService = alunoService;
        }

        /// <summary>
        /// Busca no repositório um pagamento com base na matrícula de um aluno.(Corrigir lógica!)
        /// </summary>
        /// <param name="matricula">Matrícula do aluno que realizou o pagamento.</param>
        /// <returns>Pagamento com as características descritas nos parâmetros supracitados.</returns>
        [HttpGet("{matricula}")]
        public async Task<ActionResult<Pagamento>> BuscarPagamentoMatricula(int matricula )
        {
            var pagamentoRecuperado = await _pagamentoService.RecuperarPagamentoMatriculaAlunoAsync(ConverterIntString(matricula));
            
            return pagamentoRecuperado == null? Json($"O pagamento buscado não existe na base de dados."): Json(pagamentoRecuperado);
        }

        /// <summary>
        /// Cadastra um pagamento no repositório.
        /// </summary>
        /// <param name="ano">Ano vigente da associação à APM.</param>
        /// <param name="valor">Valor da anuidade.</param>
        /// <param name="matriculaAluno">Matrícula do aluno que realizou o pagamento.</param>
        /// <param name="formaDePagamento">Forma que a pessoa realizou o pagamento.</param>
        [HttpPost]
        public async Task<IActionResult> CadastrarPagamento(int ano, int valor, int matriculaAluno, string formaDePagamento)
        {
            var pagamentoRecuperado = await _pagamentoService.RecuperarPagamentoMatriculaAlunoAsync(ConverterIntString(matriculaAluno));
            
            await _pagamentoService.CadastrarPagamentoAsync(ano, valor, ConverterIntString(matriculaAluno), formaDePagamento);        
            return Json("Pagamento cadastrado com sucesso!");
        }
        
        /// <summary>
        /// Edita no repositório um pagamento com base matrícula do aluno.(PRECISA DE CORREÇÃO)
        /// </summary>
        /// <param name="matriculaAluno">Matrícula do aluno que realizou o pagamento.</param>
        /// <param name="valorParcela">Valor da anuidade ou parcela.</param>
        [HttpPut("{matricula}/{valor}")]
        public async Task<IActionResult> EditarPagamento(int matriculaAluno, int valorParcela)
        {
            var pagamentoRecuperado = await _pagamentoService.RecuperarPagamentoMatriculaAlunoAsync(ConverterIntString(matriculaAluno));
            // Valor da anuidade está chumbado em 100 reais.
            if (pagamentoRecuperado.Valor < 100)
            {
                valorParcela += pagamentoRecuperado.Valor;
                await _pagamentoService.EditarPagamentoAsync(ConverterIntString(matriculaAluno));
            }    
            else
                return Json($"Situação quitada no sistema!");
            
            return Json("A função precisa ser melhor desenvolvida.");
        }

        /// <summary>
        /// Busca no repositório pagamento com base na matrícula do aluno.
        /// </summary>
        /// <param name="matriculaAluno">Matrícula do aluno que realizou o pagamento.</param>
        /// <returns>Pagamento com as características descritas nos parâmetros supracitados.</returns>
        [HttpGet("{matriculaAluno}")]
        public async Task<ActionResult<Pagamento>> BuscarPagamento(int matriculaAluno)
        {
            var pagamentoRecuperado = await _pagamentoService.RecuperarPagamentoMatriculaAlunoAsync(ConverterIntString(matriculaAluno));
            
            return pagamentoRecuperado == null? Json($"Não existe pagamento na matrícula{matriculaAluno} na base de dados."): Json(pagamentoRecuperado);
        }
        
        /// <summary>
        /// Deleta um pagamento no repositório com base na matrícula do aluno.
        /// </summary>
        /// <param name="matriculaAluno">Matrícula do aluno que realizou o pagamento.</param>
        [HttpDelete("{matriculaAluno}")]
        public async Task<IActionResult> DeletarClasse(int matriculaAluno)
        {
            var pagamentoRecuperado = await _pagamentoService.RecuperarPagamentoMatriculaAlunoAsync(ConverterIntString(matriculaAluno));
            if (pagamentoRecuperado == null)
                return Json($"Pagamento na matrícula: {matriculaAluno} não existe na base de dados.");
            else
            {
                await _pagamentoService.ExcluirPagamentoAsync(ConverterIntString(matriculaAluno));
                return Json("Pagamento excluído com sucesso!");
            }
        }
        
        // FUNÇÕES PRIVADAS ÚTEIS NO CONTROLLER.

        /// <summary>Converte um integer em string.</summary>
        /// <param name="content">Conteúdo a ser convertido.</param>
        /// <returns>Uma string válida ou mensagem para o usuário.</returns>
        private string ConverterIntString(int? content)
        {
            string result = "";

            if(content != null)
                result = Convert.ToString(content);
            else
                result = "Insira uma matrícula válida, por favor!";

            return result;
        }
    }
}