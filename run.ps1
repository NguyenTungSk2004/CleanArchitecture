$api = "API"

try {
    cd $api
    dotnet watch run 
}
catch {
    Write-Host "Đã xảy ra lỗi: $_"
}
finally {
    cd..
    Write-Host "Quá trình chạy API đã kết thúc."
}