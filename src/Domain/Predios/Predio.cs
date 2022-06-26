namespace Disparo.Plataforma.Domain.Predios
{
    /// <summary>Objeto de valor que representa o endereço de um prédio na ETEC. </summary>
    public class Predio
    {
        /// <summary>Número de identificação do prédio.</summary>
        public int NumeroIdentificador { get; set; }

        /// <summary>Construtor para instanciação de um prédio e acesso a suas informações.</summary>
        /// <param name="numeroIdentificador">Número de identificação do prédio.</param>
        public Predio(int numeroIdentificador)
        {
            NumeroIdentificador = numeroIdentificador;
        }
    }
}