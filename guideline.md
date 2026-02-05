# Guideline – Diretrizes de Desenvolvimento

Este documento descreve regras e práticas definidas por [@Ana-Borowsky](https://github.com/ana-borowsky) e [@RamonPelle](https://github.com/RamonPelle) para guiar o desenvolvimento deste projeto.

O objetivo é registrar decisões, padronizar fluxo de trabalho e facilitar a colaboração e a qualidade técnica.

## Índice
- [Língua](#1-língua)
- [Padrões de Projeto](#padrões-de-projeto)
- [GitHub](#github)
  - [Issues](#issues)
    - [Título](#título)
    - [Corpo](#corpo)
  - [Branches](#branches)
  - [Commits](#commits)
  - [Pull Requests](#pull-requests)
  - [Projects e Labels](#projects-e-labels)
- [Padrões de Código](#padrões-de-código)
- [Swagger](#swagger)
- [Ideias / Backlog](#ideias--backlog)

---
## 1. Língua
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

### GitHub Projects
- Utilizar o GitHub Projects para planejamento e acompanhamento do projeto.
- Labels customizadas:
  - Urgente: usar quando a tarefa precisa ser entregue muito brevemente;
  - Priorizada: deve ser entregue para a data final;
  - Bônus: não é obrigatório entregar;

### Issues
#### Título
O título da issue deve incluir uma ou duas tags. A primeira tag deve ser uma das principais, e a segunda deve ser referente à funcionalidade ou arquivo.

- Tags principais: documentação, refatoração, análise, implementação, teste, ajuste.

Exemplo de título: [Implementação][Categoria] Implementação de endpoints de categoria.

#### Corpo
- Toda issue pode incluir:
	- Motivação: por que isso é necessário?
	- Objetivo: o que deve ser entregue?
	- Cenário: passos ou contexto de uso.
	- Considerações: restrições, escopo, decisões.
	- Testes: ideias para testes unitários, critérios de aceitação e validação.

Exemplo: 
```
[Documentação] Buscar por referências e templates para arquivos de documentação

Motivação
- Possuir todo o projeto bem documentado para o desenvolvimento, apresentação e posterioridade;

Objetivo
- Buscar por referências de:
  - Como documentar APIs
  - Como estruturar issues e PR (done, documentar)
  - README files
  - Guidelines de desenvolvimento
 
Considerações
- Nem tudo que será documentado deve ser mostrado na apresentação
- filtrar tópicos relevantes para apresentar ou dados que podem ser extraídos dessa documentação
```

### Branches
-  Abrir diretamente na issue, através do webapp do GitHub.
-  Evitar abrir branch sem issue vinculada.
  
### Commits
- Regra: Commits curtos, focados em um único assunto.
- Formato recomendado com tag entre colchetes, seguindo o padrão [Conventional Commits](https://www.conventionalcommits.org/en/v1.0.0/)
- O do commit deve incluir uma ou duas tags. A primeira deve ser uma das tags principais, e a segunda deve ser referente à funcionalidade ou arquivo.

Tags principais: impl, fix, funcionalidade, docs, análise, arq, test, refactor
  
FAÇA
```
[impl][categoria] endpoints e organizacao de pastas
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
---

## Padrões de Código
- Indentação: 4 espaços.
- C#
	- Variáveis: camelCase (ex.: `processOrder`).
	- Constantes e métodos: PascalCase.
	- Preferir nomes descritivos.
- SQL
	- Tabelas/colunas: PascalCase.
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

```Durante a preparação deste arquivo, os autores usaram GitHub Copilot, no modo Agent GPT-5 para formatar em markdown e sugerir alterações gramaticais no texto. Após usar essa ferramenta, os autores revisaram e editaram o conteúdo conforme necessário e assumem total responsabilidade pelo conteúdo.```