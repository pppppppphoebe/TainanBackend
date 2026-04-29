FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY . .
#RUN dotnet publish -c Release -o /app
RUN dotnet publish -c Release -o /app --runtime linux-x64 --self-contained false

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app .
ENV ASPNETCORE_URLS=http://+:10000 
ENTRYPOINT ["dotnet", "TainanBackend.dll"]
# ENV ASPNETCORE_URLS=http://+:8080
# ENTRYPOINT ["dotnet", "TainanBackend.dll"]