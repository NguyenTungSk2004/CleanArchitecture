# Clean Architecture Project Generator

$solutionName = "HaiphongTech"
$root = "$PWD/$solutionName"

# 1. Create solution folder and solution file
mkdir $solutionName
cd $solutionName

# Create the solution file
dotnet new sln -n $solutionName

# 2. Create project folders
mkdir src
mkdir src/$solutionName.API
mkdir src/$solutionName.Application
mkdir src/$solutionName.Domain
mkdir src/$solutionName.Infrastructure
mkdir src/$solutionName.SharedKernel

# 3. Create base projects
dotnet new webapi -n "$solutionName.API" -o "src/$solutionName.API"
dotnet new classlib -n "$solutionName.Application" -o "src/$solutionName.Application"
dotnet new classlib -n "$solutionName.Domain" -o "src/$solutionName.Domain"
dotnet new classlib -n "$solutionName.Infrastructure" -o "src/$solutionName.Infrastructure"
dotnet new classlib -n "$solutionName.SharedKernel" -o "src/$solutionName.SharedKernel"
dotnet new xunit -n "$solutionName.UnitTests" -o "tests/$solutionName.UnitTests"
dotnet new xunit -n "$solutionName.IntegrationTests" -o "tests/$solutionName.IntegrationTests"

# 4. Add project references
dotnet sln add "src/$solutionName.API/$solutionName.API.csproj"
dotnet sln add "src/$solutionName.Application/$solutionName.Application.csproj"
dotnet sln add "src/$solutionName.Domain/$solutionName.Domain.csproj"
dotnet sln add "src/$solutionName.Infrastructure/$solutionName.Infrastructure.csproj"
dotnet sln add "src/$solutionName.SharedKernel/$solutionName.SharedKernel.csproj"

# Add references between projects
dotnet add "src/$solutionName.API/$solutionName.API.csproj" reference "src/$solutionName.Application/$solutionName.Application.csproj"
dotnet add "src/$solutionName.API/$solutionName.API.csproj" reference "src/$solutionName.Infrastructure/$solutionName.Infrastructure.csproj"
dotnet add "src/$solutionName.Application/$solutionName.Application.csproj" reference "src/$solutionName.Domain/$solutionName.Domain.csproj"
dotnet add "src/$solutionName.Application/$solutionName.Application.csproj" reference "src/$solutionName.SharedKernel/$solutionName.SharedKernel.csproj"
dotnet add "src/$solutionName.Domain/$solutionName.Domain.csproj" reference "src/$solutionName.SharedKernel/$solutionName.SharedKernel.csproj"
dotnet add "src/$solutionName.Infrastructure/$solutionName.Infrastructure.csproj" reference "src/$solutionName.Application/$solutionName.Application.csproj"
dotnet add "src/$solutionName.Infrastructure/$solutionName.Infrastructure.csproj" reference "src/$solutionName.Domain/$solutionName.Domain.csproj"
dotnet add "src/$solutionName.Infrastructure/$solutionName.Infrastructure.csproj" reference "src/$solutionName.SharedKernel/$solutionName.SharedKernel.csproj"

# 5. Create common folder structure for each project
mkdir src/$solutionName.Domain/Entities
mkdir src/$solutionName.Domain/ValueObjects
mkdir src/$solutionName.Domain/Events
mkdir src/$solutionName.Domain/Repositories
mkdir src/$solutionName.Domain/Services

mkdir src/$solutionName.Application/Features
mkdir src/$solutionName.Application/Common
mkdir src/$solutionName.Application/Interfaces
mkdir src/$solutionName.Application/Behaviors
mkdir src/$solutionName.Application/DTOs

mkdir src/$solutionName.Infrastructure/Persistence
mkdir src/$solutionName.Infrastructure/Repositories
mkdir src/$solutionName.Infrastructure/Services
mkdir src/$solutionName.Infrastructure/DependencyInjection

mkdir src/$solutionName.API/Controllers
mkdir src/$solutionName.API/DependencyInjection
mkdir src/$solutionName.API/Filters

mkdir src/$solutionName.SharedKernel/Base
mkdir src/$solutionName.SharedKernel/Events
mkdir src/$solutionName.SharedKernel/Interfaces
mkdir src/$solutionName.SharedKernel/Specifications
