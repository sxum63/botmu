using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoResetTool
{
  internal static class Program
  {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
      if (PriorProcess() != null)
      {

        MessageBox.Show("Chương trình đang được chạy.");
        return;
      }
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new Main());
    }
    static Process PriorProcess()
    // Returns a System.Diagnostics.Process pointing to
    // a pre-existing process with the same name as the
    // current one, if any; or null if the current process
    // is unique.
    {
      Process curr = Process.GetCurrentProcess();
      Process[] procs = Process.GetProcessesByName(curr.ProcessName);
      foreach (Process p in procs)
      {
        if ((p.Id != curr.Id) &&
            (p.MainModule.FileName == curr.MainModule.FileName))
          return p;
      }
      return null;
    }

    static void InitFirestoreEnvironment()
    {
      string value;
      bool toDelete = false;
      const string var_name = "GOOGLE_APPLICATION_CREDENTIALS";
      const string path_value = "auto-reset-muonline-e3bf3baae2c3.json";
      // Check whether the environment variable exists.
      value = Environment.GetEnvironmentVariable(var_name);
      // If necessary, create it.
      if (value == null)
      {
        Environment.SetEnvironmentVariable(var_name, path_value);
        toDelete = true;

        // Now retrieve it.
        value = Environment.GetEnvironmentVariable(var_name);
      }
      // Display the value.
      Console.WriteLine($"Test1: {value}\n");

      // Confirm that the value can only be retrieved from the process
      // environment block if running on a Windows system.
      if (Environment.OSVersion.Platform == PlatformID.Win32NT)
      {
        Console.WriteLine("Attempting to retrieve Test1 from:");
        foreach (EnvironmentVariableTarget enumValue in
                          Enum.GetValues(typeof(EnvironmentVariableTarget)))
        {
          value = Environment.GetEnvironmentVariable(var_name, enumValue);
          Console.WriteLine($"   {enumValue}: {(value != null ? "found" : "not found")}");
        }
        Console.WriteLine();
      }

      // If we've created it, now delete it.
      if (toDelete)
      {
        Environment.SetEnvironmentVariable(var_name, null);
        // Confirm the deletion.
        if (Environment.GetEnvironmentVariable(var_name) == null)
          Console.WriteLine("Test1 has been deleted.");
      }
    }
  }
}
