using System.Collections.Generic;
using System.Threading.Tasks;

namespace Disparo.Plataforma.Domain.Responsaveis
{
    /// <summary>Interface que padroniza o repositório dos responsáveis.</summary>
    public interface IResponsavelRepository
    {
        /// <summary>Cadastra no repositório um novo responsável no sistema.</summary>
        /// <param name="cpf">Cadastro de pessoa físíca do responsável do aluno.</param>
        /// <param name="nome">Nome dos pais ou responsáveis.</param>
        /// <param name="enderecoEmail">Endereço de e-mail cdos pais ou responsáveis.</param>
        /// <param name="numerosTelefones">Números para comunicação direta com o aluno.</param>
        /// <returns>Código de identificação gerado para um responsável cadastrado.</returns>
        Task<string> CadastrarResponsavelAsync(string cpf, string nome, string enderecoEmail, IEnumerable<string> numerosTelefones);
        
        /// <summary>Recuperar no repositório um responsável com base no CPF.</summary>
        /// <param name="cpf">Cadastro de pessoa física do responsável do aluno.</param>
        /// <returns>Responsável recuperado.</returns>
        Task<Responsavel> RecuperarResponsavelPorCPFAsync(string cpf);
        
        /// <summary>Edita no repositório um responsável com base no CPF.</summary>
        /// <param name="cpf">Cadastro de pessoa física do responsável do aluno.</param>
        Task EditarResponsavelAsync(string cpf);
        
        /// <summary>Exclui no repositório um responsável cadastrado no sistema com base no CPF.</summary>
        /// <param name="cpf">Cadastro de pessoa física do responsável do aluno.</param>
        Task ExcluirResponsavelAsync(string cpf);
    }
}