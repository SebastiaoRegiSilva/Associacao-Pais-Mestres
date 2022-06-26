using System.Threading.Tasks;

namespace Disparo.Plataforma.Domain.Classes
{
    /// <summary>Interface que padroniza o repositório das classes.</summary>
    public interface IClasseRepository
    {
        /// <summary>Cadastra no repositório uma nova classe no sistema.</summary>
        /// <param name="habilitacao">Nome do curso em que o aluno está matriculado.</param>
        /// <param name="anoOC">Ano vigente.</param>
        /// <param name="moduloSerie">Módulo e série vigentes.</param>
        /// <returns>Código de identificação gerado para uma classe cadastrada.</returns>
        Task<string> CadastrarClasseAsync(string habilitacao, int anoOC, string moduloSerie);
        
        /// <summary>Recuperar no repositório uma classe com base no seu código de identificação.</summary>
        /// <param name="habilitacao">Nome do curso em que o aluno está matriculado.</param>
        /// <returns>Classe recuperada.</returns>
        Task<Classe> RecuperarClassePorHabilitacaoAsync(string habilitacao);
        
        /// <summary>Edita no repositório uma classe com base no nome da matéria.</summary>
        /// <param name="habilitacao">Nome do curso em que o aluno está matriculado.</param>
        Task EditarClasseAsync(string habilitacao);
        
        /// <summary>Exclui no repositório uma classe cadastrada no sistema com base no nome da habilitação.</summary>
        /// <param name="habilitacao">Nome do curso em que o aluno está matriculado.</param>
        Task ExcluirClasseAsync(string habilitacao);
    }
}