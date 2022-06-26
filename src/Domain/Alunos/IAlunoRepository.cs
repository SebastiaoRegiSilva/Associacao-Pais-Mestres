using Disparo.Plataforma.Domain.Classes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Disparo.Plataforma.Domain.Alunos
{
    /// <summary>Interface que padroniza o repositório dos alunos.</summary>
    public interface IAlunoRepository
    {
        /// <summary>Cadastra no repositório um novo aluno.</summary>
        /// <param name="matricula">Matrícula do aluno.</param>
        /// <param name="nome">Nome.</param>
        /// <param name="dataNascimento">Data de nascimento do aluno.</param>
        /// <param name="enderecoEmail">Endereço de e-mail institucional.</param>
        /// <param name="numerosTelefones">Números para comunicação direta com o aluno.</param>
        /// <param name="classe">Classe onde o aluno foi matriculado.</param>
        Task<string>CadastrarAlunoAsync(string matricula, string nome, DateTime dataNascimento, 
        string enderecoEmail, IEnumerable<string> numerosTelefones, Classe classe);
        
        /// <summary>Edita no repositório um aluno cadastrado.</summary>
        /// <param name="matricula">Matrícula do aluno.</param>
        /// <param name="nome">Nome.</param>
        /// <param name="dataNascimento">Data de nascimento do aluno.</param>
        /// <param name="enderecoEmail">Endereço de e-mail institucional.</param>
        /// <param name="numerosTelefones">Números para comunicação direta com o aluno.</param>
        /// <param name="classe">Classe onde o aluno foi matriculado.</param>
        Task EditarAlunoAsync(string matricula, string nome, DateTime dataNascimento, 
        string enderecoEmail, IEnumerable<string> numerosTelefones, Classe classe);
        
        /// <summary>Recupera no repositório um aluno cadastrado com base na matrícula.</summary>
        /// <param name="matricula">Matrícula do aluno.</param>
        Task<Aluno>RecuperarAlunoMatriculaAsync(string matricula);
        
        /// <summary>Recuperar no repositório todos os alunos cadastrados.</summary>
        Task<IEnumerable<Aluno>>RecuperarTodosAsync();
        
        /// <summary>Exclui no repositório um aluno cadastrado no sistema com base na matrícula.</summary>
        /// <param name="matricula">Matrícula do aluno.</param>
        Task ExcluirAlunoAsync(string matricula);

        /// <summary>Exclui no repositório todos alunos cadastrados.</summary>
        Task ExcluirTodosAlunosAsync();
    }
}