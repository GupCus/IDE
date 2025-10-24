namespace WindowsForms
{
    partial class PedidoLista
    {
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            pedidosDataGridView = new DataGridView();
            agregarButton = new Button();
            actualizarButton = new Button();
            eliminarButton = new Button();
            ((System.ComponentModel.ISupportInitialize)pedidosDataGridView).BeginInit();
            SuspendLayout();
            // 
            // pedidosDataGridView
            // 
            pedidosDataGridView.AllowUserToAddRows = false;
            pedidosDataGridView.AllowUserToDeleteRows = false;
            pedidosDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            pedidosDataGridView.Location = new Point(39, 44);
            pedidosDataGridView.MultiSelect = false;
            pedidosDataGridView.Name = "pedidosDataGridView";
            pedidosDataGridView.ReadOnly = true;
            pedidosDataGridView.RowHeadersWidth = 82;
            pedidosDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            pedidosDataGridView.Size = new Size(1395, 518);
            pedidosDataGridView.TabIndex = 0;
            // 
            // agregarButton
            // 
            agregarButton.Location = new Point(1284, 604);
            agregarButton.Name = "agregarButton";
            agregarButton.Size = new Size(150, 46);
            agregarButton.TabIndex = 1;
            agregarButton.Text = "Agregar";
            agregarButton.UseVisualStyleBackColor = true;
            agregarButton.Click += agregarButton_Click;
            // 
            // actualizarButton
            // 
            actualizarButton.Enabled = false;
            actualizarButton.Location = new Point(1111, 604);
            actualizarButton.Name = "actualizarButton";
            actualizarButton.Size = new Size(150, 46);
            actualizarButton.TabIndex = 3;
            actualizarButton.Text = "Actualizar";
            actualizarButton.UseVisualStyleBackColor = true;
            actualizarButton.Click += actualizarButton_Click;
            // 
            // eliminarButton
            // 
            eliminarButton.Enabled = false;
            eliminarButton.Location = new Point(940, 604);
            eliminarButton.Name = "eliminarButton";
            eliminarButton.Size = new Size(150, 46);
            eliminarButton.TabIndex = 2;
            eliminarButton.Text = "Eliminar";
            eliminarButton.UseVisualStyleBackColor = true;
            eliminarButton.Click += eliminarButton_Click;
            // 
            // PedidoLista
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1478, 684);
            Controls.Add(eliminarButton);
            Controls.Add(actualizarButton);
            Controls.Add(agregarButton);
            Controls.Add(pedidosDataGridView);
            Name = "PedidoLista";
            Text = "Pedidos";
            Load += Pedidos_Load;
            ((System.ComponentModel.ISupportInitialize)pedidosDataGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView pedidosDataGridView;
    private Button agregarButton;
    private Button actualizarButton;
    private Button eliminarButton;
    }
}