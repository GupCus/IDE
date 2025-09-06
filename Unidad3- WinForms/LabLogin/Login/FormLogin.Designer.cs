namespace Login
{
    partial class FormLogin
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
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            txtPass = new TextBox();
            txtUsuario = new TextBox();
            btnIngresar = new Button();
            linkOlvidaPass = new LinkLabel();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(150, 34);
            label1.Name = "label1";
            label1.Size = new Size(287, 40);
            label1.TabIndex = 0;
            label1.Text = "¡Bienvenido al Sistema!\r\nPor favor digite su información de Ingreso";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(175, 112);
            label2.Name = "label2";
            label2.Size = new Size(142, 20);
            label2.TabIndex = 1;
            label2.Text = "Nombre de Usuario:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(231, 168);
            label3.Name = "label3";
            label3.Size = new Size(86, 20);
            label3.TabIndex = 2;
            label3.Text = "Contraseña:";
            // 
            // txtPass
            // 
            txtPass.Location = new Point(323, 165);
            txtPass.Name = "txtPass";
            txtPass.PasswordChar = '*';
            txtPass.PlaceholderText = "********";
            txtPass.Size = new Size(132, 27);
            txtPass.TabIndex = 3;
            // 
            // txtUsuario
            // 
            txtUsuario.Location = new Point(323, 109);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.PlaceholderText = "MiUsuario";
            txtUsuario.Size = new Size(132, 27);
            txtUsuario.TabIndex = 4;
            // 
            // btnIngresar
            // 
            btnIngresar.Location = new Point(374, 223);
            btnIngresar.Name = "btnIngresar";
            btnIngresar.Size = new Size(101, 30);
            btnIngresar.TabIndex = 5;
            btnIngresar.Text = "Ingresar";
            btnIngresar.UseVisualStyleBackColor = true;
            btnIngresar.Click += btnIngresar_Click;
            // 
            // linkOlvidaPass
            // 
            linkOlvidaPass.AutoSize = true;
            linkOlvidaPass.Location = new Point(219, 227);
            linkOlvidaPass.Name = "linkOlvidaPass";
            linkOlvidaPass.Size = new Size(149, 20);
            linkOlvidaPass.TabIndex = 6;
            linkOlvidaPass.TabStop = true;
            linkOlvidaPass.Text = "Olvidé mi contraseña";
            linkOlvidaPass.Click += linkOlvidaPass_Click;
            // 
            // FormLogin
            // 
            AcceptButton = btnIngresar;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(626, 325);
            Controls.Add(linkOlvidaPass);
            Controls.Add(btnIngresar);
            Controls.Add(txtUsuario);
            Controls.Add(txtPass);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormLogin";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Ingreso";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox txtPass;
        private TextBox txtUsuario;
        private Button btnIngresar;
        private LinkLabel linkOlvidaPass;
    }
}
