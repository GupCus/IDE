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
            pictureBox1 = new PictureBox();
            btnNvaCategoria = new Button();
            tableLayoutPanel3 = new TableLayoutPanel();
            flowSnippets = new FlowLayoutPanel();
            labelSinSeleccion = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            buttonNvoSnippet = new Button();
            labelLenguaje = new Label();
            tableLayoutPanelPrincipal.SuspendLayout();
            tableLayoutPanelIzq.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            tableLayoutPanel3.SuspendLayout();
            flowSnippets.SuspendLayout();
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
            tableLayoutPanelPrincipal.Size = new Size(1035, 793);
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
            tableLayoutPanelIzq.Size = new Size(289, 788);
            tableLayoutPanelIzq.TabIndex = 0;
            // 
            // panellenguajes
            // 
            panellenguajes.BackColor = Color.FromArgb(30, 30, 30);
            panellenguajes.Dock = DockStyle.Fill;
            panellenguajes.Location = new Point(3, 70);
            panellenguajes.Name = "panellenguajes";
            panellenguajes.Size = new Size(283, 642);
            panellenguajes.TabIndex = 0;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.FromArgb(30, 30, 30);
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(3, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(283, 61);
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
            btnNvaCategoria.Location = new Point(3, 718);
            btnNvaCategoria.Name = "btnNvaCategoria";
            btnNvaCategoria.Size = new Size(283, 67);
            btnNvaCategoria.TabIndex = 0;
            btnNvaCategoria.Text = "Nueva Categoría";
            btnNvaCategoria.UseVisualStyleBackColor = false;
            btnNvaCategoria.Click += buttonNvaCat_Click;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Controls.Add(flowSnippets, 0, 1);
            tableLayoutPanel3.Controls.Add(tableLayoutPanel1, 0, 0);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(298, 3);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 2;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 15F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 85F));
            tableLayoutPanel3.Size = new Size(734, 788);
            tableLayoutPanel3.TabIndex = 1;
            // 
            // flowSnippets
            // 
            flowSnippets.AutoScroll = true;
            flowSnippets.Controls.Add(labelSinSeleccion);
            flowSnippets.Dock = DockStyle.Fill;
            flowSnippets.Font = new Font("Consolas", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            flowSnippets.Location = new Point(3, 121);
            flowSnippets.Name = "flowSnippets";
            flowSnippets.Size = new Size(728, 664);
            flowSnippets.TabIndex = 1;
            // 
            // labelSinSeleccion
            // 
            labelSinSeleccion.Anchor = AnchorStyles.None;
            labelSinSeleccion.AutoSize = true;
            labelSinSeleccion.Font = new Font("Segoe UI", 30F, FontStyle.Italic);
            labelSinSeleccion.ForeColor = Color.DimGray;
            labelSinSeleccion.Location = new Point(183, 160);
            labelSinSeleccion.Margin = new Padding(183, 160, 137, 160);
            labelSinSeleccion.Name = "labelSinSeleccion";
            labelSinSeleccion.Size = new Size(404, 134);
            labelSinSeleccion.TabIndex = 0;
            labelSinSeleccion.Text = "No hay lenguajes seleccionados...";
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
            tableLayoutPanel1.Size = new Size(728, 112);
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
            buttonNvoSnippet.Location = new Point(570, 11);
            buttonNvoSnippet.Margin = new Padding(21, 11, 21, 11);
            buttonNvoSnippet.Name = "buttonNvoSnippet";
            buttonNvoSnippet.Size = new Size(137, 90);
            buttonNvoSnippet.TabIndex = 2;
            buttonNvoSnippet.Text = "NuevoSnippet";
            buttonNvoSnippet.UseVisualStyleBackColor = false;
            // 
            // labelLenguaje
            // 
            labelLenguaje.AutoSize = true;
            labelLenguaje.Dock = DockStyle.Fill;
            labelLenguaje.Font = new Font("Segoe UI", 25F, FontStyle.Bold | FontStyle.Italic);
            labelLenguaje.Location = new Point(3, 0);
            labelLenguaje.Name = "labelLenguaje";
            labelLenguaje.Size = new Size(543, 112);
            labelLenguaje.TabIndex = 1;
            labelLenguaje.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // FormPrincipal
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(18, 18, 18);
            ClientSize = new Size(1035, 793);
            Controls.Add(tableLayoutPanelPrincipal);
            ForeColor = Color.FromArgb(230, 230, 230);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "FormPrincipal";
            Text = "Snippet Centre";
            tableLayoutPanelPrincipal.ResumeLayout(false);
            tableLayoutPanelIzq.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            tableLayoutPanel3.ResumeLayout(false);
            flowSnippets.ResumeLayout(false);
            flowSnippets.PerformLayout();
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
        private PictureBox pictureBox1;
        private FlowLayoutPanel flowSnippets;
        private TableLayoutPanel tableLayoutPanel1;
        private Button buttonNvoSnippet;
        private Label labelLenguaje;
        private Label labelSinSeleccion;
    }
}
