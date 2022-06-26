using System.Threading.Tasks;
using Disparo.Plataforma.Domain.Alunos;

namespace Disparo.Plataforma.Domain.Emails
{
    /// <summary>Interface que padroniza o repositório dos e-mails.</summary>
    public interface IEmailRepository
    {
        /// <summary>
        /// Armazena no repositório um e-mail enviado.
        /// </summary>
        /// <param name="alunoReceptor">Aluno que vai receber o e-mail.</param>
        /// <param name="conteudoEmail">Conteúdo do email.</param>
        /// <param name="tituloEmail">Título do e-mail.</param>
        /// <param name="nomeRemetente">Nome de quem está enviando o e-mail.</param>
        /// <param name="emailRemetente">E-mail de quem está enviando o e-mail.</param>
        /// <param name="senhaRemetente">Senha de quem está enviando o e-mail.</param>
        /// <returns>Código de identificação do e-mail enviado.</returns>
        Task<string>ArmazenarEmailAsync(Aluno alunoReceptor, string conteudoEmail, string tituloEmail,
        string nomeRemetente, string emailRemetente, string senhaRemetente);
    }
}