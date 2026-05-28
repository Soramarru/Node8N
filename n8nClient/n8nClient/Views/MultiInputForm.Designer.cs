namespace n8nClient.Views
{
    partial class MultiInputForm
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
            btnSubmit = new Button();
            lblErrorDisplay = new Label();
            rtbInputString = new RichTextBox();
            lblTitle = new Label();
            lblSubTitle = new Label();
            SuspendLayout();
            // 
            // btnSubmit
            // 
            btnSubmit.Location = new Point(347, 126);
            btnSubmit.Name = "btnSubmit";
            btnSubmit.Size = new Size(75, 23);
            btnSubmit.TabIndex = 0;
            btnSubmit.Text = "Submit";
            btnSubmit.UseVisualStyleBackColor = true;
            btnSubmit.Click += btnSubmit_Click;
            // 
            // lblErrorDisplay
            // 
            lblErrorDisplay.ForeColor = Color.Red;
            lblErrorDisplay.Location = new Point(12, 152);
            lblErrorDisplay.Name = "lblErrorDisplay";
            lblErrorDisplay.Size = new Size(410, 50);
            lblErrorDisplay.TabIndex = 1;
            // 
            // rtbInputString
            // 
            rtbInputString.Location = new Point(12, 72);
            rtbInputString.Name = "rtbInputString";
            rtbInputString.Size = new Size(329, 77);
            rtbInputString.TabIndex = 2;
            rtbInputString.Text = "";
            // 
            // lblTitle
            // 
            lblTitle.AccessibleRole = AccessibleRole.None;
            lblTitle.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTitle.Location = new Point(12, 9);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(410, 34);
            lblTitle.TabIndex = 3;
            lblTitle.Text = "Title";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblSubTitle
            // 
            lblSubTitle.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblSubTitle.Location = new Point(12, 44);
            lblSubTitle.Name = "lblSubTitle";
            lblSubTitle.Size = new Size(410, 25);
            lblSubTitle.TabIndex = 4;
            lblSubTitle.Text = "Under title";
            lblSubTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // MultiInputForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(434, 211);
            Controls.Add(lblSubTitle);
            Controls.Add(lblTitle);
            Controls.Add(rtbInputString);
            Controls.Add(lblErrorDisplay);
            Controls.Add(btnSubmit);
            MaximizeBox = false;
            MaximumSize = new Size(450, 250);
            MinimumSize = new Size(450, 250);
            Name = "MultiInputForm";
            Text = "Form data";
            Load += MultiInputForm_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button btnSubmit;
        private Label lblErrorDisplay;
        private RichTextBox rtbInputString;
        private Label lblTitle;
        private Label lblSubTitle;
    }
}