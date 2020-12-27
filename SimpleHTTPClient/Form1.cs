using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SimpleHTTPClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            txtDNSlookup.Text = HttpClientHandler.GetIpAddressList(txtURL.Text);
            if (!HttpClientHandler.InvalidURL)
                textBoxResult.Text = HttpClientHandler.UrlConnect(txtURL.Text);
        }
    }
}
