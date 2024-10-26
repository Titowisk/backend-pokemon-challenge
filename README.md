# PokemonAPI
Uma API para servi�oes relacionados a pokemons e treinadores

# Tecnologias
- NET 8 / C#
- Entity Framework Core 8
- SQLite
- Swagger

# Estrutura
- PokemonAPI: Projeto principal
	- respons�vel por definir os endpoints
- PokemonApplication: Camada de aplica��o
	- respons�vel por definir regras de exibi��o de dados e verificar erros	
- PokemonDomain: Camada de dom�nio
	- respons�vel por definir as entidades e regras de neg�cio
- PokemonInfrastructure: Camada de infraestrutura
	- respons�vel pelas depend�ncias externas 

Foi utilizado a arquitetura Clean Architecture. Por ser a arquitetura mais utilizada para implementa��o de APIs.
Alguns conceitos de DDD foram utilizados para definir a modelagem.

# Como usar
- Clone o reposit�rio
- Ir no appsettings.Development.json e alterar o path do sqlite para o que desejar
- Ir no appsettings.Development.json e definir a quantidade de pokemons para popular o banco (o padr�o � 15)
	- Os pokemons s�o obtidos da api https://pokeapi.co/. Quanto maior a quantidade, mais tempo ir� demorar para popular o banco
	- A migration � aplicada automaticamente ao rodar a aplica��o
- Rodar a aplica��o utilizando o perfil https. Uma p�gina com o swagger ir� abrir
- Utiliza o swagger para testar os endpoints

> This is a challenge by Coodesh

---
# Deve conter o t�tulo do projeto [OK]
# Uma descri��o sobre o projeto em frase [OK]
# Deve conter uma lista com linguagem, framework e/ou tecnologias usadas [OK]
# Como instalar e usar o projeto (instru��es) [OK]

# Se est� usando github pessoal, referencie que � um challenge by coodesh: [OK]
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
- tratamento de exce��es global ou Problem Details ?
- Arrumar o README [OK]
- verificar requisitos [OK]
- criar seed para Trainer para facilitar ? [NO]

# Requisitos
- Documentar todo o processo de investiga��o para o desenvolvimento da atividade (README.md no seu reposit�rio); 
os resultados destas tarefas s�o t�o importantes do que o seu processo de pensamento e decis�es � medida que as completa, 
por isso tente documentar e apresentar os seus hip�teses e decis�es na medida do poss�vel.
- Get para 10 Pok�mon aleat�rios OK
- GetByID para 1 Pok�mon espec�fico OK
- Cadastro do mestre pokemon (dados b�sicos como nome, idade e cpf) em SQLite4  OK
- Post para informar que um Pok�mon foi capturado. OK
- Listagem dos Pok�mon j� capturados. OK
- Todas elas devem retornar os dados b�sicos do Pok�mon, suas evolu��es e o base64 de sprite default de cada Pok�mon. [OK]