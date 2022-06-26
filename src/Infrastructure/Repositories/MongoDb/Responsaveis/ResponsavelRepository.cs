using Disparo.Plataforma.Domain.Responsaveis;
using Disparo.Plataforma.Infrastructure.Repositories.MongoDb.Responsaveis.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Disparo.Plataforma.Infrastructure.Repositories.MongoDb.Responsaveis
{
    /// <summary>Implementação do repositório de responsáveis para o Mongo DB.</summary>
    public class ResponsavelRepository : IResponsavelRepository
    {
        /// <summary>Contexto utilizado pelo repositório de responsáveis para acessar a coleção de um responsável na base de dados.</summary> 
        private readonly ResponsavelContext _ctxResponsavel = null;

        /// <summary>Construtor com parâmetros para inicialização.</summary>
        /// <param name="conString">String para conexão com a base de dados.</param>
        /// <param name="database">Nome da base de dados onde se encontra o repositório.</param>
        public ResponsavelRepository(string conString, string database)
        {
            _ctxResponsavel = new ResponsavelContext(conString, database);
        }

        /// <summary>Cadastra no base de dados um novo responsável no sistema.</summary>
        /// <param name="cpf">Cadastro de pessoa físíca do responsável do aluno.</param>
        /// <param name="nome">Nome dos pais ou responsáveis.</param>
        /// <param name="enderecoEmail">Endereço de e-mail cdos pais ou responsáveis.</param>
        /// <param name="numerosTelefones">Números para comunicação direta com o aluno.</param>
        /// <returns>Código de identificação gerado para um responsável cadastrado.</returns>
        public async Task<string> CadastrarResponsavelAsync(string cpf, string nome, string enderecoEmail, IEnumerable<string> numerosTelefones)
        {
            var model = new ResponsavelModel
            {
                Cpf = cpf,
                Nome = nome,
                EnderecoEmail = enderecoEmail,
                NumerosTelefones = numerosTelefones
            };

            await _ctxResponsavel.Responsaveis.InsertOneAsync(model);

            return model.Id;
        }

        /// <summary>Edita na base de dados um responsável com base no CPF.</summary>
        /// <param name="cpf">Cadastro de pessoa física do responsável do aluno.</param>
        public async Task EditarResponsavelAsync(string cpf)
        {
            var responsavelRecuperado = RecuperarResponsavelPorCPFAsync(cpf);
            
            var builder = Builders<ResponsavelModel>.Filter;
            var filter = builder.Eq(r => r.Cpf, cpf);

            var update = Builders<ResponsavelModel>.Update
                .Set(r => r.Cpf, responsavelRecuperado.Result.Cpf)
                .Set(r => r.Nome, responsavelRecuperado.Result.Nome)
                .Set(r => r.EnderecoEmail, responsavelRecuperado.Result.EnderecoEmail)
                .Set(r => r.NumerosTelefones, responsavelRecuperado.Result.NumerosTelefones);

            await _ctxResponsavel.Responsaveis.UpdateOneAsync(filter, update);
        }

        /// <summary>Exclui na base de dados um responsável cadastrado no sistema com base no CPF.</summary>
        /// <param name="cpf">Cadastro de pessoa física do responsável do aluno.</param>
        public async Task ExcluirResponsavelAsync(string cpf)
        {
            var filter = Builders<ResponsavelModel>.Filter.Eq(r => r.Cpf, cpf);

            await _ctxResponsavel.Responsaveis.DeleteOneAsync(filter);
        }

        /// <summary>Recuperar na base de dados um responsável com base no CPF.</summary>
        /// <param name="cpf">Cadastro de pessoa física do responsável do aluno.</param>
        public async Task<Responsavel> RecuperarResponsavelPorCPFAsync(string cpf)
        {
            var builder = Builders<ResponsavelModel>.Filter;
            var filter = builder.Eq(r => r.Cpf, cpf);
            
            return await _ctxResponsavel.Responsaveis
                .Aggregate()
                .Match(filter)
                .FirstOrDefaultAsync();
        }
    }
}