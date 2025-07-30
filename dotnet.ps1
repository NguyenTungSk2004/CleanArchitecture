param (
    [Parameter(Mandatory = $true)]
    [string]$Command,
    
    [Parameter(ValueFromRemainingArguments = $true)]
    [string[]]$Args
)
    
. "$PSScriptRoot/Z-Powershell/run.ps1"
. "$PSScriptRoot/Z-Powershell/package.ps1"
. "$PSScriptRoot/Z-Powershell/gen_sln.ps1"

try{
    $command = $Command.ToLowerInvariant()
    switch ($command) {
        'run' {
            Start-Project 
        }
        'package' {
            Install-Packages @Args
        }
        'gen-sln' {
            New-Solution @Args
        }
        default {
            Write-Error "Unknown command: $command"
            exit 1
        }
    }
} catch {
    Write-Error "An error occurred: $_"
    exit 1
}