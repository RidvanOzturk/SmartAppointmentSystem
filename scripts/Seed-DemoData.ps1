[CmdletBinding()]
param(
    [Parameter(Mandatory = $true)]
    [ValidateSet('Local', 'Supabase')]
    [string] $Target,

    [string] $PsqlPath = 'psql',

    [switch] $ValidateOnly
)

$ErrorActionPreference = 'Stop'
$seedFile = Join-Path $PSScriptRoot 'seed-demo-data.sql'

# Standard libpq variables keep credentials out of arguments and source files.
foreach ($setting in @('PGHOST', 'PGPORT', 'PGDATABASE', 'PGUSER')) {
    if ([string]::IsNullOrWhiteSpace([Environment]::GetEnvironmentVariable($setting))) {
        throw "Set $setting for the selected $Target database first. See scripts/DEMO-DATA.md."
    }
}

if ($Target -eq 'Local' -and $env:PGHOST -notin @('localhost', '127.0.0.1', '::1')) {
    throw 'Local requires a loopback host; refusing to seed a remote database with the Local target.'
}
if ($Target -eq 'Supabase') {
    if ($env:PGHOST -notmatch '(^|\.)(supabase\.co|supabase\.com)$') {
        throw 'Supabase requires the database/pooler host shown in the Supabase Connect panel.'
    }
    if ($env:PGSSLMODE -notin @('require', 'verify-ca', 'verify-full')) {
        throw 'Set PGSSLMODE=require (or verify-full with a trusted certificate) for Supabase.'
    }
}

$psql = (Get-Command $PsqlPath -ErrorAction Stop).Source
Write-Host "Target: $Target | Host: $env:PGHOST | Port: $env:PGPORT | Database: $env:PGDATABASE"

# --no-psqlrc ignores local startup scripts; ON_ERROR_STOP stops on the first SQL error.
# -W prompts for a password if PGPASSWORD/PGPASSFILE was not provided.
$psqlArgs = @('--no-psqlrc', '--set=ON_ERROR_STOP=1')
if (!$env:PGPASSWORD -and !$env:PGPASSFILE) { $psqlArgs += '-W' }

if ($ValidateOnly) {
    # The exact same inserts/checks run, but the transaction is rolled back.
    # No generated file or credentials are written to disk.
    $sql = [IO.File]::ReadAllText($seedFile).Replace('COMMIT;', 'ROLLBACK;')
    $previousEncoding = $OutputEncoding
    try {
        $OutputEncoding = [System.Text.UTF8Encoding]::new($false)
        $sql | & $psql @psqlArgs
        if ($LASTEXITCODE -ne 0) { throw "Demo seed validation failed (psql exit $LASTEXITCODE)." }
    }
    finally { $OutputEncoding = $previousEncoding }
    Write-Host 'Validation passed; demo inserts were rolled back. Identity sequences may have advanced.'
}
else {
    & $psql @psqlArgs --file $seedFile
    if ($LASTEXITCODE -ne 0) { throw "Demo seed failed (psql exit $LASTEXITCODE); transaction rolled back." }
    Write-Host 'Demo data committed. Re-running the script preserves existing records.'
}
