namespace WindowsForms
{
    partial class PedidoDetalle
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
            components = new System.ComponentModel.Container();
            idLabel = new Label();
            idTextBox = new TextBox();
            clienteLabel = new Label();
            clienteComboBox = new ComboBox();
            fechaPedidoLabel = new Label();
            fechaPedidoTextBox = new TextBox();
            productosLabel = new Label();
            itemsDataGridView = new DataGridView();
            agregarButton = new Button();
            modificarButton = new Button();
            eliminarButton = new Button();
            aceptarButton = new Button();
            cancelarButton = new Button();
            totalProductosLabel = new Label();
            totalPrecioLabel = new Label();
            errorProvider = new ErrorProvider(components);
            ((System.ComponentModel.ISupportInitialize)itemsDataGridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            SuspendLayout();
            // 
            // idLabel
            // 
            idLabel.AutoSize = true;
            idLabel.Location = new Point(56, 64);
            idLabel.Margin = new Padding(6, 0, 6, 0);
            idLabel.Name = "idLabel";
            idLabel.Size = new Size(34, 32);
            idLabel.TabIndex = 0;
            idLabel.Text = "Id";
            // 
            // idTextBox
            // 
            idTextBox.Location = new Point(223, 58);
            idTextBox.Margin = new Padding(6);
            idTextBox.Name = "idTextBox";
            idTextBox.ReadOnly = true;
            idTextBox.Size = new Size(182, 39);
            idTextBox.TabIndex = 1;
            // 
            // clienteLabel
            // 
            clienteLabel.AutoSize = true;
            clienteLabel.Location = new Point(56, 140);
            clienteLabel.Margin = new Padding(6, 0, 6, 0);
            clienteLabel.Name = "clienteLabel";
            clienteLabel.Size = new Size(89, 32);
            clienteLabel.TabIndex = 2;
            clienteLabel.Text = "Cliente";
            // 
            // clienteComboBox
            // 
            clienteComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            clienteComboBox.FormattingEnabled = true;
            clienteComboBox.Location = new Point(223, 134);
            clienteComboBox.Margin = new Padding(6);
            clienteComboBox.Name = "clienteComboBox";
            clienteComboBox.Size = new Size(554, 40);
            clienteComboBox.TabIndex = 3;
            // 
            // fechaPedidoLabel
            // 
            fechaPedidoLabel.AutoSize = true;
            fechaPedidoLabel.Location = new Point(56, 211);
            fechaPedidoLabel.Margin = new Padding(6, 0, 6, 0);
            fechaPedidoLabel.Name = "fechaPedidoLabel";
            fechaPedidoLabel.Size = new Size(156, 32);
            fechaPedidoLabel.TabIndex = 4;
            fechaPedidoLabel.Text = "Fecha Pedido";
            // 
            // fechaPedidoTextBox
            // 
            fechaPedidoTextBox.Location = new Point(223, 204);
            fechaPedidoTextBox.Margin = new Padding(6);
            fechaPedidoTextBox.Name = "fechaPedidoTextBox";
            fechaPedidoTextBox.Size = new Size(275, 39);
            fechaPedidoTextBox.TabIndex = 5;
            // 
            // productosLabel
            // 
            productosLabel.AutoSize = true;
            productosLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            productosLabel.Location = new Point(56, 286);
            productosLabel.Margin = new Padding(6, 0, 6, 0);
            productosLabel.Name = "productosLabel";
            productosLabel.Size = new Size(131, 32);
            productosLabel.TabIndex = 8;
            productosLabel.Text = "Productos";
            // 
            // itemsDataGridView
            // 
            itemsDataGridView.AllowUserToAddRows = false;
            itemsDataGridView.AllowUserToDeleteRows = false;
            itemsDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            itemsDataGridView.Location = new Point(56, 350);
            itemsDataGridView.Margin = new Padding(6);
            itemsDataGridView.MultiSelect = false;
            itemsDataGridView.Name = "itemsDataGridView";
            itemsDataGridView.ReadOnly = true;
            itemsDataGridView.RowHeadersWidth = 82;
            itemsDataGridView.RowTemplate.Height = 25;
            itemsDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            itemsDataGridView.Size = new Size(1114, 427);
            itemsDataGridView.TabIndex = 9;
            // 
            // agregarButton
            // 
            agregarButton.Location = new Point(539, 802);
            agregarButton.Margin = new Padding(6);
            agregarButton.Name = "agregarButton";
            agregarButton.Size = new Size(186, 42);
            agregarButton.TabIndex = 10;
            agregarButton.Text = "&Agregar";
            agregarButton.UseVisualStyleBackColor = true;
            agregarButton.Click += agregarItemButton_Click;
            // 
            // modificarButton
            // 
            modificarButton.Location = new Point(762, 802);
            modificarButton.Margin = new Padding(6);
            modificarButton.Name = "modificarButton";
            modificarButton.Size = new Size(186, 42);
            modificarButton.TabIndex = 11;
            modificarButton.Text = "&Modificar";
            modificarButton.UseVisualStyleBackColor = true;
            modificarButton.Click += modificarItemButton_Click;
            // 
            // eliminarButton
            // 
            eliminarButton.Location = new Point(984, 802);
            eliminarButton.Margin = new Padding(6);
            eliminarButton.Name = "eliminarButton";
            eliminarButton.Size = new Size(186, 42);
            eliminarButton.TabIndex = 12;
            eliminarButton.Text = "&Eliminar";
            eliminarButton.UseVisualStyleBackColor = true;
            eliminarButton.Click += eliminarItemButton_Click;
            // 
            // aceptarButton
            // 
            aceptarButton.Location = new Point(836, 917);
            aceptarButton.Margin = new Padding(6);
            aceptarButton.Name = "aceptarButton";
            aceptarButton.Size = new Size(149, 64);
            aceptarButton.TabIndex = 13;
            aceptarButton.Text = "&Aceptar";
            aceptarButton.UseVisualStyleBackColor = true;
            aceptarButton.Click += aceptarButton_Click;
            // 
            // cancelarButton
            // 
            cancelarButton.Location = new Point(1021, 917);
            cancelarButton.Margin = new Padding(6);
            cancelarButton.Name = "cancelarButton";
            cancelarButton.Size = new Size(149, 64);
            cancelarButton.TabIndex = 14;
            cancelarButton.Text = "&Cancelar";
            cancelarButton.UseVisualStyleBackColor = true;
            cancelarButton.Click += cancelarButton_Click;
            // 
            // totalProductosLabel
            // 
            totalProductosLabel.AutoSize = true;
            totalProductosLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            totalProductosLabel.Location = new Point(56, 802);
            totalProductosLabel.Margin = new Padding(6, 0, 6, 0);
            totalProductosLabel.Name = "totalProductosLabel";
            totalProductosLabel.Size = new Size(201, 32);
            totalProductosLabel.TabIndex = 15;
            totalProductosLabel.Text = "Total Productos:";
            // 
            // totalPrecioLabel
            // 
            totalPrecioLabel.AutoSize = true;
            totalPrecioLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            totalPrecioLabel.Location = new Point(56, 849);
            totalPrecioLabel.Margin = new Padding(6, 0, 6, 0);
            totalPrecioLabel.Name = "totalPrecioLabel";
            totalPrecioLabel.Size = new Size(156, 32);
            totalPrecioLabel.TabIndex = 16;
            totalPrecioLabel.Text = "Total Precio:";
            // 
            // errorProvider
            // 
            errorProvider.ContainerControl = this;
            // 
            // PedidoDetalle
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1215, 1011);
            Controls.Add(totalPrecioLabel);
            Controls.Add(totalProductosLabel);
            Controls.Add(cancelarButton);
            Controls.Add(aceptarButton);
            Controls.Add(eliminarButton);
            Controls.Add(modificarButton);
            Controls.Add(agregarButton);
            Controls.Add(itemsDataGridView);
            Controls.Add(productosLabel);
            Controls.Add(fechaPedidoTextBox);
            Controls.Add(fechaPedidoLabel);
            Controls.Add(clienteComboBox);
            Controls.Add(clienteLabel);
            Controls.Add(idTextBox);
            Controls.Add(idLabel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(6);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "PedidoDetalle";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Pedido - Detalle";
            ((System.ComponentModel.ISupportInitialize)itemsDataGridView).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label idLabel;
    private TextBox idTextBox;
    private Label clienteLabel;
    private ComboBox clienteComboBox;
    private Label fechaPedidoLabel;
    private TextBox fechaPedidoTextBox;
    private Label productosLabel;
    private DataGridView itemsDataGridView;
    private Button agregarButton;
    private Button modificarButton;
    private Button eliminarButton;
    private Button aceptarButton;
    private Button cancelarButton;
    private Label totalProductosLabel;
    private Label totalPrecioLabel;
    private ErrorProvider errorProvider;
    }
}