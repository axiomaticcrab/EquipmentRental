#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see http://aka.ms/containercompat 

FROM microsoft/dotnet:2.1-aspnetcore-runtime-nanoserver-1803 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk-nanoserver-1803 AS build
WORKDIR /src
COPY src/EquipmentRental.Ui/EquipmentRental.Ui.csproj src/EquipmentRental.Ui/
RUN dotnet restore src/EquipmentRental.Ui/EquipmentRental.Ui.csproj
COPY . .
WORKDIR /src/src/EquipmentRental.Ui
RUN dotnet build EquipmentRental.Ui.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish EquipmentRental.Ui.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "EquipmentRental.Ui.dll"]
