using Disparo.Plataforma.Infrastructure.Repositories.MongoDb.Responsaveis.Models;
using MongoDB.Driver;

namespace Disparo.Plataforma.Infrastructure.Repositories.MongoDb.Responsaveis
{
    /// <summary>Contexto utilizado para acesso aos dados dos responsáveis.</summary>
    class ResponsavelContext
    {
        /// <summary>Base de dados MongoDB onde estão armazenados os responsáveis.</summary>
        private readonly IMongoDatabase _database = null;

        /// <summary>Construtor com parâmetros para inicialização.</summary>
        /// <param name="conString">String para conexão com a base de dados MongoDB.</param>
        /// <param name="database">Nome da base de dados onde se encontra o repositório.</param>
        public ResponsavelContext(string conString, string database)
        {
            var client = new MongoClient(conString);
            if (client != null)
                _database = client.GetDatabase(database);

            BsonMapConfig.Config();
        }

        /// <summary>Coleção de responsáveis.</summary>
        public IMongoCollection<ResponsavelModel> Responsaveis
        {
            get
            {
                return _database.GetCollection<ResponsavelModel>("Responsaveis");
            }
        }
    }
}