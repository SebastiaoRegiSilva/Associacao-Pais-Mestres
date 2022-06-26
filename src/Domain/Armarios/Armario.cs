using Disparo.Plataforma.Domain.Alunos;
using Disparo.Plataforma.Domain.Predios;

namespace Disparo.Plataforma.Domain.Armarios
{
    /// <summary>Objeto de valor que representa um armário para guarda de objetos pessoais do aluno.</summary>
    public class Armario
    {
        /// <summary>Número identificador do armário.</summary>
        public int NumeroIdentificador { get; set; }

        /// <summary>Prédio onde está localizado.</summary>
        public Predio Predio { get; set; }

        /// <summary>Estudante responsável pelo armário.</summary>
        public Aluno Aluno { get; set; }

        /// <summary>Ano vigente com permisssão de uso do armário.</summary>
        public int AnoValidade { get; set; }

        /// <summary>Disponibilidade do armário.</summary>
        public bool Disponivel { get; set; }

        /// <summary>Construtor com instanciação de um armário para obter informações.</summary>
        public Armario(int numeroIdentificador, Predio predio, int anoValidade, bool disponivel)
        {
            NumeroIdentificador = numeroIdentificador;
            Predio = predio;
            AnoValidade = anoValidade;
            Disponivel = disponivel;
        }
    }
}