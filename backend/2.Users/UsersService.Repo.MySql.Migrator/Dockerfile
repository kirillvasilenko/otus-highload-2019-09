FROM mcr.microsoft.com/dotnet/core/runtime:3.0

COPY bin/Release/netcoreapp3.0/publish/ app/

WORKDIR app

ENTRYPOINT ["dotnet", "UsersService.Repo.MySql.Migrator.dll"]
