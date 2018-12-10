using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WindowsFormsApplication1
{
    public partial class robotControlWin : Form
    {
        Size objSize = new Size(50, 30);
        PointF Centre = new PointF(400.0f, 400.0f);
        Robot RobotCop = new Robot(new Size(50,30));
        Robot Maria = new Robot(new Size(100, 80));
        Button btnRun = new Button();
        Button btnReset = new Button();
        Button btnExit = new Button();
        Button btnClear = new Button();
        PictureBox picMap = new PictureBox();
        Timer tmrAnimate = new Timer();
        TextBox txtInstruct = new TextBox();

        public robotControlWin()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        InitGUI();
        RobotCop.Reset(Centre);
        Maria.Reset(new Point ( 500,500));
        ControlBox = false;
        }

        private void tmrAnimate_Tick(Object sender, EventArgs e)
        {
           Maria.Update();
            RobotCop.Update();
            picMap.Refresh();
        }

        private void btnRun_Click(Object sender, EventArgs e)
        {
           Maria.Start("size 5 5" + Environment.NewLine + "repeat 40" + Environment.NewLine + "rt 10" + Environment.NewLine + "fd 10" + Environment.NewLine + "rend" + Environment.NewLine + "lt 20");
            RobotCop.Start(txtInstruct.Text);
          tmrAnimate.Enabled = !tmrAnimate.Enabled;
            if (btnRun.Text == "RUN") { btnRun.Text = "PAUSED"; }
            else if(btnRun.Text == "PAUSE") { btnRun.Text = "RUN"; }
            if (tmrAnimate.Enabled == true) { btnRun.Text = "RUNNING"; }
            else
            {
                btnRun.Text = "PAUSED";
            }

            }

        private void btnReset_Click(Object sender, EventArgs e)
        {
        RobotCop.Reset(Centre);
        Maria.Reset(new Point(500, 500));

            //   RobotCop.Draw(g, Color.White);
            picMap.Refresh();
        tmrAnimate.Enabled = false;
        btnRun.Text = "RUN";
        }

        Graphics g;
        private void Paint_PicMap (Object sender, PaintEventArgs e)
        {
        Graphics g = e.Graphics;
        GraphicsState s = g.Save();
        g.Clear(Color.White);
        RobotCop.Draw(g, Color.Black);
        g.Restore(s);

            int[] temp = new int[10];//0 to 9 //arary
            int temp1 =  10;//variable

       //     string temp22= "dskdajkskdjla";
          //  var tempppp = temp22.ToCharArray();
      //      Console.WriteLine(temp22.ToCharArray());
            char tempdsfsd = '%';
 //  Maria.Draw(g, Color.Red);
        }

        private void InitGUI()
        {
            int mywidth = 200;
            this.WindowState = FormWindowState.Maximized;
            txtInstruct.Size = new Size(mywidth, int.Parse((this.Height / 2.0f).ToString()));
            txtInstruct.Multiline = true;
            txtInstruct.ScrollBars = ScrollBars.Vertical;
            txtInstruct.Font = new Font("Arial", 15);
            List<String> Input = new List<string> {"shape circle","rand 50 100", "repeat 5", "fd rand", "rt rand", "repeat 5", "size rand rand","color rand", "rt 10", "fd rand", "rend", "rend", "end" };
            //{"repeat 5","rt 20","fd 3","rend","lt 90","fd 100", "repeat 10", "lt 20", "fd 2", "rend", "end"};

            for (int i = 0; i < Input.Count - 1; i ++)
            {
             txtInstruct.Text += Input[i];
             txtInstruct.Text += Environment.NewLine;
            }
            //("repeat 100" + Environment.NewLine + "rt 2" + Environment.NewLine + "fd 2" + Environment.NewLine + "repeat end" + Environment.NewLine + "lt 90" + Environment.NewLine + "end");
        //    txtInstruct.Text = "sp 5" + Environment.NewLine + "rt 360" + Environment.NewLine + "fd 100" + Environment.NewLine + "end";
            btnRun.Text = "RUN";
            btnExit.Text = "EXIT";
            btnReset.Text = "RESET";
            btnClear.Text = "CLEAR";
            btnRun.Size = new Size(mywidth, 50);
            btnReset.Size = new Size(mywidth, 50);
            btnExit.Size = new Size(mywidth, 50);
            btnClear.Size = new Size(mywidth, 50);
            // picMap.Image = New Bitmap("map01.jpg");
            picMap.BackColor = Color.White;
            picMap.Location = new Point(20, 20);
            picMap.Size = new Size(800, 800);
            var padding = 40;
            txtInstruct.Left = this.Width - txtInstruct.Width - padding;
            btnReset.Left = this.Width - btnReset.Width - padding;
            btnExit.Left = this.Width - btnExit.Width - padding;
            btnRun.Left = this.Width - btnRun.Width - padding;
            btnClear.Left = this.Width - btnRun.Width - padding;
            txtInstruct.Top = 20;
            btnRun.Top = txtInstruct.Height + 10;
            btnReset.Top = txtInstruct.Height + btnReset.Height + 10;
            btnExit.Top = this.Height - 2 * btnExit.Height;
            btnClear.Top = this.Height - 3 * btnExit.Height;
            this.Controls.Add(btnExit);
            this.Controls.Add(btnRun);
            this.Controls.Add(btnReset);
            this.Controls.Add(btnClear);
            this.Controls.Add(txtInstruct);
            tmrAnimate.Interval = 15;
            tmrAnimate.Enabled = false;
            btnRun.Click += new EventHandler(btnRun_Click);
            btnExit.Click += new EventHandler(delegate(Object s, EventArgs ea) { this.Close(); });
            btnReset.Click += new EventHandler(btnReset_Click);
            btnClear.Click += new EventHandler(delegate (Object s, EventArgs e) { RobotCop.clearLines(); picMap.Refresh(); });
            tmrAnimate.Tick += new EventHandler(tmrAnimate_Tick);
            picMap.Paint += new PaintEventHandler(Paint_PicMap);
            this.Controls.Add(picMap);
        }

    }
}
