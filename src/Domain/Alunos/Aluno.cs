using Disparo.Plataforma.Domain.Classes;
using System;
using System.Collections.Generic;

namespace Disparo.Plataforma.Domain.Alunos
{    
    /// <summary>Entidade que representa os estudantes.</summary>
    public class Aluno
    {
        /// <summary>Matrícula.</summary>
        public string Matricula { get; set; }

        /// <summary>Nome.</summary>
        public string Nome { get; set; }

        /// <summary>Data de nascimento do aluno.</summary>
        public DateTime DataNascimento { get; set; }

        /// <summary>Números para comunicação direta com o aluno.</summary>
        public IEnumerable<string> NumerosTelefones { get; set; }

        /// <summary>Endereço de e-mail institucional.</summary>
        public string EnderecoEmail { get; set; }

        /// <summary>Classe onde o aluno está matriculado.<summary>
        public Classe Classe { get; set; }

        /// <summary>Construtor com instanciação de um estudante para obter informações.</summary>
        /// <param name="matricula">Matrícula.</param>
        /// <param name="nome">Nome.</param>
        /// <param name="dataNascimento">Data de nascimento do aluno.</param>
        /// <param name="enderecoEmail">Endereço de e-mail institucional.</param>
        /// <param name="numerosTelefones">Números para comunicação direta com o aluno.</param>
        /// <param name="classe">Classe onde o aluno foi matriculado.</param>
        public Aluno(string matricula, string nome, DateTime dataNascimento, string enderecoEmail, IEnumerable<string> numerosTelefones, Classe classe)
        {
            Matricula = matricula;
            Nome = nome;
            DataNascimento = dataNascimento;
            EnderecoEmail = enderecoEmail;
            NumerosTelefones = numerosTelefones;
            Classe = classe;
        }
    }
}