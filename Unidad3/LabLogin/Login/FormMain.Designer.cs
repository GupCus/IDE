namespace Login
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            tlUsuarios = new ToolStripContainer();
            tlUsuario = new TableLayoutPanel();
            btnActualizar = new Button();
            btnSalir = new Button();
            dgvUsuarios = new DataGridView();
            id = new DataGridViewTextBoxColumn();
            nombre = new DataGridViewTextBoxColumn();
            apellido = new DataGridViewTextBoxColumn();
            usuario = new DataGridViewTextBoxColumn();
            email = new DataGridViewTextBoxColumn();
            habilitado = new DataGridViewCheckBoxColumn();
            tsUsuarios = new ToolStrip();
            tsbNuevo = new ToolStripButton();
            tsbEditar = new ToolStripButton();
            tsbEliminar = new ToolStripButton();
            tlUsuarios.ContentPanel.SuspendLayout();
            tlUsuarios.TopToolStripPanel.SuspendLayout();
            tlUsuarios.SuspendLayout();
            tlUsuario.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvUsuarios).BeginInit();
            tsUsuarios.SuspendLayout();
            SuspendLayout();
            // 
            // tlUsuarios
            // 
            // 
            // tlUsuarios.ContentPanel
            // 
            tlUsuarios.ContentPanel.Controls.Add(tlUsuario);
            tlUsuarios.ContentPanel.Size = new Size(800, 423);
            tlUsuarios.Dock = DockStyle.Fill;
            tlUsuarios.Location = new Point(0, 0);
            tlUsuarios.Name = "tlUsuarios";
            tlUsuarios.Size = new Size(800, 450);
            tlUsuarios.TabIndex = 3;
            tlUsuarios.Text = "toolStripContainer1";
            // 
            // tlUsuarios.TopToolStripPanel
            // 
            tlUsuarios.TopToolStripPanel.Controls.Add(tsUsuarios);
            // 
            // tlUsuario
            // 
            tlUsuario.ColumnCount = 2;
            tlUsuario.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlUsuario.ColumnStyles.Add(new ColumnStyle());
            tlUsuario.Controls.Add(btnActualizar, 0, 1);
            tlUsuario.Controls.Add(btnSalir, 1, 1);
            tlUsuario.Controls.Add(dgvUsuarios, 0, 0);
            tlUsuario.Dock = DockStyle.Fill;
            tlUsuario.Location = new Point(0, 0);
            tlUsuario.Name = "tlUsuario";
            tlUsuario.RowCount = 2;
            tlUsuario.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlUsuario.RowStyles.Add(new RowStyle());
            tlUsuario.Size = new Size(800, 423);
            tlUsuario.TabIndex = 1;
            // 
            // btnActualizar
            // 
            btnActualizar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnActualizar.Location = new Point(603, 391);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(94, 29);
            btnActualizar.TabIndex = 1;
            btnActualizar.Text = "Actualizar";
            btnActualizar.UseVisualStyleBackColor = true;
            btnActualizar.Click += btnActualizar_Click;
            // 
            // btnSalir
            // 
            btnSalir.Location = new Point(703, 391);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(94, 29);
            btnSalir.TabIndex = 2;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = true;
            btnSalir.Click += btnSalir_Click;
            // 
            // dgvUsuarios
            // 
            dgvUsuarios.AllowUserToAddRows = false;
            dgvUsuarios.AllowUserToDeleteRows = false;
            dgvUsuarios.AllowUserToResizeColumns = false;
            dgvUsuarios.AllowUserToResizeRows = false;
            dgvUsuarios.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvUsuarios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvUsuarios.Columns.AddRange(new DataGridViewColumn[] { id, nombre, apellido, usuario, email, habilitado });
            tlUsuario.SetColumnSpan(dgvUsuarios, 2);
            dgvUsuarios.Location = new Point(3, 3);
            dgvUsuarios.Name = "dgvUsuarios";
            dgvUsuarios.ReadOnly = true;
            dgvUsuarios.RowHeadersWidth = 51;
            dgvUsuarios.Size = new Size(794, 382);
            dgvUsuarios.TabIndex = 3;
            // 
            // id
            // 
            id.DataPropertyName = "Id";
            id.HeaderText = "ID";
            id.MinimumWidth = 6;
            id.Name = "id";
            id.ReadOnly = true;
            // 
            // nombre
            // 
            nombre.DataPropertyName = "Nombre";
            nombre.HeaderText = "Nombre";
            nombre.MinimumWidth = 6;
            nombre.Name = "nombre";
            nombre.ReadOnly = true;
            // 
            // apellido
            // 
            apellido.DataPropertyName = "Apellido";
            apellido.HeaderText = "Apellido";
            apellido.MinimumWidth = 6;
            apellido.Name = "apellido";
            apellido.ReadOnly = true;
            // 
            // usuario
            // 
            usuario.DataPropertyName = "UsuarioN";
            usuario.HeaderText = "Usuario";
            usuario.MinimumWidth = 6;
            usuario.Name = "usuario";
            usuario.ReadOnly = true;
            // 
            // email
            // 
            email.DataPropertyName = "Email";
            email.HeaderText = "Email";
            email.MinimumWidth = 6;
            email.Name = "email";
            email.ReadOnly = true;
            // 
            // habilitado
            // 
            habilitado.DataPropertyName = "Habilitado";
            habilitado.HeaderText = "Habilitado";
            habilitado.MinimumWidth = 6;
            habilitado.Name = "habilitado";
            habilitado.ReadOnly = true;
            // 
            // tsUsuarios
            // 
            tsUsuarios.Dock = DockStyle.None;
            tsUsuarios.GripStyle = ToolStripGripStyle.Hidden;
            tsUsuarios.ImageScalingSize = new Size(20, 20);
            tsUsuarios.Items.AddRange(new ToolStripItem[] { tsbNuevo, tsbEditar, tsbEliminar });
            tsUsuarios.Location = new Point(0, 0);
            tsUsuarios.Name = "tsUsuarios";
            tsUsuarios.Size = new Size(800, 27);
            tsUsuarios.Stretch = true;
            tsUsuarios.TabIndex = 0;
            // 
            // tsbNuevo
            // 
            tsbNuevo.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsbNuevo.Image = (Image)resources.GetObject("tsbNuevo.Image");
            tsbNuevo.ImageTransparentColor = Color.Magenta;
            tsbNuevo.Name = "tsbNuevo";
            tsbNuevo.Size = new Size(29, 24);
            tsbNuevo.Text = "toolStripButton1";
            tsbNuevo.ToolTipText = "Nuevo";
            tsbNuevo.Click += tsbNuevo_Click;
            // 
            // tsbEditar
            // 
            tsbEditar.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsbEditar.Image = (Image)resources.GetObject("tsbEditar.Image");
            tsbEditar.ImageScaling = ToolStripItemImageScaling.None;
            tsbEditar.ImageTransparentColor = Color.Magenta;
            tsbEditar.Name = "tsbEditar";
            tsbEditar.Size = new Size(29, 24);
            tsbEditar.Text = "Editar";
            tsbEditar.Click += tsbEditar_Click;
            // 
            // tsbEliminar
            // 
            tsbEliminar.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsbEliminar.ForeColor = SystemColors.ControlText;
            tsbEliminar.Image = (Image)resources.GetObject("tsbEliminar.Image");
            tsbEliminar.ImageTransparentColor = Color.Magenta;
            tsbEliminar.Name = "tsbEliminar";
            tsbEliminar.Size = new Size(29, 24);
            tsbEliminar.Text = "Eliminar";
            tsbEliminar.Click += tsbEliminar_Click;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tlUsuarios);
            IsMdiContainer = true;
            Name = "FormMain";
            Text = "Academia";
            WindowState = FormWindowState.Maximized;
            Load += FormMain_Load;
            Shown += FormMain_Shown;
            tlUsuarios.ContentPanel.ResumeLayout(false);
            tlUsuarios.TopToolStripPanel.ResumeLayout(false);
            tlUsuarios.TopToolStripPanel.PerformLayout();
            tlUsuarios.ResumeLayout(false);
            tlUsuarios.PerformLayout();
            tlUsuario.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvUsuarios).EndInit();
            tsUsuarios.ResumeLayout(false);
            tsUsuarios.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private ToolStripContainer tlUsuarios;
        private TableLayoutPanel tlUsuario;
        private Button btnActualizar;
        private Button btnSalir;
        private DataGridView dgvUsuarios;
        private ToolStrip tsUsuarios;
        private DataGridViewTextBoxColumn id;
        private DataGridViewTextBoxColumn nombre;
        private DataGridViewTextBoxColumn apellido;
        private DataGridViewTextBoxColumn usuario;
        private DataGridViewTextBoxColumn email;
        private DataGridViewCheckBoxColumn habilitado;
        private ToolStripButton tsbNuevo;
        private ToolStripButton tsbEditar;
        private ToolStripButton tsbEliminar;
    }
}