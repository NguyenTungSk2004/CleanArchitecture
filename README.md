# Hướng dẫn học tập: Kiến trúc Clean Architecture, DDD, Modular hóa trong dự án .NET

## 1. Tổng quan kiến trúc dự án

Dự án này áp dụng Clean Architecture, Domain-Driven Design (DDD), CQRS và Modular hóa, giúp tách biệt rõ ràng các tầng, dễ mở rộng, bảo trì, kiểm thử. Mỗi module nghiệp vụ được tổ chức độc lập, code rõ ràng, dễ học hỏi.

---

## 2. Cấu trúc thư mục thực tế

```
API/                  # Tầng trình bày, nhận request, trả response
  ├── Controller/         # Các controller cho API
  ├── DependencyInjection/# Cấu hình DI cho API
  ├── Properties/         # Cấu hình môi trường chạy

Application/           # Tầng ứng dụng, orchestration, CQRS, UseCase
  ├── CommandServices/     # Command handler cho nghiệp vụ
  ├── ExternalServices/    # Tích hợp dịch vụ ngoài (nếu có)
  ├── QueryServices/       # Query handler, truy vấn dữ liệu
  ├── UseCases/            # Tổ chức các UseCase theo module nghiệp vụ

Domain/                # Tầng nghiệp vụ, business logic, modular hóa
  ├── AuthModule/          # Module domain xác thực/người dùng
  ├── ProductModule/       # Module domain sản phẩm
  ├── Z-Commons/           # Thành phần dùng chung cho domain

Infrastructure/         # Tầng hạ tầng, triển khai repository, external service
  ├── DependencyInjection/# Cấu hình DI cho hạ tầng
  ├── Persistences/        # DbContext, repository, migration
  ├── QueryServices/       # Triển khai query service
  ├── Services/            # Service tích hợp hạ tầng

SharedKernel/           # Thành phần dùng chung cho toàn bộ solution
  ├── Base/                # Base class, extension
  ├── Events/              # Định nghĩa event
  ├── Exceptions/          # Exception dùng chung
  ├── Interfaces/          # Interface dùng chung
  ├── Specifications/      # Specification pattern
```

---

## 3. Ý nghĩa các module & nguyên lý kiến trúc

### API
- Chứa controller, cấu hình khởi động, nhận request, trả response.
- Không chứa logic nghiệp vụ, chỉ gọi Application.

### Application
- Tổ chức theo UseCase, CQRS, chia module theo nghiệp vụ.
- Chứa các command/query handler, validation, orchestrate luồng nghiệp vụ.
- Không phụ thuộc tầng hạ tầng hay trình bày.

### Domain (Modular hóa)
- Chia module theo domain thực tế: AuthModule, ProductModule, Z-Commons.
- Mỗi module gồm Entity, ValueObject, Rule, Service, Interface, Exception.
- Chỉ chứa business logic thuần, không phụ thuộc bất kỳ tầng nào khác.

### Infrastructure
- Chứa triển khai repository, DbContext, migration, tích hợp external service.
- Tổ chức code theo module tương ứng với domain.
- Chỉ phụ thuộc Application/Domain qua interface.

### SharedKernel
- Chứa các interface, base class, exception, event, specification dùng chung.

---

## 4. Clean Architecture là gì?

Clean Architecture giúp tách biệt các tầng, đảm bảo:
- Business logic độc lập với framework, UI, database.
- Dễ mở rộng, bảo trì, kiểm thử.
- Tầng ngoài có thể phụ thuộc tầng trong, tầng trong không phụ thuộc tầng ngoài.

### Sơ đồ phụ thuộc

```
API ──► Application ──► Domain
  │           │
  ▼           ▼
Infrastructure (chỉ phụ thuộc Application/Domain qua interface)
```

---

## 5. Domain-Driven Design (DDD) là gì?

DDD là phương pháp thiết kế phần mềm tập trung mô hình hóa nghiệp vụ thực tế, giúp code phản ánh đúng các quy tắc, thực thể, hành vi của domain.

- Xây dựng Entity, Value Object, Aggregate Root, Domain Service, Repository, Domain Event.
- Modular hóa domain thành các module độc lập.
- Giao tiếp giữa các tầng qua interface.

---

## 6. Modular hóa trong dự án

- Mỗi module nghiệp vụ (Auth, Product, ...) là một thư mục riêng trong Domain.
- Application cũng chia module theo nghiệp vụ, tách biệt command/query.
- Infrastructure tổ chức code theo module, dễ mở rộng, bảo trì.

---

## 7. Hướng dẫn học tập & phát triển

1. Đọc kỹ cấu trúc thư mục, hiểu rõ vai trò từng tầng/module.
2. Khi thêm nghiệp vụ mới:
   - Tạo module mới trong Domain (Entities, ValueObjects, Rules, Services, ...).
   - Thêm UseCase, Command/Query Handler trong Application.
   - Cài đặt repository, service tương ứng ở Infrastructure.
3. Không để logic nghiệp vụ ở API/Infrastructure.
4. Luôn giao tiếp giữa các tầng qua interface.
5. Ưu tiên code rõ ràng, có unit test cho logic nghiệp vụ.

---

## 8. Hướng dẫn build & chạy

```bash
dotnet restore
dotnet build
dotnet run --project API
```

---

## 9. Script hỗ trợ

- `gen_sln.ps1`, `init_project.ps1`, `package.ps1`, `run.ps1`: Tự động hóa các thao tác quản lý solution, build, chạy dự án.

---

## 10. Đóng góp

- Fork repo, tạo branch mới, commit và tạo pull request.
- Ưu tiên code rõ ràng, có unit test cho logic nghiệp vụ.

---

## 11. License

MIT License.
