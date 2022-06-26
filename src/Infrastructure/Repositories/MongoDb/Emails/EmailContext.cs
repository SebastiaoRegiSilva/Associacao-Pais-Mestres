using Disparo.Plataforma.Infrastructure.Repositories.MongoDb.Emails.Models;
using MongoDB.Driver;

namespace Disparo.Plataforma.Infrastructure.Repositories.MongoDb.Emails
{
    /// <summary>Contexto utilizado para acesso aos dados dos emails.</summary>
    class EmailContext
    {
        // <summary>Base de dados MongoDB onde estão armazenados os emails.</summary>
        private readonly IMongoDatabase _database = null;

        /// <summary>Construtor com parâmetros para inicialização.</summary>
        /// <param name="conString">String para conexão com a base de dados MongoDB.</param>
        /// <param name="database">Nome da base de dados onde se encontra o repositório.</param>
        public EmailContext(string conString, string database)
        {
            var client = new MongoClient(conString);
            if (client != null)
                _database = client.GetDatabase(database);

            BsonMapConfig.Config();
        }

        /// <summary>Coleção de emails.</summary>
        public IMongoCollection<EmailModel> Emails
        {
            get
            {
                return _database.GetCollection<EmailModel>("Emails");
            }
        }
    }
}
