using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameOfLife
{
    public partial class Form1 : Form
    {
        private Graphics graphics;
        private int resolution;
        private GameEngine gameEngine;
        public Form1()
        {
            InitializeComponent();
        }
        private void StartGame()
        {
            if (timer1.Enabled) return;

            nudResolution.Enabled = false;
            nudDensity.Enabled = false;
            resolution = (int) nudResolution.Value;

            gameEngine = new GameEngine(
                pictureBox1.Height / resolution,
                pictureBox1.Width / resolution,
                (int)nudDensity.Value
            );

            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(pictureBox1.Image);
            timer1.Start();
        }
        void StopGame()
        {
            if (!timer1.Enabled) return;
            nudResolution.Enabled = true;
            nudDensity.Enabled = true;
            timer1.Stop();
        }
        
        private void UpdateGen()
        {
            graphics.Clear(Color.Black);
            var field = gameEngine.GetCurrentGen();
            for (int x = 0; x < field.GetLength(0); x++)
            {
                for (int y = 0; y < field.GetLength(1); y++)
                {
                    Brush color;
                    if (field[x, y] is GrassCell) color = Brushes.Green;
                    else if (field[x, y] is GrassEaterCell) color = Brushes.Yellow;
                    else color = Brushes.Gray;
                    graphics.FillRectangle(color, x * resolution, y * resolution, resolution, resolution);
                }
            }
            pictureBox1.Refresh();
            gameEngine.NextGen();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateGen();
        }

        private void bStart_Click(object sender, EventArgs e)
        {
            StartGame();
        }

        private void bStop_Click(object sender, EventArgs e)
        {
            StopGame();
        }
    }
}
