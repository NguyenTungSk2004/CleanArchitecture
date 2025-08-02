param (
    [Parameter(Mandatory = $true)]
    [string]$Command,
    [Parameter(Mandatory = $false)]
    [string]$SubCommand,    
    [Parameter(ValueFromRemainingArguments = $true)]
    [string[]]$Args
)

Import-Module "$PSScriptRoot/Z-Powershell/Modules/Projects/run.psm1"
Import-Module "$PSScriptRoot/Z-Powershell/Modules/Projects/package.psm1"
Import-Module "$PSScriptRoot/Z-Powershell/Modules/Projects/gen_sln.psm1"
Import-Module "$PSScriptRoot/Z-Powershell/Modules/Application/gen_module.psm1"
Import-Module "$PSScriptRoot/Z-Powershell/Help/Help.psm1"

Function Start-Gen{
    Param(
        [Parameter(Mandatory = $true)]
        [string]$Command,
        [Parameter(ValueFromRemainingArguments = $true)]
        [string[]]$Args
    )
    switch($Command){
        'sln' {
            New-Solution @Args
        }
        'module' {
            New-Module @Args
        }
        default {
            Write-Error "Unknown subcommand: $Command"
            Get-Help
            exit 1
        }
    }
}   
  
try{
    $command = $Command.ToLowerInvariant()
    switch ($command) {
        'run' {
            Start-Project 
        }
        'package' {
            Install-Packages @Args
        }
        'generate' {
            Start-Gen $SubCommand @Args
        }
        default {
            Write-Error "Unknown command: $command"
            Get-Help
            exit 1
        }
    }
} catch {
    Write-Error "An error occurred: $_"
    exit 1
}