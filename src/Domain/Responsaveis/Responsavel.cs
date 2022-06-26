using System.Collections.Generic;

namespace Disparo.Plataforma.Domain.Responsaveis
{
    /// <summary>Objeto de valor que representa os pais ou responsáveis pelos alunos.</summary>
    public class Responsavel
    {
        /// <summary>Cadastro de pessoa física do responsável do aluno.</summary>
        public string Cpf { get; set; }

        /// <summary>Nome dos pai ou responsável.</summary>
        public string Nome { get; set; }

        /// <summary>Endereço de e-mail do pai ou responsável.</summary>
        public string EnderecoEmail { get; set; }

        /// <summary>Números para comunicação direta com os responsáveis.</summary>
        public IEnumerable<string> NumerosTelefones { get; set; }
        
        /// <summary>Construtor com parâmetros para instanciação de um responsável e suas informações.</summary>
        /// <param name="cpf">Cadastro de pessoa físíca do responsável do aluno.</param>
        /// <param name="nome">Nome dos pai ou responsável.</param>
        /// <param name="enderecoEmail">Endereço de e-mail do pai ou responsável.</param>
        /// <param name="numerosTelefones">Números para comunicação direta com os responsáveis.</param>
        public Responsavel(string cpf, string nome, string enderecoEmail, IEnumerable<string> numerosTelefones)
        {
            Cpf = cpf;
            Nome = nome;
            EnderecoEmail = enderecoEmail;
            NumerosTelefones = numerosTelefones;
        }
    }
}