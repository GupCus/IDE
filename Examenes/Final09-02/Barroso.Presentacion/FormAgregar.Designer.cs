namespace Barroso.Presentacion
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
            txtEmpleado = new TextBox();
            dtpFecha = new DateTimePicker();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            btnAgregar = new Button();
            txtMonto = new TextBox();
            SuspendLayout();
            // 
            // txtEmpleado
            // 
            txtEmpleado.Location = new Point(52, 152);
            txtEmpleado.Name = "txtEmpleado";
            txtEmpleado.Size = new Size(291, 27);
            txtEmpleado.TabIndex = 0;
            // 
            // dtpFecha
            // 
            dtpFecha.Location = new Point(52, 76);
            dtpFecha.Name = "dtpFecha";
            dtpFecha.Size = new Size(291, 27);
            dtpFecha.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(52, 53);
            label1.Name = "label1";
            label1.Size = new Size(47, 20);
            label1.TabIndex = 3;
            label1.Text = "Fecha";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(52, 129);
            label2.Name = "label2";
            label2.Size = new Size(77, 20);
            label2.TabIndex = 4;
            label2.Text = "Empleado";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(52, 203);
            label3.Name = "label3";
            label3.Size = new Size(53, 20);
            label3.TabIndex = 5;
            label3.Text = "Monto";
            // 
            // btnAgregar
            // 
            btnAgregar.Location = new Point(52, 290);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(291, 47);
            btnAgregar.TabIndex = 6;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = true;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // txtMonto
            // 
            txtMonto.Location = new Point(52, 226);
            txtMonto.Name = "txtMonto";
            txtMonto.Size = new Size(291, 27);
            txtMonto.TabIndex = 7;
            // 
            // FormAgregar
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(428, 366);
            Controls.Add(txtMonto);
            Controls.Add(btnAgregar);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(dtpFecha);
            Controls.Add(txtEmpleado);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormAgregar";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Agregar";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtEmpleado;
        private DateTimePicker dtpFecha;
        private Label label1;
        private Label label2;
        private Label label3;
        private Button btnAgregar;
        private TextBox txtMonto;
    }
}