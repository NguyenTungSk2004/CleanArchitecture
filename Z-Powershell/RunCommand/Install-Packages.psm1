function Install-Packages {
    param (
        # Đường dẫn gốc của dự án, mặc định là thư mục hiện tại
        [string]$Path = "."
    )
    [string]$basePath = ".\$Path" -Replace "/", "\"
    # ----------------------------
    # 📂 SharedKernel Layer
    # ----------------------------
    # Ardalis.Specification: Tạo Specification pattern để truy vấn domain phức tạp
    dotnet add "$basePath\SharedKernel\SharedKernel.csproj" package Ardalis.Specification

    # Yitter.IdGenerator để sinh Snowflake ID (tối ưu hiệu suất, tăng dần, khó đoán)
    dotnet add "$basePath\SharedKernel\SharedKernel.csproj" package Yitter.IdGenerator

    # ----------------------------
    # 📂 Application Layer
    # ----------------------------
    # MediatR: Giao tiếp giữa các UseCase (CQRS, Command/Query, Notification)
    dotnet add "$basePath\Application\Application.csproj" package MediatR

    # FluentValidation: Thư viện validate object mạnh mẽ, hỗ trợ custom logic
    dotnet add "$basePath\Application\Application.csproj" package FluentValidation

    # ----------------------------
    # 📂 Infrastructure Layer
    # ----------------------------
    # Entity Framework Core: ORM chính
    dotnet add "$basePath\Infrastructure\Infrastructure.csproj" package Microsoft.EntityFrameworkCore

    # Provider SQL Server cho EF Core
    dotnet add "$basePath\Infrastructure\Infrastructure.csproj" package Microsoft.EntityFrameworkCore.SqlServer

    # Ardalis.Specification.EF: Hỗ trợ dùng Specification pattern với EF Core
    dotnet add "$basePath\Infrastructure\Infrastructure.csproj" package Ardalis.Specification.EntityFrameworkCore

    # Microsoft.AspNetCore.Http.Abstractions: Dùng để trích xuất request info, context trong service
    dotnet add "$basePath\Infrastructure\Infrastructure.csproj" package Microsoft.AspNetCore.Http.Abstractions

    # EF Core Design: Hỗ trợ scaffolding, migration, tools dòng lệnh
    dotnet add "$basePath\Infrastructure\Infrastructure.csproj" package Microsoft.EntityFrameworkCore.Design

    # Microsoft.Extensions.Configuration: Đọc cấu hình app (appsettings.json, v.v)
    dotnet add "$basePath\Infrastructure\Infrastructure.csproj" package Microsoft.Extensions.Configuration

    # Json Configuration Provider
    dotnet add "$basePath\Infrastructure\Infrastructure.csproj" package Microsoft.Extensions.Configuration.Json

    # BCrypt.Net-Next: Thư viện mã hóa mật khẩu an toàn, hỗ trợ bcrypt
    dotnet add "$basePath\Infrastructure\Infrastructure.csproj" package BCrypt.Net-Next

    # ----------------------------
    # 📂 API Layer (Presentation)
    # ----------------------------
    # MediatR core
    dotnet add "$basePath\API\API.csproj" package MediatR

    # MediatR.Extensions.Microsoft.DependencyInjection: Hỗ trợ đăng ký MediatR vào DI container
    dotnet add "$basePath\API\API.csproj" package MediatR.Extensions.Microsoft.DependencyInjection --version 12.1.1

    # FluentValidation.AspNetCore: Tích hợp FluentValidation với ASP.NET validation pipeline
    dotnet add "$basePath\API\API.csproj" package FluentValidation.AspNetCore

    # Swashbuckle.AspNetCore: Dùng để tạo Swagger UI cho API
    dotnet add "$basePath\API\API.csproj" package Swashbuckle.AspNetCore

    # Mapster: Thư viện mapping object siêu nhanh, nhẹ hơn AutoMapper
    dotnet add "$basePath\API\API.csproj" package Mapster

    # Mapster.DependencyInjection: Đăng ký Mapster vào DI container
    dotnet add "$basePath\API\API.csproj" package Mapster.DependencyInjection

    # JWT Bearer Auth: Xác thực token (Bearer) với JWT
    dotnet add "$basePath\API\API.csproj" package Microsoft.AspNetCore.Authentication.JwtBearer
}

Export-ModuleMember -Function Install-Packages