using Disparo.Plataforma.Domain.Alunos;
using Disparo.Plataforma.Domain.Emails;

namespace Disparo.Plataforma.Infrastructure.Repositories.MongoDb.Emails.Models
{
    /// <summary>Modelo que representa um email na base de dados.</summary>
    public class EmailModel
    {   
        /// <summary>Código de identificação de um email enviado.</summary>
        public string Id { get; set; }
        
        /// <summary>Aluno que vai receber o e-mail.</summary>
        public Aluno AlunoReceptor { get; set; }
        
        /// <summary>Conteúdo do email.</summary>
        public string  ConteudoEmail { get; set; }
        
        /// <summary>Título do e-mail.</summary>
        public string TituloEmail { get; set; }
        
        /// <summary>Nome de quem está enviando o e-mail.</summary>
        public string NomeRemetente { get; set; }
        
        /// <summary>E-mail de quem está enviando o e-mail.</summary>
        public string EmailRemetente { get; set; }
        
        /// <summary>Senha de quem está enviando o e-mail.</summary>
        public string SenhaRemetente { get; set; }

        /// <summary>Converte um email no modelo do contexto Mongo para um email no domínio.</summary>
        /// <param name="emailModel">Email no modelo do contexto Mongo.</param>
        public static implicit operator Email(EmailModel emailModel)
        {
            if (emailModel == null)
                return null;

            return new Email(
                emailModel.AlunoReceptor,
                emailModel.ConteudoEmail,
                emailModel.TituloEmail,
                emailModel.NomeRemetente,
                emailModel.EmailRemetente,
                emailModel.SenhaRemetente
            );
        }
    }
}