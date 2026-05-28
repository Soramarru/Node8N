namespace n8nClient
{
    partial class AuthPage
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
            txbUsername = new TextBox();
            txbPassword = new TextBox();
            lblStaticUsername = new Label();
            lblStaticPassword = new Label();
            btnLogin = new Button();
            btnRegister = new Button();
            lblStaticLoginRegister = new Label();
            lblOutput = new Label();
            SuspendLayout();
            // 
            // txbUsername
            // 
            txbUsername.Location = new Point(351, 179);
            txbUsername.Name = "txbUsername";
            txbUsername.Size = new Size(265, 23);
            txbUsername.TabIndex = 0;
            // 
            // txbPassword
            // 
            txbPassword.Location = new Point(351, 219);
            txbPassword.Name = "txbPassword";
            txbPassword.PasswordChar = '*';
            txbPassword.Size = new Size(265, 23);
            txbPassword.TabIndex = 1;
            // 
            // lblStaticUsername
            // 
            lblStaticUsername.AutoSize = true;
            lblStaticUsername.Location = new Point(270, 182);
            lblStaticUsername.Name = "lblStaticUsername";
            lblStaticUsername.Size = new Size(60, 15);
            lblStaticUsername.TabIndex = 2;
            lblStaticUsername.Text = "Username";
            // 
            // lblStaticPassword
            // 
            lblStaticPassword.AutoSize = true;
            lblStaticPassword.Location = new Point(270, 222);
            lblStaticPassword.Name = "lblStaticPassword";
            lblStaticPassword.Size = new Size(57, 15);
            lblStaticPassword.TabIndex = 3;
            lblStaticPassword.Text = "Password";
            // 
            // btnLogin
            // 
            btnLogin.Location = new Point(270, 288);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(143, 37);
            btnLogin.TabIndex = 4;
            btnLogin.Text = "Login";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += btnLogin_Click;
            // 
            // btnRegister
            // 
            btnRegister.Location = new Point(473, 288);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(143, 37);
            btnRegister.TabIndex = 5;
            btnRegister.Text = "Register";
            btnRegister.UseVisualStyleBackColor = true;
            btnRegister.Click += btnRegister_Click;
            // 
            // lblStaticLoginRegister
            // 
            lblStaticLoginRegister.AutoSize = true;
            lblStaticLoginRegister.Font = new Font("Segoe UI", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblStaticLoginRegister.Location = new Point(351, 62);
            lblStaticLoginRegister.Name = "lblStaticLoginRegister";
            lblStaticLoginRegister.Size = new Size(226, 45);
            lblStaticLoginRegister.TabIndex = 6;
            lblStaticLoginRegister.Text = "Login/Register";
            // 
            // lblOutput
            // 
            lblOutput.Location = new Point(270, 347);
            lblOutput.Name = "lblOutput";
            lblOutput.Size = new Size(346, 105);
            lblOutput.TabIndex = 7;
            // 
            // AuthPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(884, 461);
            Controls.Add(lblOutput);
            Controls.Add(lblStaticLoginRegister);
            Controls.Add(btnRegister);
            Controls.Add(btnLogin);
            Controls.Add(lblStaticPassword);
            Controls.Add(lblStaticUsername);
            Controls.Add(txbPassword);
            Controls.Add(txbUsername);
            MaximizeBox = false;
            MaximumSize = new Size(900, 500);
            MinimumSize = new Size(900, 500);
            Name = "AuthPage";
            Text = "Authentication";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txbUsername;
        private TextBox txbPassword;
        private Label lblStaticUsername;
        private Label lblStaticPassword;
        private Button btnLogin;
        private Button btnRegister;
        private Label lblStaticLoginRegister;
        private Label lblOutput;
    }
}
