using Disparo.Plataforma.Domain.Alunos;
using System.Threading.Tasks;

namespace Disparo.Plataforma.Domain.Pagamentos
{
    /// <summary>Interface que padroniza o repositório dos pagamentos.</summary>
    public interface IPagamentoRepository
    {
        /// <summary>Cadastrar no repositório um pagamento.</summary>
        /// <param name="ano">Ano vigente da associação à APM.</param>
        /// <param name="valor">Valor da anuidade.</param>
        /// <param name="aluno">Aluno que realizou o pagamento.</param>
        /// <param name="formaDePagamento">Forma que a pessoa realizou o pagamento.</param>
        /// <returns>Código de identificação gerado para um pagamento realizado.</returns>
        Task<string> CadastrarPagamentoAsync(int ano, int valor, Aluno aluno, string formaDePagamento);
        
        /// <summary>Recuperar no repositório um pagamento com base na matrícula do aluno.</summary>
        /// <param name="matricula">Matrícula do aluno que está cadastrado na APM.</param>
        /// <returns>Classe recuperada.</returns>
        Task<Pagamento> RecuperarPagamentoMatriculaAlunoAsync(string matricula);
        
        /// <summary>Edita no repositório um pagamento com base na matrícula do aluno.</summary>
        /// <param name="matricula">Matrícula do aluno que está cadastrado na APM.</param>
        Task EditarPagamentoAsync(string matricula);
        
        /// <summary>Exclui no repositório um pagamento registrado no sistema com base na matrícula do aluno.</summary>
        /// <param name="matricula">Matrícula do aluno que está cadastrado na APM.</param>
        Task ExcluirPagamentoAsync(string matricula);
    }
}