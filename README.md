# User Management API (.NET 8)

Uma Web API robusta para gestão de usuários, desenvolvida com foco em padrões de mercado, escalabilidade e manutenibilidade.

## Tecnologias e Padrões

- **C# 12 & .NET 8 (LTS)**
- **Entity Framework Core** (ORM)
- **SQLite** (Banco de dados local para facilidade de teste)
- **Repository Pattern** (Abstração da camada de dados)
- **Clean Architecture** (Separação clara entre Domínio, Infra e API)
- **DTOs (Data Transfer Objects)** com Records para imutabilidade
- **GUIDs** para Identificadores Únicos
- **Swagger/OpenAPI** para documentação interativa

## Estrutura do Projeto

- `UserApi.Domain`: Entidades de negócio, Interfaces e DTOs.
- `UserApi.Infra`: Contexto do banco de dados (EF Core), Migrations e Repositórios.
- `UserApi.Api`: Controllers, Configurações de Injeção de Dependência e Middlewares.

## Como Executar (Linux)

1. Clone o repositório:

   ```bash
   git clone [https://github.com/thinkbruno/crud-c--fullstack.git](https://github.com/thinkbruno/crud-c--fullstack.git)
   ```

2. Restaure as dependências e rode as migrations::

   ```bash
   dotnet ef database update --project UserApi.Infra --startup-project UserApi.Api
   ```

3. Execute o projeto:

   ```bash
   cd UserApi.Api
   dotnet run
   ```

4. Acesse o Swagger (verifique a porta no terminal):
   ```bash
   http://localhost:5000/swagger
   ```

---

## Autor

Desenvolvido por Bruno Ramos como parte do aprofundamento no ecossistema .NET.

Portfolio: [brunoramos.tec.br](https://brunoramos.tec.br/)
