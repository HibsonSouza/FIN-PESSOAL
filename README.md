# Finance Manager - Gerenciador de Finanças Pessoais

Uma aplicação completa para gerenciamento de finanças pessoais, desenvolvida com .NET, Blazor WebAssembly e PostgreSQL.

## 📋 Funcionalidades

- **Contas Financeiras**: Cadastre e gerencie suas contas bancárias, carteiras digitais, etc.
- **Categorias**: Organize suas transações com categorias personalizáveis
- **Transações**: Registre receitas e despesas com detalhes completos
- **Orçamentos**: Defina metas de gastos por categoria e monitore seu progresso
- **Metas Financeiras**: Estabeleça objetivos financeiros e acompanhe seu progresso
- **Cartões de Crédito**: Gerencie seus cartões e suas faturas
- **Dashboards**: Visualize resumos e estatísticas da sua vida financeira
- **Relatórios**: Gere relatórios detalhados para análise financeira

## 🔧 Tecnologias Utilizadas

- **Backend**: ASP.NET Core 8.0, Entity Framework Core, PostgreSQL
- **Frontend**: Blazor WebAssembly, MudBlazor (componentes Material Design)
- **Autenticação**: JWT (JSON Web Tokens)
- **Deployment**: Docker, Docker Compose

## 🚀 Pré-requisitos

Para executar este projeto, você precisa ter instalado:

- [.NET SDK 8.0](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker](https://www.docker.com/products/docker-desktop)
- [Docker Compose](https://docs.docker.com/compose/install/)

## 💻 Como Executar

### Usando Docker (Recomendado)

1. Clone o repositório:
   ```bash
   git clone https://github.com/seu-usuario/finance-manager.git
   cd finance-manager
   ```

2. Execute a aplicação usando Docker Compose:
   ```bash
   docker-compose up -d
   ```

3. Acesse a aplicação em seu navegador:
   ```
   http://localhost:5000
   ```

### Desenvolvimento Local

1. Clone o repositório:
   ```bash
   git clone https://github.com/seu-usuario/finance-manager.git
   cd finance-manager
   ```

2. Configuração do PostgreSQL:
   - Instale o PostgreSQL localmente ou use um container Docker
   - Crie um banco de dados chamado `financepersonal`
   - Atualize a string de conexão em `FinanceManager/appsettings.Development.json` se necessário

3. Execute o backend:
   ```bash
   cd FinanceManager
   dotnet run
   ```

4. Em outro terminal, execute o frontend:
   ```bash
   cd ClientApp
   dotnet run
   ```

5. Acesse a aplicação em seu navegador:
   ```
   http://localhost:5001
   ```

## 📊 Estrutura do Projeto

```
FinancePessoal/
├── ClientApp/ (Blazor WebAssembly)
│   ├── Components/
│   ├── Models/
│   ├── Pages/
│   ├── Services/
│   └── wwwroot/
├── FinanceManager/ (API Backend)
│   ├── Controllers/
│   ├── Data/
│   ├── Models/
│   ├── Repositories/
│   ├── Services/
│   └── Validators/
├── Dockerfile
└── docker-compose.yml
```

## 🔒 Autenticação

Para fazer login no sistema, você pode:

1. Registrar um novo usuário na tela de cadastro
2. Usar o usuário administrador padrão:
   - Email: admin@financepersonal.com
   - Senha: Admin@123

## 💾 Banco de Dados

O sistema usa PostgreSQL para armazenamento de dados. As migrações são aplicadas automaticamente na primeira execução do sistema.

### Modelo de Dados Simplificado

- **Users**: Usuários do sistema
- **Accounts**: Contas financeiras (corrente, poupança, etc.)
- **Categories**: Categorias de transações
- **Transactions**: Registro de receitas e despesas
- **Budgets**: Orçamentos mensais por categoria
- **Goals**: Metas financeiras
- **CreditCards**: Cartões de crédito

## 📱 Capturas de Tela

_(Inserir capturas de tela aqui)_

## 🤝 Contribuindo

Contribuições são bem-vindas! Sinta-se à vontade para abrir issues ou pull requests.

## 📜 Licença

Este projeto está licenciado sob a licença MIT - veja o arquivo [LICENSE](LICENSE) para mais detalhes.
