param(
    [Parameter(Position=0)]
    [String] $MigrationName
)

dotnet tool restore

dotnet ef migrations remove --force --project .\Churritos.Dominio\ --startup-project .\Churritos.App\ --verbose