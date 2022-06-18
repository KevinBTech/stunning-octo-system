FROM mcr.microsoft.com/dotnet/sdk:6.0 AS BuildAndPublish
WORKDIR /src
COPY src .
RUN dotnet publish "BTech.ExpenseSystem.WebAPI/BTech.ExpenseSystem.WebAPI.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS Final
WORKDIR /app
EXPOSE 80
WORKDIR /app
COPY --from=BuildAndPublish /app/publish .

ENV ASPNETCORE_URLS=http://+:80
ENV DOTNET_RUNNING_IN_CONTAINER=true

ENV TZ=Europe/Paris

ENTRYPOINT ["dotnet", "BTech.ExpenseSystem.WebAPI.dll"]