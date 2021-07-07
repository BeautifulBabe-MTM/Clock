using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    class Clock : Control
    {
        private Color color;
        public Timer timer;
        private readonly int day;
        private readonly int minute;
        private readonly double Lenght;
        public Clock()
        {
            day = 12;
            Lenght = 140;
            minute = 60;

            timer = new Timer();
            timer.Interval = 1000;
            timer.Enabled = true;
            timer.Tick += Timer_Tick;
            timer.Start();

            this.Paint += ClockControl_Paint;
        }

        public Clock(Size size, Point point, Color color) : this()
        {
            this.Size = size;
            this.Location = point;
            this.color = color;
        }

        private void DrawCircle()
        {
            var g = this.CreateGraphics();

            for (int i = 0; i < day; i++)
            {
                g.FillEllipse(new SolidBrush(Color.Black), new Rectangle(new Point((int)(this.Width / 2 + Lenght * Math.Cos(Math.PI / 6 * i)),
                                                                            (int)(this.Height / 2 + Lenght * Math.Sin(Math.PI / 6 * i))),
                                                                            new Size(7, 7)));
                if (i == 3)
                {
                    float x = (float)(ClientRectangle.Width / 2 + Lenght * Math.Cos(Math.PI / 6 * i));
                    float y = (float)(ClientRectangle.Height / 2 + Lenght * Math.Sin(Math.PI / 6 * i));
                }
            }

            g.FillEllipse(new SolidBrush(this.color), new Rectangle(new Point((int)(this.ClientRectangle.Width / 2 - Lenght + 10),
                        (int)(this.ClientRectangle.Height / 2 - Lenght + 10)), new Size((int)(Lenght - 10) * 2, (int)(Lenght - 10) * 2)));

            g.FillEllipse(Brushes.AntiqueWhite, new Rectangle(100, 100, 200, 200));
        }
        private void ClockControl_Paint(object sender, PaintEventArgs e)
        {
            PaintClock(DateTime.Now);
        }
        
        private void DrawLines(DateTime date)
        {
            var draw = this.CreateGraphics();

            draw.DrawLine(new Pen(new SolidBrush(Color.Black), 1), new Point((int)(ClientRectangle.Width / 2), (int)(ClientRectangle.Height / 2)),
               new Point((int)(ClientRectangle.Width / 2 + (Lenght - 2) * Math.Sin(2 * Math.PI / minute * date.Second)), (int)(ClientRectangle.Height / 2 - (Lenght - 2) * Math.Cos(2 * Math.PI / minute * date.Second))));

            draw.DrawLine(new Pen(new SolidBrush(Color.Black), 2), new Point((int)(ClientRectangle.Width / 2), (int)(ClientRectangle.Height / 2)),
                new Point((int)(ClientRectangle.Width / 2 + (Lenght - 4) * Math.Sin(2 * Math.PI / minute * date.Minute)), (int)(ClientRectangle.Height / 2 - (Lenght - 4) * Math.Cos(2 * Math.PI / minute * date.Minute))));

            int hour = 0;
            if (date.Hour <= day)
                hour = date.Hour;
            else
                hour = date.Hour - day;

            draw.DrawLine(new Pen(new SolidBrush(Color.Black), 4), new Point((int)(ClientRectangle.Width / 2), (int)(ClientRectangle.Height / 2)),
                new Point((int)(ClientRectangle.Width / 2 + (Lenght - 10) * Math.Sin(2 * Math.PI / day * hour + 2 * Math.PI / (day * minute) * date.Minute)), (int)(ClientRectangle.Height / 2 - (Lenght - 10) * Math.Cos(2 * Math.PI / day * hour + 2 * Math.PI / (day * minute) * date.Minute))));
        }
        private void PaintClock(DateTime date)
        {
            DrawCircle();
            DrawLines(date);
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            this.Invalidate();
        }
    }
}