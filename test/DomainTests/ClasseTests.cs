using Disparo.Plataforma.Domain.Classes;
using Disparo.Plataforma.Infrastructure.Repositories.MongoDb.Classes;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;
using System.IO;
using System.Threading;

namespace Disparo.Plataforma.DomainTests
{
    /// <summary>Classe de testes para o domínio de classes.</summary>
    [TestClass]
    public class ClasseTests
    {
        /// <summary>Serviço que provê acesso aos dados e operações relacionadas as classes na plataforma.</summary>
        private static ClasseService _classeService = null;

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

            var classeRepository = new ClasseRepository(conString, database);
            _classeService = new ClasseService(classeRepository);
        }
    }
}
