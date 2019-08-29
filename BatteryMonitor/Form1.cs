using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BatteryMonitor
{
    public partial class Form1 : Form
    {

        private Timer timer1;
        string powerSource, powerLevel;

        public Form1()
        {
            timer1 = new Timer();
            timer1.Start();
            timer1.Tick += new System.EventHandler(onTimerEvent);
            timer1.Interval = 1000; //interval of 1s

            InitializeComponent();

            powerLevel = GetBatteryStatus().ToString() + " %";
            powerSource = GetPowerSource();
            //setting the title of form so that the status can be viewed when window is minimised
            this.Text = powerLevel + " - " + powerSource;

            lblPowerSource.Text = GetPowerSource();
            lblStatus.Text = powerLevel;
        }

        private void onTimerEvent(object sender, EventArgs e)
        { 
            powerSource = GetPowerSource();
            powerLevel = GetBatteryStatus().ToString() + " %";

            lblPowerSource.Text = powerSource;
            lblStatus.Text = powerLevel;

            this.Text = powerLevel + " - " + powerSource;
        }

        private void btnCloseWindow_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            Application.Exit();
        }


        public String GetPowerSource()
        {
            string strPowerLineStatus = "Default";

            // Getting the current system power status.
            switch (SystemInformation.PowerStatus.PowerLineStatus)
            {
                case PowerLineStatus.Offline:
                    strPowerLineStatus = "Battery";
                    break;
                case PowerLineStatus.Online:
                    strPowerLineStatus = "AC Power";
                    break;
                case PowerLineStatus.Unknown:
                    strPowerLineStatus = "Unknown";
                    break;
            }

            return strPowerLineStatus;
        }

        public float GetBatteryStatus()
        {           
            float batterylife;
            batterylife = SystemInformation.PowerStatus.BatteryLifePercent;
            batterylife *= 100.0f;    
            
            return batterylife;

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            lblPowerSource.Text = GetPowerSource();
            lblStatus.Text = GetBatteryStatus().ToString() + " %";
        }
    }


    
}
