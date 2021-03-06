using System;
using System.Diagnostics;
using System.Management;
using System.Windows.Forms;


namespace PC_Components_Monitor
{
    public partial class Form1 : Form
    {

        PerformanceCounter cpu = new PerformanceCounter("Processor", "% Processor Time", "_Total"); //show current processor usage in percent
        PerformanceCounter mem = new PerformanceCounter("Memory", "Available MBytes"); // show available memory in megabytes in device 
        

        System.IO.DriveInfo discC = new System.IO.DriveInfo(@"C:\");
        System.IO.DriveInfo discD = new System.IO.DriveInfo(@"D:\");

        

        public Form1()
        {
            InitializeComponent();

        }
        public static int GetPhysicalMemory()
        {
            ManagementScope oMs = new ManagementScope();
            ObjectQuery oQuery = new ObjectQuery("SELECT Capacity FROM Win32_PhysicalMemory");
            ManagementObjectSearcher oSearcher = new ManagementObjectSearcher(oMs, oQuery);
            ManagementObjectCollection oCollection = oSearcher.Get();

            long MemSize = 0;
            long mCap = 0;

           
            foreach (ManagementObject obj in oCollection)
            {
                mCap = Convert.ToInt64(obj["Capacity"]);
                MemSize += mCap;
            }
            MemSize = (MemSize / 1024) / 1024;
            return (int)MemSize;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            // Create diagram for CPU usage in %
            float cpu = this.cpu.NextValue();
            int cpuValue = (int)cpu;
            circularProgressBar1.Value = cpuValue;
            circularProgressBar1.Text = cpuValue.ToString() + " %";

            

            // Create diagram for available memory 
            float ram = mem.NextValue();
            int ramValue = (int)ram;
            circularProgressBar2.Value = (ramValue * 100) / GetPhysicalMemory();
            circularProgressBar2.Text = ram.ToString() + "MB";

            // Create diagram for memory usage 
            float ramUsage = 8192 - (mem.NextValue());
            int ramValueUsage = (int)ramUsage;
            circularProgressBar3.Value = (ramValueUsage * 100) / GetPhysicalMemory();
            circularProgressBar3.Text = ramUsage.ToString() + "MB";


            
            label5.Text = "Total free space in C partition: " + (discC.TotalFreeSpace) / 1024 / 1024 / 1024 + " GB, Total free space in D partition: " + (discD.TotalFreeSpace) / 1024 / 1024 / 1024 + " GB";

            label6.Text = "Total physical memory: " + GetPhysicalMemory() + "MB";

        }


    }
}
