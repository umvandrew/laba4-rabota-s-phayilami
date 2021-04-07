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
        private List<Graph> Mass = new List<Graph>();
        private int maxy, count;

        private class Graph
        {
            internal int Higth { get; set; }
            internal Color Color { get; }

            public Graph(int higth, int R, int G, int B)
            {
                Higth = higth;
                Color = Color.FromArgb(R, G, B);
            }
        }
       
        public Form1()
        {
            InitializeComponent();
            Create("Data1.txt");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            count = r.Next(5, 20);
            Mass.Clear();
            maxy = 0;
            
            for (int i = 0; i < count; i++)
            {
                Graph temp = new Graph(r.Next(-1000, 1000),r.Next(255),r.Next(255),r.Next(255));
                Mass.Add(temp);
                if (Math.Abs(temp.Higth) > maxy) maxy = Math.Abs(temp.Higth);
            }

            Invalidate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Read("Data1.txt");
            Invalidate();
        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            Write("Data2.txt");
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
            
            float step = 1000f / (float)Mass.Count;
            float k = Math.Abs(250f / (float)maxy);
            
            Random rn = new Random();
            SolidBrush br = new SolidBrush(Color.Black);
            Font stringFont = new Font("Times New Roman", 10, FontStyle.Regular);
            SizeF ss = new SizeF();

            for (var i = 0; i < Mass.Count; i++)
            {
                float width = step / 2;
                br.Color = Mass[i].Color;
                ss = e.Graphics.MeasureString((Mass[i].Higth).ToString(), stringFont);
                if (Mass[i].Higth < 0)
                {
                    g.FillRectangle(br,(i + 0.5f)*step,1f,width,-1f*Mass[i].Higth*k);
                    g.DrawString((Mass[i].Higth).ToString(), stringFont, new SolidBrush(Color.Black), new PointF((i + 0.75f)*step-ss.Width/2, -(Mass[i].Higth*k)));
                }
                if (Mass[i].Higth >= 0)
                {
                    g.FillRectangle(br,(i + 0.5f)*step,((-1)*Mass[i].Higth*k)-1,width,Mass[i].Higth*k);
                    g.DrawString((Mass[i].Higth).ToString(), stringFont, new SolidBrush(Color.Black), new PointF((i + 0.75f)*step-ss.Width/2, -(Mass[i].Higth*k+15)));
                }
            }

            
        }

        void Create(string path) //создание рандомных данных и запись их в файл 
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
                    if (k != 2)sb.Append(":") ;
                }
                cr.Write(sb+ "\n");
                sb.Clear();
            }

            cr.Close();
        }

        void Write(string path) //запись данных на экране в файл "Data2.txt"
        {
            StreamWriter cr = new StreamWriter(path);
            
            var sb = new StringBuilder();
            for (int i = 0; i < Mass.Count; i++)
            {
                sb.Append((Mass[i].Higth).ToString());
                sb.Append(":");
                sb.Append(Mass[i].Color.R.ToString());
                sb.Append(":") ;
                sb.Append(Mass[i].Color.G.ToString());
                sb.Append(":") ;
                sb.Append(Mass[i].Color.B.ToString());
                cr.Write(sb+ "\n");
                sb.Clear();
            }

            cr.Close();
        }
        
        void Read(string path) //чтение данных из файла 
        {
            StreamReader file = new StreamReader(path, Encoding.Default);
            string line;
            Mass.Clear();
            maxy = 0;
            while ((line = file.ReadLine()) != null)
            {
                string[] numb = line.Split(':');
                Graph temp = new Graph(int.Parse(numb[0]),int.Parse(numb[1]),int.Parse(numb[2]),int.Parse(numb[3]));
                Mass.Add(temp);
                if (Math.Abs(temp.Higth) > maxy) maxy = Math.Abs(temp.Higth);
            }
        }
        
    }
}
