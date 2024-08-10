# Use the official .NET SDK image as the base image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

ARG CACHEBUST=31133244

RUN git clone https://github.com/potchara-msu/dotnet-webapi-ef-1.git .

# Copy csproj and restore dependencies
COPY . .

# Build the application
RUN dotnet publish -c Release -o /app/publish

# Use a minimal .NET runtime image for the final image
FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:5500

EXPOSE 5500

ENTRYPOINT ["dotnet", "dotnet-webapi-ef.dll"]

