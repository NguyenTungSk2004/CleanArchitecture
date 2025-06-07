# Để tạo 1 migartion mới

dotnet ef migrations add <Tên migration> --context AppDbContext -p ../HaiphongTech.Infrastructure/HaiphongTech.Infrastructure.csproj -s HaiphongTech.API.csproj --output-dir Persistences/Migrations

# Để gỡ migration gần nhất

dotnet ef migrations remove --context AppDbContext -p ../HaiphongTech.Infrastructure/HaiphongTech.Infrastructure.csproj -s HaiphongTech.API.csproj

# Để xem danh sách migration

dotnet ef migrations list --context AppDbContext -p ../HaiphongTech.Infrastructure/HaiphongTech.Infrastructure.csproj -s HaiphongTech.API.csproj

# Để cập nhật migration lên Database 

dotnet ef database update --context AppDbContext -p ../HaiphongTech.Infrastructure/HaiphongTech.Infrastructure.csproj -s HaiphongTech.API.csproj

# Để cập nhật migration cụ thể lên Database 

dotnet ef database update <Tên migration> --context AppDbContext -p ../HaiphongTech.Infrastructure/HaiphongTech.Infrastructure.csproj -s HaiphongTech.API.csproj