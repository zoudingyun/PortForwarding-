using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PortControllerClient.Forms
{
    public partial class updateMessage : Form
    {
        public updateMessage()
        {
            InitializeComponent();

            ///设置
           webBrowser1.Navigate("http://10.80.48.144:8008/updateMessage/");


        }
       
        private void updateMessage_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
