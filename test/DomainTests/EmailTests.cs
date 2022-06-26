using Disparo.Plataforma.Domain.Emails;
using Disparo.Plataforma.Infrastructure.Repositories.MongoDb.Emails;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;
using System.IO;
using System.Threading;

namespace Disparo.Plataforma.DomainTests
{
    /// <summary>Classe de testes para o domínio de emails.</summary>
    [TestClass]
    public class EmailTests
    {
        /// <summary>Serviço que provê acesso aos dados e operações relacionadas aos emails na plataforma.</summary>
        private static EmailService _emailService = null;

        /// <summary>Método de preparação para os testes.</summary>
        [ClassInitialize]
        public static void PrepareTestsData(TestContext context)
        {
            var ptBR = new CultureInfo("pt-BR");
            Thread.CurrentThread.CurrentCulture = ptBR;
            Thread.CurrentThread.CurrentUICulture = ptBR;
            
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var config = builder.Build();

            string conString = config.GetValue<string>("DB:MongoDB:ConString");
            string database = config.GetValue<string>("DB:MongoDB:Database");

            var emailRepository = new EmailRepository(conString, database);
            _emailService = new EmailService(emailRepository);
        }
    }
}
