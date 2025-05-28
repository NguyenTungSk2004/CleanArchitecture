# -----------------------------------------------
# PowerShell script: init-clean-architecture.ps1
# Clean Architecture + CQRS + DDD + Feature-based
# -----------------------------------------------

$solutionName = "HaiphongTech"
$srcDir = "src"
$testDir = "tests"

Write-Host "🚀 Khởi tạo solution: $solutionName..." -ForegroundColor Cyan

New-Item -ItemType Directory -Name $solutionName -Force | Out-Null
Set-Location $solutionName

# Khởi tạo solution
dotnet new sln -n $solutionName
New-Item -ItemType Directory -Name $srcDir -Force | Out-Null
New-Item -ItemType Directory -Name $testDir -Force | Out-Null

# Tạo project chính (trừ API)
$projects = @(
    "HaiphongTech.Application",
    "HaiphongTech.Domain",
    "HaiphongTech.Infrastructure",
    "HaiphongTech.SharedKernel"
)
foreach ($proj in $projects) {
    dotnet new classlib -n $proj -o "$srcDir\$proj"
}

# Tạo Web API
dotnet new webapi -n "HaiphongTech.API" -o "$srcDir\HaiphongTech.API" -f net8.0 --force

# Test projects
dotnet new xunit -n "HaiphongTech.UnitTests" -o "$testDir\HaiphongTech.UnitTests"
dotnet new xunit -n "HaiphongTech.IntegrationTests" -o "$testDir\HaiphongTech.IntegrationTests"

# Add projects vào solution
Get-ChildItem -Recurse -Filter *.csproj | ForEach-Object {
    dotnet sln add $_.FullName
}

# Add reference giữa các tầng
$apiProj = "$srcDir\HaiphongTech.API\HaiphongTech.API.csproj"
dotnet add $apiProj reference `
    "$srcDir\HaiphongTech.Application\HaiphongTech.Application.csproj" `
    "$srcDir\HaiphongTech.SharedKernel\HaiphongTech.SharedKernel.csproj"

dotnet add "$srcDir\HaiphongTech.Application\HaiphongTech.Application.csproj" reference `
    "$srcDir\HaiphongTech.Domain\HaiphongTech.Domain.csproj" `
    "$srcDir\HaiphongTech.SharedKernel\HaiphongTech.SharedKernel.csproj"

dotnet add "$srcDir\HaiphongTech.Infrastructure\HaiphongTech.Infrastructure.csproj" reference `
    "$srcDir\HaiphongTech.Application\HaiphongTech.Application.csproj" `
    "$srcDir\HaiphongTech.Domain\HaiphongTech.Domain.csproj" `
    "$srcDir\HaiphongTech.SharedKernel\HaiphongTech.SharedKernel.csproj"

dotnet add "$srcDir\HaiphongTech.Domain\HaiphongTech.Domain.csproj" reference `
    "$srcDir\HaiphongTech.SharedKernel\HaiphongTech.SharedKernel.csproj"

# ----------------------------
# Tải packages cần thiết
# ----------------------------

Write-Host "📦 Installing packages..." -ForegroundColor Yellow

# Application
$appProj = "$srcDir\HaiphongTech.Application\HaiphongTech.Application.csproj"
dotnet add $appProj package MediatR

dotnet add $appProj package MediatR.Extensions.Microsoft.DependencyInjection

dotnet add $appProj package FluentValidation

# Infrastructure
$infraProj = "$srcDir\HaiphongTech.Infrastructure\HaiphongTech.Infrastructure.csproj"
dotnet add $infraProj package Microsoft.EntityFrameworkCore

dotnet add $infraProj package Microsoft.EntityFrameworkCore.SqlServer

dotnet add $infraProj package Microsoft.EntityFrameworkCore.Design

dotnet add $infraProj package Ardalis.Specification

# API
$apiProj = "$srcDir\HaiphongTech.API\HaiphongTech.API.csproj"
dotnet add $apiProj package Swashbuckle.AspNetCore

dotnet add $apiProj package FluentValidation.AspNetCore

# ----------------------------
# Tạo thư mục con (không tạo .readme.txt)
# ----------------------------

function New-Folder {
    param (
        [string]$path
    )
    New-Item -ItemType Directory -Path $path -Force | Out-Null
}

function New-SampleFile {
    param (
        [string]$path,
        [string]$filename,
        [string]$content
    )
    Set-Content -Path "$path\$filename" -Value $content
}

# DOMAIN theo chuẩn DDD
$domainFolders = @("Entities", "ValueObjects", "Aggregates", "DomainEvents", "Interfaces", "Services")
foreach ($f in $domainFolders) {
    $path = "$srcDir\HaiphongTech.Domain\$f"
    New-Folder -path $path
}

# SHARED KERNEL
$sharedFolders = @("Events", "Results", "Exceptions", "Utilities", "Interfaces", "Specifications")
foreach ($f in $sharedFolders) {
    $path = "$srcDir\HaiphongTech.SharedKernel\$f"
    New-Folder -path $path
}

# APPLICATION: Feature-based + CQRS
$featuresPath = "$srcDir\HaiphongTech.Application\Features"
New-Item -ItemType Directory -Path $featuresPath -Force | Out-Null

$features = @("Products", "Orders")
foreach ($feature in $features) {
    $base = "$featuresPath\$feature"
    $subfolders = @("Commands", "Queries", "DTOs")
    foreach ($sub in $subfolders) {
        $subPath = "$base\$sub"
        New-Folder -path $subPath

        switch ($sub) {
            "Commands" {
                New-SampleFile -path $subPath -filename "Create${feature}Command.cs" -content "// Lệnh CQRS tạo $feature"
                break
            }
            "Queries" {
                New-SampleFile -path $subPath -filename "Get${feature}ByIdQuery.cs" -content "// Truy vấn CQRS lấy $feature theo Id"
                break
            }
            "DTOs" {
                New-SampleFile -path $subPath -filename "${feature}Dto.cs" -content "// DTO đại diện cho $feature"
                break
            }
        }
    }
}

# INFRASTRUCTURE
$infraBase = "$srcDir\HaiphongTech.Infrastructure"
New-Item -ItemType Directory -Path "$infraBase\Persistence" -Force | Out-Null
$persistenceSub = @("Repositories", "DbContext", "Migrations")
foreach ($f in $persistenceSub) {
    $path = "$infraBase\Persistence\$f"
    New-Folder -path $path
}
New-Folder -path "$infraBase\External"
New-Folder -path "$infraBase\Messaging"

# Repository interface + EF implementation
New-SampleFile -path "$srcDir\HaiphongTech.SharedKernel\Specifications" -filename "IAggregateRoot.cs" -content @"
namespace HaiphongTech.SharedKernel.Specifications {
    public interface IAggregateRoot { }
}
"@

New-SampleFile -path "$srcDir\HaiphongTech.SharedKernel\Specifications" -filename "IRepository.cs" -content @"
using Ardalis.Specification;

namespace HaiphongTech.SharedKernel.Specifications {
    public interface IRepository<T> : IRepositoryBase<T> where T : class, IAggregateRoot {
        IQueryable<T> Table { get; }
    }
}
"@

New-SampleFile -path "$infraBase\Persistence\Repositories" -filename "EfRepository.cs" -content @"
using Ardalis.Specification.EntityFrameworkCore;
using HaiphongTech.SharedKernel.Specifications;
using Microsoft.EntityFrameworkCore;

namespace HaiphongTech.Infrastructure.Persistence.Repositories {
    public class EfRepository<T> : RepositoryBase<T>, IRepository<T> where T : class, IAggregateRoot {
        private readonly DbContext _dbContext;

        public EfRepository(DbContext dbContext) : base(dbContext) {
            _dbContext = dbContext;
        }

        private IQueryable<T>? _entities;
        public virtual IQueryable<T> Table => _entities ?? (_entities = _dbContext.Set<T>());
    }
}
"@

# API
$apiFolders = @("Controllers", "Middlewares", "DTOs", "Extensions")
foreach ($f in $apiFolders) {
    $path = "$srcDir\HaiphongTech.API\$f"
    New-Folder -path $path
}

Write-Host "✅ Dự án Clean Architecture + CQRS + DDD (Feature-based) đã khởi tạo thành công với hướng dẫn đặt tên!" -ForegroundColor Green
