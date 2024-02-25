namespace WikipediaWalkerWinFormsDevExpressApp
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            resultLabel = new DevExpress.XtraEditors.LabelControl();
            infoLabel = new DevExpress.XtraEditors.LabelControl();
            startArticleField = new DevExpress.XtraEditors.TextEdit();
            endArticleField = new DevExpress.XtraEditors.TextEdit();
            reverseButton = new DevExpress.XtraEditors.SimpleButton();
            findPathButton = new DevExpress.XtraEditors.SimpleButton();
            saveToFileButton = new DevExpress.XtraEditors.SimpleButton();
            graphVisualizer = new DevExpress.XtraDiagram.DiagramControl();
            ((System.ComponentModel.ISupportInitialize)startArticleField.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)endArticleField.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)graphVisualizer).BeginInit();
            SuspendLayout();
            // 
            // resultLabel
            // 
            resultLabel.Appearance.Font = new System.Drawing.Font("Franklin Gothic Medium Cond", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            resultLabel.Appearance.Options.UseFont = true;
            resultLabel.Location = new System.Drawing.Point(12, 325);
            resultLabel.Name = "resultLabel";
            resultLabel.Size = new System.Drawing.Size(0, 47);
            resultLabel.TabIndex = 0;
            // 
            // infoLabel
            // 
            infoLabel.Appearance.Font = new System.Drawing.Font("Franklin Gothic Medium Cond", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            infoLabel.Appearance.Options.UseFont = true;
            infoLabel.Location = new System.Drawing.Point(397, 42);
            infoLabel.Name = "infoLabel";
            infoLabel.Size = new System.Drawing.Size(352, 47);
            infoLabel.TabIndex = 1;
            infoLabel.Text = "Find shortest paths from";
            // 
            // startArticleField
            // 
            startArticleField.Location = new System.Drawing.Point(207, 166);
            startArticleField.Name = "startArticleField";
            startArticleField.Properties.Appearance.Font = new System.Drawing.Font("Franklin Gothic Medium Cond", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            startArticleField.Properties.Appearance.Options.UseFont = true;
            startArticleField.Size = new System.Drawing.Size(249, 54);
            startArticleField.TabIndex = 2;
            // 
            // endArticleField
            // 
            endArticleField.Location = new System.Drawing.Point(708, 166);
            endArticleField.Name = "endArticleField";
            endArticleField.Properties.Appearance.Font = new System.Drawing.Font("Franklin Gothic Medium Cond", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            endArticleField.Properties.Appearance.Options.UseFont = true;
            endArticleField.Size = new System.Drawing.Size(225, 54);
            endArticleField.TabIndex = 3;
            // 
            // reverseButton
            // 
            reverseButton.Appearance.Font = new System.Drawing.Font("Franklin Gothic Medium Cond", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            reverseButton.Appearance.Options.UseFont = true;
            reverseButton.Location = new System.Drawing.Point(503, 169);
            reverseButton.Name = "reverseButton";
            reverseButton.Size = new System.Drawing.Size(168, 51);
            reverseButton.TabIndex = 4;
            reverseButton.Text = "↔";
            reverseButton.Click += reverseButton_Click;
            // 
            // findPathButton
            // 
            findPathButton.Appearance.Font = new System.Drawing.Font("Franklin Gothic Medium Cond", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            findPathButton.Appearance.Options.UseFont = true;
            findPathButton.Location = new System.Drawing.Point(503, 269);
            findPathButton.Name = "findPathButton";
            findPathButton.Size = new System.Drawing.Size(168, 51);
            findPathButton.TabIndex = 5;
            findPathButton.Text = "Go!";
            findPathButton.Click += findPathButton_Click;
            // 
            // saveToFileButton
            // 
            saveToFileButton.Appearance.Font = new System.Drawing.Font("Franklin Gothic Medium Cond", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            saveToFileButton.Appearance.Options.UseFont = true;
            saveToFileButton.Location = new System.Drawing.Point(417, 516);
            saveToFileButton.Name = "saveToFileButton";
            saveToFileButton.Size = new System.Drawing.Size(329, 51);
            saveToFileButton.TabIndex = 6;
            saveToFileButton.Text = "Save result";
            saveToFileButton.Visible = false;
            saveToFileButton.Click += saveToFileButton_Click;
            // 
            // graphVisualizer
            // 
            graphVisualizer.Location = new System.Drawing.Point(52, 592);
            graphVisualizer.Name = "graphVisualizer";
            graphVisualizer.OptionsBehavior.SelectedStencils = new DevExpress.Diagram.Core.StencilCollection(new string[] { "BasicShapes", "BasicFlowchartShapes" });
            graphVisualizer.OptionsView.CanvasSizeMode = DevExpress.Diagram.Core.CanvasSizeMode.Fill;
            graphVisualizer.OptionsView.PageSize = new System.Drawing.SizeF(1123F, 794F);
            graphVisualizer.OptionsView.PaperKind = System.Drawing.Printing.PaperKind.A4;
            graphVisualizer.OptionsView.Theme = DevExpress.Diagram.Core.DiagramThemes.Office;
            graphVisualizer.Size = new System.Drawing.Size(1042, 396);
            graphVisualizer.TabIndex = 7;
            graphVisualizer.Text = "diagramControl1";
            graphVisualizer.Visible = false;
            // 
            // MainForm
            // 
            Appearance.BackColor = System.Drawing.Color.FromArgb(192, 255, 255);
            Appearance.Options.UseBackColor = true;
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            ClientSize = new System.Drawing.Size(1106, 1000);
            Controls.Add(graphVisualizer);
            Controls.Add(saveToFileButton);
            Controls.Add(findPathButton);
            Controls.Add(reverseButton);
            Controls.Add(endArticleField);
            Controls.Add(startArticleField);
            Controls.Add(infoLabel);
            Controls.Add(resultLabel);
            Margin = new System.Windows.Forms.Padding(4);
            Name = "MainForm";
            Text = "WikipediaWalker";
            ((System.ComponentModel.ISupportInitialize)startArticleField.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)endArticleField.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)graphVisualizer).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DevExpress.XtraEditors.LabelControl resultLabel;
        private DevExpress.XtraEditors.LabelControl infoLabel;
        private DevExpress.XtraEditors.TextEdit startArticleField;
        private DevExpress.XtraEditors.TextEdit endArticleField;
        private DevExpress.XtraEditors.SimpleButton reverseButton;
        private DevExpress.XtraEditors.SimpleButton findPathButton;
        private DevExpress.XtraEditors.SimpleButton saveToFileButton;
        private DevExpress.XtraDiagram.DiagramControl graphVisualizer;
    }
}

