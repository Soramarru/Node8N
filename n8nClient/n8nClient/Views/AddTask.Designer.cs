namespace n8nClient.Views
{
    partial class AddTask
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
            lblEdit = new Label();
            lblTipslbxTask = new Label();
            lbxTasker = new ListBox();
            btnSwitchTo = new Button();
            btnAddToPipe = new Button();
            lbxCurrentTask = new ListBox();
            SuspendLayout();
            // 
            // lblEdit
            // 
            lblEdit.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblEdit.Location = new Point(12, 5);
            lblEdit.Name = "lblEdit";
            lblEdit.Size = new Size(334, 53);
            lblEdit.TabIndex = 1;
            lblEdit.Text = "Editing";
            // 
            // lblTipslbxTask
            // 
            lblTipslbxTask.AutoSize = true;
            lblTipslbxTask.Location = new Point(739, 433);
            lblTipslbxTask.Name = "lblTipslbxTask";
            lblTipslbxTask.Size = new Size(201, 15);
            lblTipslbxTask.TabIndex = 4;
            lblTipslbxTask.Text = "Tips: double click to see description !";
            // 
            // lbxTasker
            // 
            lbxTasker.FormattingEnabled = true;
            lbxTasker.Location = new Point(739, 38);
            lbxTasker.Name = "lbxTasker";
            lbxTasker.Size = new Size(433, 394);
            lbxTasker.TabIndex = 3;
            lbxTasker.DoubleClick += lbxTasker_DoubleClick;
            // 
            // btnSwitchTo
            // 
            btnSwitchTo.Location = new Point(739, 5);
            btnSwitchTo.Name = "btnSwitchTo";
            btnSwitchTo.Size = new Size(169, 23);
            btnSwitchTo.TabIndex = 5;
            btnSwitchTo.Text = "Switch to conditional block";
            btnSwitchTo.UseVisualStyleBackColor = true;
            btnSwitchTo.Click += btnSwitchTo_Click;
            // 
            // btnAddToPipe
            // 
            btnAddToPipe.Location = new Point(533, 395);
            btnAddToPipe.Name = "btnAddToPipe";
            btnAddToPipe.Size = new Size(200, 37);
            btnAddToPipe.TabIndex = 6;
            btnAddToPipe.Text = "Add to the pipeline";
            btnAddToPipe.UseVisualStyleBackColor = true;
            btnAddToPipe.Click += btnAddToPipe_Click;
            // 
            // lbxCurrentTask
            // 
            lbxCurrentTask.FormattingEnabled = true;
            lbxCurrentTask.Location = new Point(12, 61);
            lbxCurrentTask.Name = "lbxCurrentTask";
            lbxCurrentTask.Size = new Size(381, 379);
            lbxCurrentTask.TabIndex = 8;
            lbxCurrentTask.DoubleClick += lbxCurrentTask_DoubleClick;
            // 
            // AddTask
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1184, 461);
            Controls.Add(lbxCurrentTask);
            Controls.Add(btnAddToPipe);
            Controls.Add(btnSwitchTo);
            Controls.Add(lblTipslbxTask);
            Controls.Add(lbxTasker);
            Controls.Add(lblEdit);
            MaximizeBox = false;
            MaximumSize = new Size(1200, 500);
            MinimumSize = new Size(1200, 500);
            Name = "AddTask";
            SizeGripStyle = SizeGripStyle.Hide;
            Text = "AddTask";
            Load += AddTask_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblEdit;
        private Label lblTipslbxTask;
        private ListBox lbxTasker;
        private Button btnSwitchTo;
        private Button btnAddToPipe;
        private ListBox lbxCurrentTask;
    }
}