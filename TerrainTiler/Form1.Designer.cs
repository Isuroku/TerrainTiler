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
            this.pnlBaseGrid = new System.Windows.Forms.Panel();
            this.pnlColors = new System.Windows.Forms.Panel();
            this.pnlControls = new System.Windows.Forms.Panel();
            this.lblLastLoaded = new System.Windows.Forms.Label();
            this.btnRewriteTerrain = new System.Windows.Forms.Button();
            this.tbTerrain = new System.Windows.Forms.TextBox();
            this.lbTerrains = new System.Windows.Forms.ListBox();
            this.btnLoadTerrain = new System.Windows.Forms.Button();
            this.btnSaveTerrain = new System.Windows.Forms.Button();
            this.btnDeleteTerrain = new System.Windows.Forms.Button();
            this.btnCopyTerrain = new System.Windows.Forms.Button();
            this.pnlControls.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtLog
            // 
            this.rtLog.HideSelection = false;
            this.rtLog.Location = new System.Drawing.Point(12, 606);
            this.rtLog.Name = "rtLog";
            this.rtLog.ReadOnly = true;
            this.rtLog.Size = new System.Drawing.Size(992, 115);
            this.rtLog.TabIndex = 26;
            this.rtLog.TabStop = false;
            this.rtLog.Text = "";
            // 
            // pnlBaseGrid
            // 
            this.pnlBaseGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlBaseGrid.Location = new System.Drawing.Point(223, 12);
            this.pnlBaseGrid.Name = "pnlBaseGrid";
            this.pnlBaseGrid.Size = new System.Drawing.Size(570, 570);
            this.pnlBaseGrid.TabIndex = 27;
            // 
            // pnlColors
            // 
            this.pnlColors.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlColors.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pnlColors.Location = new System.Drawing.Point(799, 12);
            this.pnlColors.Name = "pnlColors";
            this.pnlColors.Size = new System.Drawing.Size(205, 570);
            this.pnlColors.TabIndex = 28;
            // 
            // pnlControls
            // 
            this.pnlControls.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlControls.Controls.Add(this.btnCopyTerrain);
            this.pnlControls.Controls.Add(this.btnDeleteTerrain);
            this.pnlControls.Controls.Add(this.lblLastLoaded);
            this.pnlControls.Controls.Add(this.btnRewriteTerrain);
            this.pnlControls.Controls.Add(this.tbTerrain);
            this.pnlControls.Controls.Add(this.lbTerrains);
            this.pnlControls.Controls.Add(this.btnLoadTerrain);
            this.pnlControls.Controls.Add(this.btnSaveTerrain);
            this.pnlControls.Location = new System.Drawing.Point(12, 12);
            this.pnlControls.Name = "pnlControls";
            this.pnlControls.Size = new System.Drawing.Size(205, 570);
            this.pnlControls.TabIndex = 29;
            // 
            // lblLastLoaded
            // 
            this.lblLastLoaded.AutoSize = true;
            this.lblLastLoaded.Location = new System.Drawing.Point(3, 494);
            this.lblLastLoaded.Name = "lblLastLoaded";
            this.lblLastLoaded.Size = new System.Drawing.Size(35, 13);
            this.lblLastLoaded.TabIndex = 5;
            this.lblLastLoaded.Text = "label1";
            // 
            // btnRewriteTerrain
            // 
            this.btnRewriteTerrain.Location = new System.Drawing.Point(3, 353);
            this.btnRewriteTerrain.Name = "btnRewriteTerrain";
            this.btnRewriteTerrain.Size = new System.Drawing.Size(197, 23);
            this.btnRewriteTerrain.TabIndex = 4;
            this.btnRewriteTerrain.Text = "RewriteTerrain";
            this.btnRewriteTerrain.UseVisualStyleBackColor = true;
            this.btnRewriteTerrain.Click += new System.EventHandler(this.btnRewriteTerrain_Click);
            // 
            // tbTerrain
            // 
            this.tbTerrain.Location = new System.Drawing.Point(3, 327);
            this.tbTerrain.Name = "tbTerrain";
            this.tbTerrain.Size = new System.Drawing.Size(197, 20);
            this.tbTerrain.TabIndex = 3;
            // 
            // lbTerrains
            // 
            this.lbTerrains.FormattingEnabled = true;
            this.lbTerrains.Location = new System.Drawing.Point(3, 409);
            this.lbTerrains.Name = "lbTerrains";
            this.lbTerrains.Size = new System.Drawing.Size(197, 82);
            this.lbTerrains.TabIndex = 2;
            this.lbTerrains.SelectedIndexChanged += new System.EventHandler(this.lbTerrains_SelectedIndexChanged);
            // 
            // btnLoadTerrain
            // 
            this.btnLoadTerrain.Location = new System.Drawing.Point(3, 510);
            this.btnLoadTerrain.Name = "btnLoadTerrain";
            this.btnLoadTerrain.Size = new System.Drawing.Size(197, 23);
            this.btnLoadTerrain.TabIndex = 1;
            this.btnLoadTerrain.Text = "LoadTerrain";
            this.btnLoadTerrain.UseVisualStyleBackColor = true;
            this.btnLoadTerrain.Click += new System.EventHandler(this.btnLoadTerrain_Click);
            // 
            // btnSaveTerrain
            // 
            this.btnSaveTerrain.Location = new System.Drawing.Point(3, 298);
            this.btnSaveTerrain.Name = "btnSaveTerrain";
            this.btnSaveTerrain.Size = new System.Drawing.Size(197, 23);
            this.btnSaveTerrain.TabIndex = 0;
            this.btnSaveTerrain.Text = "SaveTerrain";
            this.btnSaveTerrain.UseVisualStyleBackColor = true;
            this.btnSaveTerrain.Click += new System.EventHandler(this.btnSaveTerrain_Click);
            // 
            // btnDeleteTerrain
            // 
            this.btnDeleteTerrain.Location = new System.Drawing.Point(3, 382);
            this.btnDeleteTerrain.Name = "btnDeleteTerrain";
            this.btnDeleteTerrain.Size = new System.Drawing.Size(194, 23);
            this.btnDeleteTerrain.TabIndex = 6;
            this.btnDeleteTerrain.Text = "DeleteTerrain";
            this.btnDeleteTerrain.UseVisualStyleBackColor = true;
            this.btnDeleteTerrain.Click += new System.EventHandler(this.btnDeleteTerrain_Click);
            // 
            // btnCopyTerrain
            // 
            this.btnCopyTerrain.Location = new System.Drawing.Point(3, 539);
            this.btnCopyTerrain.Name = "btnCopyTerrain";
            this.btnCopyTerrain.Size = new System.Drawing.Size(197, 23);
            this.btnCopyTerrain.TabIndex = 7;
            this.btnCopyTerrain.Text = "CopyTerrain";
            this.btnCopyTerrain.UseVisualStyleBackColor = true;
            this.btnCopyTerrain.Click += new System.EventHandler(this.btnCopyTerrain_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1014, 729);
            this.Controls.Add(this.pnlControls);
            this.Controls.Add(this.pnlColors);
            this.Controls.Add(this.pnlBaseGrid);
            this.Controls.Add(this.rtLog);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Terrain Tiler";
            this.Activated += new System.EventHandler(this.Form1_Activated);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Form1_KeyPress);
            this.pnlControls.ResumeLayout(false);
            this.pnlControls.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtLog;
        private System.Windows.Forms.Panel pnlBaseGrid;
        private System.Windows.Forms.Panel pnlColors;
        private System.Windows.Forms.Panel pnlControls;
        private System.Windows.Forms.Button btnSaveTerrain;
        private System.Windows.Forms.Button btnLoadTerrain;
        private System.Windows.Forms.TextBox tbTerrain;
        private System.Windows.Forms.ListBox lbTerrains;
        private System.Windows.Forms.Button btnRewriteTerrain;
        private System.Windows.Forms.Label lblLastLoaded;
        private System.Windows.Forms.Button btnDeleteTerrain;
        private System.Windows.Forms.Button btnCopyTerrain;
    }
}

