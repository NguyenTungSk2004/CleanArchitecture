. "$PSScriptRoot\Components\AuditableModule.ps1"

function New-Module {
    param (
        [Parameter(Mandatory = $true)]
        [string]$ModuleName,
        [bool]$SoftDelete = $true,
        [bool]$HardDelete = $true,
        [bool]$Recovery = $true
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
    if (!Test-Path $modulePath) {
        New-Item -ItemType Directory -Path $modulePath -Force | Out-Null
    }
    if (!Test-Path $auditablePath) {
        New-Item -ItemType Directory -Path $auditablePath -Force | Out-Null
    }
    if (!Test-Path $commandPath) {
        New-Item -ItemType Directory -Path $commandPath -Force | Out-Null
    }
    if (!Test-Path $queryPath) {
        New-Item -ItemType Directory -Path $queryPath -Force | Out-Null
    }
    if (!Test-Path $queryServicesPath) {
        New-Item -ItemType Directory -Path $queryServicesPath -Force | Out-Null
    }
    if ($SoftDelete) {
        New-Auditable -ModuleName $ModuleName -Path $auditablePath -Type "SoftDelete"
    }
    if ($HardDelete) {
        New-Auditable -ModuleName $ModuleName -Path $auditablePath -Type "HardDelete"
    }
    if ($Recovery) {
        New-Auditable -ModuleName $ModuleName -Path $auditablePath -Type "Recovery"
    }

}