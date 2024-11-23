# Finances-Control-App-API

# Finances-Control-App-API

## Descrição

A **Finances-Control-App-API** é a API responsável pela gestão das finanças do usuário. Ela oferece diversos recursos para gerenciar contas, transferências, e dados financeiros. A API utiliza **.NET 8.0** com **Entity Framework Core**, oferecendo uma maneira eficiente de integrar e interagir com os dados financeiros.

## Funcionalidades

A API oferece as seguintes funcionalidades principais:

### 1. **Autenticação e Autorização**
   - **Login de Usuário**: Suporte para login de usuário via **JWT (JSON Web Token)**.
   - **Autenticação**: Protege endpoints com autenticação de usuário, utilizando tokens JWT para garantir que apenas usuários autenticados possam acessar os recursos.
   - **Middleware de Contexto de Usuário**: Middleware que injeta o ID do usuário no contexto para ser acessado globalmente em todas as requisições, sem precisar verificar em cada endpoint.

### 2. **Gestão de Contas**
   - **Criar Conta**: Criar novas contas financeiras associadas ao usuário.
   - **Obter Contas**: Consultar todas as contas do usuário logado, com detalhes como número, saldo, bandeira (Mastercard, Visa, etc.), e outras informações.
   - **Atualizar Conta**: Atualizar os dados de uma conta existente (ex: mudar o nome, saldo, etc.).
   - **Excluir Conta**: Remover uma conta do sistema.

### 3. **Transferências**
   - **Criar Transferência**: Realizar transferências entre contas do usuário.
   - **Obter Transferências**: Consultar o histórico de transferências, com filtros por data, valor, origem, destino, etc.
   - **Atualizar Transferência**: Atualizar informações de transferências já realizadas.
   - **Deletar Transferência**: Excluir uma transferência do histórico.

### 4. **Orçamentos e Planejamento Financeiro**
   - **Criar Orçamento**: Criar orçamentos mensais para controlar os gastos do usuário.
   - **Obter Orçamentos**: Consultar todos os orçamentos e o progresso de cada um.
   - **Atualizar Orçamento**: Modificar os limites de orçamento para categorias de gasto.
   - **Excluir Orçamento**: Deletar orçamentos do sistema.

### 5. **Consultas Financeiras**
   - **Saldo Atual**: Consultar o saldo disponível em todas as contas associadas ao usuário.
   - **Últimos Gastos**: Consultar os últimos gastos realizados nas contas do usuário.
   - **Relatórios Financeiros**: Gerar relatórios financeiros, com gráficos e resumos sobre a evolução de saldo, transferências e orçamentos.

### 6. **Exibição de Dados no Frontend**
   - **Retorno de Cartões de Conta**: Endpoint que retorna as contas financeiras com seus respectivos saldos, números de cartões (parciais) e bandeiras para exibição em frontend.
   - **Visualização de Detalhes de Conta**: Exibir detalhes específicos da conta selecionada, como faturas, transações, etc.



## Middleware

A API utiliza **middlewares** importantes para garantir a segurança e consistência dos dados:

### 1. **UserContextMiddleware**
   - Injeta o ID do usuário autenticado no contexto de cada requisição para ser acessado globalmente nos endpoints, sem precisar ser enviado em cada chamada.

## Banco de Dados

A API utiliza **Entity Framework Core** com **SQL Server** para persistência dos dados. A estrutura do banco inclui as tabelas para **Contas**, **Transferências**, **Orçamentos** e **Usuários**. A arquitetura de banco de dados foi projetada para suportar escalabilidade e eficiência nas consultas financeiras.

## Como Executar

### Pré-requisitos
- **.NET SDK 8.0** ou superior
- **SQL Server** ou outro banco de dados configurado para usar com o Entity Framework

### Passos

1. Clone o repositório:
   ```bash
   git clone https://github.com/seuusuario/Finances-Control-App-API.git
