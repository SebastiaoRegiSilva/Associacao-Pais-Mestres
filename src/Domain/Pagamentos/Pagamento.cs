using Disparo.Plataforma.Domain.Alunos;

namespace Disparo.Plataforma.Domain.Pagamentos
{
    /// <summary>Entidade que representa os pagamentos.</summary>
    public class Pagamento
    {
        /// <summary>Ano vigente da associação à APM.</summary>
        public int Ano { get; set; }
        
        /// <summary>Valor da anuidade.</summary>
        public int Valor { get; set; }
        
        /// <summary>Aluno que realizou o pagamento.</summary>
        public Aluno Aluno { get; set; }

        /// <summary>Forma que a pessoa realizou o pagamento.</summary>
        public string FormaDePagamento { get; set; }
    
        /// <summary>
        /// Construtor com parâmetro para inicialização.
        /// </summary>
        /// <param name="ano">Ano vigente da associação à APM.</param>
        /// <param name="valor">Valor da anuidade.</param>
        /// <param name="aluno">Aluno que realizou o pagamento.</param>
        /// <param name="formaDePagamento">Forma que a pessoa realizou o pagamento.</param>
        public Pagamento(int ano, int valor, Aluno aluno, string formaDePagamento) 
        {
            Ano = ano;
            Valor = valor;
            Aluno = aluno;
            FormaDePagamento = formaDePagamento;
            // Implementar data de pagamento.
        }
    }
}