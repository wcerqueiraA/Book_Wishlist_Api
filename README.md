# Book_Wishlist_Api
API REST desenvolvida em ASP.NET Core para gerenciamento de lista de desejos de livros.

A aplicação permite adicionar, atualizar, consultar e remover livros da wishlist, 
seguindo boas práticas como Repository Pattern, DTO Pattern e AutoMapper.

O projeto utiliza Entity Framework Core com SQLite para persistência de dados.

## Stack Tecnológica

- ASP.NET Core
- Entity Framework Core
- SQLite
- Repository Pattern
- DTO Pattern
- AutoMapper
- NUnit (Testes)

## Funcionalidades

- CRUD de livros
- Validação de modelos
- Mapeamento Domain ↔ DTO
- Persistência com SQLite
- Testes unitários

## Arquitetura

O projeto segue uma arquitetura em camadas (Layered Architecture), separando responsabilidades:

- **Controllers** → Endpoints da API
- **Models/Domain** → Entidades de negócio
- **Models/DTO** → Contratos de entrada/saída
- **Repositories** → Acesso a dados (Repository Pattern)
- **Data** → DbContext / EF Core
- **Mappings** → Configuração do AutoMapper
- **CustomActionFilters** → Regras transversais (validação, etc.)
- **Migrations** → Versionamento do banco

