namespace Barroso.Presentacion
{
    partial class FormPrincipal
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
            dgvLiquidaciones = new DataGridView();
            cbEstado = new ComboBox();
            btnAgregar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvLiquidaciones).BeginInit();
            SuspendLayout();
            // 
            // dgvLiquidaciones
            // 
            dgvLiquidaciones.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLiquidaciones.Location = new Point(12, 77);
            dgvLiquidaciones.Name = "dgvLiquidaciones";
            dgvLiquidaciones.RowHeadersWidth = 51;
            dgvLiquidaciones.Size = new Size(763, 188);
            dgvLiquidaciones.TabIndex = 0;
            // 
            // cbEstado
            // 
            cbEstado.FormattingEnabled = true;
            cbEstado.Items.AddRange(new object[] { "Pendiente", "Liquidada" });
            cbEstado.Location = new Point(12, 286);
            cbEstado.Name = "cbEstado";
            cbEstado.Size = new Size(368, 28);
            cbEstado.TabIndex = 1;
            cbEstado.SelectedIndexChanged += cbEstado_SelectedIndexChanged;
            // 
            // btnAgregar
            // 
            btnAgregar.Location = new Point(449, 286);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(326, 86);
            btnAgregar.TabIndex = 2;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = true;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // FormPrincipal
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnAgregar);
            Controls.Add(cbEstado);
            Controls.Add(dgvLiquidaciones);
            Name = "FormPrincipal";
            SizeGripStyle = SizeGripStyle.Hide;
            Text = "Liquidaciones";
            Load += FormPrincipal_Load;
            ((System.ComponentModel.ISupportInitialize)dgvLiquidaciones).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvLiquidaciones;
        private ComboBox cbEstado;
        private Button btnAgregar;
    }
}
