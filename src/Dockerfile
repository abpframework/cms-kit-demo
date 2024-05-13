FROM mcr.microsoft.com/dotnet/aspnet:7.0  AS base
RUN apt-get update && apt-get install -y libgdiplus
WORKDIR /app
EXPOSE 80
COPY CmsKitDemo/bin/Release/publish .
COPY CmsKitDemo.db .
ENTRYPOINT ["dotnet", "CmsKitDemo.dll"]
