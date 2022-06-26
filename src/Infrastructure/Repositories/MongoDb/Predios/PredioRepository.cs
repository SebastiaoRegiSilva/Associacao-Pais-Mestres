using Disparo.Plataforma.Infrastructure.Repositories.MongoDb.Predios.Models;
using Disparo.Plataforma.Domain.Predios;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace Disparo.Plataforma.Infrastructure.Repositories.MongoDb.Predios
{
    /// <summary>Implementação do repositório de prédios para o Mongo DB.</summary>
    public class PredioRepository : IPredioRepository
    {
        /// <summary>Contexto utilizado pelo repositório de prédios para acessar a coleção de um prédio na base de dados.</summary> 
        private readonly PredioContext _ctxPredio = null;

        /// <summary>Construtor com parâmetros para inicialização.</summary>
        /// <param name="conString">String para conexão com a base de dados.</param>
        /// <param name="database">Nome da base de dados onde se encontra o repositório.</param>
        public PredioRepository(string conString, string database)
        {
            _ctxPredio = new PredioContext(conString, database);
        }

        /// <summary>Cadastra na base de dados um novo prédio no sistema.</summary>
        /// <param name="numeroIdentificador">Número de identificação do prédio.</param>
        public async Task<string> CadastrarPredioAsync(int numeroIdentificador)
        {
            var model = new PredioModel
            {
                NumeroIdentificador = numeroIdentificador
            };

            await _ctxPredio.Predios.InsertOneAsync(model);

            return model.Id;
        }

        /// <summary>Edita na base de dados um prédio com base no número identificador.</summary>
        /// <param name="numeroIdentificador">Número de identificação do prédio.</param>
        public async Task EditarPredioAsync(int numeroIdentificador)
        {
            var predioRecuperado = RecuperarPredioPorNumeroAsync(numeroIdentificador);
            
            var builder = Builders<PredioModel>.Filter;
            var filter = builder.Eq(p => p.NumeroIdentificador, numeroIdentificador);

            var update = Builders<PredioModel>.Update
                .Set(p => p.NumeroIdentificador, predioRecuperado.Result.NumeroIdentificador);

            await _ctxPredio.Predios.UpdateOneAsync(filter, update);
        }

        /// <summary>Exclui na base de dados um prédio cadastrado no sistema com base no número identificador.</summary>
        /// <param name="numeroIdentificador">Número de identificação do prédio.</param>
        public async Task ExcluirPredioAsync(int numeroIdentificador)
        {
            var filter = Builders<PredioModel>.Filter.Eq(p => p.NumeroIdentificador, numeroIdentificador);

            await _ctxPredio.Predios.DeleteOneAsync(filter);
        }

        /// <summary>Recupera na base de dados um prédio com base no número identificador.</summary>
        /// <param name="numeroIdentificador">Número de identificação do prédio.</param>
        /// <returns>Prédio recuperado.</returns>
        public async Task<Predio> RecuperarPredioPorNumeroAsync(int numeroIdentificador)
        {
            var builder = Builders<PredioModel>.Filter;
            var filter = builder.Eq(p => p.NumeroIdentificador, numeroIdentificador);
            
            return await _ctxPredio.Predios
                .Aggregate()
                .Match(filter)
                .FirstOrDefaultAsync();
        }
    }
}