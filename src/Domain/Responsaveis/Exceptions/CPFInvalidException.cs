namespace Disparo.Plataforma.Domain.Responsaveis.Exceptions
{
    /// <summary> Exceção de negócio lançada na tentativa de atribuição de um endereço de CPF inválido para uma entidade ou objeto de valor.</summary>
    public class CPFInvalidException : BusinessException
    {
        /// <summary>Construtor com parâmetros para inicialização.</summary>
        /// <param name="CPFInvalid">CPF inválido.</param>
        public CPFInvalidException(string CPFInvalid) :
            base("O CPF informado está no formato incorreto.") {}
    }
}