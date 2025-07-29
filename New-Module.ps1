param (
    [string]$moduleName = "Product",
    [string]$solutionName = "CleanArchitecture"
)

$modulePath = "Modules/$moduleName"
$projects = @(
    @{ Name = "$moduleName.WebAPI";         Template = "classlib" },
    @{ Name = "$moduleName.Application";    Template = "classlib" },
    @{ Name = "$moduleName.Domain";         Template = "classlib" },
    @{ Name = "$moduleName.Infrastructure"; Template = "classlib" }
)

Write-Host "🛠️  Creating module: $moduleName..."

# Create module folders and projects
foreach ($project in $projects) {
    $projectPath = "$modulePath/$($project.Name)"
    dotnet new $project.Template -n $project.Name -o $projectPath
    dotnet sln add "$projectPath/$($project.Name).csproj"
}

# Add references
dotnet add "$modulePath/$moduleName.WebAPI/$moduleName.WebAPI.csproj" reference `
           "$modulePath/$moduleName.Application/$moduleName.Application.csproj" `
           "$modulePath/$moduleName.Infrastructure/$moduleName.Infrastructure.csproj"

dotnet add "$modulePath/$moduleName.Application/$moduleName.Application.csproj" reference `
           "$modulePath/$moduleName.Domain/$moduleName.Domain.csproj"

dotnet add "$modulePath/$moduleName.Infrastructure/$moduleName.Infrastructure.csproj" reference `
           "$modulePath/$moduleName.Application/$moduleName.Application.csproj" `
           "$modulePath/$moduleName.Domain/$moduleName.Domain.csproj"

# Optional: Link to SharedKernel if exists
$sharedKernelPath = "SharedKernel/SharedKernel.csproj"
if (Test-Path $sharedKernelPath) {
    dotnet add "$modulePath/$moduleName.Domain/$moduleName.Domain.csproj" reference "$sharedKernelPath"
    dotnet add "$modulePath/$moduleName.Infrastructure/$moduleName.Infrastructure.csproj" reference "$sharedKernelPath"
    dotnet add "$modulePath/$moduleName.Application/$moduleName.Application.csproj" reference "$sharedKernelPath"
}

# Folder structure for each project
New-Item -ItemType Directory -Path "$modulePath/$moduleName.Domain/Entities","$modulePath/$moduleName.Domain/Repositories","$modulePath/$moduleName.Domain/ValueObjects","$modulePath/$moduleName.Domain/Services","$modulePath/$moduleName.Domain/Events" -Force
New-Item -ItemType Directory -Path "$modulePath/$moduleName.Application/Features","$modulePath/$moduleName.Application/Common","$modulePath/$moduleName.Application/Interfaces","$modulePath/$moduleName.Application/DTOs" -Force
New-Item -ItemType Directory -Path "$modulePath/$moduleName.Infrastructure/Services","$modulePath/$moduleName.Infrastructure/Persistence","$modulePath/$moduleName.Infrastructure/Repositories","$modulePath/$moduleName.Infrastructure/DependencyInjection" -Force
New-Item -ItemType Directory -Path "$modulePath/$moduleName.WebAPI/Controllers","$modulePath/$moduleName.WebAPI/DependencyInjection","$modulePath/$moduleName.WebAPI/Filters" -Force

Write-Host "✅ Module '$moduleName' created successfully."
