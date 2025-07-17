# NomeDoProjeto

Uma API fluente para .NET que permite escrever e verificar suas regras arquiteturais
## Índice

- [Visão Geral](#visão-geral)
- [Exemplos](#exemplos)
- [Estrutura das regras](#estrutura)

---

## Visão Geral

Este projeto facilita a aplicação de regras arquiteturais em projetos .NET, utiliza o Roslyn para realizar a analise estatica de codigo e abstrai em uma cadeia de funçoes fluente.

## Exemplos
```
var arch = Architecture.Build(solution);

ILayer camadaApi = arch.All()
    .ResideInNamespace("N_Tier.API.*")
    .As("Api layer");
ILayer camadaApplication = arch.All()
    .ResideInNamespace("N_Tier.App.*")
    .As("App layer");
ILayer camadaDataAccess = arch.All()
    .ResideInNamespace("N_Tier.DataAccess.*")
    .As("DataAccess layer");
ILayer camadaCore = arch.All()
    .ResideInNamespace("N_Tier.Core.*")
    .As("Core layer");
ILayer camadaShared = arch.All()
    .ResideInNamespace("N_Tier.Shared.*")
    .As("Shared layer");
ILayer camadaModels = arch.All()
    .ResideInNamespace("N_Tier.App.Models.*")
    .As("Models layer");

camadaApi
    .OnlyCan().Depend(camadaApplication);
camadaApplication
    .OnlyCan().Depend(camadaDataAccess);
camadaDataAccess
    .Cannot().Depend(camadaApplication)
    .And()
    .Cannot().Depend(camadaApi);
camadaCore
    .Cannot().Depend(camadaDataAccess);
arch.All()
    .Cannot().Create(camadaDataAccess);
camadaApplication
    .And().ResideInNamespace("N_Tier.App.Exceptions")
    .Must().Extends("System");
camadaModels
    .UseCustomRule(new TypeCannotHaveFunctionsRule());

var violations = arch.Check();
```

O codigo acima verifica 3 regras arquiteturais:
- Nenhuma classe do sistema deve instanciar objetos de reposi-
tório; deve-se utilizar injeção de dependência.
- Todas as classes localizadas na camada Application e no na-
mespace N_Tier.Application.Exceptions devem herdar
de Exception.
• Todas as classes do namespace Models não podem conter
métodos.


### Estrutura

#### Iniciar
Para inciar a escrita da regra, é preciso chamar a função `Architecture.Build(solution);` passando uma solution gerada pelo roslyn.

#### Filtros
Apos inicializar a Architecture, é possivel definir camadas utilizandos os filtros.
-Camadas são agrupamentos de tipo.
`ILayer camadaApi = arch.All().ResideInNamespace("API.*").As("Api layer");`
O codigo acima, define uma camada com base no namespace "API" e todos seus sub namespace.
a função As(string) é opicional, porem é recomendada usar para ter uma mensagem violação arquitetural mais personalizada

#### Condições
As codiçoes, são de fato as regras a serem seguidas pelo conjunto de tipo defindos pelos filtros e agrupados nas camadas.
`camadaCore.Cannot().Create(camadaDataAccess);`

A função acima, demonstra a regra de que a camada Core, nao pode criar nenhuma classe da camada de DataAccess. 


#### Conectores
Conectores são os conectorees logicos que permitem a concatenação de filtros e/ou condições.
`arch.All().ResideInNamespace("App.*").And().HaveNameEndingWith("Controller").Cannot().Create(camadaDataAccess).And().Must().Create("Service.*");`

o codigo acima apresenta o uso do conector logico And(), tanto nos filtros para ter uma camada mais restrita, quanto na condição, de modo que tenha mais de uma regra sendo aplicada.

#### Resultados

Para extrair os resultados, foi disponibilizado duas opções:
`arch.Check()`
para obter o resultado de todas as regras daquela arquitetura.

`camadaCore.Cannot().Create(camadaDataAccess).Check();`
Para obter o resultado somente da regra especifica.