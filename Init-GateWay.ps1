$modulePath = "GateWay"
$projects = @(
    @{ Name = "WebAPI";         Template = "webapi" },
    @{ Name = "Application";    Template = "classlib" },
    @{ Name = "Infrastructure"; Template = "classlib" }
)

Write-Host "🛠️  Creating module: .."

# Create module folders and projects
foreach ($project in $projects) {
    $projectPath = "$modulePath/$($project.Name)"
    dotnet new $project.Template -n $project.Name -o $projectPath
    dotnet sln add "$projectPath/$($project.Name).csproj"
}

# Add references
dotnet add "$modulePath/WebAPI/WebAPI.csproj" reference `
           "$modulePath/Application/Application.csproj" `
           "$modulePath/Infrastructure/Infrastructure.csproj"

dotnet add "$modulePath/Application/Application.csproj" reference `

dotnet add "$modulePath/Infrastructure/Infrastructure.csproj" reference `

# Optional: Link to SharedKernel if exists
$sharedKernelPath = "SharedKernel/SharedKernel.csproj"
if (Test-Path $sharedKernelPath) {
    dotnet add "$modulePath/Infrastructure/Infrastructure.csproj" reference "$sharedKernelPath"
    dotnet add "$modulePath/Application/Application.csproj" reference "$sharedKernelPath"
}

# Folder structure for each project
New-Item -ItemType Directory -Path "$modulePath/Application/Services","$modulePath/Application/Common","$modulePath/Application/QueryServices","$modulePath/Application/DTOs" -Force
New-Item -ItemType Directory -Path "$modulePath/Infrastructure/Services","$modulePath/Infrastructure/Persistence","$modulePath/Infrastructure/Repositories","$modulePath/Infrastructure/DependencyInjection", "$modulePath/Infrastructure/Services" -Force
New-Item -ItemType Directory -Path "$modulePath/WebAPI/Controllers","$modulePath/WebAPI/DependencyInjection" -Force

Write-Host "✅ Module '$moduleName' created successfully."
