using AutoResetTool.Definitions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization.Configuration;

namespace AutoResetTool
{
  public partial class ServiceItem : UserControl
  {
    Main _owner;
    string _path;
    bool _isStart;
    Process _process;
    Timer _timer;
    bool _isProcessing;
    internal string FileName => _path;
    internal Process Process => _process;
    private bool _ctrlF;
    private bool _isMacroSetted;
    private int _resetCount;
    private bool _isAntiLag;
    public ServiceItem()
    {
      InitializeComponent();
    }
    public ServiceItem(Main owner, string path) : this()
    {
      _owner = owner;
      _path = path;
      lblServiceName.Text = Path.GetFileNameWithoutExtension(path);
      ToolTip toolTip = new ToolTip();
      toolTip.SetToolTip(lblServiceName, path);
      _timer = new Timer();
      _timer.Interval = 1000;
      _timer.Tick += _timer_Tick;
      _tryCount = 20;
    }
    bool _isTimerRunning;
    int _tryCount;
    private void _timer_Tick(object sender, EventArgs e)
    {
      if (_process != null && _process.HasExited)
      {
        _process_Exited();
        _process.Dispose();
        _process = null;
      }
      else if (!_isProcessing && _process != null && !_process.HasExited)
      {
        _isProcessing = true;
        TryGetCharInfo(out string charName, out int level);
        lblServiceName.Text = $"[{charName}]-[{level}]-[{_resetCount}]";
        if (!_owner.IsBusy)
        {
          _owner.IsBusy = true;
          IntPtr hwd = _process.MainWindowHandle;
          if (!_isMacroSetted)
          {
            IntPtr activeWindow = WindowsAPI.GetForegroundWindow();
            WindowsAPI.SetForegroundWindow(hwd);
            if (_ctrlF)
              PressCombineKeys(WindowsAPI.VK_CONTROL, WindowsAPI.VK_F);
            PressKey(WindowsAPI.VK_RETURN);
            Task.Delay(200).Wait();
            AutoHelper.SendKeyboard(hwd, "/1 /reset");
            Task.Delay(200).Wait();
            PressKey(WindowsAPI.VK_RETURN);
            Task.Delay(200).Wait();
            _isMacroSetted = true;
            if (_ctrlF)
              PressCombineKeys(WindowsAPI.VK_CONTROL, WindowsAPI.VK_F);
            WindowsAPI.SetForegroundWindow(activeWindow);
          }
          if (level == 400)
          {
            if (_tryCount == 0)
            {
              StopTimer();
              SetButtonError(true);
              _owner.IsBusy = false;
              _isProcessing = false;
#if !TRIAL
              ResetTryCount();
#endif
              return;
            }
            IntPtr activeWindow = WindowsAPI.GetForegroundWindow();
            bool result = WindowsAPI.SetForegroundWindow(hwd);
            _tryCount--;
            PressCombineKeys(WindowsAPI.VK_MENU, WindowsAPI.VK_1);
            Task.Delay(500).Wait();
            TryGetCharInfo(out charName, out level);
            if (level != 400)
            {
              _resetCount++;
#if !TRIAL
              ResetTryCount();
#endif
            }
            WindowsAPI.SetForegroundWindow(activeWindow);
          }
          _owner.IsBusy = false;
        }
        _isProcessing = false;
      }
    }

    private void ResetTryCount()
    {
      _tryCount = 20;
    }

    private void SetButtonError(bool error)
    {
      if (error)
      {
        btnRunAuto.BackColor = Color.FromArgb(255, 128, 128);
        btnRunAuto.ForeColor = Color.White;
      }
      else
      {
        btnRunAuto.BackColor = Color.White;
        btnRunAuto.ForeColor = Color.Black;
      }
    }

    private void _process_Exited()
    {
      btnRunAuto.Text = "Run Auto";
      btnStart.Text = "Chạy Client";
      _isStart = false;
      StopTimer();
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {
      Stop(false);
      _owner.DeleteService(this);
      //PressAlt1_Inactive();
    }

    internal void StopTimer()
    {
      _timer.Stop();
      btnRunAuto.Text = "Run Auto";
      _isTimerRunning = false;
    }
    internal void StartTimer()
    {
      _timer.Interval = 1000;
      _timer.Start();
      _timer_Tick(null, EventArgs.Empty);
    }

    private Process FindWindow(string charName)
    {
      if (string.IsNullOrEmpty(charName))
        return null;
      foreach (Process pList in Process.GetProcesses())
      {
        string title = pList.MainWindowTitle.ToLower();
        GetNameLevel(title, out string name, out _);
        if (name.ToLower() == charName.ToLower())
        {
          return pList;
        }
      }
      return null;
    }

    private void TryGetCharInfo(out string charName, out int level)
    {
      charName = string.Empty;
      level = 0;
      try
      {
        IntPtr hwd = _process.MainWindowHandle;
        string title = AutoHelper.GetText(hwd);
        GetNameLevel(title, out charName, out level);
      }
      catch (Exception)
      {
      }
    }

    private void GetNameLevel(string title, out string charName, out int level)
    {
      charName = string.Empty;
      level = 0;
      Regex regex = new Regex("\\[(.*?)\\]");
      var matches = regex.Matches(title);
      if (matches.Count > 2)
      {
        string name = matches[0].Value;
        charName = name.Substring(1, name.Length - 2);
        name = matches[1].Value;
        level = int.Parse(name.Substring(1, name.Length - 2));
      }
    }

    private void btnRunAuto_Click(object sender, EventArgs e)
    {
      if (string.IsNullOrEmpty(txtCharName.Text))
      {
        MessageBox.Show("Điền tên nhân vật!");
        return;
      }
      else
      {
        int tryCount = 10;
        while (_process == null && tryCount-- > 0)
        {
          _process = FindWindow(txtCharName.Text);
        }
        if (_process == null)
        {
          MessageBox.Show("Không tìm thấy cửa sổ game!");
          return;
        }
      }
      SetButtonError(false);
      if (_isTimerRunning)
      {
        StopTimer();
      }
      else
      {
        _isTimerRunning = true;
        btnRunAuto.Text = "Stop Auto";
        StartTimer();
      }
    }

    private void btnStart_Click(object sender, EventArgs e)
    {
      if (!_isStart)
      {
        Start();
        btnStart.Text = "Dừng Client";
      }
      else
      {
        Stop(false);
        btnStart.Text = "Chạy Client";
      }
    }

    internal void Start()
    {
      try
      {
        ProcessStartInfo processStartInfo = new ProcessStartInfo(_path);
        processStartInfo.WorkingDirectory = Path.GetDirectoryName(_path);
        if (Path.GetExtension(_path) == ".txt")
          _process = Process.Start("notepad.exe", _path);
        else
          _process = Process.Start(processStartInfo);
        btnRunAuto.Enabled = true;
        btnDelete.Enabled = true;
        btnStart.Enabled = true;
        ckbCtrlF.Enabled = true;
        ckbAntiLag.Enabled = true;
        btnStart.Text = "Dừng Client";
        _isStart = true;
        _isMacroSetted = false;
        _resetCount = 0;
        ResetTryCount();
      }
      catch (Exception)
      {
        Application.Exit();
      }
    }

    private void ckbCtrlF_CheckedChanged(object sender, EventArgs e)
    {
      if (ckbCtrlF.Checked != _ctrlF)
      {
        PressCombineKeys(WindowsAPI.VK_CONTROL, WindowsAPI.VK_F);
      }
      _ctrlF = ckbCtrlF.Checked;
    }

    internal void Stop(bool killProcess)
    {
      if (killProcess && _process != null && !_process.HasExited)
        _process.Kill();
      StopTimer();
      _isStart = false;
      _isMacroSetted = false;
      btnRunAuto.Enabled = false;
      btnDelete.Enabled = false;
      ckbCtrlF.Enabled = false;
      ckbAntiLag.Enabled = false;
    }

    private void PressKey(ushort virtualKeyCode)
    {
      IntPtr activeWindow = WindowsAPI.GetForegroundWindow();
      IntPtr hwd = _process.MainWindowHandle;
      WindowsAPI.SetForegroundWindow(hwd);
      INPUT[] inputs = new INPUT[1];
      inputs[0].type = WindowsAPI.INPUT_KEYBOARD;
      inputs[0].ki.wVk = virtualKeyCode;
      WindowsAPI.SendInput(1, inputs, System.Runtime.InteropServices.Marshal.SizeOf(typeof(INPUT)));
      Task.Delay(80).Wait();
      inputs = new INPUT[1];
      inputs[0].type = WindowsAPI.INPUT_KEYBOARD;
      inputs[0].ki.wVk = virtualKeyCode;
      inputs[0].ki.dwFlags = (uint)KEYEVENTF.KEYUP;
      WindowsAPI.SendInput(1, inputs, System.Runtime.InteropServices.Marshal.SizeOf(typeof(INPUT)));
      WindowsAPI.SetForegroundWindow(activeWindow);
    }
    private void PressCombineKeys(ushort firstKey, ushort secondKey)
    {
      IntPtr activeWindow = WindowsAPI.GetForegroundWindow();
      IntPtr hwd = _process.MainWindowHandle;
      WindowsAPI.SetForegroundWindow(hwd);
      INPUT[] inputs = new INPUT[1];
      inputs[0].type = WindowsAPI.INPUT_KEYBOARD;
      inputs[0].ki.wVk = firstKey;
      WindowsAPI.SendInput(1, inputs, System.Runtime.InteropServices.Marshal.SizeOf(typeof(INPUT)));
      Task.Delay(100).Wait();

      inputs = new INPUT[1];
      inputs[0].type = WindowsAPI.INPUT_KEYBOARD;
      inputs[0].ki.wVk = secondKey;
      WindowsAPI.SendInput(1, inputs, System.Runtime.InteropServices.Marshal.SizeOf(typeof(INPUT)));
      Task.Delay(100).Wait();

      inputs = new INPUT[1];
      inputs[0].type = WindowsAPI.INPUT_KEYBOARD;
      inputs[0].ki.wVk = secondKey;
      inputs[0].ki.dwFlags = (uint)KEYEVENTF.KEYUP;
      WindowsAPI.SendInput(1, inputs, System.Runtime.InteropServices.Marshal.SizeOf(typeof(INPUT)));
      Task.Delay(100).Wait();

      inputs = new INPUT[1];
      inputs[0].type = WindowsAPI.INPUT_KEYBOARD;
      inputs[0].ki.wVk = firstKey;
      inputs[0].ki.dwFlags = (uint)KEYEVENTF.KEYUP;
      WindowsAPI.SendInput(1, inputs, System.Runtime.InteropServices.Marshal.SizeOf(typeof(INPUT)));
      WindowsAPI.SetForegroundWindow(activeWindow);
    }

    private void ServiceItem_Click(object sender, EventArgs e)
    {
      if (_process != null && !_process.HasExited)
      {
        IntPtr hwd = _process.MainWindowHandle;
        WindowsAPI.SetForegroundWindow(hwd);
      }
    }

    private void ckbAntiLag_CheckedChanged(object sender, EventArgs e)
    {
      IntPtr activeWindow = WindowsAPI.GetForegroundWindow();
      IntPtr hwd = _process.MainWindowHandle;
      WindowsAPI.SetForegroundWindow(hwd);
      _isAntiLag = !_isAntiLag;
      WindowsAPI.SetForegroundWindow(activeWindow);
    }

    [DllImport("user32", CharSet = CharSet.Auto)]
    public extern static IntPtr SendMessage(IntPtr handle, int msg, int wParam, IntPtr lParam);

    [DllImport("user32", CharSet = CharSet.Auto)]
    public extern static int SendMessage(IntPtr handle, int msg, IntPtr wParam, IntPtr lParam);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr SendMessage(IntPtr handle, int msg, int wParam, int lParam);

    [DllImport("user32", CharSet = CharSet.Auto)]
    public extern static int PostMessage(IntPtr handle, int msg, int wParam, IntPtr lParam);

    [DllImport("user32.dll", SetLastError = true)]
    static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);

    private void txtCharName_KeyUp(object sender, KeyEventArgs e)
    {
      if(e.KeyCode == Keys.Return)
      {
        btnRunAuto.PerformClick();
      }
    }
  }
}
