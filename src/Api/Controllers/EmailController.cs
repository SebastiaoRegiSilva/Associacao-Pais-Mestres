using Disparo.Plataforma.Domain.Emails;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Disparo.Plataforma.Api.Controllers
{
    /// <summary>Controller que provê endpoints relacionados entidade email.</summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : Controller
    {
        // <summary>Serviço que provê acesso aos dados e operações relaciondas aos emails.</summary>
        private readonly EmailService _emailService;

        /// <summary>Construtor com parâmetros para inicialização.</summary>
        /// <param name="emailService">Injeção de dependência do serviço que provê acesso aos dados e operações relacionadas aos emails.</param>
        public EmailController(EmailService emailService)
        {
            _emailService = emailService;
        }

        /// <summary>
        /// Envia uma email e o armazena no repositório.(Provisório - Depois criar uma collection para armazenar usuários) 
        /// </summary>
        /// <param name="matriculaAluno">Matrícula do aluno que vai receber o e-mail</param>
        /// <param name="conteudoEmail">Conteúdo do email.</param>
        /// <param name="tituloEmail">Título do e-mail.</param>
        /// <param name="nomeRemetente">Nome de quem está enviando o e-mail.</param>
        /// <param name="emailRemetente">E-mail de quem está enviando o e-mail.</param>
        /// <param name="senhaRemetente">Senha de quem está enviando o e-mail.</param>
        /// <returns>Envia e armazena emails conforme os parâmetros supracitados.</returns>
        [HttpPost]
        public async Task<IActionResult> EnviarEmail(string matriculaAluno, string conteudoEmail, string tituloEmail,
        string nomeRemetente = "Associação Fake",string emailRemetente = "associacaoTesteFake@gmail.com" , string senhaRemetente = "atireiOP@uNoGato2022")
        {
            await _emailService.ArmazenarEmailAsync(matriculaAluno, conteudoEmail, tituloEmail, nomeRemetente, emailRemetente, senhaRemetente);        
                return Json("Email enviado com sucesso!");
        }
    }
}