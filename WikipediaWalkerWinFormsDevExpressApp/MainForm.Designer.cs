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
            distanceChooseRadioGroup = new DevExpress.XtraEditors.RadioGroup();
            distanceNumberField = new DevExpress.XtraEditors.TextEdit();
            infoLabel2 = new DevExpress.XtraEditors.LabelControl();
            infoLabel3 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)startArticleField.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)endArticleField.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)graphVisualizer).BeginInit();
            ((System.ComponentModel.ISupportInitialize)distanceChooseRadioGroup.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)distanceNumberField.Properties).BeginInit();
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
            infoLabel.Location = new System.Drawing.Point(224, 38);
            infoLabel.Name = "infoLabel";
            infoLabel.Size = new System.Drawing.Size(352, 47);
            infoLabel.TabIndex = 1;
            infoLabel.Text = "Find shortest paths from";
            // 
            // startArticleField
            // 
            startArticleField.Location = new System.Drawing.Point(34, 162);
            startArticleField.Name = "startArticleField";
            startArticleField.Properties.Appearance.Font = new System.Drawing.Font("Franklin Gothic Medium Cond", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            startArticleField.Properties.Appearance.Options.UseFont = true;
            startArticleField.Size = new System.Drawing.Size(249, 54);
            startArticleField.TabIndex = 2;
            // 
            // endArticleField
            // 
            endArticleField.Location = new System.Drawing.Point(535, 162);
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
            reverseButton.Location = new System.Drawing.Point(330, 165);
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
            findPathButton.Location = new System.Drawing.Point(330, 265);
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
            saveToFileButton.Location = new System.Drawing.Point(256, 804);
            saveToFileButton.Name = "saveToFileButton";
            saveToFileButton.Size = new System.Drawing.Size(329, 51);
            saveToFileButton.TabIndex = 6;
            saveToFileButton.Text = "Save result";
            saveToFileButton.Visible = false;
            saveToFileButton.Click += saveToFileButton_Click;
            // 
            // graphVisualizer
            // 
            graphVisualizer.Appearance.HRuler.Font = new System.Drawing.Font("Times New Roman", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            graphVisualizer.Location = new System.Drawing.Point(791, 25);
            graphVisualizer.Name = "graphVisualizer";
            graphVisualizer.OptionsBehavior.SelectedStencils = new DevExpress.Diagram.Core.StencilCollection(new string[] { "BasicShapes", "BasicFlowchartShapes" });
            graphVisualizer.OptionsView.PageSize = new System.Drawing.SizeF(1123F, 794F);
            graphVisualizer.OptionsView.PaperKind = System.Drawing.Printing.PaperKind.A4;
            graphVisualizer.OptionsView.ShowGrid = false;
            graphVisualizer.OptionsView.ShowPageBreaks = false;
            graphVisualizer.OptionsView.ShowPanAndZoomPanel = true;
            graphVisualizer.OptionsView.ShowRulers = false;
            graphVisualizer.OptionsView.Theme = DevExpress.Diagram.Core.DiagramThemes.Office;
            graphVisualizer.Size = new System.Drawing.Size(965, 830);
            graphVisualizer.TabIndex = 7;
            graphVisualizer.Text = "diagramControl1";
            graphVisualizer.Visible = false;
            // 
            // distanceChooseRadioGroup
            // 
            distanceChooseRadioGroup.Location = new System.Drawing.Point(243, 482);
            distanceChooseRadioGroup.Name = "distanceChooseRadioGroup";
            distanceChooseRadioGroup.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            distanceChooseRadioGroup.Properties.Appearance.Options.UseFont = true;
            distanceChooseRadioGroup.Properties.Columns = 2;
            distanceChooseRadioGroup.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] { new DevExpress.XtraEditors.Controls.RadioGroupItem(1, "1", true, null, ""), new DevExpress.XtraEditors.Controls.RadioGroupItem(2, "2", true, null, ""), new DevExpress.XtraEditors.Controls.RadioGroupItem(3, "3", true, null, ""), new DevExpress.XtraEditors.Controls.RadioGroupItem("All", "All", true, null, "") });
            distanceChooseRadioGroup.Size = new System.Drawing.Size(382, 87);
            distanceChooseRadioGroup.TabIndex = 8;
            distanceChooseRadioGroup.SelectedIndexChanged += distanceChooseRadioGroup_SelectedIndexChanged;
            // 
            // distanceNumberField
            // 
            distanceNumberField.Location = new System.Drawing.Point(240, 670);
            distanceNumberField.Name = "distanceNumberField";
            distanceNumberField.Properties.Appearance.Font = new System.Drawing.Font("Franklin Gothic Medium Cond", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            distanceNumberField.Properties.Appearance.Options.UseFont = true;
            distanceNumberField.Size = new System.Drawing.Size(382, 54);
            distanceNumberField.TabIndex = 9;
            // 
            // infoLabel2
            // 
            infoLabel2.Appearance.Font = new System.Drawing.Font("Franklin Gothic Medium Cond", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            infoLabel2.Appearance.Options.UseFont = true;
            infoLabel2.Location = new System.Drawing.Point(243, 407);
            infoLabel2.Name = "infoLabel2";
            infoLabel2.Size = new System.Drawing.Size(309, 47);
            infoLabel2.TabIndex = 10;
            infoLabel2.Text = "Number of distances:";
            // 
            // infoLabel3
            // 
            infoLabel3.Appearance.Font = new System.Drawing.Font("Franklin Gothic Medium Cond", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            infoLabel3.Appearance.Options.UseFont = true;
            infoLabel3.Location = new System.Drawing.Point(243, 590);
            infoLabel3.Name = "infoLabel3";
            infoLabel3.Size = new System.Drawing.Size(219, 47);
            infoLabel3.TabIndex = 11;
            infoLabel3.Text = "Print max path:";
            // 
            // MainForm
            // 
            Appearance.BackColor = System.Drawing.Color.FromArgb(192, 255, 255);
            Appearance.Options.UseBackColor = true;
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            ClientSize = new System.Drawing.Size(1785, 891);
            Controls.Add(infoLabel3);
            Controls.Add(infoLabel2);
            Controls.Add(distanceNumberField);
            Controls.Add(distanceChooseRadioGroup);
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
            ((System.ComponentModel.ISupportInitialize)distanceChooseRadioGroup.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)distanceNumberField.Properties).EndInit();
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
        private DevExpress.XtraEditors.RadioGroup distanceChooseRadioGroup;
        private DevExpress.XtraEditors.TextEdit distanceNumberField;
        private DevExpress.XtraEditors.LabelControl infoLabel2;
        private DevExpress.XtraEditors.LabelControl infoLabel3;
    }
}

