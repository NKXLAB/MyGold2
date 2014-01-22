using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZedGraph;
namespace WindowsFormsApplicationBox
{
    public partial class FormBox : Form
    {
        /// <summary>
        /// 行情地址
        /// </summary>
        string urls = "http://www.icbc.com.cn/ICBCDynamicSite/Charts/GoldTendencyPicture.aspx";

        /// <summary>
        /// 知识地址
        /// </summary>
        string knowdata = "http://www.homemode.me/ww2.html";// "http://tieba.baidu.com/f?kw=%D6%BD%BB%C6%BD%F0&fr=ala0";

        /// <summary>
        /// 更新地址
        /// </summary>
        string strUpdate = "http://www.homemode.me/ww.htm";

        /// <summary>
        /// 系统
        /// </summary>
        ArrayList BVC = new ArrayList();

        public FormBox()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 时间
        /// </summary>
        ArrayList X_VAL_Time = new ArrayList();
        /// <summary>
        /// X 0-99
        /// </summary>
        ArrayList X_VAL = new ArrayList();
        /// <summary>
        /// Y
        /// </summary>
        ArrayList Y_VAL = new ArrayList();

        /// <summary>
        /// 是否是第一次获取数据
        /// </summary>
        bool IsFrist = true;

        private void Form1_Load(object sender, EventArgs e)
        {
            timer3.Interval = 100;
            timer3.Enabled = true;


            DateTime VVVV = DateTime.Now;
            string CC = VVVV.Year + "_" + VVVV.Month.ToString("D2") + "_" + VVVV.Day.ToString("D2") + "_" + VVVV.Hour.ToString("D2") + "_" + VVVV.Minute.ToString("D2") + "_" + VVVV.Second.ToString("D2") + " "  ;

            CC += "Z";
        }
        
          private void timer3_Tick(object sender, EventArgs e)
        {
            timer3.Enabled = false;


                   for (int i = 0; i < 100; i++)
            {
                X_VAL.Add((double)i);
                Y_VAL.Add((double)0);
                X_VAL_Time.Add(DateTime.Now  );
            }

                  

                   timer2.Interval = 1000;
                   timer2.Enabled = true;

            timer1.Interval = 60000;
            timer1.Enabled = true;
            
            timer1_Tick(null,null );

            webBrowser2.Navigate(knowdata);
            webBrowser1.Navigate(urls);

            webBrowser3.Navigate(strUpdate);
        }

        private void eXITToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void 登陆ToolStripMenuItem_Click(object sender, EventArgs e)
        {
             webBrowser1.Navigate(urls);

             tabControl2.SelectedTab = tabPage4;

            //webBrowser2.Navigate(url2);
       //  FormIE xx = new FormIE();
        //    xx.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            timer1.Enabled = false;

            bool X = false;

            //保存文件
            if (BVC.Count >= 20)
            {
                string AAAA = "";
                foreach (string a in BVC)
                {

                    AAAA += (a + "\r\n");

                }

                DateTime VVVV = DateTime.Now;
                string PathxD = System.IO.Path.Combine(Application.StartupPath, "DATA");

                if (System.IO.Directory.Exists(PathxD) == false)
                    System.IO.Directory.CreateDirectory(PathxD);

                string Pathx = System.IO.Path.Combine(PathxD, VVVV.Year + "_" + VVVV.Month.ToString("D2") + "_" + VVVV.Day.ToString("D2") + "_" + VVVV.Hour.ToString("D2") + "_" + VVVV.Minute.ToString("D2") + ".txt");

                System.IO.File.WriteAllText(Pathx, AAAA);

                BVC.Clear();

                webBrowser3.Navigate(strUpdate);
            }



            for (int i = 0; i < 8; i++)
            {
                  X = GetData();
                  if (X == true)
                  {


                      textBox2.Text = "成功：" + DateTime.Now.ToString() + "\t" + valx_1 + "\t" + valx_2 + "\t" + valx_3 + "\r\n" + textBox2.Text;

                      break;

                  }

                  textBox2.Text ="重试："+DateTime.Now.ToString()+ "\r\n"+ textBox2.Text;
                  System.Threading.Thread.Sleep(3000);
            
            }



          // 
           //     MessageBox.Show("数据连续获取错误，请检查！");
            if (X == false)
            {
                textBox2.Text = "数据连续获取错误：" + DateTime.Now.ToString() + "\r\n" + textBox2.Text;

            }
            else
            {
                Y_VAL.RemoveAt(0);
                X_VAL_Time.RemoveAt(0);
                Y_VAL.Add(ZZZ);

                X_VAL_Time.Add(DateTime.Now);

                BaseLine(X_VAL, Y_VAL);
            }

            webBrowser1.Navigate(urls);

            timer1.Enabled = true;



            try
            {
                webBrowser3.Document.Encoding = "gb2312";
            }
            catch
            { }
        }

        /// <summary>
        /// 中间价
        /// </summary>
        public double ZZZ = 0;


        public double valx_1 = 0;
        public double valx_2 = 0;
        public double valx_3 = 0;

        public bool  GetData()
        {

         
            try
            {
                System.Net.WebClient cc = new System.Net.WebClient();



                //<td style="width: 18%; height: 23px" align="middle">

                cc.Encoding = System.Text.Encoding.UTF8 ;// .GetEncoding("GB2312");
                string aHTML = cc.DownloadString(urls);

                string aaa1 = "<td style=\"width: 18%; height: 23px\" align=\"middle\">";
                int a1 = aHTML.IndexOf(aaa1, 0);

                string aaa2 = "<td style=\"width: 7%; height: 23px\" align=\"middle\">";
                if (a1 > 0)
                {
                    int a2 = aHTML.IndexOf(aaa2, a1);


                    if (a2 > 0)
                    {
                        string HTMLX = aHTML.Substring(a1 + aaa1.Length, a2 - a1 - aaa1.Length);

                        string A = HTMLX.Replace(" ", "");
                        char[] x = { '\r', '\n' };
                        string[] AAA = A.Split(x);

                        string data1 = AAA[14];
                        string data2 = AAA[20];
                        string data3 = AAA[26];


                  

                      //  label4.Text = DateTime.Now.ToString();

                        double nA1 = double.Parse(data1);
                        double nA2 = double.Parse(data2);
                        double nA3 = double.Parse(data3);
                        valx_1 = nA1;
                        valx_2 = nA2;
                        valx_3 = nA3;
                        int YZHEN1 =(int) (nA1 * 100);
                        int YZHEN2 = (int)(nA2 * 100);
                        int YZHEN3 = (int)(nA3 * 100);

                        if (YZHEN1 + 40 == YZHEN3 && YZHEN2 - 40 == YZHEN3 )
                        {
                            DateTime VVVV = DateTime.Now;
                            string CC = VVVV.Year + "_" + VVVV.Month.ToString("D2") + "_" + VVVV.Day.ToString("D2") + "_" + VVVV.Hour.ToString("D2") + "_" + VVVV.Minute.ToString("D2") + "_" + VVVV.Second.ToString("D2") + " " + data1 + " " + data2 + " " + data3;
                          //  listBox1.Items.Add(CC);
                            BVC.Add(CC);
                            ZZZ = nA3;
                            label5.Text ="数据刷新："+ DateTime.Now.ToString();

                            if (IsFrist == true)
                            {
                                Y_VAL.Clear();

                                for (int i = 0; i < 100; i++)
                                {
                                   
                                    Y_VAL.Add( ZZZ );

                                }

                                IsFrist = false;
                            }

                            return true;
                        }
                        else
                        {
                            int iu = 0;
                        }

                    }

                }
                else
                {
                    int xxsad = 0;
                }

            }
            catch
            {
            }
            return false ;

        }

 FormIE2 xx = new FormIE2();

        private void mToolStripMenuItem_Click(object sender, EventArgs e)
        {
           // GetData();

           
            xx.Show();

        }

        private void button1_Click(object sender, EventArgs e)
        {


           
        }

        private void button2_Click(object sender, EventArgs e)
        {
           // listBox1.Items.Clear();
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string PathxD = System.IO.Path.Combine(Application.StartupPath, "DATA");

            if (System.IO.Directory.Exists(PathxD) == false)
                System.IO.Directory.CreateDirectory(PathxD);

            string[] aaa = System.IO.Directory.GetFiles(PathxD);
            listBox2.Items.Clear();
            char[] zxx = {'\\' };
            foreach (string a in aaa)
            {

                string[] AC = a.Split(zxx);


                listBox2.Items.Add(AC[AC.Length -1]);

            }

        }


        /// <summary>
        ///  VAL
        /// </summary>
        ArrayList oldY_VAL = new ArrayList();

        /// <summary>
        /// TIME
        /// </summary>
         ArrayList oldX_VAL_Time = new ArrayList();

         
        private void listBox2_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            ArrayList DATAS = new ArrayList();


            for (int i = 0; i < listBox2.SelectedItems.Count; i++)
            {
                string one = listBox2.SelectedItems[i].ToString();

                try
                {

                    string PathxD = System.IO.Path.Combine(Application.StartupPath, "DATA");

                    if (System.IO.Directory.Exists(PathxD) == false)
                        System.IO.Directory.CreateDirectory(PathxD);


                    string FileX = System.IO.Path.Combine(PathxD, one);

                    string[] YYY = System.IO.File.ReadAllLines(FileX);

                    foreach (string v in YYY)
                    {
                        if (v.Length > 10)
                        {
                            DATAS.Add(v);
                        }

                    }

                   // textBox1.Text = YYY;
                }
                catch
                {

                }


            }

            if (listBox2.SelectedItems.Count == 1)
            {
                label8.Text = "开始时间：" + GetDateT(listBox2.SelectedItems[0].ToString());

                label7.Text = "结束时间：" + GetDateT(listBox2.SelectedItems[0].ToString());
            }


            if (listBox2.SelectedItems.Count >= 2)
            {
                label8.Text = "开始时间：" + GetDateT(listBox2.SelectedItems[0].ToString());

                label7.Text = "结束时间：" + GetDateT(listBox2.SelectedItems[listBox2.SelectedItems.Count - 1].ToString());
            }

            ArrayList X_1 = new ArrayList();
            ArrayList Y_1 = new ArrayList();
             
            StringBuilder NN = new StringBuilder();

            foreach (string a in DATAS)
            {
                NN.AppendLine(a.Replace(" ","\t"));

                string[] ax = a.Split(' ');

                X_1.Add((double)(X_1.Count));
                Y_1.Add(double.Parse(ax[ax.Length -1])  );


                string[] axx = ax[0].Split('_');

                DateTime nn = new DateTime(Int32.Parse(axx[0]),Int32.Parse(axx[1]),Int32.Parse(axx[2]),Int32.Parse(axx[3]),Int32.Parse(axx[4]),Int32.Parse(axx[5])  );
                oldX_VAL_Time.Add(nn);

                oldY_VAL.Add(double.Parse(ax[ax.Length - 1]));

            }

            textBox1.Text = NN.ToString();


            BaseLine2(X_1, Y_1);





            tabControl2.SelectedTab = tabPage5;
        }

        public string GetDateT(string dd)
        {
            Char[] x = {'_','.' };
            string[] ss = dd.Split(x);

            string rt = ss[0] + "年" + ss[1] + "月" + ss[2] + "日" + ss[3] + "时" + ss[4] + "分" ;

            return rt;
        }

        /// <summary>
        /// 划线
        /// </summary>
        /// <param name="ls"></param>
        public void BaseLine(ArrayList lsID, ArrayList lsV)
        {

            GraphPane myPane = zedGraphControl1.GraphPane;
            myPane._curveList.Clear();
            // Set the titles and axis labels
            myPane.Title.Text = "实时曲线图";
            myPane.XAxis.Title.Text = "时间(s)";
            myPane.YAxis.Title.Text = "中间价";

            // Build a PointPairList with points based on Sine wave
            PointPairList list = new PointPairList();
            for (int i = 0; i < lsID.Count; i++)
            {
                double x = (double)lsID[i];
                double y = (double)lsV[i];
                list.Add(x, y);
            }

            // Hide the legend
            myPane.Legend.IsVisible = false;
            // Hide the legend
          //  myPane.Legend.IsVisible = true;

            myPane.IsAlignGrids = true;

            zedGraphControl1.IsShowPointValues = true;

            // Add a curve
            LineItem curve = myPane.AddCurve("label", list, Color.Blue, SymbolType.Circle);


            curve.Line.Width = 1.5F;
            curve.Symbol.Fill = new Fill(Color.DarkGray);
            curve.Symbol.Size = 5;



            // Make the XAxis start with the first label at 50
            myPane.XAxis.Scale.BaseTic = 10;

            // Fill the axis background with a gradient
            //  myPane.Chart.Fill = new Fill(Color.White, Color.SteelBlue, 45.0F);

            //     myPane.Chart.Fill = new Fill(Color.DarkGray);



            zedGraphControl1.AxisChange();

            zedGraphControl1.Refresh();
        }



        /// <summary>
        /// 划线
        /// </summary>
        /// <param name="ls"></param>
        public void BaseLine2(ArrayList lsID, ArrayList lsV)
        {

            GraphPane myPane = zedGraphControl2.GraphPane;
            myPane._curveList.Clear();
            // Set the titles and axis labels
            myPane.Title.Text = "历史曲线图";
            myPane.XAxis.Title.Text = "时间(s)";
            myPane.YAxis.Title.Text = "中间价";

            // Build a PointPairList with points based on Sine wave
            PointPairList list = new PointPairList();
            for (int i = 0; i < lsID.Count; i++)
            {
                double x = (double)lsID[i];
                double y = (double)lsV[i];
                list.Add(x, y);
            }
            zedGraphControl2.IsShowPointValues = true;
            // Hide the legend
            myPane.Legend.IsVisible = false;

            myPane.IsAlignGrids = true;
            // Add a curve
            LineItem curve = myPane.AddCurve("label", list, Color.Blue, SymbolType.Circle);


            curve.Line.Width = 1.5F;
            curve.Symbol.Fill = new Fill(Color.DarkGray);
            curve.Symbol.Size = 5;



            // Make the XAxis start with the first label at 50
            myPane.XAxis.Scale.BaseTic = 10;

            // Fill the axis background with a gradient
            //  myPane.Chart.Fill = new Fill(Color.White, Color.SteelBlue, 45.0F);

            //     myPane.Chart.Fill = new Fill(Color.DarkGray);



            zedGraphControl2.AxisChange();

            zedGraphControl2.Refresh();
        }


        private void timer2_Tick(object sender, EventArgs e)
        {
            label6.Text = "当前时刻：" +DateTime.Now.ToString();
        }

        private void 交易心得ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            webBrowser2.Navigate(knowdata);

            tabControl2.SelectedTab = tabPage2;

        }

        private void 软件更新ToolStripMenuItem_Click(object sender, EventArgs e)
        {




        }

        private void 百度贴吧ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            /// <summary>
            /// 知识地址
            /// </summary>
            string knowdata2 = "http://tieba.baidu.com/f?kw=%D6%BD%BB%C6%BD%F0&fr=ala0";

            System.Diagnostics.Process.Start(knowdata2);
        }

        private void 工商银行ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            /// <summary>
            /// 知识地址
            /// </summary>
            string knowdata2 = "https://mybank.icbc.com.cn/icbc/perbank/index.jsp";

            System.Diagnostics.Process.Start(knowdata2);
        }

        private void 工行贵宾ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /// <summary>
            /// 知识地址
            /// </summary>
            string knowdata2 = "https://vip.icbc.com.cn/icbc/perbank/index.jsp";

            System.Diagnostics.Process.Start(knowdata2);
        }

        private void zedGraphControl2_Load(object sender, EventArgs e)
        {

        }

        private string zedGraphControl2_PointValueEvent(ZedGraphControl sender, GraphPane pane, CurveItem curve, int iPt)
        {
            try
            {
                label4.Text = oldY_VAL[iPt].ToString();
                label3.Text = ((DateTime)oldX_VAL_Time[iPt]).ToString();

                label3.Visible = true;
                label4.Visible = true;
            }
            catch
            { }
            return default(string);
        }

        private string zedGraphControl1_PointEditEvent(ZedGraphControl sender, GraphPane pane, CurveItem curve, int iPt)
        {
            return default(string);
        }

        private string zedGraphControl1_PointValueEvent(ZedGraphControl sender, GraphPane pane, CurveItem curve, int iPt)
        {
            
            label2.Text = Y_VAL[iPt].ToString();
            label1.Text = ((DateTime)X_VAL_Time[iPt]).ToString();

            label1.Visible = true;
            label2.Visible = true;

            return default(string);
          
        }

        private string zedGraphControl1_CursorValueEvent(ZedGraphControl sender, GraphPane pane, Point mousePt)
        {
            return default(string);
        }

        private void zedGraphControl1_Load(object sender, EventArgs e)
        {

        }

        private string zedGraphControl2_PointEditEvent(ZedGraphControl sender, GraphPane pane, CurveItem curve, int iPt)
        {
            return default(string);
        }

        

        private void 关闭ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定退出吗？", "开放源码纸黄金分析软件", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                xx.CloseX();
                IsEXIT = true;
                Application.Exit();
            }
        }

        private void 关闭ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定退出吗？", "开放源码纸黄金分析软件", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                xx.CloseX();
                IsEXIT = true;
                Application.Exit();
            }
        }

        private void 最小化ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void 显示主界面ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();

        }

        /// <summary>
        /// 一定要退出么
        /// </summary>
        bool IsEXIT = false;

        private void FormBox_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(!IsEXIT)
               e.Cancel = true;
            this.Hide();
        }

        private void 趋势图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
             
            xx.WindowState = FormWindowState.Maximized;
            xx.Show();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.Show();
        }

        private void FormBox_Resize(object sender, EventArgs e)
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

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void 百度纸黄金吧ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            百度贴吧ToolStripMenuItem_Click(null, null);
        }

        private void 工行个人用户ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            工商银行ToolStripMenuItem_Click(null, null);
        }

        private void 工行贵宾用户ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            工行贵宾ToolStripMenuItem_Click(null, null);
        }

        private void 项目源代码网站ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string knowdata2 = "https://github.com/zhangdong1981/MyGold2";

            System.Diagnostics.Process.Start(knowdata2);

        }

      



    }
}
