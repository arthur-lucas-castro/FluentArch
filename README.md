
# FluentArch

**FluentArch** é uma biblioteca para verificação de conformidade arquitetural em projetos C#, baseada no compilador Roslyn. Ela permite que arquitetos definam regras arquiteturais de forma fluente, diretamente sobre o código-fonte, com suporte a regras customizadas, granularidade fina e camadas lógicas reutilizáveis.

---

## 📌 Visão Geral

Projetos C# tendem a sofrer erosão arquitetural ao longo do tempo. FluentArch oferece uma abordagem moderna e flexível para ajudar a detectar e corrigir violações arquiteturais antes que elas impactem a manutenibilidade do sistema.

O FluentArch analisa diretamente o código-fonte (.cs), proporcionando:

- Maior precisão na detecção de dependências;
- Definição de **camadas lógicas**;
- Escrita de **regras customizadas**;
- Análise com **granularidade fina**;
- Reutilização das regras.
---

## 🧱 Componentes da API

A escrita de regras no FluentArch segue três componentes principais:

### 1. **Filtros**
Selecionam os elementos a serem analisados. Exemplo:
```csharp
arch.All()
    .ResideInNamespace("MyApp.Services")
    .HaveNameEndingWith("Service");
```

### 2. **Condições**
Definem as restrições arquiteturais:
```csharp
moduloA.Cannot().Depend(moduloB).Check();
```

### 3. **Conectores**
Unem filtros e condições, encerrando a regra:
```csharp
moduloA.Cannot().Depend(moduloB).And().Cannot().Declare(moduloB).Check();
```

---

## ✅ Tipos de Regras Arquiteturais

FluentArch suporta os seguintes tipos de verificação:

### 🧩 Divergência
```csharp
moduloA.OnlyCan().Depend(moduloB);   // Apenas A pode depender de B  
moduloA.CanOnly().Depend(moduloB);   // A só pode depender de B  
moduloA.Cannot().Depend(moduloB);    // A não pode depender de B  
```

### 🔗 Ausência
```csharp
moduloA.Must().Depend(moduloB);      // A deve depender de B
```

### 🔬 Operadores de Dependência
Você pode especificar o tipo exato de dependência:
- `Access` (uso de métodos/atributos)
- `Declare` (declarações de tipos)
- `Create` (instanciação)
- `Extend` (herança)
- `Implement` (interfaces)
- `Throw` (exceções)
- `Handle`, `Derive`, `Depend` (aliases)

---

## 🧠 Regras Customizadas

Crie validações sob medida implementando a interface `ICustomRule`:

```csharp
public class TypeCannotHaveFunctions : ICustomRule
{
    public bool DefineCustomRule(TypeEntityDto type)
    {
        return !type.Functions.Any();
    }
}
```

Uso em uma verificação:
```csharp
arch.All()
    .ResideInNamespace("DTOs")
    .UseCustomRule(new TypeCannotHaveFunctions())
    .Check();
```

---

## 🏗️ Camadas Lógicas

Camadas são agrupamentos lógicos reutilizáveis de tipos:

```csharp
ILayer camadaRepositorio = arch.All()
    .ResideInNamespace("Repositorios.*")
    .And()
    .HaveNameEndingWith("Repositorio");
```

Essas camadas podem ser reutilizadas em múltiplas regras.

---

## 💡 Exemplo Completo

```csharp
var arch = Architecture.Build(solution);

ILayer camadaApi = arch.All().ResideInNamespace("N_Tier.API.*").As("Api");
ILayer camadaApp = arch.All().ResideInNamespace("N_Tier.App.*").As("App");
ILayer camadaData = arch.All().ResideInNamespace("N_Tier.DataAccess.*").As("Data");
ILayer camadaModels = arch.All().ResideInNamespace("N_Tier.App.Models.*").As("Models");

// Regras entre camadas
camadaApi.OnlyCan().Depend(camadaApp);
camadaApp.OnlyCan().Depend(camadaData);
camadaData.Cannot().Depend(camadaApp).And().Cannot().Depend(camadaApi);

// Proibir instanciamento direto de repositórios
arch.All().Cannot().Create(camadaData);

// Exceptions devem herdar de System.Exception
camadaApp.And().ResideInNamespace("N_Tier.App.Exceptions")
          .Must().Extends("System");

// DTOs não devem conter métodos
camadaModels.UseCustomRule(new TypeCannotHaveFunctions());

// Executar todas as verificações
var violations = arch.Check();
```

---

## 🤝 Contribuições

Pull requests são bem-vindos! Consulte o guia de contribuição no repositório oficial para colaborar com o FluentArch.
