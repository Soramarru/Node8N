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
            lbxTask = new ListBox();
            lblTipslbxTask = new Label();
            btnCreatePipe = new Button();
            btnDelPiepline = new Button();
            btnAddTask = new Button();
            btnReload = new Button();
            SuspendLayout();
            // 
            // lblGreet
            // 
            lblGreet.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblGreet.Location = new Point(12, 9);
            lblGreet.Name = "lblGreet";
            lblGreet.Size = new Size(334, 53);
            lblGreet.TabIndex = 0;
            // 
            // lbxTask
            // 
            lbxTask.FormattingEnabled = true;
            lbxTask.Location = new Point(439, 12);
            lbxTask.Name = "lbxTask";
            lbxTask.Size = new Size(433, 424);
            lbxTask.TabIndex = 1;
            lbxTask.SelectedIndexChanged += lbxTask_SelectedIndexChanged;
            lbxTask.MouseDoubleClick += lbxTask_MouseDoubleClick;
            // 
            // lblTipslbxTask
            // 
            lblTipslbxTask.AutoSize = true;
            lblTipslbxTask.Location = new Point(439, 437);
            lblTipslbxTask.Name = "lblTipslbxTask";
            lblTipslbxTask.Size = new Size(201, 15);
            lblTipslbxTask.TabIndex = 2;
            lblTipslbxTask.Text = "Tips: double click to see description !";
            // 
            // btnCreatePipe
            // 
            btnCreatePipe.Location = new Point(12, 358);
            btnCreatePipe.Name = "btnCreatePipe";
            btnCreatePipe.Size = new Size(200, 37);
            btnCreatePipe.TabIndex = 3;
            btnCreatePipe.Text = "Create new pipeline";
            btnCreatePipe.UseVisualStyleBackColor = true;
            btnCreatePipe.Click += btnCreatePipe_Click;
            // 
            // btnDelPiepline
            // 
            btnDelPiepline.Location = new Point(218, 358);
            btnDelPiepline.Name = "btnDelPiepline";
            btnDelPiepline.Size = new Size(200, 37);
            btnDelPiepline.TabIndex = 4;
            btnDelPiepline.Text = "Delete pipeline";
            btnDelPiepline.UseVisualStyleBackColor = true;
            btnDelPiepline.Click += btnDelPiepline_Click;
            // 
            // btnAddTask
            // 
            btnAddTask.Location = new Point(122, 399);
            btnAddTask.Name = "btnAddTask";
            btnAddTask.Size = new Size(200, 37);
            btnAddTask.TabIndex = 5;
            btnAddTask.Text = "Add task to this pipeline";
            btnAddTask.UseVisualStyleBackColor = true;
            btnAddTask.Click += btnAddTask_Click;
            // 
            // btnReload
            // 
            btnReload.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnReload.Location = new Point(391, 12);
            btnReload.Margin = new Padding(0);
            btnReload.Name = "btnReload";
            btnReload.Size = new Size(45, 39);
            btnReload.TabIndex = 6;
            btnReload.Text = "🔄";
            btnReload.TextAlign = ContentAlignment.BottomRight;
            btnReload.UseVisualStyleBackColor = true;
            btnReload.Click += btnReload_Click;
            // 
            // PipeView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(884, 461);
            Controls.Add(btnReload);
            Controls.Add(btnAddTask);
            Controls.Add(btnDelPiepline);
            Controls.Add(btnCreatePipe);
            Controls.Add(lblTipslbxTask);
            Controls.Add(lbxTask);
            Controls.Add(lblGreet);
            MaximizeBox = false;
            MaximumSize = new Size(900, 500);
            MinimumSize = new Size(900, 500);
            Name = "PipeView";
            Text = "PipeView";
            Load += PipeView_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblGreet;
        private ListBox lbxTask;
        private Label lblTipslbxTask;
        private Button btnCreatePipe;
        private Button btnDelPiepline;
        private Button btnAddTask;
        private Button btnReload;
    }
}