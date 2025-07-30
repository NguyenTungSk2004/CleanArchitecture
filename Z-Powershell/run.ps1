function Start-Project {
    $api = "API"

    try {
        Set-Location $api
        dotnet watch run
    }
    catch {
        Write-Host "Đã xảy ra lỗi: $_"
    }
    finally {
        Set-Location ..
        Write-Host "Quá trình chạy API đã kết thúc."
    }
}