using Disparo.Plataforma.Domain.Alunos;
using Disparo.Plataforma.Domain.Pagamentos;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Disparo.Plataforma.Infrastructure.Repositories.MongoDb.Pagamentos.Models
{
    /// <summary>Modelo que representa um pagamento na base de dados.</summary>
    public class PagamentoModel
    {
        /// <summary>Código de identificação de um pagamento.</summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set;}

        /// <summary>Ano vigente da associação à APM.</summary>
        public int Ano { get; set; }
        
        /// <summary>Valor da anuidade.</summary>
        public int Valor { get; set; }
        
        /// <summary>Aluno que realizou o pagamento.</summary>
        public Aluno Aluno { get; set; }

        /// <summary>Forma que a pessoa realizou o pagamento.</summary>
        public string FormaDePagamento { get; set; }

        // <summary>Converte um pagamento no modelo do contexto Mongo para um pagamento no domínio.</summary>
        /// <param name="pagamentoModel">Pagamento no modelo do contexto Mongo.</param>
        public static implicit operator Pagamento(PagamentoModel pagamentoModel)
        {
            if (pagamentoModel == null)
                return null;

            return new Pagamento(
                pagamentoModel.Ano,
                pagamentoModel.Valor,
                pagamentoModel.Aluno,
                pagamentoModel.FormaDePagamento
            );
        }
    }
}
