function New-Auditable {
    param (
        [Parameter(Mandatory = $true)]
        [string]$ModuleName,

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
    public record ${ModuleName}${Type}Command(List<long> Ids, long UserId) : Generic${Type}Command(Ids, UserId);
    public class ${ModuleName}${Type}Handler : Generic${Type}Handler<${ModuleName}, ${ModuleName}${Type}Command>
    {
        public ${ModuleName}${Type}Handler(IRepository<${ModuleName}> repository) : base(repository) { }
    }
}
"@

    Set-Content -Path (Join-Path $Path "${ModuleName}${Type}.cs") -Value $content
    Write-Host "✅ Đã tạo file ${ModuleName}${Type}.cs tại $Path"
}
