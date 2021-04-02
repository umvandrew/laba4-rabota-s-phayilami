using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace laba4_rabota_s_phailami
{
    public partial class Form1 : Form
    {
        List<int[]> Mas = new List<int[]>();
        int maxy, count;
        private bool working = false;

        
       
        public Form1()
        {
            InitializeComponent();
            Create("Data1.txt");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            working = true;
            Random r = new Random();
            count = r.Next(5, 20);
            Mas.Clear();
            int [] temp = new int[4];
            maxy = -1000;
            for (int i = 0; i < count; i++)
            {
                temp[0] = r.Next(-1000, 1000);
                for (var j = 1; j <= 3; j++) temp[j] = r.Next(255);
                if (temp[0] > maxy) maxy = temp[0];
            }
            Mas.Add(temp);

            Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = this.CreateGraphics();
            //рисуем поле 
            int xc = 100;
            int yc = (this.Height) / 2;
            g.TranslateTransform(xc, yc);
            g.DrawLine(new Pen(Color.Black, 2f), 0, -250, 0, 250);
            g.DrawLine(new Pen(Color.Black, 2f), 0, 0, 1000, 0);
            
            float step = 1000f / (float)count;
            float k = Math.Abs(250f / (float)maxy);
            
            Random rn = new Random();
            SolidBrush br = new SolidBrush(Color.Black);
            Font stringFont = new Font("Times New Roman", 10, FontStyle.Regular);
            SizeF ss = new SizeF();

            if (working)
            {
                for (var i = 0; i < Mas.Count; i++)
                {

                    int[] temp = new int[4]; 
                    temp = Mas[i];
                    float width = step / 2;
                    br.Color = Color.FromArgb(temp[1],temp[2],temp[3]);
                    ss = e.Graphics.MeasureString(temp[0].ToString(), stringFont);

                    if (temp[0] < 0)
                    {
                        g.FillRectangle(br,(i + 0.5f)*step,1f,width,-1f*temp[0]*k);
                        g.DrawString(temp[0].ToString(), stringFont, new SolidBrush(Color.Black), new PointF((i + 0.75f)*step-ss.Width/2, -(temp[0]*k)));
                    }
                    if (temp[0] >= 0)
                    {
                        g.FillRectangle(br,(i + 0.5f)*step,((-1)*temp[0]*k)-1,width,temp[0]*k);
                        g.DrawString(temp[0].ToString(), stringFont, new SolidBrush(Color.Black), new PointF((i + 0.75f)*step-ss.Width/2, -(temp[0]*k+15)));
                    }
                }

                working = false;
            }
            
        }

        void Create(string path)
        {
            Random r = new Random();
            count = r.Next(5, 20);
            StreamWriter cr = new StreamWriter(path);
            
            var sb = new StringBuilder();
            for (int i = 0; i < count; i++)
            {
                sb.Append((r.Next(-1000, 1000)).ToString());
                sb.Append(":");
                for (var k = 0; k < 3; k++)
                {
                    sb.Append((r.Next(255)).ToString());
                    sb.Append(":");
                }
                cr.Write(sb+ "\n");
                sb.Clear();
            }

            cr.Close();
        }

        void Read(string path)
        {
            StreamReader file = new StreamReader(path, Encoding.Default);
            string line;
            Mas.Clear();
            maxy = -1000;
            while ((line = file.ReadLine()) != null)
            {
                string[] numb = line.Split(':');
                int[] temp = new int[4];
                for (int i = 0; i < 4; i++) temp[i] = Convert.ToInt32(numb[i]);
                if (temp[0] > maxy) maxy = temp[0];
                Mas.Add(temp);
            }

            Invalidate();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Create("Data2.txt");
        }
    }
}