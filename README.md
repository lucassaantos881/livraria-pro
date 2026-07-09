# 📚 Livraria — Sistema de Gerenciamento

Sistema de gerenciamento de livraria com controle de estoque de livros físicos e digitais, e gestão completa de pedidos.

Projeto desenvolvido para estudo de arquitetura em camadas com .NET, cobrindo desde a modelagem de domínio até o consumo da API por uma aplicação web.

---

## 🎯 Funcionalidades

### Livros
- CRUD completo de livros físicos e digitais
- Controle de estoque (quantidade disponível)
- Cálculo de preço diferenciado por tipo de livro
- Busca por ID

### Pedidos
- Criação de pedido com múltiplos itens
- Consulta de pedido por ID com detalhamento dos itens
- Cálculo do valor total do pedido
- Despacho do pedido (altera o status)
- Exclusão de pedidos (apenas os que ainda estão em processamento de pagamento)

---

## 🏗️ Arquitetura

O projeto está dividido em quatro camadas, cada uma com uma responsabilidade única:

```
LivrariaCore    →  Modelos de domínio, DTOs e regras de negócio
LivrariaApi     →  API REST, controllers, services e acesso a dados
LivrariaApp     →  Interface web (Blazor WebAssembly)
LivrariaTests   →  Testes unitários
```

Essa separação permite que a lógica de domínio (`LivrariaCore`) seja compartilhada entre a API e o front-end sem duplicação de código, e mantém a camada de apresentação independente da persistência.

---

## 🧬 Modelagem de domínio

A hierarquia de classes usa herança para representar os diferentes tipos de produto:

```
Produto
  └── Livro
       ├── LivroFisico    →  preço final = preço + frete
       └── LivroDigital   →  preço final = preço - 15% de desconto
```

Cada subclasse implementa seu próprio `CalculoPrecoUnitario()`, aplicando a regra de precificação correspondente. É um caso prático de **polimorfismo**: o sistema chama o mesmo método sem precisar saber qual tipo de livro está tratando.

---

## 🔄 Fluxo de status do pedido

```
PROCESSANDO_PAGAMENTO  →  EM_TRANSITO
```

- Um pedido nasce em `PROCESSANDO_PAGAMENTO`
- A ação de **despachar** move o pedido para `EM_TRANSITO`
- Apenas pedidos em `PROCESSANDO_PAGAMENTO` podem ser excluídos — depois de despachados, o pedido não pode mais ser cancelado

---

## 🛠️ Tecnologias

| Camada | Tecnologia |
|--------|-----------|
| Back-end | ASP.NET Core Web API |
| Front-end | Blazor WebAssembly |
| ORM | Entity Framework Core |
| Banco de dados | SQLite |
| Logging | Serilog (gravação em arquivo) |
| Testes | xUnit |
| Documentação da API | Swagger |

---

## ⚙️ Decisões técnicas

**Por que SQLite?**  
Por ser um banco baseado em arquivo, não exige instalação nem configuração de servidor. Isso permite clonar o repositório e executar o projeto imediatamente, o que faz sentido para um projeto de estudo e demonstração. Em produção, a escolha natural seria PostgreSQL ou SQL Server.

**Middleware de tratamento global de exceções**  
Em vez de espalhar blocos `try/catch` por todos os controllers, o `ExceptionMiddleware` intercepta todas as requisições. Quando uma exceção não tratada ocorre, ele:
1. Registra o erro nos logs
2. Retorna uma resposta JSON padronizada ao cliente

Isso mantém os controllers limpos e garante que a API nunca exponha stack traces ao consumidor.

**Separação em Core**  
Os modelos e DTOs vivem em uma Class Library isolada, consumida tanto pela API quanto pelo front-end Blazor. Assim, uma mudança no contrato de dados se propaga para as duas pontas sem risco de divergência.

---

## 🚀 Como executar

**Pré-requisitos:** .NET SDK 8.0 ou superior

```bash
# Clone o repositório
git clone https://github.com/SEU-USUARIO/livraria.git
cd livraria

# Aplique as migrations e crie o banco
cd LivrariaApi
dotnet ef database update

# Execute a API
dotnet run
```

A API estará disponível em `https://localhost:7020`.

Em outro terminal:

```bash
cd LivrariaApp
dotnet run
```

---

## 📌 Próximos passos

- [ ] Adicionar status `ENTREGUE` ao fluxo de pedidos
- [ ] Autenticação e autorização
- [ ] Ampliar a cobertura de testes
- [ ] Deploy da aplicação

---

## 👤 Autor

Desenvolvido como projeto de estudo em C# e .NET.
