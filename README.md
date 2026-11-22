# Primal Hearts 2 简体中文补丁 + 音频修复

> ⚠️ **成人内容提示**：本作是 18+ 作品，剧情文本可能含成人向内容。
> 请勿在工作场所、学校、公共场所或未成年人可见的环境下阅览；请勿在法律法规禁止此类内容的地区使用。
>
> 📜 使用前必读：[`LICENSE`](LICENSE)（禁商用 / 禁未经授权再分发 / 禁工作场所使用）
> 与 [`DISCLAIMER.md`](DISCLAIMER.md)（无担保、责任限制、权利人下架承诺）。下载即视为同意。

适用：**Steam 版 Primal Hearts 2**（appid `1368010`，主程序 `primhearts2.exe`，Marmalade / Entis 引擎）。

## 下载（Releases 页，两个附件分开装）

| 附件 | 作用 | 大小 |
|---|---|---|
| `PrimalHearts2-CN-1.0.7z.001` | 简体中文文本补丁（分卷压缩，原名 `.7z.001`） | 见 Releases |
| `audio-fix-1.0.zip` | 音频修复（耳机没声音） | 16 KB |

到 [Releases](https://github.com/hogtinbao/primalhearts2-cn/releases) 页下载。

### 关于分卷

- 只看到一个 `.7z.001` 就说明是单卷，直接用 7-Zip / WinRAR 解压即可；
- 如果出现 `.7z.001`、`.7z.002`…，**必须全部下齐放在同一目录**，然后：
  - 7-Zip：右键 `.001` →「7-Zip」→「解压到…」；
  - WinRAR：右键 `.001` →「解压到…」（会自动串接后续卷）。

## 安装顺序（重要）

1. **先装官方中文补丁（繁体）**——本补丁的 `script.noa` 是在官方版本基础上转换的，
   没装官方补丁会缺字体/界面文件，出现缺字或界面异常；
2. 买了官方 R18 内容包的，也**先装好**（R18 包内含它自己的一份脚本）；
3. **最后再装本补丁**。

顺序反了（先装本补丁、后装官方补丁）会被官方包覆盖回去，重跑一次 `安装.bat` 即可。

## 安装（三步）

1. 解压 `PrimalHearts2-CN-1.0.7z.001` 到游戏目录，得到 `PrimalHearts2-简体补丁` 文件夹；
2. 进入该文件夹，双击 `安装.bat`
   （自动备份 `script.noa`、把 `primhearts2.exe` 改名为 `primhearts2.orig.exe` 并放入启动垫片）；
3. 从 Steam / 快捷方式正常启动游戏。

撤销：双击同目录的 `还原.bat`。

## 音频修复（也可单独使用）

`audio-fix-1.0.zip` 解压后：

```powershell
# 方式一：换主程序（推荐，不改 Steam 配置）
powershell -ExecutionPolicy Bypass -File install.ps1 -GameDir "<游戏目录>"

# 方式二：Steam 启动选项
"<解压目录>\PhAudioLauncher.exe" %command%

# 诊断：每台输出设备上各进程的实时峰值
powershell -ExecutionPolicy Bypass -File meter.ps1
```

原理：这类 32 位老引擎启动时固定绑第一台声卡（板载喇叭），不读 Windows 默认设备；
垫片在游戏真正出声后把当前默认设备**原样重新声明一次**，触发系统的设备变更通知，
引擎就会重绑到当前默认设备（戴耳机=耳机，拔掉=扬声器）。不是常驻程序。

## 关于 R18 内容包

- 官方只有繁体中文，**从未发布简体版**；本补丁里的简体文本（含 R18 包内脚本的处理方式）
  全部是**作者本人的简体化成果**。
- 官方 R18 内容包（`pmht_patch_R18.noa` 2.3 GB / `pmht2_patch_R18.noa` 3.3 GB）**不在本补丁内**：
  单文件远超 GitHub 100 MB 上限，且 GitHub 禁止露骨成人内容。
- 装好官方 R18 包后，R18 场景仍是繁体；要让它也变简体，用
  [`R18-说明.md`](R18-说明.md)
  在自己机器上把该包里的脚本转一遍（三条命令，图像原样保留，产物只留在本地）。

## 校验

Releases 里每个附件都可与 [`SHA256SUMS.txt`](SHA256SUMS.txt) 对照：

```powershell
Get-FileHash .\PrimalHearts2-CN-1.0.7z.001 -Algorithm SHA256
```

## 权利

游戏、剧本原文、美术、语音、字体、官方中文补丁、R18 内容包的权利归各自权利人所有，
本项目不主张任何相关权利，也不提供获取途径。使用限制见 `LICENSE`。
