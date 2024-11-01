using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Image = System.Drawing.Image;

namespace AutoResetTool
{
  public partial class Main : Form
  {
    int startY;
    string _fileMainExe;
    List<string> fileNames;
    private string serviceFile = "Services.txt";
    internal bool IsBusy { get; set; }
    LicenseKeyModel _licenseModel;
    public Main()
    {
      InitializeComponent();
      fileNames = new List<string>();
      //check license:
#if LICENSE
      CheckLicense();
#endif
    }

    private void CheckLicense()
    {
      try
      {
        var key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("Techforus");
        const string KeyName = "LicenseKey";
        if (key != null)
        {
          string licenseKey = key.GetValue(KeyName) as string;
          if (licenseKey != null)
          {
            LicenseKeyModel model = ApiHelper.GetLicenseKeyModel(licenseKey);
            _licenseModel = model;
            if (model == null || model.IsActive)
            {
              LicenseKeyForm form = new LicenseKeyForm();
              if (form.ShowDialog() != DialogResult.OK)
              {
                Application.Exit();
                return;
              }
              _licenseModel = form._licenseKeyModel;
            }
            else
            {
              _licenseModel.IsActive = true;
              ApiHelper.UpdateLicenseKey(_licenseModel);
            }
          }
          else
          {
            LicenseKeyForm form = new LicenseKeyForm();
            if (form.ShowDialog() != DialogResult.OK)
            {
              Application.Exit();
              return;
            }
            _licenseModel = form._licenseKeyModel;
          }
        }
        else
        {
          MessageBox.Show("Cannot get License Key!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
          Application.Exit();
          return;
        }
      }
      catch (Exception)
      {
        MessageBox.Show("License Key validate failed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        Application.Exit();
        return;
      }
    }

    internal void DeleteService(ServiceItem serviceItem)
    {
      int idx = fileNames.IndexOf(serviceItem.FileName);
      if (idx >= 0)
        fileNames.RemoveAt(idx);
      pnlContainer.Controls.Remove(serviceItem);
      int count = pnlContainer.Controls.Count;
      startY = 0;
      for (int i = 0; i < count; i++)
      {
        ServiceItem item = pnlContainer.Controls[i] as ServiceItem;
        item.Location = new Point(0, startY);
        startY += item.Height + 5;
      }
      File.WriteAllLines(serviceFile, fileNames);
    }

    private ServiceItem AppendServiceItem(string fileName, bool appendFile)
    {
      try
      {
        ServiceItem serviceItem = new ServiceItem(this, fileName);
        serviceItem.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        serviceItem.Width = pnlContainer.Width - 20;
        serviceItem.Location = new Point(0, startY);
        startY += serviceItem.Height + 5;
        pnlContainer.Controls.Add(serviceItem);
        if (appendFile)
        {
          fileNames.Add(fileName);
        }
        return serviceItem;
      }
      catch (Exception)
      {
        throw;
      }
    }

    protected override void OnClosing(CancelEventArgs e)
    {
      File.WriteAllLines(serviceFile, fileNames);
      //update license status:
#if LICENSE
      _licenseModel.IsActive = false;
      ApiHelper.UpdateLicenseKey(_licenseModel);
#endif
      base.OnClosing(e);
    }

    private void Main_Load(object sender, EventArgs e)
    {
#if LICENSE
      if (_licenseModel == null)
      {
        Application.Exit();
        return;
      }
#endif
      if (File.Exists(serviceFile))
      {
        string[] lines = File.ReadAllLines(serviceFile);
        if (lines.Length > 0)
        {
          _fileMainExe = lines[0];
          txtFileMain.Text = _fileMainExe;
#if TRIAL
          fileNames.Add(lines[0]);
#else
        fileNames.AddRange(lines);
#endif
        }
      }
      startY = 0;
      foreach (var fileName in fileNames)
      {
        AppendServiceItem(fileName, false);
      }
#if TRIAL
      this.Text += " - Trial version";
#endif
    }

    private void btnAddClient_Click(object sender, EventArgs e)
    {
#if TRIAL
        if (fileNames.Count == 1)
        {
          MessageBox.Show("Bản dùng thử chỉ được phép chạy 1 client.");
          return;
        }
#endif
      var serviceItem = AppendServiceItem(txtFileMain.Text, true);
    }

    private void btnBrowse_Click(object sender, EventArgs e)
    {
      OpenFileDialog fileDialog = new OpenFileDialog();
      fileDialog.Filter = "exe|*.exe";
      if (fileDialog.ShowDialog() == DialogResult.OK)
      {
        txtFileMain.Text = fileDialog.FileName;
      }
    }
  }
}
