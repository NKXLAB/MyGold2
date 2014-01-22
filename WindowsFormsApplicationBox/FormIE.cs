using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplicationBox
{
    public partial class FormIE : Form
    {
        string urls = "http://www.icbc.com.cn/ICBCDynamicSite/Charts/GoldTendencyPicture.aspx";

       // string url2 = "http://www.icbc.com.cn/ICBCDynamicSite/Charts/AccGold.aspx?dataType=0&dataId=901&picType=3";



        public FormIE()
        {
            InitializeComponent();
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 刷新ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate(urls);
           // webBrowser2.Navigate(url2);
        }

        private void FormIE_Load(object sender, EventArgs e)
        {
            webBrowser1.Navigate(urls);
        }
    }
}
