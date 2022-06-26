using Disparo.Plataforma.Domain.Responsaveis;
using Disparo.Plataforma.Infrastructure.Repositories.MongoDb.Responsaveis;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;
using System.IO;
using System.Threading;

namespace Disparo.Plataforma.DomainTests
{
    /// <summary>Classe de testes para o domínio de responsáveis.</summary>
    [TestClass]
    public class ResponsavelTests
    {
        /// <summary>Serviço que provê acesso aos dados e operações relacionadas aos responsáveis na plataforma.</summary>
        private static ResponsavelService _responsavelService = null;

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

            var responsavelRepository = new ResponsavelRepository(conString, database);
            _responsavelService = new ResponsavelService(responsavelRepository);
        }
    }
}
