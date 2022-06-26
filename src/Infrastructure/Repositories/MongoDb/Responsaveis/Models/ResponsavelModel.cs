using Disparo.Plataforma.Domain.Responsaveis;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Disparo.Plataforma.Infrastructure.Repositories.MongoDb.Responsaveis.Models
{
    /// <summary>Modelo que representa um responsável na base de dados.</summary>
    public class ResponsavelModel
    {
        /// <summary>Código de identificação de um responsável.</summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set;}

        /// <summary>Cadastro de pessoa física do responsável do aluno.</summary>
        public string Cpf { get; set; }

        /// <summary>Nome do pai ou responsável.</summary>
        public string Nome { get; set; }

        /// <summary>Endereço de e-mail dos pais ou responsáveis.</summary>
        public string EnderecoEmail { get; set; }

        /// <summary>Números para comunicação direta com os responsáveis.</summary>
        public IEnumerable<string> NumerosTelefones { get; set; }

        // <summary>Converte um responsavel no modelo do contexto Mongo para um responsável no domínio.</summary>
        /// <param name="responsavelModel">Responsável no modelo do contexto Mongo.</param>
        public static implicit operator Responsavel(ResponsavelModel responsavelModel)
        {
            if (responsavelModel == null)
                return null;

            return new Responsavel(
                responsavelModel.Cpf,
                responsavelModel.Nome,
                responsavelModel.EnderecoEmail,
                responsavelModel.NumerosTelefones      
            );
        }
    }
}