namespace RatioRuler
{
    struct AxisLabel
    {
        public Size DrawSize;
        public String Text;
    }

    public partial class MainForm : Form
    {
        private bool dragging = false;
        private Size dragOffset = new Size();
        private AxisLabel[] axisLabels = new AxisLabel[10];

        public MainForm()
        {
            InitializeComponent();
            for (int i = 0; i < this.axisLabels.Length; i++)
            {
                this.axisLabels[i].Text = i == 0 ? "1" : $".{i}";
                this.axisLabels[i].DrawSize = TextRenderer.MeasureText(this.axisLabels[i].Text, this.Font);
            }
            this.scale.SelectedIndex = 0;
        }

        private int Width10
        {
            get { return this.controlPanel.Location.X - 30; }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //this.FormBorderStyle = FormBorderStyle.Sizable;
            this.ClientSize = new Size(500, this.ClientSize.Height);
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.Blue);
            Brush brush = new SolidBrush(Color.Blue);
            int textY = this.ClientSize.Height - this.axisLabels[0].DrawSize.Height - 10;
            
            for (int i = logScale.Checked ? 2 : 1; i <= 10; i++)
            {
                double ratio = (double)i / 10;
                if (logScale.Checked)
                {
                    ratio = Math.Log10(i);
                }
                int x = (int)Math.Round(Width10 * ratio);
                e.Graphics.DrawLine(pen, x, 0, x, this.ClientSize.Height - axisLabels[i % 10].DrawSize.Height - 20);

                if (!logScale.Checked || (i <= 5 || i == 7 || i == 10))
                {
                    int textX = x - axisLabels[i % 10].DrawSize.Width / 2 + 2;
                    e.Graphics.DrawString(axisLabels[i % 10].Text, this.Font, brush, textX, textY);
                }
            }
        }

        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Location.Y > this.ClientSize.Height / 2)
            {
                dragOffset = (Size)e.Location;
                dragging = true;
            }
            else
            {
                Clipboard.SetText(this.value.Text);
            }
        }

        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                this.Location += (Size)(e.Location - dragOffset);
                this.Update();
            }
            else
            {
                this.rulerCursor.Location = new Point(e.Location.X, 0);
                double ratio = (double)e.Location.X / Width10;
                if (logScale.Checked)
                {
                    ratio = Math.Pow(10, ratio - 1);
                }
                double scale = double.Parse(this.scale.Text);
                this.value.Text = (scale * ratio).ToString("F3");
            }
        }

        private void MainForm_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        private void MainForm_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    Cursor.Position = new Point(Cursor.Position.X - 1, Cursor.Position.Y);
                    break;
                case Keys.Right:
                    Cursor.Position = new Point(Cursor.Position.X + 1, Cursor.Position.Y);
                    break;
                default:
                    break;
            }
        }

        private void scale_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            {
                this.MainForm_KeyDown(sender, e);
                e.SuppressKeyPress = true;
            }
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            this.TopLevel = true;
            this.TopMost = true;
        }

        private void logScale_CheckedChanged(object sender, EventArgs e)
        {
            this.Invalidate();
        }
    }
}