namespace GourmetGo.Desktop.Forms
{
    partial class frmLOGIN
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
            txtCorreo = new TextBox();
            txtContrasena = new TextBox();
            btnLogin = new Button();
            SuspendLayout();
            // 
            // txtCorreo
            // 
            txtCorreo.Location = new Point(230, 121);
            txtCorreo.Name = "txtCorreo";
            txtCorreo.Size = new Size(290, 23);
            txtCorreo.TabIndex = 0;
            // 
            // txtContrasena
            // 
            txtContrasena.Location = new Point(230, 161);
            txtContrasena.Name = "txtContrasena";
            txtContrasena.Size = new Size(290, 23);
            txtContrasena.TabIndex = 1;
            // 
            // btnLogin
            // 
            btnLogin.Location = new Point(230, 207);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(290, 29);
            btnLogin.TabIndex = 2;
            btnLogin.Text = "button1";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += btnLogin_Click;
            // 
            // frmLOGIN
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnLogin);
            Controls.Add(txtContrasena);
            Controls.Add(txtCorreo);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MinimizeBox = false;
            Name = "frmLOGIN";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            Load += frmLOGIN_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtCorreo;
        private TextBox txtContrasena;
        private Button btnLogin;
    }
}
