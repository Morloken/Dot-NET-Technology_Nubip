using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TrapeziumProject
{
    public partial class Form1 : Form
    {
        public Graphics graph;
        public Pen pen1, pen2;
        public int scale = 5;
        private List<Trapezium> trapeziums = new List<Trapezium>();

        public Form1()
        {
            InitializeComponent();

            this.BackColor = Color.White;
            this.Size = new Size(900, 700);

            pen1 = new Pen(Color.Black, 2);
            pen2 = new Pen(Color.White, 2);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (graph == null && pictureBox1 != null)
                graph = pictureBox1.CreateGraphics();

            int trapCount = Trapezium.TrapeziumCount;

            int centerX = 80 + (trapCount * 100);
            int centerY = 250;
            Point center = new Point(centerX, centerY);

            int sideA = 50 + trapCount * 8;
            int sideB = 100 + trapCount * 8;
            int height = 80 + trapCount * 5;

            Trapezium newTrap = new Trapezium(center, sideA, sideB, height, this);
            trapeziums.Add(newTrap);

            label1.Text = $"Трапецій створено: {Trapezium.TrapeziumCount}";

            RedrawAll();
        }

        private void RedrawAll()
        {
            if (pictureBox1 == null)
                return;

            pictureBox1.Image = null;

            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics g = Graphics.FromImage(bmp);

            g.Clear(Color.White);

            foreach (Trapezium trap in trapeziums)
            {
                trap.Draw(g, pen1);
            }

            pictureBox1.Image = bmp;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
        }
    }
}