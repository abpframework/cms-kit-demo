FROM mcr.microsoft.com/dotnet/sdk:7.0.102-bullseye-slim AS base
WORKDIR /src
COPY . .
RUN dotnet tool install -g Volo.Abp.Cli
ENV PATH="${PATH}:/root/.dotnet/tools"
WORKDIR /src/CmsKitDemo
RUN abp install-libs
RUN dotnet publish -c Release -o bin/Release/publish

FROM mcr.microsoft.com/dotnet/aspnet:7.0-bullseye-slim AS final
RUN apt-get update && apt-get install -y libgdiplus
WORKDIR /app
EXPOSE 80
COPY --from=base /src/CmsKitDemo.db .
COPY --from=base /src/CmsKitDemo/bin/Release/publish .
ENTRYPOINT ["dotnet", "CmsKitDemo.dll"]