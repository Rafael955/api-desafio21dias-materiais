# Comandos iniciais:
``` bash
  mkdir webapi-materiais
  cd webapi-materiais
  dotnet new webapi
```

# Comandos git:
``` bash
  code .gitignore 
```
### gerei o conteúdo para ignorar como (Windows, Linux, Mac, DotnetCore, VisualStudioCore) no link: https://www.toptal.com/developers/gitignore
- Criei o repositório e rodei os comandos

``` bash
  git init
  git add .
  git commit -m "Iniciando projeto"
  git remote add origin git@github.com:Rafael955/aula-desafio-21dias-materiais.git
  git branch -M main
  git push -u origin main
```

# Componentes instalados:
``` bash
  dotnet add package Microsoft.EntityFrameworkCore --version 5.0.9
  dotnet add package EntityFrameworkPaginateCore --version 2.1.0
  dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 5.0.9
  dotnet add package Microsoft.EntityFrameworkCore.Tools --version 5.0.9
  dotnet add package Swashbuckle.AspNetCore --version 5.6.3
```

# Comandos para migração para criar:
``` bash
dotnet tool install --global dotnet-ef
dotnet ef migrations add MateriaisAdd
dotnet ef database update
```

# Comandos para atualizar database:
#### Caso você esteja utilizando a aplicação com o código clonado, rode o código abaixo!!!
``` bash
dotnet ef database update
```