namespace Login
{
    partial class FormAgregar
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
            nombre = new TextBox();
            apellido = new TextBox();
            usuario = new TextBox();
            email = new TextBox();
            btnAgregar = new Button();
            habilitado = new CheckBox();
            labelForm = new Label();
            SuspendLayout();
            // 
            // nombre
            // 
            nombre.Location = new Point(24, 58);
            nombre.Name = "nombre";
            nombre.PlaceholderText = "Nombre";
            nombre.Size = new Size(125, 27);
            nombre.TabIndex = 0;
            // 
            // apellido
            // 
            apellido.Location = new Point(172, 58);
            apellido.Name = "apellido";
            apellido.PlaceholderText = "Apellido";
            apellido.Size = new Size(125, 27);
            apellido.TabIndex = 1;
            // 
            // usuario
            // 
            usuario.Location = new Point(313, 58);
            usuario.Name = "usuario";
            usuario.PlaceholderText = "Usuario";
            usuario.Size = new Size(125, 27);
            usuario.TabIndex = 2;
            // 
            // email
            // 
            email.Location = new Point(457, 58);
            email.Name = "email";
            email.PlaceholderText = "Email";
            email.Size = new Size(125, 27);
            email.TabIndex = 3;
            // 
            // btnAgregar
            // 
            btnAgregar.Location = new Point(631, 107);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(94, 29);
            btnAgregar.TabIndex = 4;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = true;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // habilitado
            // 
            habilitado.AutoSize = true;
            habilitado.Location = new Point(588, 61);
            habilitado.Name = "habilitado";
            habilitado.Size = new Size(137, 24);
            habilitado.TabIndex = 5;
            habilitado.Text = "Está habilitado?";
            habilitado.UseVisualStyleBackColor = true;
            // 
            // labelForm
            // 
            labelForm.AutoSize = true;
            labelForm.Font = new Font("Segoe UI Semibold", 16.2F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            labelForm.Location = new Point(24, 9);
            labelForm.Name = "labelForm";
            labelForm.Size = new Size(263, 38);
            labelForm.TabIndex = 6;
            labelForm.Text = "Agregar un usuario";
            // 
            // FormAgregar
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(761, 163);
            Controls.Add(labelForm);
            Controls.Add(habilitado);
            Controls.Add(btnAgregar);
            Controls.Add(email);
            Controls.Add(usuario);
            Controls.Add(apellido);
            Controls.Add(nombre);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormAgregar";
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Agregar Usuario";
            Load += FormAgregar_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox nombre;
        private TextBox apellido;
        private TextBox usuario;
        private TextBox email;
        private Button btnAgregar;
        private CheckBox habilitado;
        private Label labelForm;
    }
}