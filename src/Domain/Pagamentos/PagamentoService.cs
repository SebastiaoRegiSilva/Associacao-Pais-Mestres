using Disparo.Plataforma.Domain.Alunos;
using System.Threading.Tasks;

namespace Disparo.Plataforma.Domain.Pagamentos
{
    /// <summary>Serviço que provê acesso aos dados dos pagamentos.</summary>
    public class PagamentoService
    {
        /// <summary>Repositório para armazenamento dos pagamentos.</summary>
        private readonly IPagamentoRepository _pagamentoRep;

        /// <summary>Serviço que provê acesso aos dados e operações relaciondas aos alunos.</summary>
        private readonly AlunoService _alunoService;
        
        /// <summary> Construtor com injeção de dependência.</summary>
        /// <param name="pagamentoRep">Repositório para armazenamento dos pagamentos.</param>
         /// <param name="alunoService">Injeção de dependência do serviço que provê acesso aos dados e operações relacionadas aos alunos.</param>

        public PagamentoService(IPagamentoRepository pagamentoRep, AlunoService alunoService)
        {
            _pagamentoRep = pagamentoRep;
            _alunoService = alunoService;
        }
        
        /// <summary>Cadastrar no repositório um pagamento.</summary>
        /// <param name="ano">Ano vigente da associação à APM.</param>
        /// <param name="valor">Valor da anuidade.</param>
        /// <param name="matriculaAluno">Matrícula do aluno que realizou o pagamento.</param>
        /// <param name="formaDePagamento">Forma que a pessoa realizou o pagamento.</param>
        /// <returns>Código de identificação gerado para um pagamento realizado.</returns>
        public async Task<string> CadastrarPagamentoAsync(int ano, int valor, string matriculaAluno, string formaDePagamento)
        {
            var alunoRecuperardo = await _alunoService.RecuperarAlunoMatriculaAsync(matriculaAluno);
            
            var idPagamento = await _pagamentoRep.CadastrarPagamentoAsync(ano, valor, alunoRecuperardo, formaDePagamento);

            return idPagamento;
        }
        
        /// <summary>Recuperar no repositório um pagamento com base na matrícula do aluno.</summary>
        /// <param name="matricula">Matrícula do aluno que está cadastrado na APM.</param>
        /// <returns>Classe recuperada.</returns>
        public async Task<Pagamento> RecuperarPagamentoMatriculaAlunoAsync(string matricula)
        {
            return await _pagamentoRep.RecuperarPagamentoMatriculaAlunoAsync(matricula);
        }
        
        /// <summary>Edita no repositório um pagamento com base na matrícula do aluno.</summary>
        /// <param name="matricula">Matrícula do aluno que está cadastrado na APM.</param>
        public async Task EditarPagamentoAsync(string matricula)
        {
            await _pagamentoRep.EditarPagamentoAsync(matricula);
        }
        
        /// <summary>Exclui no repositório um pagamento registrado no sistema com base na matrícula do aluno.</summary>
        /// <param name="matricula">Matrícula do aluno que está cadastrado na APM.</param>
        public async Task ExcluirPagamentoAsync(string matricula)
        {
            await _pagamentoRep.ExcluirPagamentoAsync(matricula);
        }
    }
}