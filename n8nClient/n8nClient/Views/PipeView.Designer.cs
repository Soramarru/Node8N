namespace n8nClient.Views
{
    partial class PipeView
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
            lblGreet = new Label();
            SuspendLayout();
            // 
            // lblGreet
            // 
            lblGreet.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblGreet.Location = new Point(12, 9);
            lblGreet.Name = "lblGreet";
            lblGreet.Size = new Size(334, 53);
            lblGreet.TabIndex = 0;
            lblGreet.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // PipeView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(884, 461);
            Controls.Add(lblGreet);
            Name = "PipeView";
            Text = "PipeView";
            Load += PipeView_Load;
            ResumeLayout(false);
        }

        #endregion

        private Label lblGreet;
    }
}