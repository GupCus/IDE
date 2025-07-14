namespace Escritorio
{
    partial class Form1
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
            dgvAlumnos = new DataGridView();
            Cargar = new Button();
            label1 = new Label();
            label2 = new Label();
            txtNombre = new TextBox();
            txtApellido = new TextBox();
            label3 = new Label();
            txtLegajo = new TextBox();
            label4 = new Label();
            txtDireccion = new TextBox();
            label5 = new Label();
            label6 = new Label();
            txtID = new Label();
            Modificar = new Button();
            label7 = new Label();
            Eliminar = new Button();
            Buscar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvAlumnos).BeginInit();
            SuspendLayout();
            // 
            // dgvAlumnos
            // 
            dgvAlumnos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAlumnos.Location = new Point(12, 64);
            dgvAlumnos.Name = "dgvAlumnos";
            dgvAlumnos.ReadOnly = true;
            dgvAlumnos.RowHeadersWidth = 51;
            dgvAlumnos.Size = new Size(813, 242);
            dgvAlumnos.TabIndex = 0;
            dgvAlumnos.SelectionChanged += dgvAlumnos_SelectionChanged;
            // 
            // Cargar
            // 
            Cargar.Location = new Point(12, 451);
            Cargar.Name = "Cargar";
            Cargar.Size = new Size(247, 29);
            Cargar.TabIndex = 1;
            Cargar.Text = "Cargar Nuevo";
            Cargar.UseVisualStyleBackColor = true;
            Cargar.Click += Cargar_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Enabled = false;
            label1.Font = new Font("Segoe UI", 25F);
            label1.Location = new Point(327, 4);
            label1.Name = "label1";
            label1.Size = new Size(189, 57);
            label1.TabIndex = 2;
            label1.Text = "Alumnos";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(23, 342);
            label2.Name = "label2";
            label2.Size = new Size(64, 20);
            label2.TabIndex = 3;
            label2.Text = "Nombre";
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(12, 365);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(247, 27);
            txtNombre.TabIndex = 4;
            txtNombre.DoubleClick += Txt_Click;
            // 
            // txtApellido
            // 
            txtApellido.Location = new Point(284, 365);
            txtApellido.Name = "txtApellido";
            txtApellido.Size = new Size(247, 27);
            txtApellido.TabIndex = 6;
            txtApellido.DoubleClick += Txt_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(295, 342);
            label3.Name = "label3";
            label3.Size = new Size(66, 20);
            label3.TabIndex = 5;
            label3.Text = "Apellido";
            // 
            // txtLegajo
            // 
            txtLegajo.Location = new Point(12, 418);
            txtLegajo.Name = "txtLegajo";
            txtLegajo.Size = new Size(247, 27);
            txtLegajo.TabIndex = 8;
            txtLegajo.DoubleClick += Txt_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(23, 395);
            label4.Name = "label4";
            label4.Size = new Size(54, 20);
            label4.TabIndex = 7;
            label4.Text = "Legajo";
            // 
            // txtDireccion
            // 
            txtDireccion.Location = new Point(284, 418);
            txtDireccion.Name = "txtDireccion";
            txtDireccion.Size = new Size(247, 27);
            txtDireccion.TabIndex = 10;
            txtDireccion.DoubleClick += Txt_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(295, 395);
            label5.Name = "label5";
            label5.Size = new Size(72, 20);
            label5.TabIndex = 9;
            label5.Text = "Dirección";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.Location = new Point(12, 309);
            label6.Name = "label6";
            label6.Size = new Size(84, 28);
            label6.TabIndex = 11;
            label6.Text = "Alumno";
            // 
            // txtID
            // 
            txtID.AutoSize = true;
            txtID.Location = new Point(129, 317);
            txtID.Name = "txtID";
            txtID.Size = new Size(0, 20);
            txtID.TabIndex = 12;
            // 
            // Modificar
            // 
            Modificar.Enabled = false;
            Modificar.Location = new Point(284, 451);
            Modificar.Name = "Modificar";
            Modificar.Size = new Size(247, 29);
            Modificar.TabIndex = 13;
            Modificar.Text = "Modificar";
            Modificar.UseVisualStyleBackColor = true;
            Modificar.Click += Modificar_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(102, 317);
            label7.Name = "label7";
            label7.Size = new Size(31, 20);
            label7.TabIndex = 14;
            label7.Text = "ID: ";
            // 
            // Eliminar
            // 
            Eliminar.BackColor = Color.White;
            Eliminar.Enabled = false;
            Eliminar.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Eliminar.ForeColor = SystemColors.MenuText;
            Eliminar.Location = new Point(549, 408);
            Eliminar.Name = "Eliminar";
            Eliminar.Size = new Size(276, 62);
            Eliminar.TabIndex = 15;
            Eliminar.Text = "ELIMINAR ALUMNO";
            Eliminar.UseVisualStyleBackColor = false;
            Eliminar.Click += Eliminar_Click;
            // 
            // Buscar
            // 
            Buscar.BackColor = Color.White;
            Buscar.Enabled = false;
            Buscar.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Buscar.ForeColor = SystemColors.MenuText;
            Buscar.Location = new Point(549, 340);
            Buscar.Name = "Buscar";
            Buscar.Size = new Size(276, 62);
            Buscar.TabIndex = 16;
            Buscar.Text = "Buscar Alumno";
            Buscar.UseVisualStyleBackColor = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(837, 492);
            Controls.Add(Buscar);
            Controls.Add(Eliminar);
            Controls.Add(label7);
            Controls.Add(Modificar);
            Controls.Add(txtID);
            Controls.Add(label6);
            Controls.Add(txtDireccion);
            Controls.Add(label5);
            Controls.Add(txtLegajo);
            Controls.Add(label4);
            Controls.Add(txtApellido);
            Controls.Add(label3);
            Controls.Add(txtNombre);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(Cargar);
            Controls.Add(dgvAlumnos);
            Name = "Form1";
            Text = "Gestión Alumnos";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dgvAlumnos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvAlumnos;
        private Button Cargar;
        private Label label1;
        private Label label2;
        private TextBox txtNombre;
        private TextBox txtApellido;
        private Label label3;
        private TextBox txtLegajo;
        private Label label4;
        private TextBox txtDireccion;
        private Label label5;
        private Label label6;
        private Label txtID;
        private Button Modificar;
        private Label label7;
        private Button Eliminar;
        private Button Buscar;
    }
}
