using Disparo.Plataforma.Domain.Alunos;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Disparo.Plataforma.Domain.Emails
{
    /// <summary>Serviço que provê acesso aos dados dos emails.</summary>
    public class EmailService
    {
        /// <summary>Repositório para armazenamento dos responsáveis.</summary>
        private readonly IEmailRepository _emailRep;

         /// <summary>Serviço que provê acesso aos dados e operações relaciondas aos alunos.</summary>
        private readonly AlunoService _alunoService;
        
        /// <summary>Construtor com injeção de dependência.</summary>
        /// <param name="responsavelRep">Repositório para armazenamento dos emails.</param>
        /// <param name="alunoService">Injeção de dependência do serviço que provê acesso aos dados e operações relacionadas aos alunos.</param>

        public EmailService(IEmailRepository emailRep, AlunoService alunoService)
        {
            _emailRep = emailRep;
            _alunoService = alunoService;
        }
        
        /// <summary>
        /// Envia e armazena no repositório um e-mail.
        /// </summary>
        /// <param name="matriculaAluno">Matrícula do aluno que vai receber o e-mail</param>
        /// <param name="conteudoEmail">Conteúdo do email.</param>
        /// <param name="tituloEmail">Título do e-mail.</param>
        /// <param name="nomeRemetente">Nome de quem está enviando o e-mail.</param>
        /// <param name="emailRemetente">E-mail de quem está enviando o e-mail.</param>
        /// <param name="senhaRemetente">Senha de quem está enviando o e-mail.</param>
        /// <returns>Código de identificação do e-mail enviado.</returns>
        public async Task<string> ArmazenarEmailAsync(string matriculaAluno, string conteudoEmail, string tituloEmail,
        string nomeRemetente, string emailRemetente, string senhaRemetente)
        {   
            var alunoRecuperado = await _alunoService.RecuperarAlunoMatriculaAsync(matriculaAluno);

            // Dispara e-mail.
            EnviarEmail(alunoRecuperado.EnderecoEmail, conteudoEmail, tituloEmail, nomeRemetente, emailRemetente, senhaRemetente);

            // Lógica para armazenamento do email.
            var idEmail = await _emailRep.ArmazenarEmailAsync(alunoRecuperado, conteudoEmail, tituloEmail, nomeRemetente, emailRemetente, senhaRemetente);

            return idEmail;
        }
        

        /// <summary>Validar se o formato do endereço de e-mail está correto.</summary>
        /// <param name="email">Endereço de e-mail a ser validado.</param>
        public async Task<bool> ValidarEmailAsync(string email)
        {
            var emailRegex = new Regex(@"^[\w-.]+@([\w-]+.)+[\w-]{2,4}$");
            
            await Task.Yield();

            return emailRegex.IsMatch(email);
        }

        // Funçõe privadas do serviço.

        /// <summary>
        /// Envia um e-mail.
        /// </summary>
        /// <param name="matriculaAluno">Matrícula do aluno que vai receber o e-mail.</param>
        /// <param name="conteudoEmail">Conteúdo do email.</param>
        /// <param name="tituloEmail">Título do e-mail.</param>
        /// <param name="nomeRemetente">Nome de quem está enviando o e-mail.</param>
        /// <param name="emailRemetente">E-mail de quem está enviando o e-mail.</param>
        /// <param name="senhaRemetente">Senha de quem está enviando o e-mail.</param>
        private async void EnviarEmail(string matriculaAluno, string conteudoEmail, string tituloEmail,
        string nomeRemetente, string emailRemetente, string senhaRemetente)
        {
            var alunoRecuperado = await _alunoService.RecuperarAlunoMatriculaAsync(matriculaAluno);
            
            MailMessage emailMessage = new MailMessage();

            // Provedor que estará enviando os e-mails.
            var smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.EnableSsl = true;
            smtpClient.Timeout = 60*60; //Timeout em segundos.
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(emailRemetente, senhaRemetente);
            // Endereço de quem vai receber e o nome do quem está enviando.
            emailMessage.From = new MailAddress(alunoRecuperado.EnderecoEmail, nomeRemetente);
            // Conteúdo do e-mail.
            emailMessage.Body = conteudoEmail;
            // Título do e-mail.
            emailMessage.Subject = tituloEmail;
            // Quanto TRUE, o Dev consegue colocar tags HTML no corpo do e-mail, para um tipo personalizado.
            emailMessage.IsBodyHtml = true;
            emailMessage.Priority = MailPriority.Normal;
            emailMessage.To.Add(alunoRecuperado.EnderecoEmail);

            smtpClient.Send(emailMessage);
        }
    }
}