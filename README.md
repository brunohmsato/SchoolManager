# SchoolManager

> Sistema de Controle Escolar desenvolvido em **C# / .NET 9**, com arquitetura em camadas, **Entity Framework Core**, **FluentValidation**, **testes automatizados** e **pipeline CI/CD** no GitHub Actions.

---

## 🧩 Sobre o projeto

O **SchoolManager** é uma API RESTful que permite gerenciar **alunos**, **disciplinas** e **notas**, gerando relatórios de médias e rankings automaticamente.  
O projeto foi criado com foco em **boas práticas de desenvolvimento backend**, arquitetura **DDD simplificada** e **testabilidade** — ideal para demonstrar domínio de C# moderno e design limpo.

---

## ⚙️ Tecnologias utilizadas

| Categoria | Tecnologias |
|------------|--------------|
| Backend | .NET 8, ASP.NET Core Web API |
| ORM / Banco | Entity Framework Core + SQLite |
| Validação | FluentValidation |
| Testes | xUnit, Moq, FluentAssertions |
| CI/CD | GitHub Actions (build + test + coverage) |
| Arquitetura | Clean Architecture / DDD |
| Outros | AutoMapper (opcional), Swagger UI |

---

## 🧱 Estrutura de pastas

```
SchoolManager/
├── API/                 # Camada de apresentação (Controllers)
├── Application/         # Serviços, DTOs e Validadores
├── Domain/              # Entidades e Interfaces
├── Infrastructure/      # Repositórios e Contexto EF Core
└── Tests/               # Testes unitários e de integração
```

---

## 🚀 Como executar localmente

### Pré-requisitos
- [.NET SDK 9.0+](https://dotnet.microsoft.com/download)
- Visual Studio / VS Code
- SQLite (ou apenas o driver embutido)

### Passos
```bash
# 1. Clone o repositório
git clone https://github.com/seuusuario/SchoolManager.git
cd SchoolManager

# 2. Restaure dependências
dotnet restore

# 3. Crie o banco de dados local
dotnet ef database update --project SchoolManager.Infrastructure

# 4. Execute o servidor
dotnet run --project SchoolManager.API
```

Acesse no navegador:
👉 http://localhost:7244/swagger

---

## 🧪 Testes automatizados

O projeto contém uma suíte completa de testes de unidade e validação de regras de negócio, cobrindo:

| Categoria | Arquivo | Casos |
|------------|----------|--------|
| Controllers | `AlunosControllerTests.cs` | CRUD completo |
| Controllers | `DisciplinasControllerTests.cs` | CRUD completo |
| Controllers | `NotasControllerTests.cs` | Criação, exclusão, validações |
| Validators | `AlunoValidatorTests.cs` | Nome, matrícula, data |
| Validators | `DisciplinaValidatorTests.cs` | Nome mínimo, obrigatório |

### Executar os testes
```bash
dotnet test --configuration Release
```

### Cobertura de testes (com pipeline CI)
```bash
dotnet test --collect:"XPlat Code Coverage"
```

---

## 🧰 Pipeline CI/CD (GitHub Actions)

O workflow `.github/workflows/dotnet-ci.yml` executa automaticamente:

✅ Build  
✅ Testes automatizados  
✅ Relatório de cobertura (coverlet + ReportGenerator)  
✅ Publicação de artefatos de build  

### Badge de status
![.NET CI](https://github.com/brunohmsato/SchoolManager/actions/workflows/dotnet-ci.yml/badge.svg)

---

## 🧠 Relatórios disponíveis

A API oferece endpoints para gerar estatísticas automáticas de desempenho:

| Endpoint | Descrição |
|-----------|------------|
| `GET /api/relatorios/medias` | Média de notas por aluno |
| `GET /api/relatorios/medias-disciplinas` | Média por disciplina |
| `GET /api/relatorios/ranking` | Ranking geral de alunos |

Exemplo de resposta:
```json
[
  { "alunoId": 1, "aluno": "João da Silva", "media": 8.5 },
  { "alunoId": 2, "aluno": "Maria Oliveira", "media": 7.8 }
]
```

---

## 🔒 Boas práticas aplicadas

- **Camadas separadas (DDD / Clean Architecture)**
- **Validação de entrada** via FluentValidation
- **Injeção de dependência (DI)**
- **Tratamento centralizado de exceções**
- **Testes de unidade e integração**
- **CI/CD configurado**
- **Mensagens de commit semânticas**

---

## 🌐 Deploy (opcional)

O projeto pode ser facilmente publicado em:

| Plataforma | Link |
|-------------|------|
| [Render.com](https://render.com) | API gratuita e simples |
| [Railway.app](https://railway.app) | Ideal para pequenos projetos |
| [Azure App Service](https://azure.microsoft.com) | Integração nativa com GitHub Actions |

Exemplo:
> 🌍 **Demo:** [https://schoolmanager.onrender.com/swagger](https://schoolmanager.onrender.com/swagger)

---

## 📚 Futuras melhorias

- 🔐 Autenticação JWT simples (usuário admin)
- 🧾 Exportação de boletins em PDF
- 📊 Dashboard frontend com gráficos (React ou Blazor)
- 🧮 Relatórios por turma e disciplina
- 🧱 Seed automático de dados de exemplo

---

## 🧑‍💻 Autor

**Bruno Sato**  
*Full Stack Developer @ Fenox Tecnologia*  
📧 [brunohmsato@gmail.com](mailto:brunosato.dev@gmail.com)  
🌐 [LinkedIn](https://linkedin.com/in/brunohmsato) | [GitHub](https://github.com/brunohmsato)

---

## 🏁 Licença
Este projeto está sob a licença MIT.  
Sinta-se à vontade para usar como referência em estudos, portfólio ou aprendizado.

---

> 💡 *“Projetos de portfólio não são sobre mostrar o que você sabe — são sobre mostrar como você pensa.”*
