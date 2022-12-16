namespace RatioRuler
{
    public partial class MainForm : Form
    {
        private bool dragging = false;
        private Size dragOffset = new Size();
        private int labelHeight, labelWidth2, labelWidth1;
        private Dictionary<double, string[]> scaledAxisLabel = new Dictionary<double, string[]>();

        public MainForm()
        {
            InitializeComponent();
            Size labelSize2 = TextRenderer.MeasureText(".5", this.Font);
            this.labelHeight = labelSize2.Height;
            this.labelWidth2 = labelSize2.Width;
            this.labelWidth1 = TextRenderer.MeasureText("5", this.Font).Width;
            this.scale.SelectedIndex = 0;

            foreach (var item in this.scale.Items)
            {
                double scale = double.Parse(item.ToString());
                string[] labels = new string[11];
                for (int i = 0; i <= 10; i++)
                {
                    int value = (int)Math.Round(scale * i);
                    int fraction = value % 10;
                    labels[i] = fraction == 0 ? $"{value / 10}" : $".{fraction}";
                }
                this.scaledAxisLabel.Add(scale, labels);
            }
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

        private void DrawAxis(PaintEventArgs e, Pen pen, Brush brush)
        {
            var calcRatio = (int i) => (double)i / 10;
            if (logScale.Checked)
            {
                calcRatio = (int i) => Math.Log10(i);
            }

            int axisLength = AxisAreaLength;
            bool[] drawLabel = Enumerable.Repeat(true, 11).ToArray();
            if (logScale.Checked)
            {
                if (axisLength < 400)
                {
                    drawLabel[9] = false;
                }
                if (axisLength < 300)
                {
                    drawLabel[8] = false;
                }
                if (axisLength < 250)
                {
                    drawLabel[6] = false;
                }
                if (axisLength < 170)
                {
                    drawLabel[4] = false;
                }
            }
            else
            {
                if (axisLength < 200)
                {
                    for (int i = 1; i <= 10; i += 2)
                    {
                        drawLabel[i] = false;
                    }
                }
            }

            for (int i = 1; i <= 10; i++)
            {
                double ratio = calcRatio(i);
                string label = "";
                if (drawLabel[i])
                {
                    label = this.scaledAxisLabel[ValueScale][i];
                }
                this.DrawTick(e, pen, brush, ratio, label);
            }
        }

        private void DrawTick(PaintEventArgs e, Pen pen, Brush brush, double ratio, string label)
        {
            int textX, textY;
            if (vertical.Checked)
            {
                textX = this.ClientSize.Width - (label.Length == 1 ? this.labelWidth1 : this.labelWidth2) - 10;
                int y = AxisAreaLength - (int)Math.Round(AxisAreaLength * ratio);
                e.Graphics.DrawLine(pen, 0, y, textX - 10, y);
                textY = y - this.labelHeight / 2;
            }
            else
            {
                textY = this.ClientSize.Height - this.labelHeight - 10;
                int x = (int)Math.Round(AxisAreaLength * ratio);
                e.Graphics.DrawLine(pen, x, 0, x, textY);
                textX = x - (label.Length == 1 ? this.labelWidth1 : this.labelWidth2) / 2 + 2;
            }

            if (textX >= 0 && textY >= 0)
            {
                e.Graphics.DrawString(label, this.Font, brush, textX, textY);
            }
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
            this.DrawAxis(e, pen, brush);
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

        private void scale_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        private void vertical_CheckedChanged(object sender, EventArgs e)
        {
            this.rulerCursor.Size = new Size(this.rulerCursor.Size.Height, this.rulerCursor.Size.Width);

            this.controlPanel.Location = new Point(0, 0);
            this.controlPanel.Anchor = 15 - this.controlPanel.Anchor;

            Size newSize = new Size(this.ClientSize.Height, this.ClientSize.Width);
            if (this.vertical.Checked)
            {
                this.controlPanel.Location = new Point(12, this.ClientSize.Height - this.controlPanel.Height - 12);
                newSize.Width = this.controlPanel.Width + 12 * 2;
            }
            else
            {
                this.controlPanel.Location = new Point(this.ClientSize.Width - this.controlPanel.Width - 12, 12);
                newSize.Height = this.controlPanel.Height + 12 * 2;
            }

            this.ClientSize = newSize;
            this.Invalidate();
        }

        private void rulerCursor_MouseDown(object sender, MouseEventArgs e)
        {
            Clipboard.SetText(this.value.Text);
        }
    }
}