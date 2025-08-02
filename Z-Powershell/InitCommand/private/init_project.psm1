# Clean Architecture Project Generator
function Initialize-Project {
    param (
        [string]$YourProjectName
    )
    
    if (-not $YourProjectName) {
        $YourProjectName = "MySolution"
    }
    # 1. Create solution folder and solution file

    dotnet new sln -n $YourProjectName

    # 2. Create project folders
    New-Item -ItemType Directory -Path "API"
    New-Item -ItemType Directory -Path "Application"
    New-Item -ItemType Directory -Path "Domain"
    New-Item -ItemType Directory -Path "Infrastructure"
    New-Item -ItemType Directory -Path "SharedKernel"

    # 3. Create base projects
    dotnet new webapi -n "API" -o "API"
    dotnet new classlib -n "Application" -o "Application"
    dotnet new classlib -n "Domain" -o "Domain"
    dotnet new classlib -n "Infrastructure" -o "Infrastructure"
    dotnet new classlib -n "SharedKernel" -o "SharedKernel"
    dotnet new xunit -n "UnitTests" -o "tests/UnitTests"
    dotnet new xunit -n "IntegrationTests" -o "tests/IntegrationTests"

    # 4. Add project references
    dotnet sln add "API/API.csproj"
    dotnet sln add "Application/Application.csproj"
    dotnet sln add "Domain/Domain.csproj"
    dotnet sln add "Infrastructure/Infrastructure.csproj"
    dotnet sln add "SharedKernel/SharedKernel.csproj"

    # Add references between projects
    dotnet add "API/API.csproj" reference "Application/Application.csproj"
    dotnet add "API/API.csproj" reference "Infrastructure/Infrastructure.csproj"
    dotnet add "Application/Application.csproj" reference "Domain/Domain.csproj"
    dotnet add "Application/Application.csproj" reference "SharedKernel/SharedKernel.csproj"
    dotnet add "Domain/Domain.csproj" reference "SharedKernel/SharedKernel.csproj"
    dotnet add "Infrastructure/Infrastructure.csproj" reference "Application/Application.csproj"
    dotnet add "Infrastructure/Infrastructure.csproj" reference "Domain/Domain.csproj"
    dotnet add "Infrastructure/Infrastructure.csproj" reference "SharedKernel/SharedKernel.csproj"
}