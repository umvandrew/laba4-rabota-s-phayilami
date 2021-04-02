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
        List<int> Mas = new List<int>();
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
            for (int i = 0; i < count; i++)  Mas.Add(r.Next(-1000, 1000));
            
            maxy = (Mas.Max());
            if (maxy < (int)Math.Abs(Mas.Min())) maxy = (int)Math.Abs(Mas.Min());

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
                for (var i = 0; i < count; i++)
                {
                    float width = step / 2;
                    br.Color = Color.FromArgb(rn.Next(255), rn.Next(255), rn.Next(255));
                    ss = e.Graphics.MeasureString(Mas[i].ToString(), stringFont);
                    if (Mas[i] < 0)
                    {
                        g.FillRectangle(br,(i + 0.5f)*step,1f,width,-1f*Mas[i]*k);
                        g.DrawString(Mas[i].ToString(), stringFont, new SolidBrush(Color.Black), new PointF((i + 0.75f)*step-ss.Width/2, -(Mas[i]*k)));
                    }
                    if (Mas[i] >= 0)
                    {
                        g.FillRectangle(br,(i + 0.5f)*step,((-1)*Mas[i]*k)-1,width,Mas[i]*k);
                        g.DrawString(Mas[i].ToString(), stringFont, new SolidBrush(Color.Black), new PointF((i + 0.75f)*step-ss.Width/2, -(Mas[i]*k+15)));
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
            while ((line = file.ReadLine()) != null)
            {
                string[] numb = line.Split(':');
                //for(var i =0;i < 4; i++)  
                //Mas.Add(line);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Create("Data2.txt");
        }
    }
}