using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
           
        }



        Graphics grp;
        Pen blackPen;
        int startX = 20;
        int startY = 0;

        Rectangle odul;
        int score = 0;
        Thread tr1;
        bool stopped = false;

        Rectangle ball=new Rectangle(20,20,20,20);
        int Xv = 10;
        int Yv = 10;

        private void Form2_Load(object sender, EventArgs e)
        {
            blackPen = new Pen(Color.Blue, 5);
            grp = CreateGraphics();

            grp.FillRectangle(blackPen.Brush, startX, startY, 100, 50);

            odul = new Rectangle(200, 20, 20, 20);

            grp.FillEllipse(blackPen.Brush, odul);

            tr1 = new Thread(redraw);
            tr1.Start();
        }

        delegate void SetTextCallback(string text);

        private void SetText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (label1.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                Invoke(d, new object[] { text });
            }
            else
            {
                label1.Text = text;
            }
        }
        private void redraw(object obj)
        {
            while (!stopped)
            {
                grp.Clear(BackColor);

                Pen p1 = new Pen(Color.Red, 3);
                grp.FillEllipse(p1.Brush,ball);
                
                moveBall();



                odul.Y = odul.Y + 5;
                startY = Size.Height - 150;

                grp.FillRectangle(blackPen.Brush, startX, startY, 100, 50);
                grp.FillEllipse(blackPen.Brush, odul);

                if (startY == odul.Y && odul.X > startX && odul.X < startX + 100)
                {
                    //  MessageBox.Show("gotcha");
                    score++;
                    int startRand = new Random().Next(10, this.Size.Width - 10);
                    odul = new Rectangle(startRand, 20, 20, 20);
                    SetText(score.ToString());
                }
                else if(odul.Y > startY+10)
                {
                    int startRand = new Random().Next(10, this.Size.Width - 10);
                    odul = new Rectangle(startRand, 20, 20, 20);
                }
                Thread.Sleep(50);
            }
        }

        private void moveBall()
        {

            int ballx = ball.X + Xv;
            int bally = ball.Y + Yv;
            ball = new Rectangle(ballx, bally, 20, 20);
            if(ball.X<10)
            {
                Xv *= -1;
            }
            if (ball.Y < 10)
                Yv *= -1;
            if (ball.X+20 > Size.Width)
                Xv *= -1;
            if (ball.Y+20 > Size.Height)
                Yv *= -1;

            if (ball.X >= startX+10 && ball.X+10 <= startX+100)
            {
                if (ball.Y+20 >= startY)
                {
                    Yv *= -1;
                }

            }

        }

        private void Form2_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void Form2_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Down:
                case Keys.Right:
                    SagaKaydir();
                    break;
                case Keys.Up:
                case Keys.Left:
                    SolaKaydir();
                    break;
            }
        }

        private void SagaKaydir()
        {
            
            startX += 5;
            if (startX+100 > Size.Width-5)
                startX = 5;
           
        }
        private void SolaKaydir()
        {
            startX -= 5;
            if (startX < 5)
            {
                startX = Size.Width - 5;
            }
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            stopped = true;
                
        }
    }
}
