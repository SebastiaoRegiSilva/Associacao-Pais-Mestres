using Disparo.Plataforma.Domain.Classes;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Disparo.Plataforma.Infrastructure.Repositories.MongoDb.Classes.Models
{
    /// <summary>Modelo que representa uma classe na base de dados.</summary>
    public class ClasseModel
    {
        /// <summary>Código de identificação de uma classe.</summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set;}

         /// <summary>Nome do curso em que o aluno está matriculado.</summary>
        public string Habilitacao { get; set; }

        /// <summary>Ano vigente.</summary>
        public int AnoOC { get; set; }

        /// <summary>Módulo e série vigentes.</summary>
        public string ModuloSerie { get; set; }

        // <summary>Converte uma classe no modelo do contexto Mongo para uma classe no domínio.</summary>
        /// <param name="classeModel">Classe no modelo do contexto Mongo.</param>
        public static implicit operator Classe(ClasseModel classeModel)
        {
            if (classeModel == null)
                return null;

            return new Classe(
                classeModel.Habilitacao,
                classeModel.AnoOC,
                classeModel.ModuloSerie     
            );
        }
    }
}