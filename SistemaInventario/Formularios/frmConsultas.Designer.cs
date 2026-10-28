using System.Drawing;
using System.Windows.Forms;

namespace SistemaInventario.Formularios
{
    partial class frmConsultas
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitulo, lblAyuda, lblEstado;
        private TextBox txtBusqueda;
        private Button btnBuscar, btnMostrarTodos;
        private DataGridView dgvProductos;
        private DataGridViewTextBoxColumn colCodigo, colNombre, colPrecio, colCantidad, colMinimo;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitulo = new Label();
            this.lblAyuda = new Label();
            this.lblEstado = new Label();
            this.txtBusqueda = new TextBox();
            this.btnBuscar = new Button();
            this.btnMostrarTodos = new Button();
            this.dgvProductos = new DataGridView();
            this.colCodigo = new DataGridViewTextBoxColumn();
            this.colNombre = new DataGridViewTextBoxColumn();
            this.colPrecio = new DataGridViewTextBoxColumn();
            this.colCantidad = new DataGridViewTextBoxColumn();
            this.colMinimo = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).BeginInit();
            this.SuspendLayout();
            this.lblTitulo.Text = "Consulta de productos";
            this.lblTitulo.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            this.lblTitulo.ForeColor = Color.FromArgb(25, 45, 72);
            this.lblTitulo.Location = new Point(28, 22);
            this.lblTitulo.Size = new Size(800, 54);
            this.lblTitulo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.lblAyuda.Text = "Busque por código o nombre. Cierre esta ventana para volver al menú.";
            this.lblAyuda.Location = new Point(32, 86);
            this.lblAyuda.Size = new Size(840, 32);
            this.lblAyuda.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.txtBusqueda.Location = new Point(32, 134);
            this.txtBusqueda.Size = new Size(478, 30);
            this.txtBusqueda.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.txtBusqueda.AccessibleName = "Buscar por código o nombre";
            this.txtBusqueda.TabIndex = 0;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.Location = new Point(526, 128);
            this.btnBuscar.Size = new Size(140, 42);
            this.btnBuscar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnBuscar.BackColor = Color.FromArgb(30, 83, 148);
            this.btnBuscar.ForeColor = Color.White;
            this.btnBuscar.FlatStyle = FlatStyle.Flat;
            this.btnBuscar.TabIndex = 1;
            this.btnBuscar.Click += new System.EventHandler(this.Buscar_Click);
            this.btnMostrarTodos.Text = "Mostrar todos";
            this.btnMostrarTodos.Location = new Point(682, 128);
            this.btnMostrarTodos.Size = new Size(190, 42);
            this.btnMostrarTodos.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnMostrarTodos.TabIndex = 2;
            this.btnMostrarTodos.Click += new System.EventHandler(this.MostrarTodos_Click);
            this.colCodigo.Name = "colCodigo";
            this.colCodigo.HeaderText = "Código";
            this.colCodigo.DataPropertyName = "Codigo";
            this.colCodigo.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.colNombre.Name = "colNombre";
            this.colNombre.HeaderText = "Nombre";
            this.colNombre.DataPropertyName = "Nombre";
            this.colNombre.FillWeight = 180F;
            this.colNombre.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.colPrecio.Name = "colPrecio";
            this.colPrecio.HeaderText = "Precio";
            this.colPrecio.DataPropertyName = "Precio";
            this.colPrecio.DefaultCellStyle.Format = "0.00";
            this.colPrecio.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.colPrecio.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.colCantidad.Name = "colCantidad";
            this.colCantidad.HeaderText = "Cantidad";
            this.colCantidad.DataPropertyName = "Cantidad";
            this.colCantidad.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.colCantidad.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.colMinimo.Name = "colMinimo";
            this.colMinimo.HeaderText = "Stock mínimo";
            this.colMinimo.DataPropertyName = "StockMinimo";
            this.colMinimo.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.colMinimo.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dgvProductos.Columns.AddRange(new DataGridViewColumn[] {
                this.colCodigo, this.colNombre, this.colPrecio, this.colCantidad, this.colMinimo });
            this.dgvProductos.AutoGenerateColumns = false;
            this.dgvProductos.ColumnHeadersHeight = 40;
            this.dgvProductos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvProductos.RowTemplate.Height = 32;
            this.dgvProductos.AllowUserToAddRows = false;
            this.dgvProductos.AllowUserToDeleteRows = false;
            this.dgvProductos.ReadOnly = true;
            this.dgvProductos.RowHeadersVisible = false;
            this.dgvProductos.MultiSelect = false;
            this.dgvProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvProductos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProductos.BackgroundColor = Color.White;
            this.dgvProductos.BorderStyle = BorderStyle.FixedSingle;
            this.dgvProductos.Location = new Point(32, 194);
            this.dgvProductos.Size = new Size(840, 338);
            this.dgvProductos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.dgvProductos.AccessibleName = "Productos encontrados";
            this.dgvProductos.TabIndex = 3;
            this.lblEstado.Location = new Point(32, 550);
            this.lblEstado.Size = new Size(840, 48);
            this.lblEstado.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.AutoScaleDimensions = new SizeF(9F, 23F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Font = new Font("Segoe UI", 12F);
            this.BackColor = Color.FromArgb(245, 247, 251);
            this.ClientSize = new Size(904, 614);
            this.MinimumSize = new Size(920, 653);
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Consultas · Inventario";
            this.AcceptButton = this.btnBuscar;
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.lblAyuda);
            this.Controls.Add(this.txtBusqueda);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.btnMostrarTodos);
            this.Controls.Add(this.dgvProductos);
            this.Controls.Add(this.lblEstado);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
