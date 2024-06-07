using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SinusoidDrawApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            this.Paint += new PaintEventHandler(MainForm_Paint);
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            IntPtr hdc = e.Graphics.GetHdc(); // Pobierz uchwyt do kontekstu urządzenia
            int width = this.ClientSize.Width;
            int height = this.ClientSize.Height;

            // Narysuj sinusoidę
            DrawSinusoid(hdc, width, height);

            e.Graphics.ReleaseHdc(hdc); // Zwolnij kontekst urządzenia
        }

        private void DrawSinusoid(IntPtr hdc, int width, int height)
        {
            int amplitude = height / 4;
            int frequency = 10;
            int xOffset = 50;
            int yOffset = height / 2;

            for (int x = 0; x < width - xOffset; x++)
            {
                double y = amplitude * Math.Sin((double)(x + xOffset) * frequency * Math.PI / 180.0) + yOffset;
                WinAPI.SetPixel(hdc, x, (int)y, 0x000000); // Ustaw piksel na czarny
            }
        }

        // Definicja metod z WIN API
        public class WinAPI
        {
            [DllImport("gdi32.dll")]
            public static extern bool SetPixel(IntPtr hdc, int X, int Y, int crColor);
        }
    }
}
