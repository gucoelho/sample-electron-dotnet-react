dotnet tool restore

rm -rf ./Churritos.Dominio/Migrations/**

dotnet ef migrations add EstruturaInicial --project .\Churritos.Dominio\ --startup-project .\Churritos.App\ --verbose

rm Churritos.App/churritos.db**

dotnet ef database update -s .\Churritos.App\Churritos.App.csproj
