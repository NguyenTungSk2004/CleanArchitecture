# ============================
# 📦 Cài đặt lại các package mới nhất cho Clean Architecture
# ============================

# Base path đến thư mục src (bạn có thể chỉnh lại nếu cần)
$basePath = ".\src"

# ----------------------------
# Domain Layer
# ----------------------------
dotnet add "$basePath\HaiphongTech.Domain\HaiphongTech.Domain.csproj" package Ardalis.GuardClauses

# ----------------------------
# SharedKernel Layer
# ----------------------------
dotnet add "$basePath\HaiphongTech.SharedKernel\HaiphongTech.SharedKernel.csproj" package Ardalis.Specification

# ----------------------------
# Application Layer
# ----------------------------
dotnet add "$basePath\HaiphongTech.Application\HaiphongTech.Application.csproj" package MediatR
dotnet add "$basePath\HaiphongTech.Application\HaiphongTech.Application.csproj" package FluentValidation

# ----------------------------
# Infrastructure Layer
# ----------------------------
dotnet add "$basePath\HaiphongTech.Infrastructure\HaiphongTech.Infrastructure.csproj" package Microsoft.EntityFrameworkCore
dotnet add "$basePath\HaiphongTech.Infrastructure\HaiphongTech.Infrastructure.csproj" package Microsoft.EntityFrameworkCore.SqlServer
dotnet add "$basePath\HaiphongTech.Infrastructure\HaiphongTech.Infrastructure.csproj" package Ardalis.Specification.EntityFrameworkCore
dotnet add "$basePath\HaiphongTech.Infrastructure\HaiphongTech.Infrastructure.csproj" package Microsoft.AspNetCore.Http.Abstractions
dotnet add "$basePath\HaiphongTech.Infrastructure\HaiphongTech.Infrastructure.csproj" package Microsoft.EntityFrameworkCore.Design
dotnet add "$basePath\HaiphongTech.Infrastructure\HaiphongTech.Infrastructure.csproj" package Microsoft.Extensions.Configuration
dotnet add "$basePath\HaiphongTech.Infrastructure\HaiphongTech.Infrastructure.csproj" package Microsoft.Extensions.Configuration.Json

# ----------------------------
# API Layer
# ----------------------------
dotnet add "$basePath\HaiphongTech.API\HaiphongTech.API.csproj" package MediatR
dotnet add "$basePath\HaiphongTech.API\HaiphongTech.API.csproj" package MediatR.Extensions.Microsoft.DependencyInjection --version 12.1.1
dotnet add "$basePath\HaiphongTech.API\HaiphongTech.API.csproj" package FluentValidation.AspNetCore
dotnet add "$basePath\HaiphongTech.API\HaiphongTech.API.csproj" package Swashbuckle.AspNetCore
dotnet add "$basePath\HaiphongTech.API\HaiphongTech.API.csproj" package Mapster
dotnet add "$basePath\HaiphongTech.API\HaiphongTech.API.csproj" package Mapster.DependencyInjection
dotnet add "$basePath\HaiphongTech.API\HaiphongTech.API.csproj" package Microsoft.AspNetCore.Authentication.JwtBearer
