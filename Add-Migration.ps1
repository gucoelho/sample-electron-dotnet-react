param(
    [Parameter(Position=0)]
    [String] $MigrationName
)
dotnet tool restore

dotnet ef migrations add $($MigrationName) --project .\Churritos.Dominio\ --startup-project .\Churritos.App\ --verbose