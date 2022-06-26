using Disparo.Plataforma.Domain.Alunos;
using Disparo.Plataforma.Domain.Predios;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Disparo.Plataforma.Domain.Armarios
{
    /// <summary>Interface que padroniza o repositório dos armários.</summary>
    public interface IArmarioRepository
    {
        /// <summary>Cadastra no repositório um novo armário.</summary>
        /// <param name="numeroIdentificador">Número identificador do armário.</param>
        /// <param name="predio">Prédio onde está localizado.</param>
        /// <param name="anoValidade">Ano vigente com permisssão de uso do armário.</param>
        /// <param name="disponivel">Disponibilidade do armário.</param>
        Task<string>CadastrarArmarioAsync(int numeroIdentificador, Predio predio, int anoValidade, bool disponivel);
        
        /// <summary>Edita no repositório um armário cadastrado.</summary>
        /// <param name="numeroIdentificador">Número identificador do armário.</param>
        /// <param name="anoValidade">Ano de validade da locação do armário.</param>
        /// <param name="predio">Prédio onde está localizado do armário.</param>
        Task EditarArmarioAsync(int numeroIdentificador, int anoValidade, Predio predio);
        
        /// <summary>Recupera no repositório um armário cadastrado com base no número.</summary>
        /// <param name="numeroIdentificador">Número identificador do armário.</param>
        Task<Armario>RecuperarArmarioNumeroIdentificadorAsync(int  numeroIdentificador);

        /// <summary>Atribuir aluno a armário no repositório de dados de um armario cadastrado.</summary>
        /// <param name="Aluno">Aluno que vai locar o armário.</param> 
        /// <param name="numeroIdentificador">Número identificador do armário.</param>
        /// <param name="anoValidade">Ano de validade da locação do armário.</param>
        
        Task AtribuirAlunoArmarioAsync(Aluno aluno,int numeroIdentificador, int anoValidade);

        /// <summary>Recupera no repositório um armário cadastrado com base no nome do aluno.</summary>
        /// <param name="nomeAluno">Nome do aluno responsável pelo armário.</param>
        // Task<Armario>RecuperarArmarioAlunoAsync(int  nomeAluno);

        /// <summary>Exclui no repositório um armário cadastrado no sistema com base no seu número.</summary>
        /// <param name="numeroIdentificador">Número identificador do armário.</param>
        Task ExcluirArmarioAsync(int numeroIdentificador);
        
        /// <summary>Lista dos armários disponíveis no repositório.</summary>
        Task<IEnumerable<Armario>> RecuperarTodosArmariosDisponiveis();
    }
}