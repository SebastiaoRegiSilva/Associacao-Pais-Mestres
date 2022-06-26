using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Disparo.Plataforma.Domain.Responsaveis
{   
    /// <summary>Serviço que provê acesso aos dados dos responsáveis.</summary>
    public class ResponsavelService
    {
        /// <summary>Repositório para armazenamento dos responsáveis.</summary>
        private readonly IResponsavelRepository _responsavelRep;
        
        /// <summary>Construtor com injeção de dependência.</summary>
        /// <param name="responsavelRep">Repositório para armazenamento dos responsáveis.</param>
        public ResponsavelService(IResponsavelRepository responsavelRep)
        {
            _responsavelRep = responsavelRep;
        }

        /// <summary>Cadastra no repositório um novo responsável no sistema.</summary>
        /// <param name="cpf">Cadastro de pessoa físíca do responsável do aluno.</param>
        /// <param name="nome">Nome dos pais ou responsáveis.</param>
        /// <param name="enderecoEmail">Endereço de e-mail cdos pais ou responsáveis.</param>
        /// <param name="numerosTelefones">Números para comunicação direta com o aluno.</param>
        /// <returns>Código de identificação gerado para um responsável cadastrado.</returns>
        public async Task<string> CadastrarResponsavelAsync(string cpf, string nome, string enderecoEmail, IEnumerable<string> numerosTelefones)
        {
            var idResponsavel = await _responsavelRep.CadastrarResponsavelAsync(cpf, nome, enderecoEmail, numerosTelefones);
            return idResponsavel;
        }
        
        /// <summary>Recuperar no repositório um responsável com base no CPF.</summary>
        /// <param name="cpf">Cadastro de pessoa física do responsável do aluno.</param>
        /// <returns>Responsável recuperado.</returns>
        public async Task<Responsavel> RecuperarResponsavelPorCPFAsync(string cpf)
        {
            return await _responsavelRep.RecuperarResponsavelPorCPFAsync(cpf);
        }
        
        /// <summary>Edita no repositório um responsável com base no CPF.</summary>
        /// <param name="cpf">Cadastro de pessoa física do responsável do aluno.</param>
        public async Task EditarResponsavelAsync(string cpf)
        {
            await _responsavelRep.EditarResponsavelAsync(cpf);
        }
        
        /// <summary>Exclui no repositório um responsável cadastrado no sistema com base no CPF.</summary>
        /// <param name="cpf">Cadastro de pessoa física do responsável do aluno.</param>
        public async Task ExcluirResponsavelAsync(string cpf)
        {
            await _responsavelRep.ExcluirResponsavelAsync(cpf);
        }
    
        // Validações.

        /// <summary>Validar via regex(regular expression) se o formato do endereço de e-mail está correto.</summary>
        /// <param name="email">Endereço de e-mail a ser validado.</param>
        public async Task<bool> ValidarEmailAsync(string email)
        {
            var emailRegex = new Regex(@"^[\w-.]+@([\w-]+.)+[\w-]{2,4}$");
            
            // Forçar o método para se assíncrono.
            await Task.Yield();

            return emailRegex.IsMatch(email);
        }

        /// <summary>Validar se o formato do CPF está correto.</summary>
        /// <param name="cpf">CPF a ser validado.</param>
        public async Task<bool> ValidarCPFAsync(string cpf)
        {
            /*
                Validação bem simples, só verifica se o formato com os pontos e traços estão corretos.
            */
            var cpfRegex = new Regex(@"^[\w-.]+@([\w-]+.)+[\w-]{2,4}$");
            
            await Task.Yield();

            return cpfRegex.IsMatch(cpf);
        }
    }
}