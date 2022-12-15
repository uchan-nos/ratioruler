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
        private AxisLabel[] axisLabels = new AxisLabel[11];

        public MainForm()
        {
            InitializeComponent();
            for (int i = 0; i < this.axisLabels.Length; i++)
            {
                if (i == 0)
                {
                    this.axisLabels[i].Text = "0";
                }
                else if (i == 10)
                {
                    this.axisLabels[i].Text = "1";
                }
                else
                {
                    this.axisLabels[i].Text = $".{i}";
                }
                this.axisLabels[i].DrawSize = TextRenderer.MeasureText(this.axisLabels[i].Text, this.Font);
            }
            this.scale.SelectedIndex = 0;
        }

        private int GetXorY(Point p)
        {
            return this.vertical.Checked ? p.Y : p.X;
        }

        private int AxisAreaLength
        {
            get
            {
                return GetXorY(this.controlPanel.Location) - 30;
            }
        }

        private int RulerCursorPosition
        {
            get
            {
                int pos = GetXorY(this.rulerCursor.Location);
                if (vertical.Checked)
                {
                    return AxisAreaLength - pos;
                }
                else
                {
                    return pos;
                }
            }
        }

        private double ValueScale
        {
            get
            {
                return double.Parse(this.scale.Text);
            }
        }

        private void UpdateValueText()
        {
            double ratio = (double)RulerCursorPosition / AxisAreaLength;
            if (logScale.Checked)
            {
                ratio = Math.Pow(10, ratio - 1);
            }
            this.value.Text = (ValueScale * ratio).ToString("F3");
        }

        private void DrawHorizontalAxis(PaintEventArgs e, Pen pen, Brush brush)
        {
            int textY = this.ClientSize.Height - this.axisLabels[0].DrawSize.Height - 10;

            for (int i = logScale.Checked ? 2 : 1; i <= 10; i++)
            {
                double ratio = logScale.Checked ? Math.Log10(i) : (double)i / 10;
                int x = (int)Math.Round(AxisAreaLength * ratio);
                e.Graphics.DrawLine(pen, x, 0, x, textY - 10);

                if (!logScale.Checked || (i <= 5 || i == 7 || i == 10))
                {
                    int textX = x - axisLabels[i].DrawSize.Width / 2 + 2;
                    e.Graphics.DrawString(axisLabels[i].Text, this.Font, brush, textX, textY);
                }
            }
        }
        private void DrawVerticalAxis(PaintEventArgs e, Pen pen, Brush brush)
        {
            int textX = this.ClientSize.Width - this.axisLabels[0].DrawSize.Width - 10;

            for (int i = logScale.Checked ? 1 : 0; i <= 9; i++)
            {
                double ratio = logScale.Checked ? Math.Log10(i) : (double)i / 10;
                int y = AxisAreaLength - (int)Math.Round(AxisAreaLength * ratio);
                e.Graphics.DrawLine(pen, 0, y, textX - 10, y);

                if (!logScale.Checked || (i <= 5 || i == 7 || i == 10))
                {
                    int textY = y - axisLabels[i].DrawSize.Height / 2;
                    e.Graphics.DrawString(axisLabels[i].Text, this.Font, brush, textX, textY);
                }
            }

            e.Graphics.DrawLine(pen, 0, 0, textX - 10, 0);
        }

        private void MoveMouseCursor(int diffX, int diffY)
        {
            Cursor.Position = new Point(Cursor.Position.X + diffX, Cursor.Position.Y + diffY);
        }

        private bool HandleKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                MoveMouseCursor(0, e.KeyCode == Keys.Down ? 1 : -1);
                return true;
            }
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Left)
            {
                MoveMouseCursor(e.KeyCode == Keys.Right ? 1 : -1, 0);
                return true;
            }
            return false;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.ClientSize = new Size(500, this.ClientSize.Height);
            this.UpdateValueText();
            this.ActiveControl = this.value;
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.Blue);
            Brush brush = new SolidBrush(Color.Blue);
            if (vertical.Checked)
            {
                DrawVerticalAxis(e, pen, brush);
            }
            else
            {
                DrawHorizontalAxis(e, pen, brush);
            }
        }

        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            Clipboard.SetText(this.value.Text);
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
                if (vertical.Checked)
                {
                    this.rulerCursor.Location = new Point(0, e.Y);
                }
                else
                {
                    this.rulerCursor.Location = new Point(e.X, 0);
                }
                this.UpdateValueText();
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
            HandleKeyDown(e);
        }

        private void scale_KeyDown(object sender, KeyEventArgs e)
        {
            if (HandleKeyDown(e))
            {
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

        private void vertical_CheckedChanged(object sender, EventArgs e)
        {
            this.rulerCursor.Size = new Size(this.rulerCursor.Size.Height, this.rulerCursor.Size.Width);
            this.controlPanel.Location = new Point(0, 0);
            this.controlPanel.Anchor = 15 - this.controlPanel.Anchor;
            if (this.vertical.Checked)
            {
                this.controlPanel.Location = new Point(12, this.ClientSize.Height - this.controlPanel.Height - 12);
            }
            else
            {
                this.controlPanel.Location = new Point(this.ClientSize.Width - this.controlPanel.Width - 12, 12);
            }

            this.ClientSize = new Size(this.ClientSize.Height, this.ClientSize.Width);
            this.Invalidate();
        }

        private void rulerCursor_MouseDown(object sender, MouseEventArgs e)
        {
            Clipboard.SetText(this.value.Text);
        }
    }
}