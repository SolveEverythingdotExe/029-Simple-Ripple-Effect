using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MainApplication
{
    public partial class MainForm : Form
    {
        private Color DarkShade = Color.FromArgb(110, 110, 110);
        private Color BlueShade = Color.FromArgb(50, 70, 255);
        private Color WhiteShade = Color.FromArgb(180, 180, 180);

        //we will store the waves in here
        List<Wave> Waves = new List<Wave>();

        private Random random = new Random();

        public MainForm()
        {
            InitializeComponent();

            //start animation
            timer1.Start();

            //remove flickerring
            DoubleBuffered = true;
        }

        private void MainForm_MouseClick(object sender, MouseEventArgs e)
        {
            int maxHeight = random.Next(Height / 4, Height);

            //create waves
            Waves.Add(new Wave(e.Location, new Size(10, 10), maxHeight, BlueShade, 0, 0));
            Waves.Add(new Wave(e.Location, new Size(20, 20), maxHeight, RandomizeColor(), 5, 0));
            Waves.Add(new Wave(e.Location, new Size(30, 30), maxHeight, RandomizeColor(), 10, 0));
            Waves.Add(new Wave(e.Location, new Size(40, 40), maxHeight, RandomizeColor(), 15, 0));
            Waves.Add(new Wave(e.Location, new Size(80, 80), maxHeight, RandomizeColor(), 35, 0));
            Waves.Add(new Wave(e.Location, new Size(110, 110), maxHeight, WhiteShade, 50, 0));
            Waves.Add(new Wave(e.Location, new Size(150, 150), maxHeight, BlueShade, 70, 0));

            //force to paint the form
            Invalidate();
        }

        private Color RandomizeColor()
        {
            return (random.Next(0, 1000) % 2 == 0) ? DarkShade : WhiteShade;
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            //remove pixelation
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            using (SolidBrush brush = new SolidBrush(BlueShade))
            {
                using (Pen pen = new Pen(brush))
                {
                    for (int i = 0; i < Waves.Count; i++)
                    {
                        Wave wave = Waves[i];

                        pen.Color = wave.Color;
                        wave.Inflate += 10; //increase the size of the waves

                        //limit the size of the wave
                        if (wave.Size.Width + wave.Inflate <= wave.MaxHeight)
                        {
                            int x = wave.Location.X - wave.LocationOffset - (wave.Inflate / 2);
                            int y = wave.Location.Y - wave.LocationOffset - (wave.Inflate / 2);
                            int w = wave.Size.Width + wave.Inflate;
                            int h = wave.Size.Height + wave.Inflate;

                            e.Graphics.DrawArc(pen, x, y, w, h, 0, 360);
                        }
                        else
                            //pop the wave/ripple if it already reached it's max size
                            Waves.Remove(wave);
                    }
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //repaint the form
            Invalidate();
        }
    }
}
