using System.Threading.Tasks;

namespace Disparo.Plataforma.Domain.Predios
{
    /// <summary>Serviço que provê acesso aos dados dos prédios.</summary>
    public class PredioService
    {
        /// <summary>Repositório para armazenamento dos prédios.</summary>
        private readonly IPredioRepository _predioRep;
        
        /// <summary>Construtor com injeção de dependência.</summary>
        /// <param name="predioRep">Repositório para armazenamento dos prédios.</param>
        public PredioService(IPredioRepository predioRep)
        {
            _predioRep = predioRep;
        }
        
        /// <summary>Cadastra no repositório um novo prédio no sistema.</summary>
        /// <param name="numeroIdentificador">Número de identificação do prédio.</param>
        /// <returns>Código de identificação gerado para um prédio cadastrado.</returns>
        public async Task<string> CadastrarPredioAsync(int numeroIdentificador)
        {
            var idPredio = await _predioRep.CadastrarPredioAsync(numeroIdentificador);
            
            return idPredio;
        }
        
        /// <summary>Recupera no repositório um prédio com base no número identificador.</summary>
        /// <param name="numeroIdentificador">Número de identificação do prédio.</param>
        /// <returns>Prédio recuperado.</returns>
        public async Task<Predio> RecuperarPredioPorNumeroAsync(int numeroIdentificador)
        {
            return await _predioRep.RecuperarPredioPorNumeroAsync(numeroIdentificador);
        }

        /// <summary>Edita no repositório um prédio com base no número identificador.</summary>
        /// <param name="numeroIdentificador">Número de identificação do prédio.</param>
        public async Task EditarPredioAsync(int numeroIdentificador)
        {
            await _predioRep.EditarPredioAsync(numeroIdentificador);
        }
        
        /// <summary>Exclui no repositório um prédio cadastrado no sistema com base no número identificador.</summary>
        /// <param name="numeroIdentificador">Número de identificação do prédio.</param>
        public async Task ExcluirPredioAsync(int numeroIdentificador)
        {
            await _predioRep.ExcluirPredioAsync(numeroIdentificador);
        }

        /// <summary>Valida se o prédio já existe ou não para operações dentro da aplicação.<summary>
        /// <param name="numeroIdentificador">Número de identificação do prédio.</param>
        /// <returns></returns>
        public async Task<bool> ValidarPredioExiste(int numeroIdentificador)
        {
            bool predioExiste = false;
            
            // Forçar o método para ser assíncrono.
            await Task.Yield();
            
            var predioRecuperado = await _predioRep.RecuperarPredioPorNumeroAsync(numeroIdentificador);

            if(predioRecuperado != null)
                predioExiste = true;
            
            return predioExiste;
        }
    }
}