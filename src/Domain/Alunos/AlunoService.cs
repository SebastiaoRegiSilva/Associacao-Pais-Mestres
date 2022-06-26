using Disparo.Plataforma.Domain.Classes;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Disparo.Plataforma.Domain.Alunos
{
    /// <summary>Serviço que provê acesso aos dados dos alunos.</summary>
    public class AlunoService
    {
        /// <summary>Repositório para armazenamento dos alunos.</summary>
        private readonly IAlunoRepository _alunoRep;
        
        /// <summary>Construtor com injeção de dependência.</summary>
        /// <param name="alunoRep">Repositório para armazenamento dos alunos.</param>
        public AlunoService(IAlunoRepository alunoRep)
        {
            _alunoRep = alunoRep;
        }
    
        /// <summary>Cadastra no repositório um novo aluno.</summary>
        /// <param name="matricula">Matrícula do aluno.</param>
        /// <param name="nome">Nome.</param>
        /// <param name="dataNascimento">Data de nascimento do aluno.</param>
        /// <param name="enderecoEmail">Endereço de e-mail institucional.</param>
        /// <param name="numerosTelefones">Números para comunicação direta com o aluno.</param>
        /// <param name="classe">Classe onde o aluno foi matriculado.</param>
        /// <returns>Código de identificação gerado para um aluno cadastrado.</returns>
        public async Task<string> CadastrarAlunoAsync(string matricula, string nome, DateTime dataNascimento, 
        string enderecoEmail, IEnumerable<string> numerosTelefones, Classe classe)
        {
            var idConta = await _alunoRep.CadastrarAlunoAsync(matricula, nome, dataNascimento, enderecoEmail, numerosTelefones, classe);

            return idConta;
        }

        /// <param name="nome">Nome.</param>
        /// <param name="dataNascimento">Data de nascimento do aluno.</param>
        /// <param name="enderecoEmail">Endereço de e-mail institucional.</param>
        /// <param name="numerosTelefones">Números para comunicação direta com o aluno.</param>
        /// <param name="classe">Classe onde o aluno foi matriculado.</param>
        public async Task EditarAlunoAsync(string matricula, string nome, DateTime dataNascimento, 
        string enderecoEmail, IEnumerable<string> numerosTelefones, Classe classe)
        {
            await _alunoRep.EditarAlunoAsync(matricula, nome, dataNascimento, enderecoEmail, numerosTelefones, classe);
        }

        /// <summary>Recupera no repositório um aluno cadastrado com base na matrícula.</summary>
        /// <param name="matricula">Matrícula do aluno.</param>
        public async Task<Aluno>RecuperarAlunoMatriculaAsync(string matricula)
        {
            return await _alunoRep.RecuperarAlunoMatriculaAsync(matricula);
        }

        /// <summary>
        /// Recuperar no repositório todos os alunos cadastrados.
        /// </summary>
        public async Task<IEnumerable<Aluno>> RecuperarTodosAlunoAsync()
        {
            return await _alunoRep.RecuperarTodosAsync();
        }

        /// <summary>Exclui no repositório um aluno cadastrado no sistema com base na matrícula.</summary>
        /// <param name="matricula">Matrícula do aluno.</param>
        public async Task ExcluirAlunoAsync(string matricula)
        {
            await _alunoRep.ExcluirAlunoAsync(matricula);
        }

        /// <summary>Exclui no repositório todos alunos cadastrados.</summary>
        public async Task ExcluirTodosAlunosAsync()
        {
            await _alunoRep.ExcluirTodosAlunosAsync();
        }

        // Validações.
        
        /// <summary>Validar via regex(regular expression) se o formato do endereço de e-mail está correto.</summary>
        /// <param name="email">Endereço de e-mail a ser validado.</param>
        public async Task<bool> ValidarEmailAsync(string email)
        {
            // Melhorar REGEX - etec.sp.gov.br.
            var emailRegex = new Regex(@"^[\w-.]+@([\w-]+.)+[\w-]{2,4}$");
            
            // Forçar o método para se assíncrono.
            await Task.Yield();

            return emailRegex.IsMatch(email);
        }
    }
}