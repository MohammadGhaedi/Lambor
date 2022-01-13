dotnet tool update --global dotnet-ef --version 5.0.8
dotnet build
dotnet ef --startup-project ../Lambor/ database update --context MsSqlDbContext
pause