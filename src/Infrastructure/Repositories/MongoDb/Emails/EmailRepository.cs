using Disparo.Plataforma.Domain.Alunos;
using Disparo.Plataforma.Domain.Emails;
using Disparo.Plataforma.Infrastructure.Repositories.MongoDb.Emails.Models;
using System.Threading.Tasks;

namespace Disparo.Plataforma.Infrastructure.Repositories.MongoDb.Emails
{
    /// <summary>Implementação do repositório de emails para o Mongo DB.</summary>
    public class EmailRepository : IEmailRepository
    {
        /// <summary>Contexto utilizado pelo repositório de emails para acessar a coleção de um email na base de dados.</summary> 
        private readonly EmailContext _ctxEmail = null;

        /// <summary>Construtor com parâmetros para inicialização.</summary>
        /// <param name="conString">String para conexão com a base de dados.</param>
        /// <param name="database">Nome da base de dados onde se encontra o repositório.</param>
        public EmailRepository(string conString, string database)
        {
            _ctxEmail = new EmailContext(conString, database);
        }

        /// <summary>
        /// Armazena na base de dados um e-mail enviado.
        /// </summary>
        /// <param name="alunoReceptor">Aluno que vai receber o e-mail.</param>
        /// <param name="conteudoEmail">Conteúdo do email.</param>
        /// <param name="tituloEmail">Título do e-mail.</param>
        /// <param name="nomeRemetente">Nome de quem está enviando o e-mail.</param>
        /// <param name="emailRemetente">E-mail de quem está enviando o e-mail.</param>
        /// <param name="senhaRemetente">Senha de quem está enviando o e-mail.</param>
        /// <returns>Código de identificação gerado para um email enviado.</returns>
        public async Task<string> ArmazenarEmailAsync(Aluno alunoReceptor, string conteudoEmail, string tituloEmail, string nomeRemetente, string emailRemetente, string senhaRemetente)
        {
            var model = new EmailModel
            {
                AlunoReceptor = alunoReceptor,
                ConteudoEmail = conteudoEmail,
                TituloEmail = tituloEmail,
                NomeRemetente = nomeRemetente,
                EmailRemetente = emailRemetente,
                SenhaRemetente = senhaRemetente
            };

            await _ctxEmail.Emails.InsertOneAsync(model);

            return model.Id;
        }
    }
}