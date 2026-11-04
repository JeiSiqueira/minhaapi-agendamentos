MinhaApi — Agendamentos (Back-end)

API REST para um sistema de agendamento de salão de beleza, desenvolvida em C# com .NET 8 e MySQL.

A API permite gerenciar usuários, serviços, profissionais e agendamentos, com arquitetura em camadas, soft delete e documentação via Swagger.

Front-end: minhaapp-agendamentos (React + TypeScript)

<!-- Adicione aqui uma captura de tela do Swagger: ![Swagger](docs/swagger.png) -->
Tecnologias
C#
.NET 8
ASP.NET Core Web API
Entity Framework Core
MySQL
Swagger / OpenAPI
Postman
Git / GitHub
Funcionalidades
CRUD de Usuários, Serviços, Profissionais e Agendamentos
Soft delete (IsDeleted) para Usuários, Serviços e Profissionais
Validação das chaves estrangeiras antes de criar um agendamento (usuário, serviço e profissional precisam existir)
Mapeamento no EF Core (OnModelCreating) com nomes de tabelas e colunas em minúsculas, compatível com o schema MySQL
Carregamento automático de variáveis de ambiente a partir de um arquivo .env
Documentação interativa com Swagger (ambiente de desenvolvimento)
Coleção do Postman incluída no repositório
Arquitetura

O projeto é organizado em camadas, separando as responsabilidades da aplicação:

Controllers → Services → Repositories → DbContext (AppDbContext)
Controllers: recebem as requisições HTTP e retornam as respostas
Services: concentram as regras de negócio e validações
Repositories: acesso aos dados
AppDbContext: configuração do EF Core e mapeamento com o MySQL
Endpoints
Usuários
Método	Rota	Descrição
GET	/api/usuarios	Lista usuários (não deletados)
GET	/api/usuarios/{id}	Busca usuário por id
POST	/api/usuarios	Cria usuário
PUT	/api/usuarios	Atualiza usuário
DELETE	/api/usuarios/{id}	Soft delete (IsDeleted = true)
Serviços
Método	Rota	Descrição
GET	/api/servicos	Lista serviços (não deletados)
GET	/api/servicos/{id}	Busca serviço por id
POST	/api/servicos	Cria serviço
PUT	/api/servicos	Atualiza serviço
DELETE	/api/servicos/{id}	Soft delete
Profissionais
Método	Rota	Descrição
GET	/api/profissionais	Lista profissionais (não deletados)
GET	/api/profissionais/{id}	Busca profissional por id
POST	/api/profissionais	Cria profissional
PUT	/api/profissionais	Atualiza profissional
DELETE	/api/profissionais/{id}	Soft delete
Agendamentos
Método	Rota	Descrição
GET	/api/Agendamentos	Lista agendamentos
GET	/api/Agendamentos/{id}	Busca agendamento por id
POST	/api/Agendamentos	Cria agendamento
PUT	/api/Agendamentos	Atualiza agendamento
DELETE	/api/Agendamentos/{id}	Remove agendamento
Banco de dados
MySQL com Entity Framework Core
O script sql/schema_mysql.sql contém o DDL usado para criar o banco
Usuario.Id é do tipo Guid (armazenado como binary(16)); Servico.Id e Profissional.Id são int
Como executar
Pré-requisitos
.NET 8 SDK
MySQL
Git
Passo a passo
Clone o repositório:
bash
git clone https://github.com/JeiSiqueira/minhaapi-agendamentos.git
cd minhaapi-agendamentos
Crie o banco de dados executando o script sql/schema_mysql.sql no MySQL.
Configure a conexão com o banco. A forma recomendada é criar um arquivo .env na raiz do projeto (já incluído no .gitignore):
env
DB_HOST=localhost
DB_NAME=minhaapidb
DB_USER=root
DB_PASS=sua_senha

O Program.cs carrega o .env automaticamente, sem sobrescrever variáveis de ambiente já existentes.

Como alternativa, ajuste a connection string no appsettings.json:

json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=minhaapidb;User=root;Password=sua_senha;"
}

Nunca faça commit de senhas reais. Use apenas o .env ou variáveis de ambiente.

Execute a API:
bash
dotnet run

No PowerShell, para rodar em ambiente de desenvolvimento:

powershell
$env:ASPNETCORE_ENVIRONMENT="Development"
dotnet run
Documentação e testes
Swagger: em ambiente de desenvolvimento, acesse https://localhost:{porta}/swagger. A porta aparece na saída do dotnet run.
Postman: importe a coleção postman/MinhaApi.postman_collection.json e altere a variável baseUrl, se necessário.
Fluxo para testar um agendamento
Crie um usuário, um serviço e um profissional
Use os IDs retornados para criar o agendamento (a API valida as chaves estrangeiras antes de inserir)
Melhorias futuras
Hash de senhas (BCrypt ou Argon2). Atualmente as senhas são armazenadas em texto
Autenticação e autorização com JWT
Testes automatizados (xUnit)
Docker e docker-compose
EF Core Migrations
Controle de disponibilidade e conflito de horários por profissional
Autora

Jeimili Siqueira Mendes GitHub | LinkedIn

Projeto desenvolvido para estudo, prática e construção de portfólio profissional, com foco em APIs REST com C# e .NET e integração com banco de dados relacional.
