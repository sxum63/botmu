using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoChangeMac
{
  public partial class Form1 : Form
  {
    public List<Process> _listProcess;
    string _configFile = "config.txt";
    string _batchFile = "mac_change.bat";
    public Form1()
    {
      InitializeComponent();
      _listProcess = new List<Process>();
    }

    private void btnRun_Click(object sender, EventArgs e)
    {
      if (File.Exists(txtFilePath.Text))
      {
        if (_listProcess.Count > 0 && _listProcess.Count % 3 == 0)
        {
          Process p = Process.Start(_batchFile);
          if (p.WaitForExit(1000))
          {
            goto START_NEW_PROCESS;
          }
          return;
        }
      START_NEW_PROCESS:
        {
          ProcessStartInfo processStartInfo = new ProcessStartInfo(txtFilePath.Text);
          processStartInfo.WorkingDirectory = Path.GetDirectoryName(txtFilePath.Text);
          Process process = Process.Start(processStartInfo);
          process.Exited += Process_Exited;
          _listProcess.Add(process);
        }
      }
    }

    private void Process_Exited(object sender, EventArgs e)
    {

    }

    private void Form1_Load(object sender, EventArgs e)
    {
      if (File.Exists(_configFile))
      {
        string path = File.ReadAllText(_configFile);
        txtFilePath.Text = path;
      }
    }

    private void btnBrowse_Click(object sender, EventArgs e)
    {
      OpenFileDialog fileDialog = new OpenFileDialog();
      if (fileDialog.ShowDialog() == DialogResult.OK)
      {
        txtFilePath.Text = fileDialog.FileName;
        File.WriteAllText(_configFile, fileDialog.FileName);
      }
    }
  }
}
