namespace RatioRuler
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.rulerCursor = new System.Windows.Forms.PictureBox();
            this.value = new System.Windows.Forms.Label();
            this.controlPanel = new System.Windows.Forms.Panel();
            this.logScale = new System.Windows.Forms.CheckBox();
            this.scale = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.rulerCursor)).BeginInit();
            this.controlPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // rulerCursor
            // 
            this.rulerCursor.BackColor = System.Drawing.Color.OrangeRed;
            this.rulerCursor.Location = new System.Drawing.Point(50, 0);
            this.rulerCursor.Margin = new System.Windows.Forms.Padding(0);
            this.rulerCursor.Name = "rulerCursor";
            this.rulerCursor.Size = new System.Drawing.Size(1, 30);
            this.rulerCursor.TabIndex = 0;
            this.rulerCursor.TabStop = false;
            // 
            // value
            // 
            this.value.BackColor = System.Drawing.Color.White;
            this.value.Location = new System.Drawing.Point(68, 0);
            this.value.Name = "value";
            this.value.Size = new System.Drawing.Size(71, 32);
            this.value.TabIndex = 1;
            this.value.Text = "1.234";
            // 
            // controlPanel
            // 
            this.controlPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.controlPanel.Controls.Add(this.logScale);
            this.controlPanel.Controls.Add(this.scale);
            this.controlPanel.Controls.Add(this.label2);
            this.controlPanel.Controls.Add(this.label1);
            this.controlPanel.Controls.Add(this.value);
            this.controlPanel.Location = new System.Drawing.Point(370, 12);
            this.controlPanel.Name = "controlPanel";
            this.controlPanel.Size = new System.Drawing.Size(142, 135);
            this.controlPanel.TabIndex = 3;
            // 
            // logScale
            // 
            this.logScale.AutoSize = true;
            this.logScale.Location = new System.Drawing.Point(3, 81);
            this.logScale.Name = "logScale";
            this.logScale.Size = new System.Drawing.Size(80, 36);
            this.logScale.TabIndex = 6;
            this.logScale.Text = "log";
            this.logScale.UseVisualStyleBackColor = true;
            this.logScale.CheckedChanged += new System.EventHandler(this.logScale_CheckedChanged);
            // 
            // scale
            // 
            this.scale.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.scale.FormattingEnabled = true;
            this.scale.Items.AddRange(new object[] {
            "1",
            "2",
            "5",
            "10"});
            this.scale.Location = new System.Drawing.Point(72, 35);
            this.scale.Name = "scale";
            this.scale.Size = new System.Drawing.Size(67, 40);
            this.scale.TabIndex = 5;
            this.scale.KeyDown += new System.Windows.Forms.KeyEventHandler(this.scale_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 32);
            this.label2.TabIndex = 4;
            this.label2.Text = "scale";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 32);
            this.label1.TabIndex = 3;
            this.label1.Text = "value";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(524, 159);
            this.Controls.Add(this.controlPanel);
            this.Controls.Add(this.rulerCursor);
            this.Cursor = System.Windows.Forms.Cursors.Cross;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MinimumSize = new System.Drawing.Size(15, 0);
            this.Name = "MainForm";
            this.Opacity = 0.8D;
            this.Text = "RatioRuler 比率定規";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MainForm_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MainForm_KeyPress);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.rulerCursor)).EndInit();
            this.controlPanel.ResumeLayout(false);
            this.controlPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private PictureBox rulerCursor;
        private Label value;
        private Panel controlPanel;
        private Label label2;
        private Label label1;
        private ComboBox scale;
        private CheckBox logScale;
    }
}