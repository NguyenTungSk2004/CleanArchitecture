function Initialize-Solution {
    param (
        # Đường dẫn gốc của dự án, mặc định là thư mục hiện tại
        [string]$solutionName
    )
    if (-not $solutionName) {
        $solutionName = "MySolution"
    }    
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
}

Export-ModuleMember -Function Initialize-Solution