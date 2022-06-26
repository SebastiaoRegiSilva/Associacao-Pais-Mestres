using Disparo.Plataforma.Domain.Predios;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Disparo.Plataforma.Infrastructure.Repositories.MongoDb.Predios.Models
{
    /// <summary>Modelo que representa um prédio na base de dados.</summary>
    public class PredioModel
    {
        /// <summary>Código de identificação de um prédio.</summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set;}

        /// <summary>Número de identificação do prédio.</summary>
        public int NumeroIdentificador { get; set; }

        // <summary>Converte um prédio no modelo do contexto Mongo para um prédio no domínio.</summary>
        /// <param name="predioModel">Prédio no modelo do contexto Mongo.</param>
        public static implicit operator Predio(PredioModel predioModel)
        {
            if (predioModel == null)
                return null;

            return new Predio(
                predioModel.NumeroIdentificador     
            );
        }
    }
}