using Disparo.Plataforma.Infrastructure.Repositories.MongoDb.Alunos.Models;
using MongoDB.Driver;

namespace Disparo.Plataforma.Infrastructure.Repositories.MongoDb.Alunos
{
    /// <summary>Contexto utilizado para acesso aos dados dos alunos.</summary>
    class AlunoContext
    {
        /// <summary>Base de dados MongoDB onde estão armazenados os alunos.</summary>
        private readonly IMongoDatabase _database = null;

        /// <summary>Construtor com parâmetros para inicialização.</summary>
        /// <param name="conString">String para conexão com a base de dados MongoDB.</param>
        /// <param name="database">Nome da base de dados onde se encontra o repositório.</param>
        public AlunoContext(string conString, string database)
        {
            var client = new MongoClient(conString);
            if (client != null)
                _database = client.GetDatabase(database);

            BsonMapConfig.Config();
        }

        /// <summary>Coleção de alunos.</summary>
        public IMongoCollection<AlunoModel> Alunos
        {
            get
            {
                return _database.GetCollection<AlunoModel>("Alunos");
            }
        }
    }
}