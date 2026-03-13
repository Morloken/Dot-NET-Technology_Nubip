using System;
using System.Drawing;
using System.Windows.Forms;

namespace TrapeziumProject
{
    public class Trapezium
    {
        public static int TrapeziumCount = 0;

        private Point center;
        //Два розміри основ
        private int sideA;
        private int sideB;
        private int height;
        private Form1 form;

        public Trapezium()//Конструктор БЕЗ параметрів
        {
            sideA = 0;
            sideB = 0;
            height = 0;
        }

        public Trapezium(Point c, int a, int b, int h, Form1 form1)// Конструктор З параметрами
        {
            ++TrapeziumCount;

            center = c;
            sideA = a;
            sideB = b;
            height = h;
            form = form1;
        }

        // ===== МЕТОД МАЛЮВАННЯ =====
        public void Draw(Graphics graph, Pen pen)
        {
            if (graph == null || pen == null)
                return;

            if (sideA <= 0 || sideB <= 0 || height <= 0)
                return;

            try
            {
                // Координати вершин трапеції
                Point[] points = new Point[4]
                {
                    // Нижня основа (довша)
                    new Point(center.X - sideB / 2, center.Y + height / 2),
                    new Point(center.X + sideB / 2, center.Y + height / 2),
                    
                    // Верхня основа (коротша)
                    new Point(center.X + sideA / 2, center.Y - height / 2),
                    new Point(center.X - sideA / 2, center.Y - height / 2)
                };

                // Малюємо трапецію
                graph.DrawPolygon(pen, points);

               
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка: {ex.Message}");
            }
        }

        public void Show(Graphics graph, Pen pen)
        {
            Draw(graph, pen);
        }

        public void Hide(Graphics graph, Pen pen)
        {
            Pen hidePen = new Pen(Color.White, pen.Width);
            Draw(graph, hidePen);
        }

        public int SideA { get { return sideA; } }
        public int SideB { get { return sideB; } }
        public int Height { get { return height; } }
    }
}