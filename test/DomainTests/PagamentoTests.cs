using Disparo.Plataforma.Domain.Pagamentos;
using Disparo.Plataforma.Infrastructure.Repositories.MongoDb.Pagamentos;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;
using System.IO;
using System.Threading;

namespace Disparo.Plataforma.DomainTests
{
    /// <summary>Classe de testes para o domínio de pagamentos.</summary>
    [TestClass]
    public class PagamentoTests
    {
        /// <summary>Serviço que provê acesso aos dados e operações relacionadas aos pagamentos na plataforma.</summary>
        private static PagamentoService _pagamentoService = null;

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

            var pagamentoRepository = new PagamentoRepository(conString, database);
            _pagamentoService = new PagamentoService(pagamentoRepository);
        }
    }
}
