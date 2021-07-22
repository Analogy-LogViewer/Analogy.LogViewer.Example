
namespace Analogy.LogViewer.Example.IAnalogy
{
    partial class ExampleUserControlUC
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.btnGenerator = new System.Windows.Forms.Button();
            this.btnGeneratorHide = new System.Windows.Forms.Button();
            this.btnGneratorShow = new System.Windows.Forms.Button();
            this.btnStopPlotting = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Empty label";
            // 
            // btnGenerator
            // 
            this.btnGenerator.Location = new System.Drawing.Point(12, 62);
            this.btnGenerator.Name = "btnGenerator";
            this.btnGenerator.Size = new System.Drawing.Size(94, 29);
            this.btnGenerator.TabIndex = 8;
            this.btnGenerator.Text = "create";
            this.btnGenerator.UseVisualStyleBackColor = true;
            this.btnGenerator.Click += new System.EventHandler(this.btnGenerator_Click);
            // 
            // btnGeneratorHide
            // 
            this.btnGeneratorHide.Location = new System.Drawing.Point(6, 162);
            this.btnGeneratorHide.Name = "btnGeneratorHide";
            this.btnGeneratorHide.Size = new System.Drawing.Size(94, 29);
            this.btnGeneratorHide.TabIndex = 7;
            this.btnGeneratorHide.Text = "Hide plot";
            this.btnGeneratorHide.UseVisualStyleBackColor = true;
            this.btnGeneratorHide.Click += new System.EventHandler(this.btnGeneratorHide_Click);
            // 
            // btnGneratorShow
            // 
            this.btnGneratorShow.Location = new System.Drawing.Point(12, 97);
            this.btnGneratorShow.Name = "btnGneratorShow";
            this.btnGneratorShow.Size = new System.Drawing.Size(131, 29);
            this.btnGneratorShow.TabIndex = 6;
            this.btnGneratorShow.Text = "start plotting";
            this.btnGneratorShow.UseVisualStyleBackColor = true;
            this.btnGneratorShow.Click += new System.EventHandler(this.btnGneratorShow_Click);
            // 
            // btnStopPlotting
            // 
            this.btnStopPlotting.Location = new System.Drawing.Point(12, 132);
            this.btnStopPlotting.Name = "btnStopPlotting";
            this.btnStopPlotting.Size = new System.Drawing.Size(131, 29);
            this.btnStopPlotting.TabIndex = 9;
            this.btnStopPlotting.Text = "stop plotting";
            this.btnStopPlotting.UseVisualStyleBackColor = true;
            this.btnStopPlotting.Click += new System.EventHandler(this.btnStopPlotting_Click);
            // 
            // ExampleUserControlUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnStopPlotting);
            this.Controls.Add(this.btnGenerator);
            this.Controls.Add(this.btnGeneratorHide);
            this.Controls.Add(this.btnGneratorShow);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ExampleUserControlUC";
            this.Size = new System.Drawing.Size(711, 562);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGenerator;
        private System.Windows.Forms.Button btnGeneratorHide;
        private System.Windows.Forms.Button btnGneratorShow;
        private System.Windows.Forms.Button btnStopPlotting;
    }
}
