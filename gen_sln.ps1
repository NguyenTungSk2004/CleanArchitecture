# Tạo lại file solution (.sln) cho dự án .NET
# Sử dụng PowerShell
# Yêu cầu: dotnet CLI đã cài đặt

# Đặt tên solution
$solutionName = "CleanArchitecture"
$solutionFile = "$solutionName.sln"

$solution = Get-ChildItem -Path . -Recurse -Filter *.sln
    
# Xóa file .sln cũ nếu tồn tại
foreach ($file in $solution) {
    if (Test-Path $file.FullName) {
        Remove-Item $file.FullName -Force
    }
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
