[repo]:https://github.com/edkardoso/kchef

# FUSC - Fluent Use Case - 2.0.0

--------------------------------------

> Esta biblioteca tem o objetivo de organizar a escrita do código segundo os conceitos
da **Clean Architecture**, separando as responsabilidades em UseCase, Validator e Presenter.
## Componentes

- **UseCase** - É o principal componente da biblioteca. Deve conter toda a lógica da funcionalidade, distribuída nos _métodos de ação_.


- **Validator** - É o componente encarregado por validar os dados de entrada, enviando mensagens classificadas de acordo com a sua severidade. Seu uso é opcional.
- **Presenter** - Responsável por formatar a saída do resultado produzido pelo _UseCase_. Seu uso também é opcional.
- **MediatorUseCase** - Coordena toda a parte de iteração entre os componentes, injetando depedências, publicando eventos, permitindo manter o fraco acomplamento dos componentes. Embora não seja obrigatório o FUSC perde muito do seu poder sem esse componente.    

### Boas Práticas
O FUSC usa boas práticas de codificação e Padrões de Projetos, sendo desenvolvido de forma totalmente testável e flexível para se adequar as customizações que se fizerem necessárias. Além disso, foi desenvolvido sem a inclusão de bibliotecas de terceiros para evitar problemas de interdependencias.
buscando, sobre tudo, diminuir o acomplamento entre as partes,
padronizar as soluções possibilitando organizar cada característica
- [x] Template Método
- [x] Mediator
- [x] Object Null
- [x] Observer
- [x] Classes de no máximo 100 linhas (desconsiderando comentários)
- [x] Um único Try/Catch para toda biblioteca
- [x] Testes unitários
- [x] Suporte a versão .NET 6.0  

### Criação de UseCase

A criação de UseCase pode ser realizada através do comando
File > New > UseCase.
Ou codificando a seguinte estrutura:

#### Caso de uso para calcular o quadrado de um número
Possui um valor de entrada e de saída do tipo _int_
```
internal class SimpleUseCase : UseCase<int, int>
{
    protected override string NameUseCase => "SimpleUseCase";

    public override Task<int> OnExecuteAsync(int input, CancellationToken cancellationToken)
        => Task.FromResult(input * input);
}


```

A lógica da funcionalidade deve ser implementada no método de ação _**OnExecuteAsync**_. Existem outros métodos de acão opcionais como:
- _**OnActionBeforeStart**_ - Ação a ser executada antes da execução do método _OnExecuteAsync_;
- _**OnActionComplete**_ - Ação que sempre será executada depois do método _OnExecuteAsync_, mesmo que ocorra um erro;
- _**OnActionException**_ - Ação que será invocada se houver uma exceção;

Os métodos de ação nos permite uma ótima maneira de organizar o nosso código. Vamos discutir sobre eles no próximo tópico.

Agora, voltando para a construção dos _UseCases_, é normal nos depararmos com cenários onde não existem valor de entrada ou de saída (ou ainda ambos). Para esses cenários deve-se usar o struc _NoValue_ para representar essa condição.
```
internal class SimpleUseCase : UseCase<int, NoValue>{}

internal class SimpleUseCase : UseCase<NoValue, int>{}

internal class SimpleUseCase : UseCase<NoValue, NoValue>{}


````
Desta maneira já estamos aptos a criar os nossos _UseCases_.

### Métodos de Ação

#### _**OnActionBeforeStart**_ 
> **Opcional**. Qualquer comportamento que anteceda o inicío da lógica principal deveria ser neste método, como por exemplo um registro de log ou ainda uma validação
> dos dados de entrada. Contudo, para um cenário de validação recomendamos a utilização do componente _Validator_. Veja mais sobre ele no tópico correspondente.
> Parâmetros do método:
> - **_input_**: dados de entrada
>- **_user_** : Dados do usuário (é **obrigatório** o uso do componente _Mediator_ para ter essa informação)

#### _**OnExecuteAsync**_
> **Obrigatório**. Deve conter a principal lógica da funcionalidade. Para uma melhor organização, no seu desenvolvimento, devem-se respeitar as fronteiras de cada método, restrigindo
o seu escopo a cada ação correspondente. 
> O retorno deste método deverá ser o mesmo tipado no _UseCase_ e ficará disponível na propriedade Output do _Presenter_ associado. O FUSC garantirá que todo _UseCase_ sempre tenha
um _Presenter_ por padrão. Saiba mais detalhes no tópico Presenter.

#### _**OnActionException**_
> **Opcional**. Se durante a execução de do código implementado no _UseCase_ ocorrer uma exceção o método correspondente a esta ação será disparado.
> Parâmetros do método:
> - **_exception_**: Exceção ocorrida
> - **_input_**: dados de entrada
>- **_user_** : Dados do usuário (é **obrigatório** o uso do componente _Mediator_ para ter essa informação)

> Caso deseje interromper o fluxo do caso de uso retorne _False_. O padrão é _True_.


#### _**OnActionComplete**_ 


Com exceção ao método _OnExecuteAsync_, todos os demais exigem um retorno boleano.
Para **continuar** o fluxo normal retorne **_True_** ou o método pai que tem esse comportamento como padrão.
Se desejar **interromper** o fluxo do _UseCase_ retorne **_False_**.

builder.Services.AddMediatorUseCase((mediator) =>
{
    mediator.Services.AddScoped<GetProductsUseCase>();
    mediator.Builder();

});
```
