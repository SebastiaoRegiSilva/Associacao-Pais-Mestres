using Disparo.Plataforma.Domain.Alunos;
using Disparo.Plataforma.Domain.Armarios;
using Disparo.Plataforma.Domain.Predios;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Disparo.Plataforma.Api.Controllers
{
    /// <summary>Controller que provê endpoints relacionados a entidade armário.</summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ArmarioController : Controller
    {
        /// <summary>Serviço que provê acesso aos dados e operações relaciondas aos armários.</summary>
        private readonly ArmarioService _armarioService;

        /// <summary>Serviço que provê acesso aos dados e operações relaciondas aos prédios.</summary>
        private readonly PredioService _predioService;
        
        /// <summary>Serviço que provê acesso aos dados e operações relaciondas aos alunos.</summary>
        private readonly AlunoService _alunoService;

        /// <summary>Construtor com parâmetros para inicialização.</summary>
        /// <param name="armarioService">Injeção de dependência do serviço que provê acesso aos dados e operações relacionadas aos armários.</param>
        /// <param name="predioService">Injeção de dependência do serviço que provê acesso aos dados e operações relacionadas aos prédios.</param>
        /// <param name="alunoService">Injeção de dependência do serviço que provê acesso aos dados e operações relacionadas aos alunos.</param>
        public ArmarioController(ArmarioService armarioService, PredioService predioService, AlunoService alunoService)
        {
            _armarioService = armarioService;
            _predioService = predioService;
            _alunoService = alunoService;
        }

        /// <summary>Busca no repositório armário com base em seu número de identificação.</summary>
        /// <param name="numeroIdentificador">Número de identificação do armário.</param>
        [HttpGet("{numeroIdentificador}")]
        public async Task<ActionResult<Armario>> BuscarArmarioNumero(int numeroIdentificador)
        {
            var armarioRecuperado = await _armarioService.RecuperarArmarioNumeroIdentificadorAsync(numeroIdentificador);
            
            return armarioRecuperado == null? Json($"O armário com o número {numeroIdentificador} não existe na base de dados."): Json(armarioRecuperado);
        }

        /// <summary>Cadastra armário no repositório.</summary>
        /// <param name="numeroIdentificador">Número de identificação do armário.</param>
        /// <param name="anoValidade">Ano de validade da locação do armário.</param>
        /// <param name="numeroPredio">Prédio onde está localizado do armário.</param>
        [HttpPost]
        public async Task<IActionResult> CadastrarArmario(int numeroIdentificador, int anoValidade,int numeroPredio)
        {
            var armarioRecuperado = await _armarioService.RecuperarArmarioNumeroIdentificadorAsync(numeroIdentificador);
            
            var predioRecuperado = await _predioService.RecuperarPredioPorNumeroAsync(numeroPredio);
            
            // Se o prédio não existir ele será criado nesse momento.
            if(predioRecuperado == null)
            {
                await _predioService.CadastrarPredioAsync(numeroPredio);
                predioRecuperado = await _predioService.RecuperarPredioPorNumeroAsync(numeroPredio);
            }
            bool disponivel = true;

            await _armarioService.CadastrarArmarioAsync(predioRecuperado.NumeroIdentificador, numeroIdentificador, anoValidade, disponivel);        
            return Json("Armário cadastrado com sucesso!");
        }

        /// <summary>
        /// Lista de todos os armários disponíveis no sistema.
        /// </summary>
        /// <returns>Os armários disponíveis.</returns>
        [HttpGet]
        public async Task<ActionResult<Armario>> ListarTodosArmariosDisponiveis()
        {
            // Recupera todos armários disponíveis.
            var listaArmarios = await _armarioService.ListarTodosArmariosDisponiveisAsync();
            
            return Json(listaArmarios); 
        }
        
        /// <summary>Edita armário no repositório.FUNÇÃO PRECISA SER CORRIGIDA.</summary>
        /// <param name="numeroIdentificador">Número de identificação do armário.</param>
        /// <param name="anoValidade">Ano de validade da locação do armário.</param>
        /// <param name="numeroPredio">Prédio onde está localizado do armário.</param>
        [HttpPut]
        public async Task<IActionResult> EditarArmario(int numeroIdentificador, int anoValidade = 2021 ,int numeroPredio = 1)
        {
            // Recuperar o prédio.
            var predioRecuperado = await _predioService.RecuperarPredioPorNumeroAsync(numeroPredio);
            
            var armarioRecuperado = await _armarioService.RecuperarArmarioNumeroIdentificadorAsync(numeroIdentificador);
            
            // Vai cadastrar o armário.
            if (armarioRecuperado == null)
                Redirect($"https://localhost:5001/api/Armario?numeroIdentificador={numeroIdentificador}&anoValidade={anoValidade}&numeroPredio={predioRecuperado.NumeroIdentificador}");
            
            await _armarioService.EditarArmarioAsync(numeroIdentificador, anoValidade, numeroPredio);
            return Json("Armário editada com sucesso!");
        }

        /// <summary>Atribui um armário a um aluno no repositório.</summary>
        /// <param name="matricula">Matrícula do aluno que vai locar o armário.</param>
        /// <param name="numeroIdentificador">Número identificador do armário.</param>
        /// <param name="anoValidade">Ano de validade da locação do armário.</param>
        [HttpPut("{numeroIdentificador}/{matricula}")]
        public async Task<IActionResult> AtribuirAlunoArmario(string matricula, int numeroIdentificador ,int anoValidade)
        {
            // Recuperar o aluno.
            var alunoRecuperado = await _alunoService.RecuperarAlunoMatriculaAsync(matricula);
            
            var armarioRecuperado = await _armarioService.RecuperarArmarioNumeroIdentificadorAsync(numeroIdentificador);
            
            await _armarioService.AtribuirAlunoArmarioAsync(alunoRecuperado.Matricula, armarioRecuperado.NumeroIdentificador, anoValidade);
            
            return Json("Aluno atribuído com sucesso!");
        }
        
        /// <summary>Exclui um armário no repositório com base em seu código de identificação..</summary>
        /// <param name="numeroIdentificador">Número identificador do armário.</param>
        [HttpDelete]
        public async Task<IActionResult> DeletarArmario(int numeroIdentificador)
        {
            var armarioRecuperado = await _armarioService.RecuperarArmarioNumeroIdentificadorAsync(numeroIdentificador);
            if (armarioRecuperado == null)
                return Json($"Armário com o nome {numeroIdentificador} não existe na base de dados.");
            else
            {
                await _armarioService.ExcluirArmarioAsync(numeroIdentificador);
                return Json("Armário excluído com sucesso!");
            }
        }
    }
}