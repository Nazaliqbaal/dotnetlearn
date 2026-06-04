FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY dotnet-learn/dotnet-learn.csproj dotnet-learn/
RUN dotnet restore dotnet-learn/dotnet-learn.csproj
COPY dotnet-learn/ dotnet-learn/
RUN dotnet publish dotnet-learn/dotnet-learn.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "dotnet-learn.dll"]
