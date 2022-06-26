using Disparo.Plataforma.Domain.Alunos;
using Disparo.Plataforma.Domain.Armarios;
using Disparo.Plataforma.Domain.Predios;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Disparo.Plataforma.Infrastructure.Repositories.MongoDb.Armarios.Models
{
    /// <summary>Modelo que representa um armário na base de dados.</summary>
    public class ArmarioModel
    {
        /// <summary>Código de identificação de um armário.</summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set;}

        /// <summary>Número identificador do armário.</summary>
        public int NumeroIdentificador { get; set; }

        /// <summary>Prédio onde está localizado.</summary>
        public Predio Predio { get; set; }

        /// <summary>Estudante responsável pelo armário.</summary>
        public Aluno Aluno { get; set; }

        /// <summary>Ano vigente com permisssão de uso do armário.</summary>
        public int AnoValidade { get; set; }
        
        /// <summary>Identificar que tal armário já possui um responsável.</summary>
        public bool Disponivel { get; set; }

        // <summary>Converte um armário no modelo do contexto Mongo para um armário no domínio.</summary>
        /// <param name="armarioModel">Armário no modelo do contexto Mongo.</param>
        public static implicit operator Armario(ArmarioModel armarioModel)
        {
            if (armarioModel == null)
                return null;

            return new Armario(
                armarioModel.NumeroIdentificador,
                armarioModel.Predio,
                armarioModel.AnoValidade,
                armarioModel.Disponivel
            );
        }
    }
}