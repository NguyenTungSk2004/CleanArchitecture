# Clean Architecture Project Generator

$solutionName = "SkTeam"
$root = "$PWD/$solutionName"

# 1. Create solution folder and solution file
mkdir $solutionName
cd $solutionName

# Create the solution file
dotnet new sln -n $solutionName

# 2. Create project folders
mkdir $solutionName.API
mkdir $solutionName.Application
mkdir $solutionName.Domain
mkdir $solutionName.Infrastructure
mkdir $solutionName.SharedKernel

# 3. Create base projects
dotnet new webapi -n "$solutionName.API" -o "$solutionName.API"
dotnet new classlib -n "$solutionName.Application" -o "$solutionName.Application"
dotnet new classlib -n "$solutionName.Domain" -o "$solutionName.Domain"
dotnet new classlib -n "$solutionName.Infrastructure" -o "$solutionName.Infrastructure"
dotnet new classlib -n "$solutionName.SharedKernel" -o "$solutionName.SharedKernel"
dotnet new xunit -n "$solutionName.UnitTests" -o "tests/$solutionName.UnitTests"
dotnet new xunit -n "$solutionName.IntegrationTests" -o "tests/$solutionName.IntegrationTests"

# 4. Add project references
dotnet sln add "$solutionName.API/$solutionName.API.csproj"
dotnet sln add "$solutionName.Application/$solutionName.Application.csproj"
dotnet sln add "$solutionName.Domain/$solutionName.Domain.csproj"
dotnet sln add "$solutionName.Infrastructure/$solutionName.Infrastructure.csproj"
dotnet sln add "$solutionName.SharedKernel/$solutionName.SharedKernel.csproj"

# Add references between projects
dotnet add "$solutionName.API/$solutionName.API.csproj" reference "$solutionName.Application/$solutionName.Application.csproj"
dotnet add "$solutionName.API/$solutionName.API.csproj" reference "$solutionName.Infrastructure/$solutionName.Infrastructure.csproj"
dotnet add "$solutionName.Application/$solutionName.Application.csproj" reference "$solutionName.Domain/$solutionName.Domain.csproj"
dotnet add "$solutionName.Application/$solutionName.Application.csproj" reference "$solutionName.SharedKernel/$solutionName.SharedKernel.csproj"
dotnet add "$solutionName.Domain/$solutionName.Domain.csproj" reference "$solutionName.SharedKernel/$solutionName.SharedKernel.csproj"
dotnet add "$solutionName.Infrastructure/$solutionName.Infrastructure.csproj" reference "$solutionName.Application/$solutionName.Application.csproj"
dotnet add "$solutionName.Infrastructure/$solutionName.Infrastructure.csproj" reference "$solutionName.Domain/$solutionName.Domain.csproj"
dotnet add "$solutionName.Infrastructure/$solutionName.Infrastructure.csproj" reference "$solutionName.SharedKernel/$solutionName.SharedKernel.csproj"

# 5. Create common folder structure for each project
mkdir $solutionName.Domain/Entities
mkdir $solutionName.Domain/ValueObjects
mkdir $solutionName.Domain/Events
mkdir $solutionName.Domain/Repositories
mkdir $solutionName.Domain/Services

mkdir $solutionName.Application/Features
mkdir $solutionName.Application/Common
mkdir $solutionName.Application/Interfaces
mkdir $solutionName.Application/Behaviors
mkdir $solutionName.Application/DTOs

mkdir $solutionName.Infrastructure/Persistence
mkdir $solutionName.Infrastructure/Repositories
mkdir $solutionName.Infrastructure/Services
mkdir $solutionName.Infrastructure/DependencyInjection

mkdir $solutionName.API/Controllers
mkdir $solutionName.API/DependencyInjection
mkdir $solutionName.API/Filters

mkdir $solutionName.SharedKernel/Base
mkdir $solutionName.SharedKernel/Events
mkdir $solutionName.SharedKernel/Interfaces
mkdir $solutionName.SharedKernel/Specifications
