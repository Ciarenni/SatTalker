using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
using System.IO.Pipes;
using System.Diagnostics;
using System.Threading;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Reflection;
using SpaceHardwareClub;

namespace SatTalker
{
    public partial class SatTalker : Form
    {
        string azimuth, elevation, downlink, uplink, oldDopp, offsetString, oldAz, oldEl, duplexOffset;
        string DDELoc, orbitronLoc, locBase;//AzEleDopLoc;
        bool listening, debugEnabled = false, duplexPlus, transmitting = false;
        int offset;
        const int port = 6767;

        //NamedPipeClientStream client = new NamedPipeClientStream("OrbitronPipe");
        SerialPort radioPort, rotatePort;
        SerialPortSetup portSetup;
        Thread sendCmd;
        DateTime lastUpdate;
        TcpChannel tcpChannel;
        TCPMessage tcpMsg;

        delegate void SetAzCallback(string az);
        delegate void SetElCallback(string el);
        delegate void SetDopLisCallback(string dopL);
        delegate void SetDopTalkCallback(string dopT);
        delegate void SetOffsetCallback(string offst);

        public SatTalker()
        {
            InitializeComponent();
            //AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
            //{
            //    String resourceName = "AssemblyLoadingAndReflection." + new AssemblyName(args.Name).Name + ".dll";
            //    using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            //    {
            //        Byte[] assemblyData = new Byte[stream.Length];
            //        stream.Read(assemblyData, 0, assemblyData.Length);
            //        return Assembly.Load(assemblyData);
            //    }
            //};

            portSetup = new SerialPortSetup(this);
            portSetup.ShowDialog();

            sendCmd = new Thread(sendingCmd);

            locBase = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);//Environment.GetFolderPath(Environment.SpecialFolder.Desktop) +"\\Orbitron\\";
            DDELoc = locBase + @"\DDEDriver\DDETest.exe";
            orbitronLoc = locBase+@"\Orbitron.exe";
            //AzEleDopLoc = locBase+@"mydde\AzEleDop.txt";

            //radioPort = new SerialPort("COM6", 9600, Parity.None, 8, StopBits.One);//delete later, only used for speedy testing
            bool dde = false;
            bool orbitron = false;
            offset = 0;

            foreach (Process clsProc in Process.GetProcesses())
            {
                if (clsProc.ProcessName.Contains("Orbitron"))
                    orbitron = true;
                if (clsProc.ProcessName.Contains("DDETest"))
                {
                    dde = true;
                }
            }

            if (orbitron == false && dde == true)
            {
                foreach (Process clsProc in Process.GetProcesses())
                {
                    if (clsProc.ProcessName.Contains("DDETest"))
                    {
                        clsProc.Kill();
                        System.Diagnostics.Process.Start(DDELoc);
                    }
                }
            }

            try
            {
                if (orbitron == false)
                    System.Diagnostics.Process.Start(orbitronLoc);
                if (dde == false)
                    System.Diagnostics.Process.Start(DDELoc);
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Orbitron or DDETest was not found. Please run SatTalker from the base Orbitron directory with the DDE driver" +
                    " in a folder called DDEDriver at the same location.", "Orbitron/DDETest not found");
            }

            setListening(true);
            UpdateParameters();
            if(!debugEnabled)
                sendCmd.Start();

            tcpMsg = new TCPMessage();
            tcpChannel = new TcpChannel(port);
            ChannelServices.RegisterChannel(tcpChannel, false);
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(TCPMessage), "GetMessage", WellKnownObjectMode.SingleCall);
            RemotingServices.Marshal(tcpMsg, "GetMessage");
        }

        private void setListening(bool listen)
        {
            if (listen)
            {
                downlinkBtn.Enabled = false;
                uplinkBtn.Enabled = true;
            }
            else
            {
                downlinkBtn.Enabled = true;
                uplinkBtn.Enabled = false;
            }
            listening = listen;
        }

        private bool UpdateParameters()
        {
            string message;
            string[] msgParts;

            try
            {
                message = tcpMsg.getParams();
            }
            catch
            {
                return false;
            }

            if (message == string.Empty || message == null)
                return false;
            /*if (File.Exists(AzEleDopLoc))
            {
                string[] inputArray = new string[4];
                int count = 0;

                FileInfo f = new FileInfo(AzEleDopLoc);
                StreamReader read = new StreamReader(Stream.Null);
                try
                {
                    read = f.OpenText();
                }
                catch (IOException ioe)
                {
                    return false;
                }

                while (!read.EndOfStream)
                {
                    inputArray[count] = read.ReadLine();
                    count++;
                }
                read.Close();

                for (int i = 0; i < inputArray.Length; i++)
                {
                    switch (i)
                    {
                        case 0: azimuth = inputArray[i];
                            count++;
                            break;
                        case 1: elevation = inputArray[i];
                            count++;
                            break;
                        case 2: downlink = inputArray[i];
                            count++;
                            break;
                        case 3: uplink = inputArray[i];
                            count++;
                            break;
                    }
                }
            }*/


            msgParts = message.Split('|');//0=name,1=azimuth,2=elevation,3=downlink,4=uplink

            azimuth = msgParts[1];
            elevation = msgParts[2];
            downlink = msgParts[3];
            uplink = msgParts[4];
            
            if (Int32.Parse(downlink) > Int32.Parse(uplink))
            {
                duplexOffset = (Int32.Parse(downlink) - Int32.Parse(uplink)).ToString();
                while (duplexOffset.Length < 8)
                    duplexOffset = '0' + duplexOffset;
                duplexPlus = true;
            }
            else
            {
                duplexOffset = (Int32.Parse(uplink) - Int32.Parse(downlink)).ToString();
                while (duplexOffset.Length < 8)
                    duplexOffset = '0' + duplexOffset;
                duplexPlus = false;
            }

            /*
            //parse az string for data
            azimuth = azimuth.Substring(5);
            trash = azimuth.Split('.');
            azimuth = trash[0];

            //parse ele string for data
            elevation = elevation.Substring(5);
            trash = elevation.Split('.');
            elevation = trash[0];

            //parse dopLis string for data
            if (downlink.Length >= 15)
                downlink = downlink.Substring(8,9);

            //parse dopTal string for data
            if (uplink.Length >= 15)
                uplink = uplink.Substring(8,9);*/
            configureParams();
            return true;
        }

        private void configureParams()
        {
            //configure radio params
            //for the frequency, the thousands place must be a 0 or 5 or the radio will not recognize it
            int num = 0;//, freq;

            Int32.TryParse(downlink, out num);
            if (num == 0)
                downlink = "00000000000";

            Int32.TryParse(uplink, out num);
            if (num == 0)
                uplink = "00000000000";


            downlink = (Convert.ToInt32(downlink) + offset).ToString();
            uplink = (Convert.ToInt32(uplink) + offset).ToString();

            if (downlink == "0")
                downlink = "000000000";
            if (uplink == "0")
                uplink = "000000000";
            /*
             * This code was needed for the D700-A
             * Since the Icom-9100 is more precise, this code is now obsolete
            if (downlink[5] >= '3' && downlink[5] <= '7')
                downlink = downlink.Substring(0, 5) + '5';
            else if (downlink[5] == '8' || downlink[5] == '9')
            {
                freq = Convert.ToInt32(downlink);
                freq += 10;
                downlink = freq.ToString().Substring(0, 5) + '0';
            }
            else
                downlink = downlink.Substring(0, 5) + '0';

            if (uplink[5] >= '3' && uplink[5] <= '7')
                uplink = uplink.Substring(0, 5) + '5';
            else if (uplink[5] == '8' || uplink[5] == '9')
            {
                freq = Convert.ToInt32(uplink);
                freq += 10;
                uplink = freq.ToString().Substring(0, 5) + '0';
            }
            else
                uplink = uplink.Substring(0, 5) + '0';
            */
            //the icom is looking for 10 digits for the frequency, so add a 0 to the begining
            while (downlink.Length < 10)
            {
                downlink = '0' + downlink;
            }
            while (uplink.Length < 10)
            {
                uplink = '0' + uplink;
            }

            //configure rotater params
            if (Convert.ToInt32(elevation) < 0)
                elevation = "000";
            elevation = Math.Round(Convert.ToDouble(elevation)).ToString();
            switch (azimuth.Length)
            {
                case 1: azimuth = "00" + azimuth;
                    break;
                case 2: azimuth = "0" + azimuth;
                    break;
            }
            switch (elevation.Length)
            {
                case 1: elevation = "00" + elevation;
                    break;
                case 2: elevation = "0" + elevation;
                    break;
            }
            offsetString=Math.Abs(offset).ToString();
            while (offsetString.Length <= 8)
            {
                offsetString = '0' + offsetString;
            }
            if(offset >= 0)
                offsetString = "+"+ offsetString + "000";
            else if(offset < 0)
                offsetString = "-" + offsetString + "000";
        }

        private void sendingCmd()
        {
            string curDopp;
            byte[] freqArray = new byte[11];
            byte[] offsetArray = new byte[12];
            byte[] duplexCmd = new byte[7];
            //header bytes
            freqArray[0] = offsetArray[0] = duplexCmd[0] = 0xFE;//Convert.ToByte("FE",16);
            freqArray[1] = offsetArray[1] = duplexCmd[1] = 0xFE;//Convert.ToByte("FE", 16);
            freqArray[2] = offsetArray[2] = duplexCmd[2] = 0x7C;//Convert.ToByte("7C", 16);
            freqArray[3] = offsetArray[3] = duplexCmd[3] = 0xE0;// Convert.ToByte("E0", 16);
            //command number for setting frequency
            freqArray[4] = 0x05;//Convert.ToByte("05", 16);
            //command for setting duplex offset
            offsetArray[4] = 0x1A;
            offsetArray[5] = 0x05;
            offsetArray[6] = 0x00;
            offsetArray[7] = 0x17;
            //command for setting duplex plus/minus
            duplexCmd[4] = 0x0F;
            //end flag byte
            freqArray[10] = offsetArray[11] = duplexCmd[6] = 0xFD;// Convert.ToByte("FD", 16);
            radioPort.Open();
            while (true)
            {
                if (DateTime.Now > lastUpdate.AddMilliseconds(10))
                {
                    while (!UpdateParameters())
                    {
                        Thread.Sleep(5);
                    }
                    if (oldAz != azimuth || oldEl != elevation)
                    {
                        rotatePort.Open();
                        rotatePort.Write("W" + azimuth + " " + elevation + "\r\n");
                        rotatePort.Close();
                        oldAz = azimuth;
                        oldEl = elevation;
                    }
                        showParams();
                        lastUpdate = DateTime.Now;
                }

                if (listening)
                    curDopp = downlink;
                else
                    curDopp = uplink;

                if (oldDopp != curDopp)
                {
                    freqArray[5] = Convert.ToByte(curDopp[8].ToString() + curDopp[9].ToString(),16);
                    freqArray[6] = Convert.ToByte(curDopp[6].ToString() + curDopp[7].ToString(),16);
                    freqArray[7] = Convert.ToByte(curDopp[4].ToString() + curDopp[5].ToString(),16);
                    freqArray[8] = Convert.ToByte(curDopp[2].ToString() + curDopp[3].ToString(),16);
                    freqArray[9] = Convert.ToByte(curDopp[0].ToString() + curDopp[1].ToString(),16);
                    offsetArray[8] = Convert.ToByte(duplexOffset[4].ToString() + duplexOffset[5].ToString(),16);
                    offsetArray[9] = Convert.ToByte(duplexOffset[2].ToString() + duplexOffset[3].ToString(),16);
                    offsetArray[10] = Convert.ToByte(duplexOffset[0].ToString() + duplexOffset[1].ToString(),16);
                    if (duplexPlus)
                        duplexCmd[5] = Convert.ToByte("1" + "1", 16);
                    else
                        duplexCmd[5] = Convert.ToByte("1" + "2", 16);

                    //radioPort.Open();
                    //Thread.Sleep(100);
                    if (!transmitting)
                    {
                        radioPort.Write(freqArray, 0, freqArray.Length);
                        radioPort.Write(duplexCmd, 0, duplexCmd.Length);
                        radioPort.Write(offsetArray, 0, offsetArray.Length);
                    }
                    //radioPort.Close();
                    oldDopp = curDopp;
                    /*
                     * This code was used to change the frequency on the D700-A
                    radioPort.Open();
                    radioPort.Write("TC 1\r\n");
                    radioPort.Write("TNC 0\r\n");
                    radioPort.Write("FQ " + curDopp + ",0\r\n");
                    radioPort.Write("TNC 2\r\n");
                    radioPort.Write("TC 0\r\n");
                    radioPort.Close();
                    oldDopp = curDopp;
                    */
                }

                
            }
        }

        private void showParams()
        {
            this.setAzText(azimuth);
            this.setElText(elevation);
            this.setDopLisText(downlink);
            this.setDopTalkText(uplink);
            this.setOffsetText(offsetString);
        }

        private void setAzText(string az)
        {
            if (this.azParamLabel.InvokeRequired)
            {
                SetAzCallback d = new SetAzCallback(setAzText);
                this.Invoke(d, new object[] { azimuth });
            }
            else
            {
                this.azParamLabel.Text = azimuth;
            }
        }

        private void setElText(string el)
        {
            if (this.elParamLabel.InvokeRequired)
            {
                SetElCallback d = new SetElCallback(setElText);
                this.Invoke(d, new object[] { elevation });
            }
            else
            {
                this.elParamLabel.Text = elevation;
            }
        }

        private void setDopLisText(string dopLis)
        {
            if (this.dopLisParamLabel.InvokeRequired)
            {
                SetDopLisCallback d = new SetDopLisCallback(setDopLisText);
                this.Invoke(d, new object[] { downlink });
            }
            else
            {
                this.dopLisParamLabel.Text = downlink;
            }
        }

        private void setDopTalkText(string dopTalk)
        {
            if (this.dopTalkParamLabel.InvokeRequired)
            {
                SetDopTalkCallback d = new SetDopTalkCallback(setDopTalkText);
                this.Invoke(d, new object[] { uplink });
            }
            else
            {
                this.dopTalkParamLabel.Text = uplink;
            }
        }

        private void setOffsetText(string el)
        {
            if (this.dopTalkParamLabel.InvokeRequired)
            {
                SetDopTalkCallback d = new SetDopTalkCallback(setOffsetText);
                this.Invoke(d, new object[] { offsetString });
            }
            else
            {
                this.offsetValueLabel.Text = offsetString;
            }
        }

        public void setupPorts(string radioName, string rotaterName, bool debug)
        {
            debugEnabled = debug;
            radioPort = new SerialPort(radioName, 9600, Parity.None, 8, StopBits.One);
            radioPort.DataReceived += new SerialDataReceivedEventHandler(radioPort_DataReceived);
            rotatePort = new SerialPort(rotaterName, 9600, Parity.None, 8, StopBits.One);
        }

        private void radioPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            byte[] input = new byte[radioPort.ReadBufferSize];
            radioPort.Read(input, 0, radioPort.ReadBufferSize);
            if (input[4] == Convert.ToByte("00", 16))
            {
                transmitting = !transmitting;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            sendCmd.Abort();
            if (rotatePort.IsOpen)
                rotatePort.Close();
            if (radioPort.IsOpen)
                radioPort.Close();
        }

        private void downlinkBtn_Click(object sender, EventArgs e)
        {
            setListening(true);
        }

        private void uplinkBtn_Click(object sender, EventArgs e)
        {
            setListening(false);
        }

        private void offsetIncBtn_Click(object sender, EventArgs e)
        {
            offset += 5;
        }

        private void offsetDecBtn_Click(object sender, EventArgs e)
        {
            offset -= 5;
        }

        private void offsetReset_Click(object sender, EventArgs e)
        {
            offset = 0;
        }
    }
}