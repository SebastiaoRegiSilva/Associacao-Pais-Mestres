O que é domínio?

A **classe de domínio** nada mais é do que uma representação de uma **entidade de negócio**. Por exemplo, no caso de um sistema para uma pizzaria, provavelmente teríamos classes de domínio como pizza, bebida, cliente, ingredientes, pedido, entre outras.

Diferença entre Entidade e Objeto de Valor.

Uma Entidade, além de possuir um Identificador, possui o seu tempo de vida dentro do dominio. 
Toda entidade, possui acontecimentos que houveram em seu ciclo de existência, ou seja, as Entidades tem uma história de vida.

Quanto aos Objetos de Valor, esses não possuem nenhum tipo de vinculo com o ciclo de vida do dominio ou aplicação. 
Todo e qualquer objeto de valor, devido sua Imutabilidade (tema abordado no próximo tópico abaixo), podem ser destruídos e criados novamente.
Imutabilidade do Objeto de Valor: Como o próprio nome já diz por si, 
Imutabilidade é a capacidade do objeto de valor não permitir a alteração dos valores de suas propriedades, 
até que um novo objeto seja criado e receba novos dados em seu construtor.

# Importante.
 O meu suposto Objeto de Valor necessita mudar seu estado e seus valores. Se Sim, ele não é um Objeto de Valor. Lembre-se: Um Objeto de Valor,**NÃO** pode permitir que seus valores sejam alterados para a mesma instância.

A grande questão que guia a distinção de existência entre uma Entidade e um Objeto de Valor, 
é que um OV não tem vida própria, ou seja, sua existência depende essencialmente a existência de uma ou mais entidades.