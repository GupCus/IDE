namespace WinFormsSnippet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPrincipal));
            tableLayoutPanelPrincipal = new TableLayoutPanel();
            tableLayoutPanelIzq = new TableLayoutPanel();
            panellenguajes = new Panel();
            botonej = new Button();
            pictureBox1 = new PictureBox();
            btnNvaCategoria = new Button();
            tableLayoutPanel3 = new TableLayoutPanel();
            flowLayoutPanel1 = new FlowLayoutPanel();
            labelTitulo = new Label();
            textBoxSnippet = new RichTextBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            buttonNvoSnippet = new Button();
            labelLenguaje = new Label();
            tableLayoutPanelPrincipal.SuspendLayout();
            tableLayoutPanelIzq.SuspendLayout();
            panellenguajes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            tableLayoutPanel3.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanelPrincipal
            // 
            tableLayoutPanelPrincipal.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 28.5714283F));
            tableLayoutPanelPrincipal.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 71.42857F));
            tableLayoutPanelPrincipal.Controls.Add(tableLayoutPanelIzq, 0, 0);
            tableLayoutPanelPrincipal.Controls.Add(tableLayoutPanel3, 1, 0);
            tableLayoutPanelPrincipal.Dock = DockStyle.Fill;
            tableLayoutPanelPrincipal.Location = new Point(0, 0);
            tableLayoutPanelPrincipal.Name = "tableLayoutPanelPrincipal";
            tableLayoutPanelPrincipal.RowStyles.Add(new RowStyle());
            tableLayoutPanelPrincipal.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanelPrincipal.Size = new Size(1036, 490);
            tableLayoutPanelPrincipal.TabIndex = 0;
            // 
            // tableLayoutPanelIzq
            // 
            tableLayoutPanelIzq.BackColor = Color.FromArgb(30, 30, 30);
            tableLayoutPanelIzq.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelIzq.Controls.Add(panellenguajes, 0, 1);
            tableLayoutPanelIzq.Controls.Add(pictureBox1, 0, 0);
            tableLayoutPanelIzq.Controls.Add(btnNvaCategoria, 0, 2);
            tableLayoutPanelIzq.Dock = DockStyle.Fill;
            tableLayoutPanelIzq.Location = new Point(3, 3);
            tableLayoutPanelIzq.Name = "tableLayoutPanelIzq";
            tableLayoutPanelIzq.RowCount = 3;
            tableLayoutPanelIzq.RowStyles.Add(new RowStyle());
            tableLayoutPanelIzq.RowStyles.Add(new RowStyle(SizeType.Percent, 90F));
            tableLayoutPanelIzq.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanelIzq.Size = new Size(290, 484);
            tableLayoutPanelIzq.TabIndex = 0;
            // 
            // panellenguajes
            // 
            panellenguajes.BackColor = Color.FromArgb(30, 30, 30);
            panellenguajes.Controls.Add(botonej);
            panellenguajes.Dock = DockStyle.Fill;
            panellenguajes.Location = new Point(3, 71);
            panellenguajes.Name = "panellenguajes";
            panellenguajes.Size = new Size(284, 368);
            panellenguajes.TabIndex = 0;
            // 
            // botonej
            // 
            botonej.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            botonej.FlatAppearance.BorderSize = 0;
            botonej.FlatStyle = FlatStyle.Flat;
            botonej.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            botonej.ForeColor = Color.Gainsboro;
            botonej.Location = new Point(0, 0);
            botonej.Name = "botonej";
            botonej.Size = new Size(284, 29);
            botonej.TabIndex = 0;
            botonej.Text = "C#";
            botonej.UseVisualStyleBackColor = true;
            botonej.Click += botonej_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.FromArgb(30, 30, 30);
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(3, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(284, 62);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            // 
            // btnNvaCategoria
            // 
            btnNvaCategoria.BackColor = Color.FromArgb(30, 30, 30);
            btnNvaCategoria.Cursor = Cursors.Hand;
            btnNvaCategoria.Dock = DockStyle.Fill;
            btnNvaCategoria.FlatAppearance.BorderColor = Color.FromArgb(0, 122, 204);
            btnNvaCategoria.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 100, 180);
            btnNvaCategoria.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 122, 204);
            btnNvaCategoria.FlatStyle = FlatStyle.Flat;
            btnNvaCategoria.Location = new Point(3, 445);
            btnNvaCategoria.Name = "btnNvaCategoria";
            btnNvaCategoria.Size = new Size(284, 36);
            btnNvaCategoria.TabIndex = 0;
            btnNvaCategoria.Text = "Nueva Categoría";
            btnNvaCategoria.UseVisualStyleBackColor = false;
            btnNvaCategoria.Click += button1_Click;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Controls.Add(flowLayoutPanel1, 0, 1);
            tableLayoutPanel3.Controls.Add(tableLayoutPanel1, 0, 0);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(299, 3);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 2;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 15F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 85F));
            tableLayoutPanel3.Size = new Size(734, 484);
            tableLayoutPanel3.TabIndex = 1;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(labelTitulo);
            flowLayoutPanel1.Controls.Add(textBoxSnippet);
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.Font = new Font("Consolas", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            flowLayoutPanel1.Location = new Point(3, 75);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(728, 406);
            flowLayoutPanel1.TabIndex = 1;
            // 
            // labelTitulo
            // 
            labelTitulo.Anchor = AnchorStyles.None;
            labelTitulo.AutoSize = true;
            flowLayoutPanel1.SetFlowBreak(labelTitulo, true);
            labelTitulo.Font = new Font("Segoe UI", 15F);
            labelTitulo.Location = new Point(20, 10);
            labelTitulo.Margin = new Padding(20, 10, 3, 5);
            labelTitulo.Name = "labelTitulo";
            labelTitulo.Size = new Size(81, 35);
            labelTitulo.TabIndex = 0;
            labelTitulo.Text = "label1";
            // 
            // textBoxSnippet
            // 
            textBoxSnippet.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            textBoxSnippet.BackColor = SystemColors.WindowFrame;
            flowLayoutPanel1.SetFlowBreak(textBoxSnippet, true);
            textBoxSnippet.Location = new Point(20, 53);
            textBoxSnippet.Margin = new Padding(20, 3, 20, 3);
            textBoxSnippet.Name = "textBoxSnippet";
            textBoxSnippet.Size = new Size(685, 120);
            textBoxSnippet.TabIndex = 1;
            textBoxSnippet.Text = "";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 75.4717F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 24.5283031F));
            tableLayoutPanel1.Controls.Add(buttonNvoSnippet, 1, 0);
            tableLayoutPanel1.Controls.Add(labelLenguaje, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(3, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(728, 66);
            tableLayoutPanel1.TabIndex = 2;
            // 
            // buttonNvoSnippet
            // 
            buttonNvoSnippet.BackColor = Color.FromArgb(30, 30, 30);
            buttonNvoSnippet.Cursor = Cursors.Hand;
            buttonNvoSnippet.Dock = DockStyle.Fill;
            buttonNvoSnippet.FlatAppearance.BorderColor = Color.FromArgb(0, 122, 204);
            buttonNvoSnippet.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 100, 180);
            buttonNvoSnippet.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 122, 204);
            buttonNvoSnippet.FlatStyle = FlatStyle.Flat;
            buttonNvoSnippet.Location = new Point(569, 10);
            buttonNvoSnippet.Margin = new Padding(20, 10, 20, 10);
            buttonNvoSnippet.Name = "buttonNvoSnippet";
            buttonNvoSnippet.Size = new Size(139, 46);
            buttonNvoSnippet.TabIndex = 2;
            buttonNvoSnippet.Text = "NuevoSnippet";
            buttonNvoSnippet.UseVisualStyleBackColor = false;
            // 
            // labelLenguaje
            // 
            labelLenguaje.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            labelLenguaje.AutoSize = true;
            labelLenguaje.Font = new Font("Segoe UI", 25F, FontStyle.Bold | FontStyle.Italic);
            labelLenguaje.Location = new Point(3, 0);
            labelLenguaje.Name = "labelLenguaje";
            labelLenguaje.Size = new Size(543, 57);
            labelLenguaje.TabIndex = 1;
            labelLenguaje.Text = "Lenguaje";
            labelLenguaje.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // FormPrincipal
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(18, 18, 18);
            ClientSize = new Size(1036, 490);
            Controls.Add(tableLayoutPanelPrincipal);
            ForeColor = Color.FromArgb(230, 230, 230);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "FormPrincipal";
            Text = "Snippet Centre";
            tableLayoutPanelPrincipal.ResumeLayout(false);
            tableLayoutPanelIzq.ResumeLayout(false);
            panellenguajes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            tableLayoutPanel3.ResumeLayout(false);
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanelPrincipal;
        private TableLayoutPanel tableLayoutPanelIzq;
        private Button btnNvaCategoria;
        private TableLayoutPanel tableLayoutPanel3;
        private Panel panellenguajes;
        private Button botonej;
        private PictureBox pictureBox1;
        private FlowLayoutPanel flowLayoutPanel1;
        private Label labelTitulo;
        private RichTextBox textBoxSnippet;
        private TableLayoutPanel tableLayoutPanel1;
        private Button buttonNvoSnippet;
        private Label labelLenguaje;
    }
}
