dotnet tool restore

Remove-Item -Recurse -Force .\Churritos.Dominio\Migrations\**

dotnet ef migrations add EstruturaInicial --project .\Churritos.Dominio\ --startup-project .\Churritos.App\ --verbose

Remove-Item Churritos.App\churritos.db**

dotnet ef database update -s .\Churritos.App\Churritos.App.csproj
