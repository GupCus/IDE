namespace WindowsForms
{
    partial class ClienteDetalle
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
            components = new System.ComponentModel.Container();
            nombreTextBox = new TextBox();
            nombreLabel = new Label();
            aceptarButton = new Button();
            errorProvider = new ErrorProvider(components);
            cancelarButton = new Button();
            apellidoLabel = new Label();
            apellidoTextBox = new TextBox();
            emailLabel = new Label();
            emailTextBox = new TextBox();
            paisLabel = new Label();
            paisComboBox = new ComboBox();
            fechaAltaLabel = new Label();
            fechaAltaTextBox = new TextBox();
            idLabel = new Label();
            idTextBox = new TextBox();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            SuspendLayout();
            // 
            // nombreTextBox
            // 
            nombreTextBox.Location = new Point(243, 109);
            nombreTextBox.Name = "nombreTextBox";
            nombreTextBox.Size = new Size(200, 39);
            nombreTextBox.TabIndex = 0;
            // 
            // nombreLabel
            // 
            nombreLabel.AutoSize = true;
            nombreLabel.Location = new Point(44, 109);
            nombreLabel.Name = "nombreLabel";
            nombreLabel.Size = new Size(102, 32);
            nombreLabel.TabIndex = 1;
            nombreLabel.Text = "Nombre";
            // 
            // aceptarButton
            // 
            aceptarButton.Location = new Point(444, 480);
            aceptarButton.Name = "aceptarButton";
            aceptarButton.Size = new Size(150, 46);
            aceptarButton.TabIndex = 2;
            aceptarButton.Text = "Aceptar";
            aceptarButton.UseVisualStyleBackColor = true;
            aceptarButton.Click += aceptarButton_Click;
            // 
            // errorProvider
            // 
            errorProvider.ContainerControl = this;
            // 
            // cancelarButton
            // 
            cancelarButton.Location = new Point(614, 480);
            cancelarButton.Name = "cancelarButton";
            cancelarButton.Size = new Size(150, 46);
            cancelarButton.TabIndex = 3;
            cancelarButton.Text = "Cancelar";
            cancelarButton.UseVisualStyleBackColor = true;
            cancelarButton.Click += cancelarButton_Click;
            // 
            // apellidoLabel
            // 
            apellidoLabel.AutoSize = true;
            apellidoLabel.Location = new Point(44, 176);
            apellidoLabel.Name = "apellidoLabel";
            apellidoLabel.Size = new Size(102, 32);
            apellidoLabel.TabIndex = 5;
            apellidoLabel.Text = "Apellido";
            // 
            // apellidoTextBox
            // 
            apellidoTextBox.Location = new Point(243, 176);
            apellidoTextBox.Name = "apellidoTextBox";
            apellidoTextBox.Size = new Size(200, 39);
            apellidoTextBox.TabIndex = 1;
            // 
            // emailLabel
            // 
            emailLabel.AutoSize = true;
            emailLabel.Location = new Point(44, 253);
            emailLabel.Name = "emailLabel";
            emailLabel.Size = new Size(71, 32);
            emailLabel.TabIndex = 7;
            emailLabel.Text = "Email";
            // 
            // emailTextBox
            // 
            emailTextBox.Location = new Point(243, 253);
            emailTextBox.Name = "emailTextBox";
            emailTextBox.Size = new Size(380, 39);
            emailTextBox.TabIndex = 2;
            // 
            // paisLabel
            // 
            paisLabel.AutoSize = true;
            paisLabel.Location = new Point(44, 320);
            paisLabel.Name = "paisLabel";
            paisLabel.Size = new Size(54, 32);
            paisLabel.TabIndex = 13;
            paisLabel.Text = "País";
            // 
            // paisComboBox
            // 
            paisComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            paisComboBox.FormattingEnabled = true;
            paisComboBox.Location = new Point(243, 320);
            paisComboBox.Name = "paisComboBox";
            paisComboBox.Size = new Size(300, 40);
            paisComboBox.TabIndex = 3;
            // 
            // fechaAltaLabel
            // 
            fechaAltaLabel.AutoSize = true;
            fechaAltaLabel.Location = new Point(44, 387);
            fechaAltaLabel.Name = "fechaAltaLabel";
            fechaAltaLabel.Size = new Size(124, 32);
            fechaAltaLabel.TabIndex = 9;
            fechaAltaLabel.Text = "Fecha Alta";
            // 
            // fechaAltaTextBox
            // 
            fechaAltaTextBox.Location = new Point(243, 387);
            fechaAltaTextBox.Name = "fechaAltaTextBox";
            fechaAltaTextBox.ReadOnly = true;
            fechaAltaTextBox.Size = new Size(200, 39);
            fechaAltaTextBox.TabIndex = 0;
            fechaAltaTextBox.TabStop = false;
            // 
            // idLabel
            // 
            idLabel.AutoSize = true;
            idLabel.Location = new Point(44, 33);
            idLabel.Name = "idLabel";
            idLabel.Size = new Size(34, 32);
            idLabel.TabIndex = 11;
            idLabel.Text = "Id";
            // 
            // idTextBox
            // 
            idTextBox.Location = new Point(243, 33);
            idTextBox.Name = "idTextBox";
            idTextBox.ReadOnly = true;
            idTextBox.Size = new Size(200, 39);
            idTextBox.TabIndex = 0;
            idTextBox.TabStop = false;
            // 
            // ClienteDetalle
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 570);
            Controls.Add(idLabel);
            Controls.Add(idTextBox);
            Controls.Add(fechaAltaLabel);
            Controls.Add(fechaAltaTextBox);
            Controls.Add(paisLabel);
            Controls.Add(paisComboBox);
            Controls.Add(emailLabel);
            Controls.Add(emailTextBox);
            Controls.Add(apellidoLabel);
            Controls.Add(apellidoTextBox);
            Controls.Add(cancelarButton);
            Controls.Add(aceptarButton);
            Controls.Add(nombreLabel);
            Controls.Add(nombreTextBox);
            Name = "ClienteDetalle";
            Text = "Cliente";
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox nombreTextBox;
        private Label nombreLabel;
        private Button aceptarButton;
        private ErrorProvider errorProvider;
        private Button cancelarButton;
        private Label apellidoLabel;
        private TextBox apellidoTextBox;
        private Label paisLabel;
        private ComboBox paisComboBox;
        private Label fechaAltaLabel;
        private TextBox fechaAltaTextBox;
        private Label emailLabel;
        private TextBox emailTextBox;
        private Label idLabel;
        private TextBox idTextBox;
    }
}