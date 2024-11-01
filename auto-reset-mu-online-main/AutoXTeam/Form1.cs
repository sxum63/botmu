using Binarysharp.MemoryManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoXTeam
{
  public partial class Form1 : Form
  {

    const int PROCESS_VM_READ = 0x0010;
    const int PROCESS_VM_WRITE = 0x0020;
    const int PROCESS_VM_OPERATION = 0x0008;
    public Form1()
    {
      InitializeComponent();
    }

    private void Form1_Load(object sender, EventArgs e)
    {
      // Example process name and memory address to read/write (adjust these as necessary)
      string processName = "main";
      int memoryAddress = 0x139A080; // Replace with the actual memory address you want to access
      // Open the process
      using (var sharp = new MemorySharp(Process.GetProcessesByName(processName).First()))
      {
        IntPtr address = new IntPtr(memoryAddress);  // Replace with actual memory address
        // Writing 4 bytes to memory
        byte[] newData = new byte[] { 177, 0, 144 };
        sharp.Write(address, newData);
        Console.WriteLine("2-byte value written successfully.");
        byte[] newValue = sharp.Read<byte>(address, 3);
      }
    }

    private void btnReadInfo_Click(object sender, EventArgs e)
    {
      // Example process name and memory address to read/write (adjust these as necessary)
      string processName = "main";
      using (var sharp = new MemorySharp(Process.GetProcessesByName(processName).First()))
      {
        // 1. Get the base address of "main.exe"
        var module = sharp.Modules.RemoteModules.First(m => m.Name == "main.exe");
        IntPtr baseAddress = module.BaseAddress + 0x09F94F10;
        // Step 2: Apply the offsets [272, 0, 532, 164]
        try
        {
          // Dereference base_address + 0x110 (272)
          IntPtr ptr1 = sharp.Read<IntPtr>(baseAddress + 0x110);

          // Dereference ptr1 + 0x0 (0)
          IntPtr ptr2 = sharp.Read<IntPtr>(ptr1 + 0x0);

          // Dereference ptr2 + 0x214 (532)
          IntPtr ptr3 = sharp.Read<IntPtr>(ptr2 + 0x214);

          // Final address: ptr3 + 0xA4 (164)
          IntPtr finalAddress = ptr3 + 0xA4;

          // Step 3: Read 2 bytes from the final address
          byte[] data = sharp.Read<byte>(finalAddress, 2);

          // Convert the 2-byte array to a hexadecimal string for display
          string hexValue = BitConverter.ToString(data).Replace("-", " ");

          Console.WriteLine($"2-byte value at address {finalAddress.ToInt64():X}: {hexValue}");
        }
        catch (Exception)
        {

        }
      }
    }

    private void btnMoveChar_Click(object sender, EventArgs e)
    {
      // Example process name and memory address to read/write (adjust these as necessary)
      string processName = "main";
      int memoryAddress = 0x20570656; // Replace with the actual memory address you want to access
      // Open the process
      using (var sharp = new MemorySharp(Process.GetProcessesByName(processName).First()))
      {
        // Example: Writing a 2-byte value (short or ushort) to memory
        IntPtr address = new IntPtr(memoryAddress); // Replace with the actual memory address
        short value = 6913; // Or ushort if needed
        IntPtr currentAddress = IntPtr.Add(address, 8);
        sharp.Write(currentAddress, value);
        int x = 150; // Or ushort if needed
        currentAddress = IntPtr.Add(address, 68);
        sharp.Write(currentAddress, x);
        int y = 115; // Or ushort if needed
        currentAddress = IntPtr.Add(address, 72);
        sharp.Write(currentAddress, y);
        Console.WriteLine("2-byte value written successfully.");
      }
    }

    private void btnChart_Click(object sender, EventArgs e)
    {
      // Example process name and memory address to read/write (adjust these as necessary)
      string processName = "main.exe";
      int memoryAddress = 0x018DEBA8; // Replace with the actual memory address you want to access

      Process process = Process.GetProcessesByName(processName)[0];
      IntPtr processHandle = AutoXTeamHelper.OpenProcess(PROCESS_VM_READ | PROCESS_VM_WRITE | PROCESS_VM_OPERATION, false, process.Id);

      if (processHandle != IntPtr.Zero)
      {
        // Example: Reading memory
        byte[] buffer = new byte[4]; // Example: reading 4 bytes
        int bytesRead = 0;
        bool success = AutoXTeamHelper.ReadProcessMemory(processHandle, (IntPtr)memoryAddress, buffer, buffer.Length, out bytesRead);

        if (success)
        {
          Console.WriteLine($"Memory value: {BitConverter.ToString(buffer, 0)}");
        }
        else
        {
          Console.WriteLine("Failed to read memory.");
        }

        // Example: Writing memory
        byte[] newBuffer = BitConverter.GetBytes(1234); // Example: writing the value 1234
        int bytesWritten = 0;
        success = AutoXTeamHelper.WriteProcessMemory(processHandle, (IntPtr)memoryAddress, newBuffer, newBuffer.Length, out bytesWritten);

        if (success)
        {
          Console.WriteLine("Memory written successfully.");
        }
        else
        {
          Console.WriteLine("Failed to write memory.");
        }

        AutoXTeamHelper.CloseHandle(processHandle);
      }
      else
      {
        Console.WriteLine("Failed to open process.");
      }
    }
  }
}
