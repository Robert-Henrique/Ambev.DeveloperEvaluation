
# API para registro de vendas da equipe DeveloperStore

Este projeto é uma implementação de uma API CRUD para registros de vendas.

## Funcionalidades

- **Cadastro de Vendas**: Permite registrar informações como número da venda, data, cliente, valor total, filial, produtos, quantidades, preços unitários e descontos.
- **Regra de Desconto**: Aplica descontos baseados na quantidade de itens comprados:
  - 10% de desconto para 4+ itens.
  - 20% de desconto para 10 a 20 itens.
  - Limite de 20 itens por produto.
- **Eventos**: Publicação de eventos como `VendaCriada`, `VendaModificada`, `VendaCancelada`, `ItemCancelado`.

## Tecnologias Utilizadas

- **Backend**: .NET 8.0, C#
- **Banco de Dados**: PostgreSQL
- **Testes**: xUnit, Faker, NSubstitute
- **Frameworks**: Mediator, AutoMapper, MassTransit
- **Versionamento**: Git (utilizando Git Flow e Commits Semânticos)

## Como Rodar

1. Clone o repositório:  
   `git clone https://github.com/seu-repositorio.git`
   
2. Instale as dependências:  
   `dotnet restore`

3. Execute a aplicação:  
   `dotnet run`

4. Para rodar os testes:  
   `dotnet test`

## Estrutura do Projeto

```
root
├── src/                # Código fonte
├── tests/              # Testes unitários
└── README.md           # Este arquivo
```

## Regras de Negócio

- Compras acima de 4 itens têm 10% de desconto.
- Compras entre 10 e 20 itens têm 20% de desconto.
- Limite máximo de 20 itens por produto.
- Nenhum desconto para compras abaixo de 4 itens.

## Considerações

Este projeto foca em boas práticas de desenvolvimento, incluindo separação de camadas, testes automatizados, e a implementação de regras de negócios complexas.
