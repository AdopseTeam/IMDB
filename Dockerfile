FROM mcr.microsoft.com/dotnet/sdk:5.0 AS base
WORKDIR /IMDB

COPY *.csproj ./
RUN dotnet restore


COPY . ./
RUN dotnet publish -c Release -o /out


FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /IMDB
COPY --from=base /out .
CMD dotnet IMDB.dll