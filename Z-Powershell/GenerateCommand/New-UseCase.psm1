function New-Auditable {
    param (
        [Parameter(Mandatory = $true)]
        [string]$ModuleName,

        [Parameter(Mandatory = $true)]
        [string]$FunctionName,

        [Parameter(Mandatory = $true)]
        [string]$Path,

        [Parameter(Mandatory = $true)]
        [ValidateSet("SoftDelete", "HardDelete", "Recovery")]
        [string]$Type
    )

    $content = @"
using Application.UseCases.BaseAuditable.${Type};
using Domain.${ModuleName}Module.Entities;
using SharedKernel.Interfaces;

namespace Application.UseCases.${ModuleName}Module.Auditable
{
    public record ${FunctionName}${Type}Command(List<long> Ids, long UserId) : Generic${Type}Command(Ids, UserId);
    public class ${FunctionName}${Type}Handler : Generic${Type}Handler<${FunctionName}, ${FunctionName}${Type}Command>
    {
        public ${FunctionName}${Type}Handler(IRepository<${FunctionName}> repository) : base(repository) { }
    }
}
"@

    Set-Content -Path (Join-Path $Path "${ModuleName}${Type}.cs") -Value $content
    Write-Host "✅ Đã tạo file ${ModuleName}${Type}.cs tại $Path"
}

function New-UseCase {
    param (
        [Parameter(Mandatory = $true)]
        [string]$ModuleName,
        [Parameter(Mandatory = $true)]
        [string]$FunctionName
    )

    # Chuyển sang PascalCase (chữ cái đầu viết hoa)
    $ModuleName = $ModuleName.Substring(0,1).ToUpper() + $ModuleName.Substring(1)
    $ModuleFolder = "${ModuleName}Module"
    $useCasesPath = "Application/UseCases"
    $modulePath = Join-Path $useCasesPath $ModuleFolder

    $auditablePath = Join-Path $modulePath "Auditable"
    $commandPath = Join-Path $modulePath "Commands"
    $queryPath = Join-Path $modulePath "Queries"
    $queryServicesPath = Join-Path $modulePath "QueryServices"

    # Tạo thư mục nếu chưa có
    if (-not (Test-Path $modulePath)) {
        New-Item -ItemType Directory -Path $modulePath -Force | Out-Null
    }
    if (-not (Test-Path $auditablePath)) {
        New-Item -ItemType Directory -Path $auditablePath -Force | Out-Null
    }
    if (-not (Test-Path $commandPath)) {
        New-Item -ItemType Directory -Path $commandPath -Force | Out-Null
    }
    if (-not (Test-Path $queryPath)) {
        New-Item -ItemType Directory -Path $queryPath -Force | Out-Null
    }
    if (-not (Test-Path $queryServicesPath)) {
        New-Item -ItemType Directory -Path $queryServicesPath -Force | Out-Null
    }
    
    New-Auditable -ModuleName $ModuleName -FunctionName $FunctionName -Path $auditablePath -Type "SoftDelete"
    New-Auditable -ModuleName $ModuleName -FunctionName $FunctionName -Path $auditablePath -Type "HardDelete"
    New-Auditable -ModuleName $ModuleName -FunctionName $FunctionName -Path $auditablePath -Type "Recovery"
}

Export-ModuleMember -Function New-UseCase