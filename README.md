----------
# PetChance EMR 
#### Video Demo: https://vimeo.com/1150545370?share=copy&fl=sv&fe=ci
#### Description: PetChance EMR is an open-source Electronic Medical Record (EMR) system designed for pet owners. The goal of the project is to bring the “organized medical timeline” experience of human EMRs (appointments, medications, notes, documents, and structured findings) into a form that’s practical for pets.

Many pet owners end up tracking health details across vet invoices, scattered paper notes, text messages, and photos. This project centralizes those details into a single system where you can:

-   store pets and their identifying details 
-   track medications and schedules
-   record notes/findings over time
-   view a pet profile as a single “source of truth”
    
From a software perspective, this repository is also meant to demonstrate a real-world, full-stack application built with shared models, a server API, and a modern UI client.

The solution is split into three projects:

-   `Epic-Pet-EMR.Shared` — shared DTOs and enums used by both server and client
-   `Epic-Pet-EMR` — backend API (domain models, database access, controllers, mapping)
-   `Epic-Pet-EMR-Ui` — Blazor WebAssembly UI (client-side web app)
    
This README explains what the app does, how the repository is organized, how to run it locally, and where to extend it.

----------

## Features

Current functionality focuses on the core “pet chart” workflow:

-   Pets
    -   create pets and view a list of pets
    -   edit pet details using UI forms
    -   delete pets with confirmation
    -   display profile images (supports absolute URLs and server-hosted paths)
-   UI patterns implemented (intentionally)
    -   form validation using Blazor `EditForm`
    -   “draft editing” (edit a copy, then save/commit to the list only after a successful API call)
    -   modal-style confirmation overlay for destructive actions
-   Shared-model architecture
    -   the client and server share DTO types so request/response shapes are consistent
    -   mapping layers convert between server domain models and shared DTOs
----------


## Why I chose this project

I didn’t build this just to “prove I can code.” I built it because I’ve lived the problem from both sides.

As a nurse, I’m used to caring for patients with messy, incomplete histories — and I know how much better decisions get when the right information is organized, current, and easy to scan. As a pet owner, I’ve felt the frustration of trying to pull everything together in the moment: vet notes in one portal, lab results in an email, medication instructions on a bottle, photos on a phone, and “what happened last time?” living in my own memory.

Pet health data is fragmented, and when you’re stressed or sleep-deprived, that fragmentation gets even worse.

So I made this project as a capstone-style portfolio application, but with a real purpose: take all those scattered pieces and turn them into something structured, searchable, and usable. Something that feels more like an actual record than a pile of screenshots.

Technically, it also showcases a complete, non-trivial implementation:

-   a clear problem being solved (pet health data is fragmented)
-   a full working system (UI + API + shared models)
-   thoughtful structure (DTOs, separation of concerns, mapping)
-   clean organization (three-project solution, services, pages/components)

It’s the foundation for an EMR-style system that could genuinely grow into something useful (documents, vet visits, assessments, body-map findings, history timelines, and more).

----------

## Tech stack

-   .NET 8
-   ASP.NET Core Web API (server)
-   Blazor WebAssembly (client UI)
-   Shared project for DTOs/enums (`Epic-Pet-EMR.Shared`)
----------

## Requirements

-   .NET 8 SDK
-   Optional: Visual Studio 2022/2023+, VS Code, Rider, or another editor

----------

## Run locally

1.  From repository root:
`dotnet restore
dotnet build` 

2.  Start the backend (API):
`cd Epic-Pet-EMR
dotnet run` 

3.  Start the UI:
`cd ../Epic-Pet-EMR-Ui
dotnet run` 

4.  Open the UI URL shown in the UI project’s output (ports are defined in `Epic-Pet-EMR-Ui/Properties/launchSettings.json`).
    

----------

## Project layout and important files

Paths are relative to repository root.

### Epic-Pet-EMR.Shared/

Shared DTOs/enums used by both the server and the UI. The point of this project is to keep client/server “contracts” consistent and avoid duplicated model definitions.

-   `Epic-Pet-EMR.Shared.csproj` — shared types package used by server and client.
-   `Models/Enum.cs` — shared enums (example: `Sex`).
-   `Models/MedicationDto.cs` — DTOs used by UI and API.
-   `Models/*.cs` — additional DTOs used across client/server.

### Epic-Pet-EMR/ (server)

ASP.NET Core API project. Responsible for persistence, business logic, and exposing endpoints the UI consumes.

-   `Epic-Pet-EMR.csproj`
-   `Program.cs` — server startup: DI registration, middleware, routing, CORS, host settings.
-   `Models/Pet.cs` — domain model representing a pet.
-   `Models/Medication.cs` — domain model for medications.
-   `Mappers/PetMapper.cs` — maps domain models ↔ shared DTOs.
-   `Mappers/MedicationMapper.cs` — medication mapping helpers.
-   `Controllers/` (if present) — API controllers implementing endpoints the UI calls.
-   `Properties/launchSettings.json` — server launch configuration and ports.

### Epic-Pet-EMR-Ui/ (Blazor Web UI)

Blazor WebAssembly client project. Responsible for rendering pages, handling forms, and calling the API through service classes.

-   `Epic-Pet-EMR-Ui.csproj`
-   `Program.cs` — registers UI services (like `HttpClient`, API services).
-   `Pages/Pets.razor` — main pet management page:
    -   loads pets from API on startup
    -   supports row-level edit via a draft copy + `EditForm`
    -   supports adding pets via `EditForm`
    -   supports delete confirmation overlay
    -   handles `ProfilePic` paths (local vs absolute URLs)
-   `Pages/PetProfile.razor` — pet profile view (single pet).
-   `Pages/Brain.razor`, `Pages/MAR.razor` — additional feature pages (examples / in-progress areas).
-   `Components/Grid.razor` — shared UI component used by pages.
-   `Services/PetApi.cs` — client-side service for API calls:
    -   `GetAllPetsAsync`
    -   `AddPetAsync`
    -   `UpdatePetAsync`
    -   `DeletePet`
-   `Services/MedicationScheduleService.cs` — medication scheduling helper logic.
-   `wwwroot/index.html` — Blazor host page.
-   `wwwroot/css/app.css` — global styles.
-   `wwwroot/js/colorTools.js` — small JS utilities used by UI to analyze profile pictures and generate color variables.
-   `Properties/launchSettings.json` — UI launch configuration and ports.

----------

## Key UI behaviors (implementation details)

### Pets list + editing (`Pages/Pets.razor`)

-   Data loading:
    -   `petsLoadedFromApi` is populated during `OnInitializedAsync` via `PetApi.GetAllPetsAsync()`.
-   Editing workflow (draft pattern):
    -   `EnableEdit` sets `petCurrentlyBeingEdited` to the list item
    -   creates a deep copy into `petDraftBeingEdited`
    -   the table is wrapped in an `EditForm` bound to the draft
    -   `SaveChangesAsync` calls the API to persist changes
    -   after success, the draft values are copied back into the original list item
        
This pattern avoids UI showing “saved” changes until the server confirms the update succeeded.
-   Canceling:
    -   discards the draft by clearing `petCurrentlyBeingEdited`
-   Deleting:
    -   shows a modal overlay (`deleteconfirm`)
    -   calls `PetApi.DeletePet(pet)` on confirmation
-   Image handling:
    -   if `ProfilePic` starts with `http`, it’s used directly 
    -   otherwise the UI prefixes a backend base URL (example currently uses `https://localhost:60174{pic}`)
    -   update that base URL to match your backend URL when running locally

----------

## API surface expected by UI

The UI assumes a basic CRUD API for pets. Typical endpoints:

-   `GET /api/pets` — get all pets
-   `POST /api/pets` — add a pet
-   `PUT /api/pets/{id}` — update a pet
-   `DELETE /api/pets/{id}` — delete a pet
    
If your server routes differ, update `Epic-Pet-EMR-Ui/Services/PetApi.cs`.

----------

## Design choices (what to notice)

-   Shared DTOs:
    -   Keeps client/server contract consistent.
    -   Makes it easier to refactor without mismatched request/response shapes.
-   Mapping layer:
    -   Server domain models don’t have to match DTOs 1:1.
    -   Makes it easier to evolve the DB model without breaking the UI immediately.
-   Service layer in UI (`PetApi.cs`):
    -   Keeps HTTP logic out of `.razor` pages.
    -   Pages stay focused on UI state and rendering.

----------

## Limitations / known gaps

This is an active project and currently focuses on core flows rather than a fully featured EMR.

Examples of features that may be partial or not implemented yet:

-   authentication / multi-user accounts
-   role-based access (owner vs vet vs family member)
-   document uploads and storage strategy
-   full medication administration record (MAR) auditing
-   full vet visit timelines and structured problem lists
-   automated reminders / scheduling integrations
    
----------

## Extending the app

Common extension path:
1.  Add or modify DTOs in `Epic-Pet-EMR.Shared/Models/`
2.  Update server domain models in `Epic-Pet-EMR/Models/`
3.  Update mapping in `Epic-Pet-EMR/Mappers/`
4.  Update API endpoints/controllers in `Epic-Pet-EMR/Controllers/`
5.  Update UI forms/pages in `Epic-Pet-EMR-Ui/Pages/`
    
----------

## Troubleshooting

-   UI can’t reach backend:
    
    -   confirm both projects are running
    -   check CORS settings in `Epic-Pet-EMR/Program.cs`
    -   verify ports match `launchSettings.json`
-   Images not loading:
    -   ensure the base URL prefix in `Pets.razor` matches your server URL
    -   confirm the backend is configured to serve static files if using server-hosted images
        
