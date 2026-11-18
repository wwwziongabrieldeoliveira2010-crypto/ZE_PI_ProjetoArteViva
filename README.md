# ZE_PI_ProjetoArteViva
Projeto para ser apresentado para o PI, com objetivo de se um sistema de Eventos Culturais.
Instalar Dependecias:dotnet add package Microsoft.EntityFrameworkCore --version 10.0.12
dotnet add package Microsoft.EntityFrameworkCore.Design --version 10.0.12
dotnet add package MySql.EntityFrameworkCore --version 10.0.12 Depois colocar a Ferramenta Global do EF:dotnet tool install --global dotnet-ef --version 10.0.12, caso ja estiver instalado, atualiza:dotnet tool update --global dotnet-ef --version 10.0.12. pacotes que não podem estar instalado e devem ser removidos:dotnet remove package Microsoft.EntityFrameworkCore.SqlServer
dotnet remove package System.Security.Cryptography.Pkcs
dotnet remove package System.Security.Cryptography.Xml
por fim:dotnet restore
dotnet build
dotnet ef database update
dotnet run

Nome da porta para entrar:http://localhost:5000
