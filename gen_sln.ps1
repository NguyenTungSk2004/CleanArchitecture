# Tạo lại file solution (.sln) cho dự án .NET
# Sử dụng PowerShell
# Yêu cầu: dotnet CLI đã cài đặt

# Đặt tên solution
$solutionName = "SkTeam"
$solutionFile = "$solutionName.sln"

# Xóa file .sln cũ nếu tồn tại
if (Test-Path $solutionFile) {
    Remove-Item $solutionFile -Force
}

# Tạo file .sln mới
dotnet new sln -n $solutionName

# Tìm tất cả file .csproj trong thư mục con
$csprojFiles = Get-ChildItem -Path . -Recurse -Filter *.csproj

# Thêm từng project vào solution
foreach ($proj in $csprojFiles) {
    dotnet sln $solutionFile add $proj.FullName
}

Write-Host "Đã tạo lại file $solutionFile và add tất cả project .csproj."
