using HaiphongTech.API.DependencyInjection;
using HaiphongTech.Infrastructure.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// ----------------------------
// 🧩 Đăng ký các dịch vụ
// ----------------------------
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddInfrastructure(connectionString!);
builder.Services.AddServices();

// Swagger (tùy chọn)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ----------------------------
// 🚀 Build & Run
// ----------------------------
var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
