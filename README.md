# Encurtador de url

![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=csharp&logoColor=white)
![.Net](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)
![Redis](https://img.shields.io/badge/redis-%23DD0031.svg?style=for-the-badge&logo=redis&logoColor=white)
![Postgres](https://img.shields.io/badge/postgres-%23316192.svg?style=for-the-badge&logo=postgresql&logoColor=white)

# Sobre o projeto

Projeto feito em C# + ASP.NET + Entity Framework Core que implementa um encurtador de url. Para o api foi utilizado PostgreSQL como base dados, Redis para o cache, utilização de logs para observabilidade, biblioteca OneOf para tratamento de erros da api e Docker para containerização da aplicação.

A api contém apenas duas rotas. Uma GET com um codigo de parametro, associado a url desejada que fará o redirecionamento para o link original, e, uma rota POST que cria uma nova url encurtada.

A rota GET possui a seguinte regra de negócio: O codigo passado por parametro é buscado no redis primeiramente e se existir é redirecionado para a url de destino. Caso não exista é buscado na base dados e se existente redireciona a url cadastrada, senão é retornado o satus http not found.

Já a rota POST é passado um dto no body apenas com o campo url. Se o campo for vazio é retornado um bad request, caso contrário o fluxo continua e buscará na base de dados se já existe a url cadastrada. Caso exista retornará o registro existente senão um novo registro será criado no banco de dados e retornará um status created

# Como rodar o projeto

1 - Para utilizar o projeto é necessário clonar o repositório e ter o docker instalado em sua máquina

```bash
git clone https://github.com/gabrielferreira02/encurtador-de-link.git
cd encurtador-de-link
```

2 - Execute os comandos
```bash
docker compose build
docker compose up # Adicione -d se quiser que rode em segundo plano.
```

3 - Para acessar a documentação do projeto, após iniciada a api acesse 
```bash
http://localhost:5140/scalar
```
