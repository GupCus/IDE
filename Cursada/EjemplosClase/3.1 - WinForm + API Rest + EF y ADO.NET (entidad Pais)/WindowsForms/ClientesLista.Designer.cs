namespace WindowsForms
{
    partial class ClientesLista
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
            clientesDataGridView = new DataGridView();
            agregarButton = new Button();
            eliminarButton = new Button();
            modificarButton = new Button();
            buscarTextBox = new TextBox();
            buscarButton = new Button();
            ((System.ComponentModel.ISupportInitialize)clientesDataGridView).BeginInit();
            SuspendLayout();
            // 
            // clientesDataGridView
            // 
            clientesDataGridView.AllowUserToOrderColumns = true;
            clientesDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            clientesDataGridView.Location = new Point(39, 90);
            clientesDataGridView.MultiSelect = false;
            clientesDataGridView.Name = "clientesDataGridView";
            clientesDataGridView.ReadOnly = true;
            clientesDataGridView.RowHeadersWidth = 82;
            clientesDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            clientesDataGridView.Size = new Size(1395, 520);
            clientesDataGridView.TabIndex = 0;
            // 
            // agregarButton
            // 
            agregarButton.Location = new Point(1284, 641);
            agregarButton.Name = "agregarButton";
            agregarButton.Size = new Size(150, 46);
            agregarButton.TabIndex = 1;
            agregarButton.Text = "Agregar";
            agregarButton.UseVisualStyleBackColor = true;
            agregarButton.Click += agregarButton_Click;
            // 
            // eliminarButton
            // 
            eliminarButton.Location = new Point(940, 641);
            eliminarButton.Name = "eliminarButton";
            eliminarButton.Size = new Size(150, 46);
            eliminarButton.TabIndex = 2;
            eliminarButton.Text = "Eliminar";
            eliminarButton.UseVisualStyleBackColor = true;
            eliminarButton.Click += eliminarButton_Click;
            // 
            // modificarButton
            // 
            modificarButton.Location = new Point(1111, 641);
            modificarButton.Name = "modificarButton";
            modificarButton.Size = new Size(150, 46);
            modificarButton.TabIndex = 3;
            modificarButton.Text = "Modificar";
            modificarButton.UseVisualStyleBackColor = true;
            modificarButton.Click += modificarButton_Click;
            // 
            // buscarTextBox
            // 
            buscarTextBox.Location = new Point(39, 38);
            buscarTextBox.Name = "buscarTextBox";
            buscarTextBox.PlaceholderText = "Buscar por nombre, apellido o email...";
            buscarTextBox.Size = new Size(400, 39);
            buscarTextBox.TabIndex = 4;
            // 
            // buscarButton
            // 
            buscarButton.Location = new Point(450, 38);
            buscarButton.Name = "buscarButton";
            buscarButton.Size = new Size(120, 39);
            buscarButton.TabIndex = 5;
            buscarButton.Text = "Buscar";
            buscarButton.UseVisualStyleBackColor = true;
            buscarButton.Click += buscarButton_Click;
            // 
            // ClientesLista
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1478, 716);
            Controls.Add(buscarButton);
            Controls.Add(buscarTextBox);
            Controls.Add(modificarButton);
            Controls.Add(eliminarButton);
            Controls.Add(agregarButton);
            Controls.Add(clientesDataGridView);
            Name = "ClientesLista";
            Text = "Clientes";
            Load += Clientes_Load;
            ((System.ComponentModel.ISupportInitialize)clientesDataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView clientesDataGridView;
        private Button agregarButton;
        private Button eliminarButton;
        private Button modificarButton;
        private TextBox buscarTextBox;
        private Button buscarButton;
    }
}