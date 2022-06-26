using Disparo.Plataforma.Domain.Alunos.Exceptions;

namespace Disparo.Plataforma.Domain.Predios.Exceptions
{
    /// <summary> Exceção de negócio lançada na tentativa de atribuição de um número inválido para uma entidade ou objeto de valor.</summary>
    public class IdentificationNumberInvalidException : BusinessException
    {
        /// <summary>Construtor com parâmetros para inicialização.</summary>
        /// <param name="invalidNumber">Endereço de e-mail inválido.</param>dd
        public IdentificationNumberInvalidException(int invalidNumber) :
            base($"O número do prédio {invalidNumber} já existe na base da dados.") {}
    }
}