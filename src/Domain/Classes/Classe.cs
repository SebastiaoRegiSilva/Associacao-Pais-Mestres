namespace Disparo.Plataforma.Domain.Classes
{
    /// <summary>Objeto de valor que representa uma classe.</summary>
    public class Classe
    {
        /// <summary>Nome do curso em que o aluno está matriculado.</summary>
        public string Habilitacao { get; set; }

        /// <summary>Ano vigente.</summary>
        public int AnoOC { get; set; }

        /// <summary>Módulo e série vigentes.</summary>
        public string ModuloSerie { get; set; }

        /// <summary>Construtor com parâmetros para instancição de uma classe e suas informações.</summary>
        /// <param name="habilitacao">Nome do curso em que o aluno está matriculado.</param>
        /// <param name="anoOC">Ano vigente.</param>
        /// <param name="moduloSerie">Módulo e série vigentes.</param>
        public Classe(string habilitacao, int anoOC, string moduloSerie)
        {
            Habilitacao = habilitacao;
            AnoOC = anoOC;
            ModuloSerie = moduloSerie;
        }
    }
}