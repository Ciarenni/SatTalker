using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;

namespace SatTalker
{
    public partial class SerialPortSetup : Form
    {
        SatTalker satTalk;
        bool debugEnabled = false, exit = true;
        public SerialPortSetup(SatTalker main)
        {
            InitializeComponent();
            satTalk = main;

            radioComCombo.Items.AddRange(SerialPort.GetPortNames());
            rotaterComCombo.Items.AddRange(SerialPort.GetPortNames());
            if (SerialPort.GetPortNames().Length > 0)
            {
                radioComCombo.SelectedIndex = 0;
                rotaterComCombo.SelectedIndex = 0;
            }
        }

        private void serialCancelBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void serialSetupBtn_Click(object sender, EventArgs e)
        {
            if ((radioComCombo.SelectedIndex.ToString() == rotaterComCombo.SelectedIndex.ToString()) && !debugEnabled)
                MessageBox.Show("You must specify different ports for the radio and the rotater.", "Need different ports");
            else
            {
                satTalk.setupPorts(radioComCombo.SelectedItem.ToString(), rotaterComCombo.SelectedItem.ToString(), debugEnabled);
                exit = false;
                this.Close();
            }
        }

        private void debugLabel_DoubleClick(object sender, EventArgs e)
        {
            debugEnabled = !debugEnabled;
            MessageBox.Show("DebugEnabled is now set to " + debugEnabled.ToString() + ".", "Debug enabled/disabled");
        }

        private void SerialPortSetup_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (exit)
                Application.Exit();
        }
    }
}
