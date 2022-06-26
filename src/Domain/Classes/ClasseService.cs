using System.Threading.Tasks;

namespace Disparo.Plataforma.Domain.Classes
{
    /// <summary>Serviço que provê acesso aos dados das classes.</summary>
    public class ClasseService
    {
        /// <summary>Repositório para armazenamento dos classes.</summary>
        private readonly IClasseRepository _classeRep;
        
        /// <summary> Construtor com injeção de dependência.</summary>
        /// <param name="classeRep">Repositório para armazenamento dos classes.</param>
        public ClasseService(IClasseRepository classeRep)
        {
            _classeRep = classeRep;
        }
        
        /// <summary>Cadastra no repositório uma nova classe no sistema.</summary>
        /// <param name="habilitacao">Nome do curso em que o aluno está matriculado.</param>
        /// <param name="anoOC">Ano vigente.</param>
        /// <param name="moduloSerie">Módulo e série vigentes.</param>
        /// <returns>Código de identificação gerado para uma classe cadastrada.</returns>
        public async Task<string> CadastrarClasseAsync(string habilitacao, int anoOC, string moduloSerie)
        {
            var idClasse = await _classeRep.CadastrarClasseAsync(habilitacao, anoOC, moduloSerie);

            return idClasse;
        }
        
        /// <summary>Recuperar no repositório uma classe com base no seu código de identificação.</summary>
        /// <param name="habilitacao">Nome do curso em que o aluno está matriculado.</param>
        /// <returns>Classe recuperada.</returns>
        public async Task<Classe> RecuperarClassePorHabilitacaoAsync(string habilitacao)
        {
            return await _classeRep.RecuperarClassePorHabilitacaoAsync(habilitacao);
        }
        
        /// <summary>Edita no repositório uma classe com base no nome da matéria.</summary>
        /// <param name="habilitacao">Nome do curso em que o aluno está matriculado.</param>
        public async Task EditarClasseAsync(string habilitacao)
        {
            await _classeRep.EditarClasseAsync(habilitacao);
        }
        
        /// <summary>Exclui no repositório uma classe cadastrada no sistema com base no nome da habilitação.</summary>
        /// <param name="habilitacao">Nome do curso em que o aluno está matriculado.</param>
        public async Task ExcluirClasseAsync(string habilitacao)
        {
            await _classeRep.ExcluirClasseAsync(habilitacao);
        }
    }
}