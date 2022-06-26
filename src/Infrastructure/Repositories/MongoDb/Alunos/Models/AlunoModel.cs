using Disparo.Plataforma.Domain.Alunos;
using Disparo.Plataforma.Domain.Classes;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Disparo.Plataforma.Infrastructure.Repositories.MongoDb.Alunos.Models
{
    /// <summary>Modelo que representa um aluno na base de dados.</summary>
    public class AlunoModel
    {
        /// <summary>Código de identificação de um aluno.</summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set;}
        
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
        
        // <summary>Converte um aluno no modelo do contexto Mongo para um aluno no domínio.</summary>
        /// <param name="alunoModel">Aluno no modelo do contexto Mongo.</param>
        public static implicit operator Aluno(AlunoModel alunoModel)
        {
            if (alunoModel == null)
                return null;

            return new Aluno(
                alunoModel.Matricula,
                alunoModel.Nome,
                alunoModel.DataNascimento,
                alunoModel.EnderecoEmail,
                alunoModel.NumerosTelefones,
                alunoModel.Classe
            );
        }
    }
}