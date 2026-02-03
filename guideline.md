# Guideline – Diretrizes de Desenvolvimento

Este documento descreve regras e práticas definidas por [@Ana-Borowsky](https://github.com/ana-borowsky) e [@RamonPelle](https://github.com/RamonPelle) para guiar o desenvolvimento deste projeto.

O objetivo é registrar decisões, padronizar fluxo de trabalho e facilitar a colaboração e a qualidade técnica.

## Índice
- [Guideline – Diretrizes de Desenvolvimento](#guideline--diretrizes-de-desenvolvimento)
  - [Índice](#índice)
  - [Língua](#língua)
  - [Padrões de Projeto](#padrões-de-projeto)
  - [GitHub](#github)
    - [Issues](#issues)
    - [Branches](#branches)
    - [Commits](#commits)
    - [Pull Requests](#pull-requests)
    - [Projects e Labels](#projects-e-labels)
  - [Padrões de Código](#padrões-de-código)
  - [Swagger](#swagger)
  - [Ideias / Backlog](#ideias--backlog)

---

## Língua
- Padrão: Português para documentação, mensagens de commit e PRs.
- Exceção: Inglês se for termos técnicos especificos.
- Justificativa: Não ser uma barreira na apresentação, além de evitar possíveis erros pelo fato dos contribuidores não terem inglês como língua materna.
---

## Padrões de Projeto
- Arquitetura base: MVC (API ASP.NET Core 8, sem frontend dedicado).
- Documentação de endpoints via Swagger/OpenAPI com exemplos.
- Camadas sugeridas: `API`, `Domain`, `Data` (Entity Framework Core) e utilitários. (AQUI da pra fazer um diagrama também)
- Princípios de Desenvolvimento: SOLID, DRY, TDA, KISS e YAGNIy.
---

## GitHub

### Issues
- Toda issue pode incluir:
	- Motivação: por que isso é necessário?
	- Objetivo: o que deve ser entregue?
	- Cenário: passos ou contexto de uso.
	- Considerações: restrições, escopo, decisões.
	- Testes: ideias para testes unitários, critérios de aceitação e validação.

### Branches
-  Abrir diretamente na issue, através do webapp do GitHub.
-  Evitar abrir branch sem issue vinculada.
  
### Commits
- Regra: Commits curtos, focados em um único assunto.
- Formato recomendado com tag entre colchetes, seguindo o padrão [Conventional Commits](https://www.conventionalcommits.org/en/v1.0.0/)
  
FAÇA

```
[docs]: Atualiza README com passos de execução
```

NÃO FAÇA
```
mudanca readme
```

### Pull Requests
- Exigir code review antes de merge.
- Corpo do PR deve conter:
	- Release Notes (RN): resumo objetivo do que passou a existir.
	- Testing Notes (TN): instruções claras de como validar.

Exemplo de PR:

```
Título: [nome da branch]

RN:
- Agora o sistema é capaz de ...

TN:
1) Seguir o cenário da issue
2) Verificar que a feature ...
```

### Projects e Labels
- Usar GitHub Projects para planejamento e acompanhamento.
- Labels customizadas:
  - Urgente: usar quando a tarefa precisa ser entregue muito brevemente;
  - Priorizada: deve ser entregue para a data final;
  - Bônus: não é obrigatório entregar;

---

## Padrões de Código
- Indentação: 4 espaços. (ver com Ana, uso o format on save com o formatter da MS pelo VSCODE, talvez dê para padronizar nele)
- C#
	- Variáveis: camelCase (ex.: `processOrder`).
	- Constantes e métodos: PascalCase.
	- Preferir nomes descritivos.
- SQL
	- Tabelas/colunas: snake_case.
	- Scripts com `UPPERCASE` para palavras-chave (`SELECT`, `WHERE`).
- Princípios
	- Aplicar SOLID, DRY, TDA, KISS, YAGNIy.

---

## Swagger
- Adicionar documentação nos arquivos para descrever:
	- Sumário, parâmetros, possíveis respostas (2xx, 4xx, 5xx).
	- Exemplos de request/response.
- Manter consistência de nomes e descrições; atualizar ao modificar endpoints.

---

## Ideias / Backlog
- Testes unitários – Secundário.
- Implementar fluxo de venda; considerar requisitos das outras opções.
- Colocar explicações detalhadas no Swagger (exemplos, responses, erros).
- GitHub Projects – usar labels customizadas.
- Internacionalização (i18n) – baixa prioridade.

---

## Decisões de Projeto
### Ferramenta para confecção de diagramas
Foram comparadas duas ferramentas para a confecção de diagramas para este projeto: BrModelo e Mermaid JS.

Optou-se pelo uso do Mermaid JS, devido a:
- Praticidade/ rapidez para confecção de diagramas,
- Maior variabilidade de modelos que podem ser confeccionados com o uso da ferramenta, uma vez que serão produzidos outros tipos no decorrer do projeto, e padronização é um fator relevante.
- Visualizar alterações facilmente pelo Git.

```Durante a preparação deste arquivo, os autores usaram GitHub Copilot, no modo Agent GPT-5 para formatar em markdown e sugerir alterações gramaticais no texto. Após usar essa ferramenta, os autores revisaram e editaram o conteúdo conforme necessário e assumem total responsabilidade pelo conteúdo.```