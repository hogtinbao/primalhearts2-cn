# Primal Hearts 2 简体中文补丁 + 音频修复

> ⚠️ **成人内容提示**：本作是 18+ 作品，剧情文本可能含成人向内容。
> 请勿在工作场所、学校、公共场所或未成年人可见的环境下阅览；请勿在法律法规禁止此类内容的地区使用。
>
> 📜 使用前必读：[`LICENSE`](LICENSE)（禁商用 / 禁未经授权再分发 / 禁工作场所使用）
> 与 [`DISCLAIMER.md`](DISCLAIMER.md)（无担保、责任限制、权利人下架承诺）。下载即视为同意。

适用：**Steam 版 Primal Hearts 2**（appid `1368010`，主程序 `primhearts2.exe`，Marmalade / Entis 引擎）。

## 包内容

这个补丁做两件事：

1. **简体中文**：官方只提供繁体中文，补丁把剧情脚本（`script.noa`）里的
   `text_ZHTW` / `name_ZHTW` 文本转换为简体；
2. **修耳机无声**：这类 32 位老引擎启动时固定绑第一台声卡（板载喇叭），无视 Windows 默认设备。
   启动垫片会在游戏真正出声后重新声明一次默认设备，让引擎跟随当前默认设备
   （戴耳机=耳机，拔掉=扬声器）。

补丁**只有剧情文本和启动垫片**，不含任何图像、语音、字体或 R18 内容包。

## 安装（三步）

1. 下载 [`release/PrimalHearts2-CN-1.0.zip`](release/PrimalHearts2-CN-1.0.zip)，解压到游戏目录（会得到 `PrimalHearts2-简体补丁` 文件夹）；
2. 进入该文件夹，双击 `安装.bat`；
3. 从 Steam / 快捷方式正常启动游戏。

`安装.bat` 做的事：备份原 `script.noa` → 覆盖为简体版；把 `primhearts2.exe` 改名为
`primhearts2.orig.exe`（备份）→ 放入启动垫片。

## 回退

双击同目录的 `还原.bat`，原主程序与原始 `script.noa` 都会还原。

## 校验

包内 `SHA256SUMS.txt` 有逐文件校验和；仓库根目录 [`SHA256SUMS.txt`](SHA256SUMS.txt) 是整个 zip 的。

## 只想要音频修复

`audio-fix/` 里是同一份启动垫片的可执行文件与源码：

```powershell
# 方式一：换主程序（推荐，无需改 Steam 配置）
powershell -ExecutionPolicy Bypass -File audio-fix/install.ps1 -GameDir "<游戏目录>"

# 方式二：Steam 启动选项
"<仓库目录>\audio-fix\PhAudioLauncher.exe" %command%

# 诊断：看每台输出设备上各进程的实时峰值
powershell -ExecutionPolicy Bypass -File audio-fix/meter.ps1
```

它不是常驻程序：只在游戏启动那几秒运行，游戏退出即退出。

## 已知限制

- 仅适用于对应 appid 的 Steam 版；其它版本/语言版的 `script.noa` 结构可能不同。
- Steam「验证游戏文件完整性」或游戏更新会还原主程序与脚本，重跑 `安装.bat` 即可。
- 需要 .NET Framework 4.x（Windows 10/11 自带）。

## 权利

游戏、剧本原文、美术、语音、字体、官方中文补丁、R18 内容包的权利归各自权利人所有，
本项目不主张任何相关权利，也不提供获取途径。使用限制见 `LICENSE`。
