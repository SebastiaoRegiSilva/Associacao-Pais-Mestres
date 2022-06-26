using Disparo.Plataforma.Infrastructure.Repositories.MongoDb.Alunos.Models;
using Disparo.Plataforma.Infrastructure.Repositories.MongoDb.Armarios.Models;
using Disparo.Plataforma.Infrastructure.Repositories.MongoDb.Classes.Models;
using Disparo.Plataforma.Infrastructure.Repositories.MongoDb.Emails.Models;
using Disparo.Plataforma.Infrastructure.Repositories.MongoDb.Pagamentos.Models;
using Disparo.Plataforma.Infrastructure.Repositories.MongoDb.Predios.Models;
using Disparo.Plataforma.Infrastructure.Repositories.MongoDb.Responsaveis.Models;
using MongoDB.Bson.Serialization;


namespace Disparo.Plataforma.Infrastructure.Repositories.MongoDb
{
    /*
        Comentário acerca do BsonMap no Mongo: 
        Representa um mapeamento entre uma classe e um documento BSON.
    */
    
    /// <summary>Classe de configuração responsável pelo mapeamento geral dos modelos na base de dados.</summary>
    public static class BsonMapConfig
    {
        /// <summary>Variável que verifica se o método já foi acessado por outro contexto.</summary>
        private static bool _hit = false;

        /// <summary>Método de mapeamento dos modelos.</summary>
        public static void Config()
        {
            if (_hit)
                return;

            // Cria e registra um mapade classe.
            BsonClassMap.RegisterClassMap<AlunoModel>(map =>
            {
                // Automapeia a classe.
                map.AutoMap();
                
                // Define se os elementos extras devem ser ignorados ao desserializar.
                map.SetIgnoreExtraElements(true);
            });

            BsonClassMap.RegisterClassMap<ArmarioModel>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
            });

            BsonClassMap.RegisterClassMap<ClasseModel>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
            });

            BsonClassMap.RegisterClassMap<EmailModel>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
            });

            BsonClassMap.RegisterClassMap<PredioModel>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
            });

            BsonClassMap.RegisterClassMap<ResponsavelModel>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
            });

            BsonClassMap.RegisterClassMap<PagamentoModel>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
            });

            _hit = true;
        }
    }
}