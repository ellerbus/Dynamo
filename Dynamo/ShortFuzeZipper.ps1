param ( [Parameter(Mandatory=$true)][string]$source, [Parameter(Mandatory=$true)][string]$target )

function AppendToZip($archive, $path)
{
    if ($path -match "^.*\\(obj|bin|packages|node_modules|\.vs)(\\.+)?$")
    {
        Write-Host "Skipped $path"
    }
    else
    {
        Write-Host "Searching $path"

        foreach ($x in [System.IO.Directory]::GetDirectories($path))
        {
            AppendToZip $archive $x
        }

        foreach ($f in [System.IO.Directory]::GetFiles($path))
        {
            $ext = [System.IO.Path]::GetExtension($f)

            if ($ext -notmatch "\.(suo|user)")
            {
                $name = $f.replace("$source\", "")
     
                Write-Host " .. $name"

                $entry = [System.IO.Compression.ZipFileExtensions]::CreateEntryFromFile($archive, $f, $name)
            }
        }
    }
}

$source = [System.IO.Path]::GetFullPath($source.TrimEnd("\"))
$target = [System.IO.Path]::GetFullPath($target.TrimEnd("\"))

Add-Type -Assembly System.IO.Compression, System.IO.Compression.FileSystem

Set-ExecutionPolicy Bypass -Scope CurrentUser

$ErrorActionPreference = "Stop"

$zip = "$target\ShortFuze.zip"

if (Test-Path $zip)
{
    Write-Host "Removing Zip $zip"
    Remove-Item $zip -Force
}

Write-Host "Source: $source"
Write-Host "Target: $target"

$archive = [System.IO.Compression.ZipFile]::Open($zip, [System.IO.Compression.ZipArchiveMode]::Create)

try
{
    AppendToZip $archive $source
}
finally
{
    $archive.Dispose()
}