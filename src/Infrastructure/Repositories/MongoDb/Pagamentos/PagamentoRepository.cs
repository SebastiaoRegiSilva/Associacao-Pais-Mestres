using Disparo.Plataforma.Domain.Alunos;
using Disparo.Plataforma.Domain.Pagamentos;
using Disparo.Plataforma.Infrastructure.Repositories.MongoDb.Pagamentos.Models;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace Disparo.Plataforma.Infrastructure.Repositories.MongoDb.Pagamentos
{
    /// <summary>Implementação do repositório de pagamentos para o Mongo DB.</summary>
    public class PagamentoRepository : IPagamentoRepository
    {
        /// <summary>Contexto utilizado pelo repositório de pagamentos para acessar a coleção de um classe na base de dados.</summary> 
        private readonly PagamentoContext _ctxPagamento = null;

        /// <summary>Construtor com parâmetros para inicialização.</summary>
        /// <param name="conString">String para conexão com a base de dados.</param>
        /// <param name="database">Nome da base de dados onde se encontra o repositório.</param>
        public PagamentoRepository(string conString, string database)
        {
            _ctxPagamento = new PagamentoContext(conString, database);
        }

        /// <summary>Cadastrar na base de dados um pagamento.</summary>
        /// <param name="ano">Ano vigente da associação à APM.</param>
        /// <param name="valor">Valor da anuidade.</param>
        /// <param name="aluno">Aluno que realizou o pagamento.</param>
        /// <param name="formaDePagamento">Forma que a pessoa realizou o pagamento.</param>
        /// <returns>Código de identificação gerado para um pagamento realizado.</returns>
        public async Task<string> CadastrarPagamentoAsync(int ano, int valor, Aluno aluno, string formaDePagamento)
        {
            var model = new PagamentoModel
            {
                Ano = ano,
                Valor = valor,
                Aluno = aluno,
                FormaDePagamento = formaDePagamento
            };

            await _ctxPagamento.Pagamentos.InsertOneAsync(model);

            return model.Id;
        }

        /// <summary>Edita na base de dados um pagamento com base na matrícula do aluno.</summary>
        /// <param name="matricula">Matrícula do aluno que está cadastrado na APM.</param>
        public async Task EditarPagamentoAsync(string matricula)
        {
            var pagamentoRecuperado = RecuperarPagamentoMatriculaAlunoAsync(matricula);
            
            var builder = Builders<PagamentoModel>.Filter;
            var filter = builder.Eq(p => p.Aluno.Matricula, matricula);

            var update = Builders<PagamentoModel>.Update
                .Set(p => p.Ano, pagamentoRecuperado.Result.Ano)
                .Set(p => p.Valor, pagamentoRecuperado.Result.Valor)
                .Set(p => p.Aluno.Matricula, matricula)
                .Set(p => p.FormaDePagamento, pagamentoRecuperado.Result.FormaDePagamento);

            await _ctxPagamento.Pagamentos.UpdateOneAsync(filter, update);
        }

        /// <summary>Exclui na base de dados um pagamento registrado no sistema com base na matrícula do aluno.</summary>
        /// <param name="matricula">Matrícula do aluno que está cadastrado na APM.</param>
        public async Task ExcluirPagamentoAsync(string matricula)
        {
            var filter = Builders<PagamentoModel>.Filter.Eq(p => p.Aluno.Matricula, matricula);
            
            await _ctxPagamento.Pagamentos.DeleteOneAsync(filter);
        }

        /// <summary>Recuperar na base de dados um pagamento com base na matrícula do aluno.</summary>
        /// <param name="matricula">Matrícula do aluno que está cadastrado na APM.</param>
        /// <returns>Pagamento recuperado.</returns>
        public async Task<Pagamento> RecuperarPagamentoMatriculaAlunoAsync(string matricula)
        {
            var builder = Builders<PagamentoModel>.Filter;
            var filter = builder.Eq(p => p.Aluno.Matricula, matricula);
            
            return await _ctxPagamento.Pagamentos
                .Aggregate()
                .Match(filter)
                .FirstOrDefaultAsync();
        }
    }
}