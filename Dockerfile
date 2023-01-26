FROM  mcr.microsoft.com/dotnet/sdk:6.0.302 AS build-env
WORKDIR /Referencias
RUN mkdir EventBus
RUN mkdir Services
WORKDIR /Referencias/EventBus
RUN mkdir EventBus
RUN mkdir EventBusRabbitMQ
RUN mkdir IntegrationEventLogEF
WORKDIR /Referencias/ReferenciasService/src
RUN mkdir Application
RUN mkdir Domain
RUN mkdir Infrastructure


# Copy csproj and restore as distinct layers
COPY ReferenciasService/src/Application/*.csproj ./Application
COPY ReferenciasService/src/Domain/*.csproj ./Domain
COPY ReferenciasService/src/Infrastructure/*.csproj ./Infrastructure

WORKDIR /Referencias/EventBus
COPY EventBus/EventBus/*.csproj ./EventBus
COPY EventBus/IntegrationEventLogEF/*.csproj ./IntegrationEventLogEF
COPY EventBus/EventBusRabbitMQ/*.csproj ./EventBusRabbitMQ


WORKDIR /Referencias/ReferenciasService/src/Application
RUN dotnet restore

# Copy everything else and build
WORKDIR /Referencias
COPY . ./
WORKDIR /Referencias/ReferenciasService/src/Application
RUN dotnet publish -c Release -o out

# Build runtime image
FROM  mcr.microsoft.com/dotnet/sdk:6.0.302 
WORKDIR /app
COPY --from=build-env /Referencias/ReferenciasService/src/Application/out .
ENTRYPOINT ["dotnet", "Application.dll"]