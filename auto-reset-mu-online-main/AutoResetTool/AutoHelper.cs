using AutoResetTool.Definitions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using System.Text.RegularExpressions;
using System.Runtime;

namespace AutoResetTool
{
  internal static class AutoHelper
  {
    [DllImport("user32.dll", SetLastError = true)]
    internal static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, uint wParam, StringBuilder lParam);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, uint wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, uint wParam, ref int lParam);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, uint wParam, int lParam);
    [DllImport("User32.dll")]
    static extern int SetForegroundWindow(IntPtr point);

    [DllImport("user32.dll")]
    static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);
    [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    public static extern int GetWindowTextLength(IntPtr hWnd);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);
    [DllImport("user32.dll")]
    internal static extern bool SetCursorPos(int x, int y);
    /// <summary>
    /// Get windows caption.
    /// </summary>
    /// <param name="hWnd"></param>
    /// <returns></returns>
    public static string GetText(IntPtr hWnd)
    {
      // Allocate correct string length first
      int length = GetWindowTextLength(hWnd);
      StringBuilder sb = new StringBuilder(length + 1);
      GetWindowText(hWnd, sb, sb.Capacity);
      return sb.ToString();
    }

    public static void SendMouseClick(IntPtr hWnd, int X, int Y)
    {
      const uint WM_LBUTTONDOWN = 0x0201;
      const uint WM_LBUTTONUP = 0x0202;
      SetForegroundWindow(hWnd);
      if (GetWindowRect(hWnd, out RECT windowRect))
      {
        //X = X - windowRect.X;
        //Y = Y - windowRect.Y;
        SetCursorPos(X + windowRect.X, Y + windowRect.Y);
        Thread.Sleep(500);
        int lparm = (Y << 16) | (X & 0xFFFF);
        SendMessage(hWnd, WM_LBUTTONDOWN, 0, lparm);
        SendMessage(hWnd, WM_LBUTTONUP, 0, lparm);
      }
    }

    public static void SendKeyboard(IntPtr hWnd, char key)
    {
      WindowsAPI.SwitchWindow(hWnd);
      PressKey(key);
    }

    public static void SendKeyboard(IntPtr hWnd, string keys)
    {
      SetForegroundWindow(hWnd);
      SendKeys.Send(keys);
    }

    public static Bitmap CaptureScreen(IntPtr hWnd, Rectangle capturedRect)
    {
      SetForegroundWindow(hWnd);
      if (GetWindowRect(hWnd, out RECT windowRect))
      {
        Bitmap bmp = new Bitmap(capturedRect.Width, capturedRect.Height, PixelFormat.Format32bppArgb);
        Graphics g = Graphics.FromImage(bmp);
        g.CopyFromScreen(windowRect.X + capturedRect.X, windowRect.Y + capturedRect.Y, 0, 0, bmp.Size, CopyPixelOperation.SourceCopy);
        return bmp;
      }
      return null;
    }

    public static string CaptureLocation(IntPtr hWnd, Rectangle capturedRect)
    {
      string fileName = "capture.png";
      Bitmap bmp = CaptureScreen(hWnd, capturedRect);
      if (bmp == null)
        return null;
      bmp.Save(fileName, System.Drawing.Imaging.ImageFormat.Png);
      return fileName;
    }
    public static void PressKey(char ch)
    {
      byte vk = WindowsAPI.VkKeyScan(ch);
      ushort scanCode = (ushort)WindowsAPI.MapVirtualKey(vk, 0);
      uint length = 2;
      INPUT[] inputs = new INPUT[length];
      inputs[0].type = WindowsAPI.INPUT_KEYBOARD;
      inputs[0].ki.dwFlags = 0;
      inputs[0].ki.wVk = scanCode;

      inputs[1].type = WindowsAPI.INPUT_KEYBOARD;
      inputs[1].ki.wVk = scanCode;
      inputs[1].ki.dwFlags = (uint)KEYEVENTF.KEYUP;

      uint intReturn = WindowsAPI.SendInput(length, inputs, System.Runtime.InteropServices.Marshal.SizeOf(inputs[0]));
      if (intReturn != length)
      {
        throw new Exception("Could not send key: " + scanCode);
      }
    }

    public static void KeyDown(ushort scanCode)
    {
      INPUT[] inputs = new INPUT[1];
      inputs[0].type = WindowsAPI.INPUT_KEYBOARD;
      inputs[0].ki.dwFlags = WindowsAPI.WM_KEYDOWN;
      inputs[0].ki.wScan = (ushort)(scanCode & 0xff);

      uint intReturn = WindowsAPI.SendInput((uint)inputs.Length, inputs, System.Runtime.InteropServices.Marshal.SizeOf(inputs[0]));
      if (intReturn != 1)
      {
        throw new Exception("Could not send key: " + scanCode);
      }
    }

    public static void KeyUp(ushort scanCode)
    {
      INPUT[] inputs = new INPUT[1];
      inputs[0].type = WindowsAPI.INPUT_KEYBOARD;
      inputs[0].ki.wScan = scanCode;
      inputs[0].ki.dwFlags = WindowsAPI.KEYEVENTF_KEYUP;
      uint intReturn = WindowsAPI.SendInput((uint)inputs.Length, inputs, System.Runtime.InteropServices.Marshal.SizeOf(inputs[0]));
      if (intReturn != 1)
      {
        throw new Exception("Could not send key: " + scanCode);
      }
    }
    //public static Point GetLocationFromImage(string imagePath)
    //{
    //  return GetLocationFromImage(File.ReadAllBytes(imagePath));
    //}
    //public static Point GetLocationFromImage(byte[] imageData)
    //{
    //  var text = "";
    //  //var engine = new TesseractEngine(@"./traineddata", "eng", EngineMode.Default);
    //  using (var engine = new TesseractEngine(@"./traineddata", "eng", EngineMode.Default))
    //  {
    //    using (var img = Pix.LoadFromMemory(imageData))
    //    {
    //      using (var page = engine.Process(img))
    //      {
    //        text = page.GetText();
    //        text = text.Replace("\n", "").Trim();
    //      }
    //    }
    //  }
    //  string[] arr = text.Split(' ');
    //  if (arr.Length > 1)
    //  {
    //    string locate = arr[arr.Length - 1];
    //    string[] pos = locate.Split(',');
    //    if (pos.Length > 1)
    //    {
    //      string strX = pos[0], strY = pos[1];
    //      strX = CorrectTextToNumber(strX);
    //      strY = CorrectTextToNumber(strY);
    //      if (int.TryParse(strX, out int x) && int.TryParse(strY, out int y))
    //        return new Point(x, y);
    //    }
    //  }
    //  return Point.Empty;
    //}

    //internal static string GetTextFromImage(Bitmap bitmap)
    //{
    //  var text = "";
    //  //var engine = new TesseractEngine(@"./traineddata", "eng", EngineMode.Default);
    //  using (var engine = new TesseractEngine(@"./traineddata", "eng", EngineMode.Default))
    //  {
    //    using (var stream = new MemoryStream())
    //    {
    //      bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
    //      using (var img = Pix.LoadFromMemory(stream.GetBuffer()))
    //      {
    //        using (var page = engine.Process(img))
    //        {
    //          text = page.GetText();
    //        }
    //      }
    //    }
    //  }
    //  return text;
    //}

    private static string CorrectTextToNumber(string text)
    {
      return Regex.Replace(text.Replace("z", "2").Replace("o", "0").Replace("s", "5").Replace("i", "1").Replace("l", "1").Trim(), @"[^\d]", "");
    }
  }
  /// <summary>
  /// 
  /// </summary>
  public class WindowsAPI
  {
    /// <summary>
    /// 
    /// </summary>
    public const uint WM_KEYDOWN = 0x100;

    /// <summary>
    /// 
    /// </summary>
    public const uint WM_KEYUP = 0x101;

    /// <summary>
    /// 
    /// </summary>
    public const uint WM_LBUTTONDOWN = 0x201;

    /// <summary>
    /// 
    /// </summary>
    public const uint WM_LBUTTONUP = 0x202;

    public const uint WM_CHAR = 0x102;

    /// <summary>
    /// 
    /// </summary>
    public const int MK_LBUTTON = 0x01;

    /// <summary>
    /// 
    /// </summary>
    public const int VK_RETURN = 0x0d;

    public const int VK_ESCAPE = 0x1b;

    /// <summary>
    /// 
    /// </summary>
    public const int VK_TAB = 0x09;

    /// <summary>
    /// 
    /// </summary>
    public const int VK_LEFT = 0x25;

    /// <summary>
    /// 
    /// </summary>
    public const int VK_UP = 0x26;

    /// <summary>
    /// 
    /// </summary>
    public const int VK_RIGHT = 0x27;

    /// <summary>
    /// 
    /// </summary>
    public const int VK_DOWN = 0x28;

    /// <summary>
    /// 
    /// </summary>
    public const int VK_F5 = 0x74;

    /// <summary>
    /// 
    /// </summary>
    public const int VK_F6 = 0x75;

    /// <summary>
    /// 
    /// </summary>
    public const int VK_F7 = 0x76;

    public const int VK_CONTROL = 0x11;
    public const int VK_F = 0x46;
    /// <summary>
    /// Alt key
    /// </summary>
    public const int VK_MENU = 0x12;
    public const int VK_SHIFT = 0x10;
    public const int VK_1 = 0x31;
    public const int VK_2 = 0x32;
    public const int VK_3 = 0x33;
    public const int VK_4 = 0x34;
    public const int VK_5 = 0x35;
    public const int VK_6 = 0x36;
    public const int VK_7 = 0x37;
    public const int VK_8 = 0x38;
    public const int VK_9 = 0x39;

    /// <summary>
    /// The GetForegroundWindow function returns a handle to the foreground window.
    /// </summary>
    [DllImport("user32.dll")]
    public static extern IntPtr GetForegroundWindow();

    [DllImport("kernel32.dll")]
    public static extern uint GetCurrentThreadId();

    [DllImport("user32.dll", SetLastError = true)]
    public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

    [DllImport("user32.dll")]
    public static extern bool AttachThreadInput(uint idAttach, uint idAttachTo, bool fAttach);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern bool ReadProcessMemory(
      IntPtr hProcess,
      IntPtr lpBaseAddress,
      [Out()] byte[] lpBuffer,
      int dwSize,
      out int lpNumberOfBytesRead
     );

    public static void SwitchWindow(IntPtr windowHandle)
    {
      if (GetForegroundWindow() == windowHandle)
        return;

      IntPtr foregroundWindowHandle = GetForegroundWindow();
      uint currentThreadId = GetCurrentThreadId();
      uint temp;
      uint foregroundThreadId = GetWindowThreadProcessId(foregroundWindowHandle, out temp);
      AttachThreadInput(currentThreadId, foregroundThreadId, true);
      SetForegroundWindow(windowHandle);
      AttachThreadInput(currentThreadId, foregroundThreadId, false);

      while (GetForegroundWindow() != windowHandle)
      {
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="hwndParent"></param>
    /// <param name="hwndChildAfter"></param>
    /// <param name="lpszClass"></param>
    /// <param name="lpszWindow"></param>
    /// <returns></returns>
    [DllImport("user32.dll", SetLastError = true)]
    public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="hWnd"></param>
    /// <param name="msg"></param>
    /// <param name="wParam"></param>
    /// <param name="lParam"></param>
    /// <returns></returns>
    [DllImport("User32.Dll", EntryPoint = "PostMessageA")]
    public static extern bool PostMessage(IntPtr hWnd, uint msg, int wParam, int lParam);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="ch"></param>
    /// <returns></returns>
    [DllImport("user32.dll")]
    public static extern byte VkKeyScan(char ch);

    [DllImport("user32.dll")]
    public static extern uint MapVirtualKey(uint uCode, uint uMapType);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static IntPtr FindWindow(string name)
    {
      Process[] procs = Process.GetProcesses();

      foreach (Process proc in procs)
      {
        if (proc.MainWindowTitle == name)
        {
          return proc.MainWindowHandle;
        }
      }

      return IntPtr.Zero;
    }

    [DllImport("user32.dll")]
    public static extern IntPtr SetFocus(IntPtr hWnd);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool SetForegroundWindow(IntPtr hWnd);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="low"></param>
    /// <param name="high"></param>
    /// <returns></returns>
    public static int MakeLong(int low, int high)
    {
      return (high << 16) | (low & 0xffff);
    }

    [DllImport("User32.dll")]
    public static extern uint SendInput(uint numberOfInputs, [MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] INPUT[] input, int structSize);

    [DllImport("user32.dll")]
    public static extern IntPtr GetMessageExtraInfo();

    public const int INPUT_MOUSE = 0;
    public const int INPUT_KEYBOARD = 1;
    public const int INPUT_HARDWARE = 2;
    public const uint KEYEVENTF_EXTENDEDKEY = 0x0001;
    public const uint KEYEVENTF_KEYUP = 0x0002;
    public const uint KEYEVENTF_UNICODE = 0x0004;
    public const uint KEYEVENTF_SCANCODE = 0x0008;
    public const uint XBUTTON1 = 0x0001;
    public const uint XBUTTON2 = 0x0002;
    public const uint MOUSEEVENTF_MOVE = 0x0001;
    public const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
    public const uint MOUSEEVENTF_LEFTUP = 0x0004;
    public const uint MOUSEEVENTF_RIGHTDOWN = 0x0008;
    public const uint MOUSEEVENTF_RIGHTUP = 0x0010;
    public const uint MOUSEEVENTF_MIDDLEDOWN = 0x0020;
    public const uint MOUSEEVENTF_MIDDLEUP = 0x0040;
    public const uint MOUSEEVENTF_XDOWN = 0x0080;
    public const uint MOUSEEVENTF_XUP = 0x0100;
    public const uint MOUSEEVENTF_WHEEL = 0x0800;
    public const uint MOUSEEVENTF_VIRTUALDESK = 0x4000;
    public const uint MOUSEEVENTF_ABSOLUTE = 0x8000;
  }

  [StructLayout(LayoutKind.Sequential)]
  public struct MOUSEINPUT
  {
    int dx;
    int dy;
    uint mouseData;
    uint dwFlags;
    uint time;
    IntPtr dwExtraInfo;
  }

  [StructLayout(LayoutKind.Sequential)]
  public struct KEYBDINPUT
  {
    public ushort wVk;
    public ushort wScan;
    public uint dwFlags;
    public uint time;
    public IntPtr dwExtraInfo;
  }

  [StructLayout(LayoutKind.Sequential)]
  public struct HARDWAREINPUT
  {
    uint uMsg;
    ushort wParamL;
    ushort wParamH;
  }

  [StructLayout(LayoutKind.Explicit)]
  public struct INPUT
  {
    [FieldOffset(0)]
    public int type;
    [FieldOffset(4)] //*
    public MOUSEINPUT mi;
    [FieldOffset(4)] //*
    public KEYBDINPUT ki;
    [FieldOffset(4)] //*
    public HARDWAREINPUT hi;
  }

  public class KeysTest
  {
    [DllImport("user32.dll", SetLastError = true)]
    static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);

    [DllImport("user32.dll")]
    public static extern IntPtr PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

    const uint WM_KEYDOWN = 0x100;
    const uint WM_KEYUP = 0x101;
    const uint Key_Down = 0x0001;
    const uint Key_Up = 0x0002;
    private volatile bool _shouldStop;
    private Process _sroProcess;
    private Keys _modefirKey;
    private Keys _pressKey;

    public KeysTest(Process targetSro, Keys modKey, Keys pressKey)
    {
      _sroProcess = targetSro;
      _modefirKey = modKey;
      _pressKey = pressKey;
    }

    public void SendKeys()
    {
      SendKeyWithModefir(_sroProcess, (byte)_modefirKey, (byte)_pressKey);
    }

    public void SendKeyWithModefir(Process _p, byte ModKey, byte PressKey)
    {
      while (!_shouldStop)
      {
        keybd_event(ModKey, 0, Key_Down, 0);
        Thread.Sleep(50);
        PostMessage(_p.MainWindowHandle, WM_KEYDOWN, (IntPtr)(PressKey), (IntPtr)(0));
        PostMessage(_p.MainWindowHandle, WM_KEYUP, (IntPtr)(PressKey), (IntPtr)(0));
        Thread.Sleep(50);
        keybd_event(ModKey, 0, Key_Up, 0);
      }
    }

    public void RequestStop()
    {
      _shouldStop = true;
    }

    ~KeysTest()
    {
      GC.Collect();
    }
  }
}
