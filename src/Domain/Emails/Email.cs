using System.Collections.Generic;
using Disparo.Plataforma.Domain.Alunos;

namespace Disparo.Plataforma.Domain.Emails
{
    /// <summary>Entidade que representa o e-mail de está sendo enviado mensagem.</summary>
    public class Email
    {
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

        /// <summary>Construtor para instanciação de um email que será enviado. </summary>
         /// <param name="alunoReceptor">Aluno que vai receber o e-mail.</param>
        /// <param name="conteudoEmail">Conteúdo do email.</param>
        /// <param name="tituloEmail">Título do e-mail.</param>
        /// <param name="nomeRemetente">Nome de quem está enviando o e-mail.</param>
        /// <param name="emailRemetente">E-mail de quem está enviando o e-mail.</param>
        /// <param name="senhaRemetente">Senha de quem está enviando o e-mail.</param>
        public Email(Aluno alunoReceptor, string conteudoEmail, string tituloEmail, string nomeRemetente, string emailRemetente, string senhaRemetente)
        {
            AlunoReceptor = alunoReceptor;
            ConteudoEmail = conteudoEmail;
            TituloEmail = tituloEmail;
            NomeRemetente = nomeRemetente;
            EmailRemetente = emailRemetente;
            SenhaRemetente = senhaRemetente;
        }
    }
}