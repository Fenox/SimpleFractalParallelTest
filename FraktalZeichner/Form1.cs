using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace FraktalZeichner
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            MakeFractal();
        }

        private Bitmap _map;

        private float _minX = -1.5f;
        private float _maxX = 1.5f;
        private float _minY = -1.5f;
        private float _maxY = 1.5f;

        private void MakeFractal() 
        {
            JuliaFraktal fraktal = new JuliaFraktal
            {
                ColorMethod = ColorMethod1
            };

            Width = 500;
            Height = 500;
            _map = new Bitmap(Width, Height, PixelFormat.Format32bppArgb);
            
            ImageArray arr = new ImageArray(_map);
            arr.DoPixelWiseParallel((x, y, width, height, val) =>
            {
                float a = x * (_maxX - _minX) / width + _minX;
                float b = y * (_maxY - _minY) / height + _minY;

                Complex c = new Complex(a,b);
                return fraktal.IsInSet(c).ToArgb();
            });

            arr.EndProcessing();
        }

        private static Color ColorMethod1(int iterations, int maxIterations)
        {
            int val = (int)(iterations / (float)maxIterations * 5 * 255 * 10);

            Color newColor;
            int fade = val % 255;

            if (val < 255)
                newColor = Color.FromArgb(100, fade, 0);
            else if (val < 255 * 2)
                newColor = Color.FromArgb(255 - fade, 255, 0);
            else if (val < 255 * 3)
                newColor = Color.FromArgb(0, 255, fade);
            else if (val < 255 * 4)
                newColor = Color.FromArgb(0, 255 - fade, 255);
            else if (val < 255 * 5)
                newColor = Color.FromArgb(fade, 0, 255);
            else
                newColor = Color.FromArgb(255, 0, 255 - fade);

            return newColor;
        }
        
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            const float zoomVal = .5f;
            if (e.KeyCode == Keys.Add)
            {
                _minY += zoomVal;
                _minX += zoomVal;
                _maxX -=zoomVal;
                _maxY -= zoomVal;
            }

            MakeFractal();
            Invalidate();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(_map, 0, 0);
        }
    }    
}
