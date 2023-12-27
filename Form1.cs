using System;
using System.Drawing;
using System.Windows.Forms;
namespace lr_3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClearGraph();

            double startValue = Convert.ToDouble(textBox1.Text);
            double endValue = Convert.ToDouble(textBox2.Text);
            double step = Convert.ToDouble(textBox3.Text);
            listBox1.Items.Add($"{"x",-10} {"y",-10}");
            listBox1.Items.Add(new string('-', 22));

            PlotGraph(startValue, endValue, step);
        }

        private void PlotGraph(double startValue, double endValue, double step)
        {
            using (Graphics g = panel1.CreateGraphics())
            {
                Pen pen = new Pen(Color.Green);
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;

                int xInterval = panel1.Width / 10;
                int yInterval = panel1.Height / 10;

                // Draw x-axis and y-axis
                g.DrawLine(pen, 0, panel1.Height / 2, panel1.Width, panel1.Height / 2);
                g.DrawLine(pen, panel1.Width / 2, 0, panel1.Width / 2, panel1.Height);

                for (int i = 0; i <= panel1.Width; i += xInterval)
                {
                    g.DrawLine(pen, i, panel1.Height / 2 - 5, i, panel1.Height / 2 + 5);
                    double xValue = startValue + (endValue - startValue) * i / panel1.Width;
                    g.DrawString($"{xValue:F2}", DefaultFont, Brushes.Black, i, panel1.Height / 2 + 5);
                }

                for (int i = 0; i <= panel1.Height; i += yInterval)
                {
                    g.DrawLine(pen, panel1.Width / 2 - 5, i, panel1.Width / 2 + 5, i);
                    double yValue = 100 - i * 100.0 / panel1.Height;
                    g.DrawString($"{yValue:F2}", DefaultFont, Brushes.Black, panel1.Width / 2 + 5, i);
                }

                pen.Color = Color.Blue;
                pen.Width = 2;

                for (double x = startValue; x <= endValue; x += step)
                {
                    double y = CalculateFunction(x);
                    int pixelX = (int)((x - startValue) / (endValue - startValue) * panel1.Width);
                    int pixelY = (int)((1 - (y / 100.0)) * panel1.Height);
                    g.FillEllipse(Brushes.Blue, pixelX - 2, pixelY - 2, 4, 4);

                    if (x > startValue)
                    {
                        int prevPixelX = (int)((x - startValue - step) / (endValue - startValue) * panel1.Width);
                        int prevPixelY = (int)((1 - (CalculateFunction(x - step) / 100.0)) * panel1.Height);
                        g.DrawLine(pen, prevPixelX, prevPixelY, pixelX, pixelY);
                    }

                    listBox1.Items.Add($"{x,-10} {y,-10}");
                }
            }
        }

        private double CalculateFunction(double x)
        {
            return 3 * Math.Pow(x, 5) + 20 * Math.Pow(x, 3) - 20 * x + 3;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ClearGraph();
        }

        private void ClearGraph()
        {
            panel1.Invalidate();
            listBox1.Items.Clear();
        }
    }
}