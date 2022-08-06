using System;
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

        int boxV = 10;

        Rectangle odul;
        int score = 0;
        Thread tr1;
        bool stopped = false;

        Rectangle ball = new Rectangle(20, 20, 20, 20);
        int Xv = 10;
        int Yv = 10;
        DestinationBox dest1;

        int boxMoving = 0;//0 sabit, 1 right, 2 left;

        private void setBoxMoving(int val)
        {
            boxMoving = val;
            Thread.Sleep(500);
            boxMoving = 0;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            ball.X = panel1.Location.X + 10;
            ball.Y = panel1.Location.Y + 10;

            startX = 20;
            startY = panel1.Height - 55;

            blackPen = new Pen(Color.Blue, 5);
            grp = panel1.CreateGraphics();

            dest1 = new DestinationBox() { Rect = new Rectangle(10, 10, 410, 50) };

            dest1.Colors.Add(Color.Red);
            dest1.Colors.Add(Color.Yellow);
            dest1.Colors.Add(Color.Blue);
            dest1.Colors.Add(Color.AliceBlue);


            tr1 = new Thread(redraw);
            tr1.Start();
        }

        delegate void SetTextCallback(Label lbl, string text);

        private void SetText(Label lbl, string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (lbl.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                Invoke(d, new object[] { lbl, text });
            }
            else
            {
                lbl.Text = text;
            }
        }
        private void redraw(object obj)
        {
            while (!stopped)
            {
                grp.Clear(BackColor);

                Pen p1 = new Pen(Color.Red, 3);
                grp.FillEllipse(p1.Brush, ball);

                Pen p2 = new Pen(dest1.C);
                grp.FillRectangle(p2.Brush, dest1.Rect);

                moveBall();

                // odul.Y = odul.Y + 5;
                //   startY = Size.Height - 150;

                grp.FillRectangle(blackPen.Brush, startX, startY, 100, 50);

                Thread.Sleep(100);
            }
        }

        private void moveBall()
        {

            int ballx = ball.X + Xv;
            int bally = ball.Y + Yv;
            ball = new Rectangle(ballx, bally, 20, 20);
            if (ball.X <= 0)
            {
                Xv *= -1;
            }
            if (ball.Y <= 0)
            {

                Yv *= -1;
            }
            if (ball.X + 10 >= panel1.Width)
                Xv *= -1;
            if (ball.Y + 10 >= panel1.Height)
            {
                // MessageBox.Show(ball.Y.ToString());
                Yv *= -1;
            }
            if (ball.X >= (startX + 10) && ball.X + 10 <= (startX + 100) && Yv>0)
            {
                if (ball.Y + 20 >= startY)
                {

                    if(boxMoving==1)
                    {
                        if (Xv > 0)
                        {
                            Xv += 30;
                        }
                        else
                        {
                            Xv -= 30;
                        }
                    }
                    else if(boxMoving==2)
                    {
                        if (Xv > 0)
                        {
                            Xv -= 30;
                        }
                        else
                        {
                            Xv += 30;
                        }
                    }

                    Yv *= -1;

                }

            }

            if (Yv < 0)
            {
                CheckCrush();
            }

        }

        private void CheckCrush()
        {
            SetText(label3, ball.X.ToString());
            SetText(label4, dest1.Rect.X.ToString());
            SetText(label6, (dest1.Rect.X + dest1.Rect.Width).ToString());

            if (ball.X > dest1.Rect.X && ball.X <( dest1.Rect.X + dest1.Rect.Width))
            {

                SetText(label1, ball.Y.ToString());
                SetText(label2, (dest1.Rect.Y + dest1.Rect.Height).ToString());
                SetText(label5, Math.Abs(ball.Y - (dest1.Rect.Y + dest1.Rect.Height)).ToString());
                if (Math.Abs(ball.Y - (dest1.Rect.Y + dest1.Rect.Height))<=5)
                {


                    if (dest1.Colors.Count() <= dest1.ColorIndex + 1)
                    {
                        dest1.Rect = new Rectangle(0, 0, 0, 0);
                        dest1.ColorIndex--;
                    }

                    dest1.ColorIndex++;

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

            startX += 10;
            if (startX + 50 > panel1.Width)
                startX = 5;

            Thread thr = new Thread(() => setBoxMoving(1));
            thr.Start();

        }
        private void SolaKaydir()
        {
            startX -= 5;
            if (startX < 0)
            {
                startX = panel1.Width - 5;
            }
            Thread thr = new Thread(() => setBoxMoving(2));
            thr.Start();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            stopped = true;

        }
    }
}
