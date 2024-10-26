# PokemonAPI
Uma API para serviçoes relacionados a pokemons e treinadores

# Tecnologias
- NET 8 / C#
- Entity Framework Core 8
- SQLite
- Swagger

# Estrutura
- PokemonAPI: Projeto principal
	- responsável por definir os endpoints
- PokemonApplication: Camada de aplicação
	- responsável por definir regras de exibição de dados e verificar erros	
- PokemonDomain: Camada de domínio
	- responsável por definir as entidades e regras de negócio
- PokemonInfrastructure: Camada de infraestrutura
	- responsável pelas dependências externas 

Foi utilizado a arquitetura Clean Architecture. Por ser a arquitetura mais utilizada para implementação de APIs.
Alguns conceitos de DDD foram utilizados para definir a modelagem.

# Como usar
- Clone o repositório
- Ir no appsettings.Development.json e alterar o path do sqlite para o que desejar
- Ir no appsettings.Development.json e definir a quantidade de pokemons para popular o banco (o padrão é 15)
	- Os pokemons são obtidos da api https://pokeapi.co/. Quanto maior a quantidade, mais tempo irá demorar para popular o banco
	- A migration é aplicada automaticamente ao rodar a aplicação
- Rodar a aplicação utilizando o perfil https. Uma página com o swagger irá abrir
- Utiliza o swagger para testar os endpoints

> This is a challenge by Coodesh

---
# Deve conter o título do projeto [OK]
# Uma descrição sobre o projeto em frase [OK]
# Deve conter uma lista com linguagem, framework e/ou tecnologias usadas [OK]
# Como instalar e usar o projeto (instruções) [OK]

# Se está usando github pessoal, referencie que é um challenge by coodesh: [OK]
> This is a challenge by Coodesh

dotnet ef migrations remove -p src/PokemonInfrastructure -s src/PokemonAPI

dotnet ef migrations add Initial -p src/PokemonInfrastructure -s src/PokemonApi --output-dir=Persistence/Migrations


```json
{
  "name": "Ash Ketchup",
  "age": 14,
  "cpf": "07442878040"
}
```

# O que falta
- tratamento de exceções global ou Problem Details ?
- Arrumar o README [OK]
- verificar requisitos [OK]
- criar seed para Trainer para facilitar ? [NO]

# Requisitos
- Documentar todo o processo de investigação para o desenvolvimento da atividade (README.md no seu repositório); 
os resultados destas tarefas são tão importantes do que o seu processo de pensamento e decisões à medida que as completa, 
por isso tente documentar e apresentar os seus hipóteses e decisões na medida do possível.
- Get para 10 Pokémon aleatórios OK
- GetByID para 1 Pokémon específico OK
- Cadastro do mestre pokemon (dados básicos como nome, idade e cpf) em SQLite4  OK
- Post para informar que um Pokémon foi capturado. OK
- Listagem dos Pokémon já capturados. OK
- Todas elas devem retornar os dados básicos do Pokémon, suas evoluções e o base64 de sprite default de cada Pokémon. [OK]