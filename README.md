# Inimco Developer Exercise

Een full-stack oplossing voor de Inimco developer exercise: een gebruiker voert voornaam, 
achternaam, social skills en social media accounts in, en krijgt feedback over het aantal 
klinkers/medeklinkers, de omgekeerde naam, en de data in JSON-formaat.

## Architectuur
UI (Angular)  →  Business Logic (.NET Core Web API)  →  Repository (JSON-bestand)
- **Frontend**: Angular 20, standalone components, Reactive Forms
- **Backend**: .NET Core 9, ASP.NET Core Web API, layered architecture (Controller → Service → Repository)
- **Opslag**: file-based repository (`Data/persons.json`)
- **Testing**: xUnit + Moq, unit tests op de business logic
- **Patterns**: Dependency Injection, Repository Pattern, DTO pattern

## Vereisten

- .NET 9 SDK
- Node.js 20+ en Angular CLI 20+

## Starten

### Backend
```bash
cd backend/InimcoExercise.Api
dotnet run
```
Swagger: http://localhost:5088/swagger

### Frontend
```bash
cd frontend
ng serve
```
App: http://localhost:4200

## Tests draaien

```bash
cd backend
dotnet test
```

## Projectstructuur
inimco-developer-exercise/

├── backend/

│   ├── InimcoExercise.Api/

│   │   ├── Controllers/       → REST endpoints

│   │   ├── Services/          → Business logic (vowel/consonant/reverse)

│   │   ├── Repositories/      → Data persistence (file-based)

│   │   ├── Models/            → Domeinmodellen

│   │   └── DTOs/               → Request/Response contracten + validatie

│   └── InimcoExercise.Api.Tests/

└── frontend/

└── src/app/

├── components/

│   ├── person-form/        → Formulier (Reactive Forms, FormArray)

│   └── result-display/     → Toont resultaten

├── services/                → HTTP communicatie met backend

└── models/                  → TypeScript interfaces

## Design keuzes

- **Layered architecture + Repository pattern**: scheidt verantwoordelijkheden en maakt 
  het makkelijk om de opslag later te vervangen (bv. door een database) zonder de business 
  logic aan te raken.
- **DTOs**: ontkoppelen het API-contract van het interne domeinmodel, en dragen de 
  validatie-attributen.
- **Dependency Injection**: `IPersonRepository` en `IPersonAnalysisService` zijn interfaces, 
  geregistreerd in `Program.cs` — maakt de service testbaar met mocks.
- **Unit tests**: testen de pure business logic geïsoleerd via een gemockte repository.