using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            p = new Point(50, 50);
            bitisP = new Point(50, 100);
            //  Pen blackPen = new Pen(Color.Black, 3);

        }

        Point p;
        Point bitisP;
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //Pen blackPen = new Pen(Color.Black, 3);
            //Pen whitePen = new Pen(Color.White, 5);

            //e.Graphics.DrawLine(blackPen, new Point(10, 10), new Point(100, 10));
            //e.Graphics.DrawLine(whitePen, new Point(10, 20), new Point(100, 20));

            //e.Graphics.DrawLine(blackPen, p, bitisP);


        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 frm = new Form2();
          //  frm.WindowState = FormWindowState.Maximized;
            frm.Show();
            return;
            string[] p1Str = textBox1.Text.Split(",");
            int size = Convert.ToInt32(textBox2.Text);
            p = new Point(Convert.ToInt32(p1Str[0]), Convert.ToInt32(p1Str[1]));

            Point p1 = new Point(p.X + size, p.Y);
            Point p2 = new Point(p.X+size , p.Y+size);
            Point p3 = new Point(p.X, p.Y+size);

            Pen blackPen = new Pen(Color.Black, 3);
            CreateGraphics().DrawLine(blackPen, p, p1);
            CreateGraphics().DrawLine(blackPen, p1, p2);
            CreateGraphics().DrawLine(blackPen, p2, p3);
            CreateGraphics().DrawLine(blackPen, p3, p);

            //    Point[] arrPoint = new Point[] { new Point(10, 10), new Point(20, 20), new Point(30, 35), new Point(46, 56) };

            //CreateGraphics().DrawCurve(blackPen, arrPoint);
         //   Rectangle r=new Rectangle()
            CreateGraphics().DrawRectangle(blackPen, 20, 20, 100, 50);
            Brush brush = blackPen.Brush;
            CreateGraphics().FillRectangle(brush, 20, 80, 100, 50);

            CreateGraphics().DrawArc(blackPen, 20, 150, 100, 50, 90, 120);

            CreateGraphics().DrawEllipse(blackPen, 20, 210, 50, 50);
            CreateGraphics().DrawString("asd", new Font(FontFamily.GenericSansSerif, 200),blackPen.Brush,280,20);
            
            //bitisP = new Point(Convert.ToInt32(p2Str[0]), Convert.ToInt32(p2Str[1]));
            //this.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var points = textBoxBaslangic.Text.Split(",");
            Point pBaslangic = new Point(Convert.ToInt32(points[0]), Convert.ToInt32(points[1]));
            int size = Convert.ToInt32(textBoxSize.Text);

            int sizeF = size / 2;

            Point p1 = new Point(pBaslangic.X - sizeF, pBaslangic.Y - sizeF);

            Pen blackPen = new Pen(Color.Black, 3);

            for(int i=0;i<5;i++)
            {
                CreateGraphics().DrawRectangle(blackPen, p1.X-(i*sizeF), p1.Y-(i*sizeF), (i+1)*size, (i + 1) * size);
            }

          

        }

       

    }
}

