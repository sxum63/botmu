using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoResetTool
{
  public partial class LicenseKeyForm : Form
  {
    public LicenseKeyForm()
    {
      InitializeComponent();
    }
    internal LicenseKeyModel _licenseKeyModel;
    private void button1_Click(object sender, EventArgs e)
    {
      string key = textBox1.Text;
      LicenseKeyModel model = ApiHelper.GetLicenseKeyModel(key);
      if (model != null && model.IsActive == false)
      {
        try
        {
          //save:
          model.IsActive = true;
          bool success = ApiHelper.UpdateLicenseKey(model);
          if (success)
          {
            var registryKey = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("Techforus");
            if (registryKey != null)
            {
              registryKey.SetValue("LicenseKey", key);
            }
            _licenseKeyModel = model;
            DialogResult = DialogResult.OK;
            this.Close();
            return;
          }
        }
        catch (Exception)
        {
          MessageBox.Show("Invalid Key!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
      }
      MessageBox.Show("Invalid Key!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }
  }
}
