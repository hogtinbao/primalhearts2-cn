# 编译启动垫片
# 用法: powershell -ExecutionPolicy Bypass -File build.ps1
$ErrorActionPreference = 'Stop'
$here = Split-Path -Parent $MyInvocation.MyCommand.Path
$src  = Join-Path $here 'PhAudioLauncher.cs'
$out  = Join-Path $here 'PhAudioLauncher.exe'

$csc = @(
    "$env:WINDIR\Microsoft.NET\Framework64\v4.0.30319\csc.exe",
    "$env:WINDIR\Microsoft.NET\Framework\v4.0.30319\csc.exe"
) | Where-Object { Test-Path $_ } | Select-Object -First 1

if (-not $csc) { throw '找不到 .NET Framework 4.x 的 csc.exe（Windows 自带，通常无需安装）' }

& $csc /nologo /target:winexe /out:$out $src
if ($LASTEXITCODE -ne 0) { throw "编译失败 (exit $LASTEXITCODE)" }
Write-Host "OK -> $out"
