$dataSource = "localhost";
$initialCatalog = "RandomBot";
$provider = "Microsoft.EntityFrameworkCore.SqlServer";
$entityFolderPath = "Entities";

$connectionString = "Data Source=$($dataSource);Initial Catalog=$($initialCatalog);Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"
$dbContextName = "RandomBotDbContext";

If (!(Test-Path $entityFolderPath)) {
    New-Item -ItemType Directory -Force -Path $entityFolderPath;
}

Set-Location $entityFolderPath;
Remove-Item *.cs;
Set-Location ..
dotnet ef dbcontext scaffold $connectionString $provider -d -f -c $dbContextName -v -o $entityFolderPath;