# Clean Architecture .NET 9 - Kiến trúc thư mục thực tế

## Tổng quan

Dự án tổ chức theo Clean Architecture, DDD, CQRS, Specification Pattern, sử dụng tiếng Việt cho tên class/method, tập trung phát triển theo module tính năng (features-based).

---

## Cấu trúc thư mục thực tế

```
SkTeam.sln
SkTeam/
├── API/                # Tầng trình bày (Web API)
│   ├── API.csproj
│   ├── Program.cs
│   ├── appsettings.json
│   ├── Controller/
│   │   ├── ProductsController.cs
│   │   └── BaseApi/
│   │       └── DeleteAndRecoveryController.cs
│   ├── DependencyInjection/
│   ├── bin/
│   ├── obj/
│   └── Properties/
├── Application/        # Tầng ứng dụng (CQRS, UseCase)
│   ├── Application.csproj
│   ├── Behaviors/
│   ├── DTOs/
│   ├── QueryServices/
│   ├── Services/
│   │   ├── BaseServices/
│   │   │   ├── HardDelete/
│   │   │   ├── Recovery/
│   │   │   └── SoftDelete/
│   │   └── Products/
│   │       ├── Commands/
│   │       │   └── CreateProduct/
│   │       └── Queries/
│   ├── bin/
│   ├── obj/
├── Domain/             # Tầng nghiệp vụ (Business Logic)
│   ├── Domain.csproj
│   ├── Entities/
│   │   ├── Products/
│   │   │   └── ProductAggregate/
│   │   └── Users/
│   ├── Events/
│   ├── Exceptions/
│   ├── Repositories/
│   ├── Rules/
│   ├── Services/
│   ├── ValueObjects/
│   │   └── Product/
│   ├── bin/
│   ├── obj/
├── Infrastructure/     # Tầng hạ tầng (Data, External Services)
│   ├── Infrastructure.csproj
│   ├── DependencyInjection/
│   ├── Persistences/
│   │   ├── AppDbContext.cs
│   │   ├── AppDbContextFactory.cs
│   │   ├── EfRepository.cs
│   │   ├── EntityConfigurations/
│   │   └── Migrations/
│   ├── QueryServices/
│   ├── Repositories/
│   │   └── Products/
│   ├── Services/
│   ├── bin/
│   ├── obj/
├── SharedKernel/       # Thành phần dùng chung
│   ├── SharedKernel.csproj
│   ├── Base/
│   ├── Events/
│   ├── Exceptions/
│   ├── Interfaces/
│   ├── Specifications/
│   ├── bin/
│   ├── obj/
```

---

## Mô tả các tầng

- **API**: Chứa các controller, cấu hình khởi động, xử lý HTTP request/response.
- **Application**: Chứa các service nghiệp vụ, CQRS handler, DTO, validation, orchestrate luồng nghiệp vụ.
- **Domain**: Chứa entity, value object, repository interface, rule, exception, domain service, tập trung toàn bộ business logic.
- **Infrastructure**: Chứa triển khai repository, DbContext, migration, tích hợp external service, cấu hình DI cho hạ tầng.
- **SharedKernel**: Chứa các interface, base class, exception, specification, event dùng chung giữa các tầng.

---

## Quy tắc phát triển

- Tên class, method, biến sử dụng tiếng Việt rõ nghĩa.
- Tách biệt rõ các tầng, không để tầng dưới phụ thuộc tầng trên.
- Áp dụng CQRS: Command và Query tách biệt.
- Sử dụng Specification Pattern cho các truy vấn phức tạp.
- Tổ chức code theo module tính năng (feature-based) trong Application/Services.
- Không để logic nghiệp vụ ở tầng API hoặc Infrastructure.

---

## Hướng dẫn build và chạy

```bash
# Restore package
dotnet restore

# Build solution
dotnet build SkTeam.sln

# Chạy API
dotnet run --project SkTeam/API
```

---

## Script hỗ trợ

- `clean_obj_bin.ps1`: Xóa toàn bộ thư mục bin và obj trong solution.
- `init_project.ps1`: Khởi tạo lại solution, cài đặt các package cần thiết.

---

## Đóng góp

- Fork repo, tạo branch mới, commit và tạo pull request.
- Ưu tiên code rõ ràng, có unit test cho logic nghiệp vụ.

---

## License

MIT License.
