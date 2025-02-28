HydroMeasure
Sistema de Monitoramento de Consumo de Água para Condomínios
HydroMeasure é uma solução completa para gestão e monitoramento do consumo de água em condomínios residenciais e comerciais, permitindo o controle eficiente dos recursos hídricos e detecção precoce de anomalias.
Características Principais

Multiplataforma: Aplicação híbrida que funciona em navegadores web e dispositivos móveis (Android e iOS)
Gestão de Condomínios: Cadastro e administração de múltiplos condomínios
Controle de Unidades: Gerenciamento de apartamentos, casas ou unidades comerciais
Registro de Medições: Acompanhamento das leituras dos hidrômetros
Detecção de Anomalias: Sistema de alertas para consumo anormal e possíveis vazamentos
Relatórios Detalhados: Visualização de dados de consumo por período, unidade ou condomínio
Dashboard Interativo: Visualização gráfica dos dados de consumo

Tecnologias Utilizadas

.NET 9: Framework base para todo o sistema
Blazor Hybrid: Interface de usuário utilizando Blazor em MAUI e ASP.NET Core
Entity Framework Core: ORM para acesso a dados
MudBlazor: Componentes Material Design para Blazor
CQRS com MediatR: Padrão de arquitetura para separação de responsabilidades
SQLite: Banco de dados leve para armazenamento de dados

Arquitetura
O projeto segue os princípios da Clean Architecture, organizado nas seguintes camadas:

HydroMeasure.Domain: Entidades e regras de negócio
HydroMeasure.Application: Casos de uso e DTOs
HydroMeasure.Infrastructure: Implementações de persistência
HydroMeasure.Api: API REST para acesso aos dados
HydroMeasure.Shared: Componentes compartilhados
HydroMeasure.Hibrid: Interface de usuário Blazor Hybrid
HydroMeasure.Tests: Testes automatizados

Benefícios

Economia de Água: Detecção precoce de vazamentos e consumo excessivo
Transparência: Visualização clara dos dados de consumo para síndicos e moradores
Gestão Eficiente: Facilita o controle e cobrança do consumo individual
Sustentabilidade: Promove o uso consciente de recursos hídricos
Manutenção Preventiva: Identifica problemas antes que causem danos maiores

Requisitos

.NET 9 SDK
Visual Studio 2025 ou superior (ou VS Code)
SQLite

Instalação
bashCopy# Clone o repositório
git clone https://github.com/YourUsername/HydroMeasure.git

# Navegue até o diretório
cd HydroMeasure

# Restaure os pacotes
dotnet restore

# Execute as migrações do banco de dados
dotnet ef database update --project HydroMeasure.Infrastructure --startup-project HydroMeasure.Api

# Inicie a API
dotnet run --project HydroMeasure.Api

# Em outro terminal, inicie a aplicação web
dotnet run --project HydroMeasure.Hibrid.Web
