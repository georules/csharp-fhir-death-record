FROM microsoft/dotnet:sdk AS build-env
WORKDIR /app
COPY bin/Debug/netcoreapp2.1/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "FhirDeathRecord.HTTP.dll"]