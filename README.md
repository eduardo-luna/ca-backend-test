**Teste para vaga de Desenvolvimento Back-end .NET**
---------------------
API REST para gerenciar faturamento de clientes.
---------------------

**Tecnologias utilizadas**

* .NET 8.0
* MediatR
* EntityFrameworkCore 8.0
* Postgres
* XUnit
* Moq

---------------------
**Configuração do projeto**
*Docker compose*
Há uma configuração de Docker Compose que pode ser iniciada diretamente no visual studio. Essa opção irá configurar o projeto e uma instância do postgres automaticamente.
Para utilizar essa opção, certifique-se de ter o docker engine rodando e que as portas 8080, 8081 e 5432 estão disponíveis no seu ambiente. Caso não estejam, você pode alterar as portas de saída em: *ca-backend-test\docker-compose.yml*

*Diretamente no visual studio*
Certifique-se de ter uma instância de postgres disponível
Configure a conexão de acordo com a sua instância de postgres em *\ca-backend-test\NexerAPI\appsettings.json*


**Endpoints**

Swagger disponível em /swagger-ui/index.html

*Billing*
* /api/billing/{id} - Importa uma invoice da API externa para o banco de dados local

*Customer*
* POST /api/customers - Cria um novo cliente
* GET /api/customers - Retorna todos os clientes existentes no banco de dados
* PUT /api/customers/{id} - Atualiza um cliente
* DELETE /api/customers/{id} - Deleta um cliente
* GET /api/customers/{id} - Busca um cliente por id

*Product*
* POST /api/products - Cria um novo produto
* GET /api/products - Retorna todos os produtos existentes no banco de dados
* PUT /api/products/{id} - Atualiza um produto
* DELETE /api/products/{id} - Deleta um produto
* GET /api/products/{id} - Busca um produto por id
