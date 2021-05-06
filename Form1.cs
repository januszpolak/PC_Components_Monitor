using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace PC_Components_Monitor
{
    public partial class Form1 : Form
    {

        PerformanceCounter perform = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        PerformanceCounter mem = new PerformanceCounter("Memory", "Available MBytes");
        



        public Form1()
        {
            InitializeComponent();
            

        }

        

        private void timer1_Tick(object sender, EventArgs e)
        {
        
            float cpu = perform.NextValue();
            int cpuValue = (int)cpu;
            circularProgressBar1.Value = cpuValue;
            circularProgressBar1.Text = cpuValue.ToString() + " %";


            float ram = mem.NextValue();
            int ramValue = (int)ram;
            circularProgressBar2.Value = (ramValue * 100) / 8192;
            circularProgressBar2.Text = ram.ToString() + "MB";
        }
    }
}
