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
  public partial class GenerateKey : Form
  {
    public GenerateKey()
    {
      InitializeComponent();
    }

    private void btnCopy_Click(object sender, EventArgs e)
    {
      Clipboard.SetText(txtKey.Text);
    }

    private void btnGenerate_Click(object sender, EventArgs e)
    {
      LicenseKeyModel licenseKeyModel = new LicenseKeyModel
      {
        ActiveDate = DateTime.Now.Ticks / 1000,
        ExpiredDate = DateTime.Now.AddYears(1).Ticks / 1000,
        ComputerName = "Admin",
        Key = GenerateRandomString(30)
      };
      if (ApiHelper.UpdateLicenseKey(licenseKeyModel))
      {
        MessageBox.Show("Tạo key thành công!");
        txtKey.Text = licenseKeyModel.Key;
      }
      else
      {
        MessageBox.Show("Tạo key thất bại!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private string GenerateRandomString(int length)
    {
      string pattern = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
      int size = pattern.Length;
      Random random = new Random();
      StringBuilder stringBuilder = new StringBuilder();
      for (int i = 0; i < length; i++)
      {
        stringBuilder.Append(pattern[random.Next(0, size - 1)]);
      }
      return stringBuilder.ToString();
    }
  }
}
