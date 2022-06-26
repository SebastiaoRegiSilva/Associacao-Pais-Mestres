using System.Collections.Generic;
using System.Threading.Tasks;
using Disparo.Plataforma.Domain.Alunos;
using Disparo.Plataforma.Domain.Armarios;
using Disparo.Plataforma.Infrastructure.Repositories.MongoDb.Armarios.Models;
using Disparo.Plataforma.Domain.Predios;
using MongoDB.Driver;

namespace Disparo.Plataforma.Infrastructure.Repositories.MongoDb.Armarios
{
    /// <summary>Implementação do repositório de armários para o Mongo DB.</summary>
    public class ArmarioRepository : IArmarioRepository
    {
        /// <summary>Contexto utilizado pelo base de dados de armários para acessar a coleção de um armário na base de dados.</summary> 
        private readonly ArmarioContext _ctxArmario = null;

        /// <summary>Construtor com parâmetros para inicialização.</summary>
        /// <param name="conString">String para conexão com a base de dados.</param>
        /// <param name="database">Nome da base de dados onde se encontra o base de dados.</param>
        public ArmarioRepository(string conString, string database)
        {
            _ctxArmario = new ArmarioContext(conString, database);
        }

        /// <summary>Cadastra na base de dados um novo armário.</summary>
        /// <param name="numeroIdentificador">Número identificador do armário.</param>
        /// <param name="predio">Prédio onde está localizado.</param>
        /// <param name="anoValidade">Ano vigente com permisssão de uso do armário.</param>
        /// <param name="disponivel">Disponibilidade do armário.</param>
        /// <returns>O código de identificação do armário cadastradado.</returns>
        public async Task<string> CadastrarArmarioAsync(int numeroIdentificador, Predio predio, int anoValidade, bool disponivel)
        {            
            var model = new ArmarioModel
            {
                NumeroIdentificador = numeroIdentificador,
                Predio = predio,
                AnoValidade = anoValidade,
                Disponivel = disponivel
            };

            await _ctxArmario.Armarios.InsertOneAsync(model);

            return model.Id;
        }

        /// <summary>Edita no base de dados um armario cadastrado.</summary>
        /// <param name="numeroIdentificador">Número identificador do armário.</param>
        /// <param name="anoValidade">Ano de validade da locação do armário.</param>
        /// <param name="predio">Prédio onde está localizado do armário.</param>
        public async Task EditarArmarioAsync(int numeroIdentificador, int anoValidade, Predio predio)
        {
            var armarioRecuperado = RecuperarArmarioNumeroIdentificadorAsync(numeroIdentificador);
            
            var builder = Builders<ArmarioModel>.Filter;
            var filter = builder.Eq(a => a.NumeroIdentificador, numeroIdentificador);

            var update = Builders<ArmarioModel>.Update
                .Set(a => a.NumeroIdentificador, armarioRecuperado.Result.NumeroIdentificador)
                .Set(a => a.Predio, predio)
                .Set(a => a.Aluno, armarioRecuperado.Result.Aluno)
                .Set(a => a.AnoValidade,anoValidade);

            await _ctxArmario.Armarios.UpdateOneAsync(filter, update);
        }
        
        /// <summary>Atribuir aluno a armário na base de dados de um armario cadastrado.</summary>
        /// <param name="Aluno">Aluno que vai locar o armário.</param> 
        /// <param name="numeroIdentificador">Número identificador do armário.</param>
        /// <param name="anoValidade">Ano de validade da locação do armário.</param>
        
        public async Task AtribuirAlunoArmarioAsync(Aluno aluno,int numeroIdentificador, int anoValidade)
        {
            var armarioRecuperado = RecuperarArmarioNumeroIdentificadorAsync(numeroIdentificador);
            
            var builder = Builders<ArmarioModel>.Filter;
            var filter = builder.Eq(a => a.NumeroIdentificador, numeroIdentificador);

            var update = Builders<ArmarioModel>.Update
                .Set(a => a.Aluno, aluno)
                .Set(a => a.AnoValidade,anoValidade)
                .Set(a => a.Disponivel, false);

            await _ctxArmario.Armarios.UpdateOneAsync(filter, update);
        }
        
        /// <summary>Recupera na base de dados um armario cadastrado com base no número.</summary>
        /// <param name="numeroIdentificador">Número identificador do armário.</param>
        public async Task<Armario>RecuperarArmarioNumeroIdentificadorAsync(int numeroIdentificador)
        {
            var builder = Builders<ArmarioModel>.Filter;
            var filter = builder.Eq(a => a.NumeroIdentificador, numeroIdentificador);
            
            return await _ctxArmario.Armarios
                .Aggregate()
                .Match(filter)
                .FirstOrDefaultAsync();
        }
        
        /// <summary>Recupera no base de dados um armario cadastrado com base no número.</summary>
        /// <param name="numeroIdentificador">Número identificador do armário.</param>
        public async Task<IEnumerable<Armario>>RecuperarTodosArmariosDisponiveis()
        {
            var builder = Builders<ArmarioModel>.Filter;
            var filter = builder.Eq(a => a.Disponivel, true);
            
            return (IEnumerable<Armario>)await _ctxArmario.Armarios
                .Aggregate()
                .Match(filter)
                .FirstOrDefaultAsync();
        }

        /// <summary>Recupera no base de dados um armário cadastrado com base no nome do aluno.</summary>
        /// <param name="nomeAluno">Nome do aluno responsável pelo armário.</param>
        public async Task<Armario>RecuperarArmarioAlunoAsync(string nomeAluno)
        {
            var builder = Builders<ArmarioModel>.Filter;
            var filter = builder.Eq(a => a.Aluno.Nome, nomeAluno);
            
            return await _ctxArmario.Armarios
                .Aggregate()
                .Match(filter)
                .FirstOrDefaultAsync();
        }

        /// <summary>Exclui no base de dados um armário cadastrado no sistema com base no seu número.</summary>
        /// <param name="numeroIdentificador">Número identificador do armário.</param>
        public async Task ExcluirArmarioAsync(int numeroIdentificador)
        {
            var filter = Builders<ArmarioModel>.Filter.Eq(a => a.NumeroIdentificador, numeroIdentificador);

            await _ctxArmario.Armarios.DeleteOneAsync(filter);
        }
    }
}