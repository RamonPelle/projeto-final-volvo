
  # TechStore
  
> API RESTful desenvolvida em ASP.NET Core para gerenciamento de um sistema de vendas (e-commerce), permitindo controle de produtos, categorias, clientes e pedidos.

Este projeto foi desenvolvido como trabalho final do curso Desenvolvimento de Software na plataforma .NET e SQL Server - Extensão, parceria da PUCPR e da Volvo.


![.NET](https://img.shields.io/badge/.NET-8.0-512BD4)
![EF Core](https://img.shields.io/badge/EFCore-8-512BD4?logo=dotnet&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQLServer-CC2927?logo=microsoftsqlserver&logoColor=white)
![Swagger](https://img.shields.io/badge/Swagger-OpenAPI-85EA2D?logo=swagger&logoColor=black)

Descrição do sistema

## Índice
- [TechStore](#techstore)
  - [Índice](#índice)
  - [Arquitetura (MVC)](#arquitetura-mvc)
  - [Stack Tecnológica](#stack-tecnológica)
  - [Recursos Técnicos Implementados](#recursos-técnicos-implementados)
  - [Diagrama do banco de dados](#diagrama-do-banco-de-dados)
  - [Funcionalidades](#funcionalidades)
  - [Swagger](#swagger)
  - [Como Executar o Projeto](#como-executar-o-projeto)
    - [Pré-requisitos](#pré-requisitos)
    - [Conexão com o banco de dados](#conexão-com-o-banco-de-dados)
    - [Rode o programa](#rode-o-programa)
  - [Portas e Acessos](#portas-e-acessos)
  - [Estrutura do projeto](#estrutura-do-projeto)
    - [Diagrama de camadas](#diagrama-de-camadas)
    - [Diagrama de sequência](#diagrama-de-sequência)
  - [Diretrizes de Desenvolvimento](#diretrizes-de-desenvolvimento)

---

## Arquitetura (MVC)
O projeto utiliza o padrão Model-View-Controller (MVC) para garantir a separação de responsabilidades e facilitar a testabilidade.

- **Models**: Representam a estrutura de dados e regras de negócio.
- **Views**: Este projeto não tem frontend. Foi utilizado o swagger
- **Controllers**: Lidam com as requisições, interagem com os serviços/models e retornam os dados necessários.

---

## Stack Tecnológica
- Linguagem: C# 12
- Framework: ASP.NET Core Web API 8.0
- ORM: Entity Framework Core 8.0.12 (EF Core)
- Consultas: LINQ (Language Integrated Query)
- Banco de Dados: SQL Server
- Documentação: Swagger (Swashbuckle.AspNetCore 6.6.2 + Microsoft.AspNetCore.OpenApi 8.0.12)

---

## Recursos Técnicos Implementados

- DTOs (Data Transfer Objects)
- Paginação via query parameters
- Filtros dinâmicos
- Enum para controle de status do pedido
- API RESTful
- Documentação via Swagger

---

## Diagrama do banco de dados

<img width="477" height="769" alt="Image" src="https://github.com/user-attachments/assets/5837fede-c12d-4250-adeb-d9e20e4e3a0e" />

---

## Funcionalidades
Principais capacidades do sistema:
- Gestão de Produtos
  - Cadastrar produto
  - Atualizar produto
  - Excluir produto
  - Buscar produto por ID
  - Listar produtos com paginação (skip / take)
  - Filtrar produtos por: Nome, Preço mínimo, Preço máximo
  - Listar produtos por categoria
  - Atualizar estoque de produto

- Gestão de Categorias
  - Cadastrar categoria
  - Atualizar categoria
  - Excluir categoria
  - Buscar categoria por ID
  - Listar todas as categorias
  - Obter valor total vendido por categoria

- Gestão de Clientes
  - Cadastrar cliente
  - Atualizar cliente
  - Excluir cliente
  - Buscar cliente por ID
  - Listar clientes

- Gestão de Pedidos
  - Criar pedido
  - Associar pedido a um cliente
  - Listar pedidos com filtros: Por cliente, Por status
  - Buscar pedido por ID
  - Finalizar pedido
  - Excluir pedido
  - Cálcular total de pedidos

- Gestão de Itens do Pedido
  - Adicionar itens ao pedido
  - Remover item do pedido
  - Associação item ↔ produto
  - Registro do preço unitário no momento da compra

---

## Swagger
Visão do swagger da aplicação.

<img width="1828" height="877" alt="Image" src="https://github.com/user-attachments/assets/60a811db-d4fe-4970-870e-6ba8948d8da8" />

<img width="1828" height="877" alt="Image" src="https://github.com/user-attachments/assets/2e17cf34-a710-45e0-aaec-7fbdd733a2cc" />

---

## Como Executar o Projeto

### Pré-requisitos
- SDK do .NET 8 instalado.

- AutoMapper.Extensions.Microsoft.DependencyInjection (12.0.1): Integração do AutoMapper com o sistema de Injeção de Dependência do ASP.NET Core.

```dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection --version 12.0.1```


- BCrypt.Net-Next (4.0.3): Biblioteca para hash seguro de senhas.

```dotnet add package BCrypt.Net-Next --version 4.0.3```


- Microsoft.EntityFrameworkCore (8.0.12): ORM para acesso e manipulação de dados.

```dotnet add package Microsoft.EntityFrameworkCore --version 8.0.12```


- Microsoft.EntityFrameworkCore.SqlServer (8.0.12): Provider do Entity Framework Core para SQL Server.

```dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 8.0.12```


- Microsoft.EntityFrameworkCore.Design (8.0.12): Ferramentas para suporte a migrations.

```dotnet add package Microsoft.EntityFrameworkCore.Design --version 8.0.12```


- Microsoft.AspNetCore.OpenApi (8.0.12): Integração com OpenAPI no .NET 8.

```dotnet add package Microsoft.AspNetCore.OpenApi --version 8.0.12```


- Swashbuckle.AspNetCore (6.6.2): Geração automática de documentação Swagger.

```dotnet add package Swashbuckle.AspNetCore --version 6.6.2```


- Swashbuckle.AspNetCore.Annotations (6.6.2): Suporte a anotações adicionais no Swagger.

```dotnet add package Swashbuckle.AspNetCore.Annotations --version 6.6.2```


### Conexão com o banco de dados
Altere o usuário e a senha do seu banco no arquivo appsettings.json.

### Rode o programa
Entre na pasta TechStore/
```bash
dotnet run
```
---
## Portas e Acessos

| Serviço        | URL / Acesso                     | Descrição                  |
|----------------|----------------------------------|----------------------------|
| Swagger UI     | https://localhost:xxxx/swagger   | Documentação da API        |

---

## Estrutura do projeto

### Diagrama de camadas

<img width="1201" height="363" alt="Image" src="https://github.com/user-attachments/assets/b7d108e5-bfc9-435b-81ac-48f98cbb4d8d" />



### Diagrama de sequência
<img width="1423" height="675" alt="Image" src="https://github.com/user-attachments/assets/1c5cb6b6-2a9e-4dc9-80e0-00270c03e8e3" />



```

TechStore/
├── Controllers/
    ├── APIs/
│   │   ├── ProdutoController.cs
│   │   ├── PedidoController.cs
│   │   ├── CategoriaController.cs
│   │   └── ClienteController.cs
│   │
├── Data/
│   ├── TechstoreContext.cs
├── DTOs/
│   ├── Request/
│   │   ├── CategoriaRequest.cs
│   │   ├── ClienteEditarRequest.cs
│   │   ├── ClienteRequest.cs
│   │   ├── ItemPedidoRequest.cs
│   │   ├── LoginRequest.cs
│   │   ├── PedidoRequest.cs
│   │   └──  ProdutoRequest.cs
│   ├── Response/
│   │   ├── CategoriaResponse.cs
│   │   ├── ItemPedidoResponse.cs
│   │   ├── PedidoResponse.cs
│   │   ├── ProdutoResponse.cs
│   │   └──  ValorPorCategoriaResponse.cs
├── Mappings/
│   ├── CategoriaProfile.cs
│   ├── ClienteProfile.cs
│   ├── PedidoProfile.cs
│   └── ProdutoProfile.cs
├── Middlewares/
│   └── TrataentoExcecoesMiddleware.cs
├── Models/
│   ├── Enums/
│   │   └──  StatusPedido.cs
│   ├── Categoria.cs
│   ├── Cliente.cs
│   ├── ItemPedido.cs
│   ├── Pedido.cs
│   └── Peroduto.cs
├── Repository/
│   ├── APIs/
│   │   ├── CategoriaRepository.cs
│   │   ├── ClienteRepository.cs
│   │   ├── ItemPedidoRepository.cs
│   │   ├── PedidoRepository.cs
│   │   └── ProdutoRepository.cs
├── Services/
│   ├── APIs/
│   │   ├── CategoriaService.cs
│   │   ├── ClienteService.cs
│   │   ├── ItemPedidoService.cs
│   │   ├── PedidoService.cs
│   │   └── ProdutoService.cs
│   ├── SenhaService.cs
├── Utils
│   └── ValidadorEntidade.cs
├── GUIDELINE.md
└── README.md
```

---

## Diretrizes de Desenvolvimento
- Consulte o arquivo `GUIDELINE.md` detalhes.

---


Desenvolvido por [@Ana-Borowsky](https://github.com/ana-borowsky) e [@RamonPelle](https://github.com/RamonPelle).



```Durante a preparação dos arquivos de documentação (README.md, GUIDELINE.md e Swagger annotations) os autores usaram GitHub Copilot, no modo Agent GPT-5 para formatar e sugerir ajustes para o texto. Após usar essa ferramenta, os autores revisaram e editaram o conteúdo conforme necessário e assumem total responsabilidade pelo conteúdo.```
