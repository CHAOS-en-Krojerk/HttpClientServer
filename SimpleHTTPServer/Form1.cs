using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web;
using System.Text;
using System.Windows.Forms;

namespace SimpleHTTPServer
{
    public partial class Form1 : Form
    {
        HttpListener listener = new HttpListener();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string getHostName = Dns.GetHostName();
            IPHostEntry hostEntry = Dns.GetHostByName(getHostName);
            foreach(IPAddress ip in hostEntry.AddressList)
            {
                ipAddressesCombo.Items.Add(ip.ToString());
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using(var folderDialog = new FolderBrowserDialog())
            {
                DialogResult dialogResult = folderDialog.ShowDialog();
                if(dialogResult==DialogResult.OK && !string.IsNullOrWhiteSpace(folderDialog.SelectedPath))
                {
                    txtBrowse.Text = folderDialog.SelectedPath;
                }
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            foreach(var s in ipAddressesCombo.Items)
            {
                listener.Prefixes.Add(s.ToString()+txtPort.Text);
            }
            listener.Start();
            textBoxConsole.Text = "Listening....";
            btnStop.Enabled = true;
            btnRun.Enabled = false;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if(!btnRun.Enabled)
            {
                listener.Stop();
            }
        }
    }
}
