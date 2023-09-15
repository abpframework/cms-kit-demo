FROM mcr.microsoft.com/dotnet/sdk:7.0.102-bullseye-slim AS base
WORKDIR /src
COPY CmsKitDemo.db .
COPY CmsKitDemo/ .
RUN dotnet tool install -g Volo.Abp.Cli
ENV PATH="${PATH}:/root/.dotnet/tools"
RUN abp install-libs
RUN dotnet publish -c Release -o bin/Release/publish

FROM mcr.microsoft.com/dotnet/aspnet:7.0-bullseye-slim AS final
WORKDIR /libs
COPY --from=base /src/wwwroot/libs/ .
WORKDIR /app
EXPOSE 80
COPY --from=base /src/CmsKitDemo.db .
COPY --from=base /src/bin/Release/publish .
ENTRYPOINT ["dotnet", "CmsKitDemo.dll"]