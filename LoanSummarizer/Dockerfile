FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["LoanSummarizer/LoanSummarizer.csproj", "LoanSummarizer/"]
RUN dotnet restore "LoanSummarizer/LoanSummarizer.csproj"
COPY . .
WORKDIR "/src/LoanSummarizer"
RUN dotnet build "LoanSummarizer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LoanSummarizer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LoanSummarizer.dll"]
