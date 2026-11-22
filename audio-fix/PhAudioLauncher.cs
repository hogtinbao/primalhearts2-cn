using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

[ComImport, Guid("BCDE0395-E52F-467C-8E3D-C4579291692E")]
class MMDeviceEnumeratorComObject { }

[ComImport, Guid("A95664D2-9614-4F35-A746-DE8DB63617E6"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
interface IMMDeviceEnumerator
{
    int EnumAudioEndpoints(int dataFlow, int stateMask, out IMMDeviceCollection devices);
    int GetDefaultAudioEndpoint(int dataFlow, int role, out IMMDevice device);
    int GetDevice(string id, out IMMDevice device);
    int RegisterEndpointNotificationCallback(IntPtr client);
    int UnregisterEndpointNotificationCallback(IntPtr client);
}

[ComImport, Guid("0BD7A1BE-7A1A-44DB-8397-CC5392387B5E"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
interface IMMDeviceCollection
{
    int GetCount(out int count);
    int Item(int index, out IMMDevice device);
}

[ComImport, Guid("D666063F-1587-4E43-81F1-B948E807363F"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
interface IMMDevice
{
    int Activate(ref Guid iid, int clsCtx, IntPtr activationParams, [MarshalAs(UnmanagedType.IUnknown)] out object iface);
    int OpenPropertyStore(int access, out IPropertyStore store);
    int GetId([MarshalAs(UnmanagedType.LPWStr)] out string id);
    int GetState(out int state);
}

[ComImport, Guid("886D8EEB-8CF2-4446-8D02-CDBA1DBDCF99"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
interface IPropertyStore
{
    int GetCount(out int count);
    int GetAt(int index, out PROPERTYKEY key);
    int GetValue(ref PROPERTYKEY key, out PROPVARIANT value);
    int SetValue(ref PROPERTYKEY key, ref PROPVARIANT value);
    int Commit();
}

[StructLayout(LayoutKind.Sequential, Pack = 4)]
struct PROPERTYKEY { public Guid fmtid; public int pid; }

[StructLayout(LayoutKind.Explicit)]
struct PROPVARIANT
{
    [FieldOffset(0)] public short vt;
    [FieldOffset(8)] public IntPtr ptr;
}

[ComImport, Guid("77AA99A0-1BD6-484F-8BC7-2C654C9A9B6F"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
interface IAudioSessionManager2
{
    int GetAudioSessionControl(IntPtr audioSessionGuid, int streamFlags, out IAudioSessionControl sessionControl);
    int GetSimpleAudioVolume(IntPtr audioSessionGuid, int streamFlags, out IntPtr audioVolume);
    int GetSessionEnumerator(out IAudioSessionEnumerator sessionEnum);
    int RegisterSessionNotification(IntPtr sessionNotification);
    int UnregisterSessionNotification(IntPtr sessionNotification);
    int RegisterDuckNotification(string sessionId, IntPtr duckNotification);
    int UnregisterDuckNotification(IntPtr duckNotification);
}

[ComImport, Guid("E2F5BB11-0570-40CA-ACDD-3AA01277DEE8"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
interface IAudioSessionEnumerator
{
    int GetCount(out int sessionCount);
    int GetSession(int sessionIndex, out IAudioSessionControl session);
}

[ComImport, Guid("F4B1A599-7266-4319-A8CA-E70ACB11E8CD"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
interface IAudioSessionControl
{
    int GetState(out int state);
    int GetDisplayName(out IntPtr name);
    int SetDisplayName([MarshalAs(UnmanagedType.LPWStr)] string name, ref Guid eventContext);
    int GetIconPath(out IntPtr path);
    int SetIconPath([MarshalAs(UnmanagedType.LPWStr)] string path, ref Guid eventContext);
    int GetGroupingParam(out Guid param);
    int SetGroupingParam(ref Guid param, ref Guid eventContext);
    int RegisterAudioSessionNotification(IntPtr newNotifications);
    int UnregisterAudioSessionNotification(IntPtr newNotifications);
}

[ComImport, Guid("BFB7FF88-7239-4FC9-8FA2-07C950BE9C6D"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
interface IAudioSessionControl2
{
    int GetState(out int state);
    int GetDisplayName(out IntPtr name);
    int SetDisplayName([MarshalAs(UnmanagedType.LPWStr)] string name, ref Guid eventContext);
    int GetIconPath(out IntPtr path);
    int SetIconPath([MarshalAs(UnmanagedType.LPWStr)] string path, ref Guid eventContext);
    int GetGroupingParam(out Guid param);
    int SetGroupingParam(ref Guid param, ref Guid eventContext);
    int RegisterAudioSessionNotification(IntPtr newNotifications);
    int UnregisterAudioSessionNotification(IntPtr newNotifications);
    int GetSessionIdentifier(out IntPtr id);
    int GetSessionInstanceIdentifier(out IntPtr id);
    int GetProcessId(out uint pid);
    int IsSystemSoundsSession();
    int SetDuckingPreference(bool optOut);
}

[ComImport, Guid("870af99c-171d-4f9e-af0d-e63df40c2bc9")]
class CPolicyConfigClient { }

[ComImport, Guid("f8679f50-850a-41cf-9c72-430f290290c8"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
interface IPolicyConfig
{
    int GetMixFormat([MarshalAs(UnmanagedType.LPWStr)] string id, out IntPtr fmt);
    int GetDeviceFormat([MarshalAs(UnmanagedType.LPWStr)] string id, int def, out IntPtr fmt);
    int ResetDeviceFormat([MarshalAs(UnmanagedType.LPWStr)] string id);
    int SetDeviceFormat([MarshalAs(UnmanagedType.LPWStr)] string id, IntPtr end, IntPtr mix);
    int GetProcessingPeriod([MarshalAs(UnmanagedType.LPWStr)] string id, int def, out long a, out long b);
    int SetProcessingPeriod([MarshalAs(UnmanagedType.LPWStr)] string id, ref long p);
    int GetShareMode([MarshalAs(UnmanagedType.LPWStr)] string id, out int mode);
    int SetShareMode([MarshalAs(UnmanagedType.LPWStr)] string id, ref int mode);
    int GetPropertyValue([MarshalAs(UnmanagedType.LPWStr)] string id, IntPtr key, IntPtr val);
    int SetPropertyValue([MarshalAs(UnmanagedType.LPWStr)] string id, IntPtr key, IntPtr val);
    int SetDefaultEndpoint([MarshalAs(UnmanagedType.LPWStr)] string id, int role);
    int SetEndpointVisibility([MarshalAs(UnmanagedType.LPWStr)] string id, int visible);
}

static class Audio
{
    public static List<string> EndpointNames()
    {
        var names = new List<string>();
        var en = (IMMDeviceEnumerator)(object)new MMDeviceEnumeratorComObject();
        IMMDeviceCollection col;
        en.EnumAudioEndpoints(0, 1, out col);
        int n; col.GetCount(out n);
        for (int i = 0; i < n; i++)
        {
            IMMDevice d; col.Item(i, out d);
            IPropertyStore ps; d.OpenPropertyStore(0, out ps);
            var k = new PROPERTYKEY(); k.fmtid = new Guid("a45c254e-df1c-4efd-8020-67d146a850e0"); k.pid = 14;
            PROPVARIANT v; ps.GetValue(ref k, out v);
            names.Add(v.vt == 31 && v.ptr != IntPtr.Zero ? Marshal.PtrToStringAuto(v.ptr) : "?");
        }
        return names;
    }

    public static string DefaultId(int role)
    {
        var en = (IMMDeviceEnumerator)(object)new MMDeviceEnumeratorComObject();
        IMMDevice d;
        int hr = en.GetDefaultAudioEndpoint(0, role, out d);
        if (hr != 0 || d == null) return null;
        string id; d.GetId(out id);
        return id;
    }

    public static string DefaultName(int role)
    {
        var en = (IMMDeviceEnumerator)(object)new MMDeviceEnumeratorComObject();
        IMMDevice d;
        int hr = en.GetDefaultAudioEndpoint(0, role, out d);
        if (hr != 0 || d == null) return null;
        IPropertyStore ps; d.OpenPropertyStore(0, out ps);
        var k = new PROPERTYKEY(); k.fmtid = new Guid("a45c254e-df1c-4efd-8020-67d146a850e0"); k.pid = 14;
        PROPVARIANT v; ps.GetValue(ref k, out v);
        return v.vt == 31 && v.ptr != IntPtr.Zero ? Marshal.PtrToStringAuto(v.ptr) : null;
    }

    // Fire a "default endpoint changed" notification without changing the user's choice:
    // re-assert each role's current default device.
    public static string ReassertDefault()
    {
        var pc = (IPolicyConfig)(object)new CPolicyConfigClient();
        var parts = new List<string>();
        int[] roles = { 0, 1, 2 };
        foreach (int r in roles)
        {
            string id = DefaultId(r);
            if (id == null) { parts.Add("role" + r + "=nodevice"); continue; }
            int hr = pc.SetDefaultEndpoint(id, r);
            parts.Add("role" + r + "=0x" + hr.ToString("X8"));
        }
        return string.Join(", ", parts.ToArray());
    }

    public static bool HasSession(uint pid)
    {
        var en = (IMMDeviceEnumerator)(object)new MMDeviceEnumeratorComObject();
        IMMDeviceCollection col;
        en.EnumAudioEndpoints(0, 1, out col);
        int n; col.GetCount(out n);
        for (int i = 0; i < n; i++)
        {
            IMMDevice dev; col.Item(i, out dev);
            try
            {
                Guid iid = typeof(IAudioSessionManager2).GUID;
                object o; dev.Activate(ref iid, 1, IntPtr.Zero, out o);
                var mgr = (IAudioSessionManager2)o;
                IAudioSessionEnumerator se; mgr.GetSessionEnumerator(out se);
                int sc; se.GetCount(out sc);
                for (int s = 0; s < sc; s++)
                {
                    IAudioSessionControl ctl; se.GetSession(s, out ctl);
                    var c2 = (IAudioSessionControl2)ctl;
                    uint sp; c2.GetProcessId(out sp);
                    if (sp == pid) return true;
                }
            }
            catch { }
        }
        return false;
    }

    public static string SessionsOf(uint pid)
    {
        var hits = new List<string>();
        var en = (IMMDeviceEnumerator)(object)new MMDeviceEnumeratorComObject();
        IMMDeviceCollection col;
        en.EnumAudioEndpoints(0, 1, out col);
        int n; col.GetCount(out n);
        for (int i = 0; i < n; i++)
        {
            IMMDevice dev; col.Item(i, out dev);
            IPropertyStore ps; dev.OpenPropertyStore(0, out ps);
            var k = new PROPERTYKEY(); k.fmtid = new Guid("a45c254e-df1c-4efd-8020-67d146a850e0"); k.pid = 14;
            PROPVARIANT v; ps.GetValue(ref k, out v);
            string dn = v.vt == 31 && v.ptr != IntPtr.Zero ? Marshal.PtrToStringAuto(v.ptr) : "?";
            try
            {
                Guid iid = typeof(IAudioSessionManager2).GUID;
                object o; dev.Activate(ref iid, 1, IntPtr.Zero, out o);
                var mgr = (IAudioSessionManager2)o;
                IAudioSessionEnumerator se; mgr.GetSessionEnumerator(out se);
                int sc; se.GetCount(out sc);
                for (int s = 0; s < sc; s++)
                {
                    IAudioSessionControl ctl; se.GetSession(s, out ctl);
                    var c2 = (IAudioSessionControl2)ctl;
                    uint sp; c2.GetProcessId(out sp);
                    if (sp == pid) { int st; ctl.GetState(out st); hits.Add(dn + " (state=" + st + ")"); }
                }
            }
            catch { }
        }
        return hits.Count == 0 ? "(none)" : string.Join(" | ", hits.ToArray());
    }
}

static class Program
{
    static string logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "launcher.log");

    static void Log(string msg)
    {
        try { File.AppendAllText(logPath, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "  " + msg + Environment.NewLine); }
        catch { }
    }

    [STAThread]
    static int Main(string[] args)
    {
        // No arguments: we were started as the game's own exe (shim in place).
        // Find the original binary next to us: <name>.orig.exe
        if (args.Length == 0)
        {
            string dir = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
            string stem = Path.GetFileNameWithoutExtension(Process.GetCurrentProcess().MainModule.FileName);
            string orig = Path.Combine(dir, stem + ".orig.exe");
            if (!File.Exists(orig))
            {
                Log("shim mode: " + orig + " not found -> nothing to do");
                return 2;
            }
            Log("shim mode: launching " + orig);
            return RunAndNudge(orig, "");
        }

        return RunAndNudge(ResolveSelf(args[0]) ?? args[0], BuildArgs(args));
    }

    // If the requested target is this very shim (e.g. Steam launch options point at the
    // launcher while the game exe itself was swapped for the shim), fall back to the
    // sibling <name>.orig.exe instead of spawning ourselves forever.
    static string ResolveSelf(string exe)
    {
        try
        {
            string selfPath = Path.GetFullPath(Process.GetCurrentProcess().MainModule.FileName);
            string target = Path.GetFullPath(exe);
            if (string.Equals(selfPath, target, StringComparison.OrdinalIgnoreCase))
            {
                string orig = Path.Combine(Path.GetDirectoryName(selfPath),
                                           Path.GetFileNameWithoutExtension(selfPath) + ".orig.exe");
                if (File.Exists(orig))
                {
                    Log("target is the shim itself -> using " + orig);
                    return orig;
                }
                Log("target is the shim itself and no .orig.exe next to it -> abort");
                return null;
            }
        }
        catch { }
        return exe;
    }

    static string BuildArgs(string[] args)
    {
        var sb = new System.Text.StringBuilder();
        for (int i = 1; i < args.Length; i++)
        {
            if (sb.Length > 0) sb.Append(' ');
            sb.Append(args[i].IndexOf(' ') >= 0 ? "\"" + args[i] + "\"" : args[i]);
        }
        return sb.ToString();
    }

    static int RunAndNudge(string exe, string argline)
    {
        var psi = new ProcessStartInfo(exe);
        psi.Arguments = argline;
        psi.UseShellExecute = false;
        psi.WorkingDirectory = Path.GetDirectoryName(exe);
        Log("launching: " + exe + " " + psi.Arguments);

        Process p;
        try { p = Process.Start(psi); }
        catch (Exception e) { Log("launch failed: " + e.Message); return 1; }

        string target = Audio.DefaultName(1);
        Log("default device at start: " + target);

        bool nudged = false;
        var sw = Stopwatch.StartNew();
        while (sw.Elapsed.TotalSeconds < 90)
        {
            Thread.Sleep(400);
            if (p.HasExited) { Log("game exited before audio init"); break; }
            if (Audio.HasSession((uint)p.Id))
            {
                Thread.Sleep(1200);
                Log("audio session detected, re-asserting default endpoint: " + Audio.ReassertDefault());
                nudged = true;
                break;
            }
        }

        if (nudged)
        {
            Thread.Sleep(2500);
            Log("second nudge: " + Audio.ReassertDefault());
            Thread.Sleep(1500);
            Log("game audio now on: " + Audio.SessionsOf((uint)p.Id));
        }
        else
        {
            Log("WARN: no audio session detected; nothing done");
        }

        p.WaitForExit();
        Log("game exited, code " + p.ExitCode);
        return p.ExitCode;
    }
}
