using Disparo.Plataforma.Domain.Classes;
using Disparo.Plataforma.Infrastructure.Repositories.MongoDb.Classes.Models;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace Disparo.Plataforma.Infrastructure.Repositories.MongoDb.Classes
{
    /// <summary>Implementação do repositório de classes para o Mongo DB.</summary>
    public class ClasseRepository : IClasseRepository
    {
        /// <summary>Contexto utilizado pelo repositório de classes para acessar a coleção de um classe na base de dados.</summary> 
        private readonly ClasseContext _ctxClasse = null;

        /// <summary>Construtor com parâmetros para inicialização.</summary>
        /// <param name="conString">String para conexão com a base de dados.</param>
        /// <param name="database">Nome da base de dados onde se encontra o repositório.</param>
        public ClasseRepository(string conString, string database)
        {
            _ctxClasse = new ClasseContext(conString, database);
        }

        /// <summary>Cadastra a base de dados uma nova classe no sistema.</summary>
        /// <param name="habilitacao">Nome do curso em que o aluno está matriculado.</param>
        /// <param name="anoOC">Ano vigente.</param>
        /// <param name="moduloSerie">Módulo e série vigentes.</param>
        /// <returns>Código de identificação gerado para uma classe cadastrada.</returns>
        public async Task<string> CadastrarClasseAsync(string habilitacao, int anoOC, string moduloSerie)
        {
            var model = new ClasseModel
            {
                Habilitacao = habilitacao,
                AnoOC = anoOC,
                ModuloSerie = moduloSerie
            };

            await _ctxClasse.Classes.InsertOneAsync(model);

            return model.Id;
        }

        /// <summary>Edita na base de dados uma classe com base no nome da matéria.</summary>
        /// <param name="habilitacao">Nome do curso em que o aluno está matriculado.</param>
        public async Task EditarClasseAsync(string habilitacao)
        {
            var classeRecuperada = RecuperarClassePorHabilitacaoAsync(habilitacao);
            
            var builder = Builders<ClasseModel>.Filter;
            var filter = builder.Eq(c => c.Habilitacao, habilitacao);

            var update = Builders<ClasseModel>.Update
                .Set(c => c.Habilitacao, classeRecuperada.Result.Habilitacao)
                .Set(c => c.AnoOC, classeRecuperada.Result.AnoOC)
                .Set(c => c.ModuloSerie, classeRecuperada.Result.ModuloSerie);

            await _ctxClasse.Classes.UpdateOneAsync(filter, update);
        }

        /// <summary>Exclui na base de daos uma classe cadastrada no sistema com base no nome da habilitação.</summary>
        /// <param name="habilitacao">Nome do curso em que o aluno está matriculado.</param>
        public async Task ExcluirClasseAsync(string habilitacao)
        {
            var filter = Builders<ClasseModel>.Filter.Eq(c => c.Habilitacao, habilitacao);

            await _ctxClasse.Classes.DeleteOneAsync(filter);
        }

        /// <summary>Recuperar na base de dados uma classe com base no seu código de identificação.</summary>
        /// <param name="habilitacao">Nome do curso em que o aluno está matriculado.</param>
        /// <returns>Classe recuperada.</returns>
        public async Task<Classe> RecuperarClassePorHabilitacaoAsync(string habilitacao)
        {
            var builder = Builders<ClasseModel>.Filter;
            var filter = builder.Eq(c => c.Habilitacao, habilitacao);
            
            return await _ctxClasse.Classes
                .Aggregate()
                .Match(filter)
                .FirstOrDefaultAsync();
        }
    }
}