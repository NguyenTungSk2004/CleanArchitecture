function Get-Help {
    Write-Host "Usage: <command> [options]"
    Write-Host ""
    Write-Host "Commands:"
    Write-Host "  run         Runs the project"
    Write-Host "  package     Installs a package"
    Write-Host "  generate    Generates a solution or module"
    Write-Host ""
    Write-Host "Options:"
    Write-Host "  -h, --help  Show help information"
}

Export-ModuleMember -Function Get-Help