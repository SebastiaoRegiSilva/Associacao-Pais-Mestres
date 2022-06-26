using Disparo.Plataforma.Domain.Alunos;
using Disparo.Plataforma.Domain.Predios;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Disparo.Plataforma.Domain.Armarios
{
    /// <summary>Serviço que provê acesso aos dados dos armários.</summary>
    public class ArmarioService
    {
        /// <summary>Repositório para armazenamento dos armários.</summary>
        private readonly IArmarioRepository _armarioRep;
        
        /// <summary>Serviço que provê acesso aos dados e operações de alunos.</summary>
        private readonly AlunoService _alunoService;

        /// <summary>Serviço que provê acesso aos dados e operaçãoes de prédios.</summary>
        private readonly PredioService _predioService;

        /// <summary>Construtor com injeção de dependência.</summary>
        /// <param name="armarioRep">Repositório para armazenamento dos armários.</param>
        /// <param name="alunoService">Serviço que provê acesso aos dados e operaçãoes de alunos.</param>
        /// <param name="predioService">Serviço que provê acesso aos dados e operaçãoes de prédios.</param>
        public ArmarioService(IArmarioRepository armarioRep, AlunoService alunoService, PredioService predioService)
        {
            _armarioRep = armarioRep;
            _alunoService = alunoService;
            _predioService = predioService;
        }
        
        /// <summary>Cadastra no repositório um novo armário.</summary>
        /// <param name="numeroIdentificador">Número identificador do armário.</param>
        /// <param name="numeroPredio">Prédio onde está localizado.</param>
        /// <param name="anoValidade">Ano vigente com permisssão de uso do armário.</param>
        /// <param name="disponivel">Disponibilidade do armário.</param>
        /// <returns>Código de identificação gerado para um armário cadastrado.</returns>
        public async Task<string>CadastrarArmarioAsync(int numeroPredio, int numeroIdentificador, int anoValidade, bool disponivel)
        {
            var predioRecuperado = await _predioService.RecuperarPredioPorNumeroAsync(numeroPredio);

            var idArmario = await _armarioRep.CadastrarArmarioAsync(numeroIdentificador, predioRecuperado, anoValidade, disponivel);

            return idArmario;
        }
        
        /// <summary>Edita no repositório um armário cadastrado.</summary>
        /// <param name="numeroIdentificador">Número identificador do armário.</param>
        /// <param name="anoValidade">Ano de validade da locação do armário.</param>
        /// <param name="numeroPredio">Prédio onde está localizado do armário.</param>
        public async Task EditarArmarioAsync(int numeroIdentificador, int anoValidade,int numeroPredio)
        {
            var predioRecuperado = await _predioService.RecuperarPredioPorNumeroAsync(numeroPredio);

            await _armarioRep.EditarArmarioAsync(numeroIdentificador, anoValidade, predioRecuperado);
        }
        
        /// <summary>Recupera no repositório um armário cadastrado com base no número.</summary>
        /// <param name="numeroIdentificador">Número identificador do armário.</param>
        public async Task<Armario>RecuperarArmarioNumeroIdentificadorAsync(int  numeroIdentificador)
        {
            return await _armarioRep.RecuperarArmarioNumeroIdentificadorAsync(numeroIdentificador);
        }

         /// <summary>Atribuir aluno a armário no repositório de um armario cadastrado.</summary>
        /// <param name="matricula">Matrícula do aluno que vai locar o armário.</param> 
        /// <param name="numeroIdentificador">Número identificador do armário.</param>
        /// <param name="anoValidade">Ano de validade da locação do armário.</param>
        
        public async Task AtribuirAlunoArmarioAsync(string matricula, int numeroIdentificador, int anoValidade)
        {
            var alunoRecuperado = await _alunoService.RecuperarAlunoMatriculaAsync(matricula);

            await _armarioRep.AtribuirAlunoArmarioAsync(alunoRecuperado, numeroIdentificador, anoValidade);
        }

        /// <summary>Lista dos armários disponíveis no repositório.</summary>
        public async Task<IEnumerable<Armario>> ListarTodosArmariosDisponiveisAsync()
        {
            return await _armarioRep.RecuperarTodosArmariosDisponiveis();
        }

        /// <summary>Recupera no repositório um armário cadastrado com base no nome do aluno.</summary>
        /// <param name="nomeAluno">Nome do aluno responsável pelo armário.</param>
        //Task<Armario>RecuperarArmarioAlunoAsync(int  nomeAluno){}

        /// <summary>Exclui no repositório um armário cadastrado no sistema com base no seu número.</summary>
        /// <param name="numeroIdentificador">Número identificador do armário.</param>
        public async Task ExcluirArmarioAsync(int  numeroIdentificador)
        {
            await _armarioRep.ExcluirArmarioAsync(numeroIdentificador);
        }
    }
}