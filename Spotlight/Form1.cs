using System;
using System.Drawing;
using System.Windows.Forms;

namespace Spotlight
{
    public partial class Form1 : Form
    {
        int  spotlightSize = 150;
        bool drawSpotlight = false;
        int x;
        int y;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                this.Close();
                return;
            }
            x = e.X;
            y = e.Y;
            drawSpotlight = true;
            Graphics screenGraphics = Graphics.FromImage(screen);
            screenGraphics.FillRectangle(highlight, 0, 0, this.Size.Width, this.Size.Height);
            screenGraphics.CopyFromScreen(currentMonitor.Bounds.X, currentMonitor.Bounds.Y, 0, 0, new Size(currentMonitor.Bounds.Width, currentMonitor.Bounds.Height),CopyPixelOperation.SourceCopy);
            Invalidate();
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (drawSpotlight)
            {
                x = e.X;
                y = e.Y;
                Invalidate();
            }
        }

        Bitmap screen;
        Brush highlight = new SolidBrush(Color.FromArgb(128, 96, 96, 96));
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if (drawSpotlight)
            {
                g.DrawImage(screen, 0, 0, this.Size.Width, this.Size.Height);
                g.FillRectangle(highlight, 0, 0, this.Size.Width, this.Size.Height);
                g.FillEllipse(Brushes.Red, x - spotlightSize, y - spotlightSize, spotlightSize*2, spotlightSize*2);
            }
            else
            {
                g.FillRectangle(Brushes.Red, 0, 0, this.Size.Width, this.Size.Height);
            }
        }

        private void Form1_MouseLeave(object sender, EventArgs e)
        {
            drawSpotlight = false;
            Invalidate();
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            drawSpotlight = false;
            Invalidate();
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if( e.KeyCode == Keys.F1)
            {
                MessageBox.Show("Esc - Quit\nUp/Down - Change spotlight size\nRight - Switch monitors\n-Ferose Khan J, 4 Dec 2019.");
            }
            else if( e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            else if( e.KeyCode == Keys.Up)
            {
                spotlightSize += 50;
            }
            else if (e.KeyCode == Keys.Down)
            {
                spotlightSize -= 50;
            }
            else if(e.KeyCode == Keys.Right)
            {
                currentMonitor = Screen.AllScreens[++currentMonitorIndex % Screen.AllScreens.Length];
                MoveForm();
            }
        }

        private void Form1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        Screen currentMonitor = Screen.AllScreens[0];
        int currentMonitorIndex = 0;
        private void Form1_Resize(object sender, EventArgs e)
        {
            MoveForm();
        }

        void MoveForm()
        {
            if (currentMonitor.Bounds.X != this.Left || currentMonitor.Bounds.Width != this.Width)
            {
                this.SetBounds(currentMonitor.Bounds.X, currentMonitor.Bounds.Y, currentMonitor.Bounds.Width, currentMonitor.Bounds.Height);
                screen = new Bitmap(currentMonitor.Bounds.Width, currentMonitor.Bounds.Height);
            }
        }
    }
}
