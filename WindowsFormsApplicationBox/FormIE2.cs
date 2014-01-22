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
    public partial class FormIE2 : Form
    {
          // string urls = "http://www.icbc.com.cn/ICBCDynamicSite/Charts/GoldTendencyPicture.aspx";

     string urls = "http://www.icbc.com.cn/ICBCDynamicSite/Charts/AccGold.aspx?dataType=0&dataId=901&picType=3";



        public FormIE2()
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

        public void CloseX()
        {
            IsEXIT = true;
        }

        /// <summary>
        /// 一定要退出么
        /// </summary>
        bool IsEXIT = false;

        private void FormIE2_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (!IsEXIT)
                e.Cancel = true;
          
            this.Hide();
        }

        private void FormIE2_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Maximized)
            {
                //最大化时所需的操作 
                //  MessageBox.Show("max");
            }
            else if (WindowState == FormWindowState.Minimized)
            {
                

                this.Hide();
                //最小化时所需的操作
                //  MessageBox.Show("min");
            } 
        }
    }
}
