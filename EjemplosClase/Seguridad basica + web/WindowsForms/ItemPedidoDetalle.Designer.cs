namespace WindowsForms
{
    partial class ItemPedidoDetalle
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
        this.components = new System.ComponentModel.Container();
        this.productoLabel = new Label();
        this.productoComboBox = new ComboBox();
        this.cantidadLabel = new Label();
        this.cantidadTextBox = new TextBox();
        this.precioUnitarioLabel = new Label();
        this.precioUnitarioTextBox = new TextBox();
        this.aceptarButton = new Button();
        this.cancelarButton = new Button();
        this.errorProvider = new ErrorProvider(this.components);
        ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
        this.SuspendLayout();
        // 
        // productoLabel
        // 
        this.productoLabel.AutoSize = true;
        this.productoLabel.Location = new Point(30, 30);
        this.productoLabel.Name = "productoLabel";
        this.productoLabel.Size = new Size(56, 15);
        this.productoLabel.TabIndex = 0;
        this.productoLabel.Text = "Producto";
        // 
        // productoComboBox
        // 
        this.productoComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        this.productoComboBox.FormattingEnabled = true;
        this.productoComboBox.Location = new Point(150, 27);
        this.productoComboBox.Name = "productoComboBox";
        this.productoComboBox.Size = new Size(300, 23);
        this.productoComboBox.TabIndex = 1;
        this.productoComboBox.SelectedIndexChanged += new EventHandler(this.productoComboBox_SelectedIndexChanged);
        // 
        // cantidadLabel
        // 
        this.cantidadLabel.AutoSize = true;
        this.cantidadLabel.Location = new Point(30, 70);
        this.cantidadLabel.Name = "cantidadLabel";
        this.cantidadLabel.Size = new Size(55, 15);
        this.cantidadLabel.TabIndex = 2;
        this.cantidadLabel.Text = "Cantidad";
        // 
        // cantidadTextBox
        // 
        this.cantidadTextBox.Location = new Point(150, 67);
        this.cantidadTextBox.Name = "cantidadTextBox";
        this.cantidadTextBox.Size = new Size(100, 23);
        this.cantidadTextBox.TabIndex = 3;
        // 
        // precioUnitarioLabel
        // 
        this.precioUnitarioLabel.AutoSize = true;
        this.precioUnitarioLabel.Location = new Point(30, 110);
        this.precioUnitarioLabel.Name = "precioUnitarioLabel";
        this.precioUnitarioLabel.Size = new Size(91, 15);
        this.precioUnitarioLabel.TabIndex = 4;
        this.precioUnitarioLabel.Text = "Precio Unitario";
        // 
        // precioUnitarioTextBox
        // 
        this.precioUnitarioTextBox.Location = new Point(150, 107);
        this.precioUnitarioTextBox.Name = "precioUnitarioTextBox";
        this.precioUnitarioTextBox.Size = new Size(120, 23);
        this.precioUnitarioTextBox.TabIndex = 5;
        // 
        // aceptarButton
        // 
        this.aceptarButton.Location = new Point(290, 160);
        this.aceptarButton.Name = "aceptarButton";
        this.aceptarButton.Size = new Size(80, 30);
        this.aceptarButton.TabIndex = 6;
        this.aceptarButton.Text = "&Aceptar";
        this.aceptarButton.UseVisualStyleBackColor = true;
        this.aceptarButton.Click += new EventHandler(this.aceptarButton_Click);
        // 
        // cancelarButton
        // 
        this.cancelarButton.Location = new Point(380, 160);
        this.cancelarButton.Name = "cancelarButton";
        this.cancelarButton.Size = new Size(80, 30);
        this.cancelarButton.TabIndex = 7;
        this.cancelarButton.Text = "&Cancelar";
        this.cancelarButton.UseVisualStyleBackColor = true;
        this.cancelarButton.Click += new EventHandler(this.cancelarButton_Click);
        // 
        // errorProvider
        // 
        this.errorProvider.ContainerControl = this;
        // 
        // ItemPedidoDetalle
        // 
        this.AutoScaleDimensions = new SizeF(7F, 15F);
        this.AutoScaleMode = AutoScaleMode.Font;
        this.ClientSize = new Size(484, 211);
        this.Controls.Add(this.cancelarButton);
        this.Controls.Add(this.aceptarButton);
        this.Controls.Add(this.precioUnitarioTextBox);
        this.Controls.Add(this.precioUnitarioLabel);
        this.Controls.Add(this.cantidadTextBox);
        this.Controls.Add(this.cantidadLabel);
        this.Controls.Add(this.productoComboBox);
        this.Controls.Add(this.productoLabel);
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "ItemPedidoDetalle";
        this.StartPosition = FormStartPosition.CenterParent;
        this.Text = "Item Pedido - Detalle";
        ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    #endregion

    private Label productoLabel;
    private ComboBox productoComboBox;
    private Label cantidadLabel;
    private TextBox cantidadTextBox;
    private Label precioUnitarioLabel;
    private TextBox precioUnitarioTextBox;
    private Button aceptarButton;
    private Button cancelarButton;
    private ErrorProvider errorProvider;
    }
}