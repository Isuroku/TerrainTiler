namespace TerrainTiler
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.rtLog = new System.Windows.Forms.RichTextBox();
            this.pnlGrid = new System.Windows.Forms.Panel();
            this.pnlColors = new System.Windows.Forms.Panel();
            this.pnlControls = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // rtLog
            // 
            this.rtLog.HideSelection = false;
            this.rtLog.Location = new System.Drawing.Point(12, 606);
            this.rtLog.Name = "rtLog";
            this.rtLog.Size = new System.Drawing.Size(992, 115);
            this.rtLog.TabIndex = 26;
            this.rtLog.Text = "";
            // 
            // pnlGrid
            // 
            this.pnlGrid.Location = new System.Drawing.Point(223, 12);
            this.pnlGrid.Name = "pnlGrid";
            this.pnlGrid.Size = new System.Drawing.Size(570, 570);
            this.pnlGrid.TabIndex = 27;
            this.pnlGrid.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlGrid_Paint);
            this.pnlGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlGrid_MouseDown);
            this.pnlGrid.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlGrid_MouseMove);
            this.pnlGrid.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnlGrid_MouseUp);
            // 
            // pnlColors
            // 
            this.pnlColors.Location = new System.Drawing.Point(799, 12);
            this.pnlColors.Name = "pnlColors";
            this.pnlColors.Size = new System.Drawing.Size(205, 570);
            this.pnlColors.TabIndex = 28;
            this.pnlColors.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlColors_Paint);
            // 
            // pnlControls
            // 
            this.pnlControls.Location = new System.Drawing.Point(12, 12);
            this.pnlControls.Name = "pnlControls";
            this.pnlControls.Size = new System.Drawing.Size(205, 570);
            this.pnlControls.TabIndex = 29;
            this.pnlControls.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlControls_Paint);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1014, 729);
            this.Controls.Add(this.pnlControls);
            this.Controls.Add(this.pnlColors);
            this.Controls.Add(this.pnlGrid);
            this.Controls.Add(this.rtLog);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Terrain Tiler";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtLog;
        private System.Windows.Forms.Panel pnlGrid;
        private System.Windows.Forms.Panel pnlColors;
        private System.Windows.Forms.Panel pnlControls;
    }
}

