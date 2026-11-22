# 把启动垫片装进游戏目录（换主程序 / exe swap）
#
#   powershell -ExecutionPolicy Bypass -File install.ps1 -GameDir "D:\Steam\steamapps\common\SomeGame"
#   powershell -ExecutionPolicy Bypass -File install.ps1 -GameDir "..." -ExeName game.exe
#   powershell -ExecutionPolicy Bypass -File install.ps1 -GameDir "..." -Revert
#
# 做的事：<game>.exe -> <game>.orig.exe，然后把垫片复制成 <game>.exe。
# 垫片启动时找不到参数，就会去运行旁边的 <自己名>.orig.exe，并在游戏真正出声后
# 重新声明一次默认输出设备，让引擎跟随系统的默认设备。
param(
    [Parameter(Mandatory = $true)][string]$GameDir,
    [string]$ExeName,
    [switch]$Revert
)

$ErrorActionPreference = 'Stop'
$here = Split-Path -Parent $MyInvocation.MyCommand.Path
$shim = Join-Path $here 'PhAudioLauncher.exe'

if (-not (Test-Path $GameDir)) { throw "目录不存在: $GameDir" }

function Get-GameExe {
    param([string]$Dir, [string]$Name)
    if ($Name) {
        $p = Join-Path $Dir $Name
        if (-not (Test-Path $p)) { throw "找不到 $p" }
        return Get-Item $p
    }
    # 排除垫片自身与 *.orig.exe 后，取体积最大的 exe（游戏主程序通常最大）
    $cands = Get-ChildItem $Dir -Filter *.exe -File |
        Where-Object { $_.Name -notlike '*.orig.exe' -and $_.Length -gt 512KB }
    if (-not $cands) { throw '目录里没找到可换的主程序，请用 -ExeName 指定' }
    if ($cands.Count -gt 1) {
        $biggest = $cands | Sort-Object Length -Descending | Select-Object -First 1
        Write-Host "发现多个 exe，默认取最大的: $($biggest.Name)"
        return $biggest
    }
    return $cands[0]
}

$exe = Get-GameExe -Dir $GameDir -Name $ExeName
$orig = Join-Path $GameDir ($exe.BaseName + '.orig.exe')

if ($Revert) {
    if (-not (Test-Path $orig)) { throw "没有找到备份 $orig，可能已经是原始状态" }
    Copy-Item $orig $exe.FullName -Force
    Remove-Item $orig -Force
    Write-Host "已还原: $($exe.Name)"
    return
}

if (-not (Test-Path $shim)) { throw "缺少 $shim，请先运行 build.ps1" }

if (Test-Path $orig) {
    Write-Host "已经装过了（存在 $([IO.Path]::GetFileName($orig))），跳过"
    return
}

Rename-Item $exe.FullName -NewName ([IO.Path]::GetFileName($orig))
Copy-Item $shim $exe.FullName -Force
Write-Host "已安装: $($exe.Name) -> 垫片；原程序备份为 $([IO.Path]::GetFileName($orig))"
Write-Host "直接按 Steam / 快捷方式启动即可。日志在同目录 launcher.log。"
