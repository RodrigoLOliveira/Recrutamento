# Recrutamento

Este é um projeto de exemplo para um sistema de recrutamento, desenvolvido utilizando .NET 9 e WPF.

## Pré-requisitos

Antes de começar, certifique-se de ter os seguintes softwares instalados:

- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/)
- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (ou outro banco de dados compatível)

## Configuração do Projeto

### 1. Clonar o Repositório

Clone o repositório para sua máquina local:

```
git clone https://github.com/RodrigoLOliveira/Recrutamento.git cd recrutamento
```


### 2. Configurar o Banco de Dados

1. Crie um banco de dados no SQL Server.
2. Atualize a string de conexão no arquivo `appsettings.json` localizado em `Recrutamento.API`:

```
{ 
    "ConnectionStrings": 
    {
        "DefaultConnection": "Server=SEU_SERVIDOR;Database=SEU_BANCO_DE_DADOS;User Id=SEU_USUARIO;Password=SUA_SENHA;" 
    }, 
    "Jwt": 
    { 
        "Key": "SUA_CHAVE_SECRETA", 
        "Issuer": "seu-issuer" 
    } 
}
```


### 3. Aplicar Migrações

Abra o terminal no diretório do projeto e execute os seguintes comandos para aplicar as migrações e criar o banco de dados:

```
cd Recrutamento.API dotnet ef database update
```


### 4. Configurar a Solução para Iniciar Múltiplos Projetos

1. No Visual Studio, clique com o botão direito do mouse na solução no Solution Explorer e selecione **Properties**.
2. Na janela de propriedades da solução, selecione **Startup Project** no menu à esquerda.
3. Selecione a opção **Multiple startup projects**.
4. Configure os projetos `Recrutamento.API` e `Recrutamento.Wpf` para iniciar. Defina a ação como **Start** para ambos os projetos.

### 5. Executar a Solução

No Visual Studio, pressione F5 ou Ctrl+F5 para iniciar ambos os projetos (API e WPF).

## Funcionalidades

### Autenticação

- **Login**: Permite que os usuários façam login no sistema.
- **Registro**: Permite que novos usuários se registrem no sistema.

### Tarefas

- **Criar Tarefa**: Permite que os usuários criem novas tarefas.
- **Listar Tarefas**: Exibe uma lista de todas as tarefas do usuário autenticado.
- **Filtrar Tarefas**: Permite filtrar tarefas por status.
- **Editar Tarefa**: Permite editar uma tarefa existente.
- **Excluir Tarefa**: Permite excluir uma tarefa existente.

## Estrutura do Projeto

- `Recrutamento.API`: Projeto da API, responsável por fornecer endpoints para autenticação e gerenciamento de tarefas.
- `Recrutamento.Wpf`: Projeto WPF, responsável pela interface do usuário.
- `Recrutamento.Domain`: Contém as entidades e enums do domínio.
- `Recrutamento.Infra`: Contém a interação com o banco de dados, como contexto e repositórios.
