# BlueAgenda



Aplicação **.NET 8** com API construída em **arquitetura limpa** e utilizando **SQL Server**.



---



## 🚀 Como Rodar o Projeto



### 🐳 Usando Docker (Recomendado)



1.  Atualize as variáveis de ambiente no arquivo **`docker-compose.yml`**.

2.  Execute o comando:



    ```bash

    docker-compose up -d --build

    ```



> **A API será iniciada automaticamente.**



---



### 💻 Modo Desenvolvimento (Sem Docker)



1.  Atualize as variáveis no arquivo **`appsettings.Development.json`**.

2.  Execute os comandos:



    ```bash

    dotnet build

    dotnet run --project src/BlueAgenda.Api

    ```
