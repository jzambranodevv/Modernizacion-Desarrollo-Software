using System.Drawing;
using System.Windows.Forms;

namespace SistemaInventario.Formularios
{
    partial class frmInventario
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitulo, lblAyuda, lblEstado, lblReposicion, lblDestino, lblTipo, lblUnidades;
        private Button btnIngresar, btnActualizar, btnEliminar, btnMovimiento;
        private GroupBox grpMovimientos;
        private ComboBox cmbTipo;
        private TextBox txtUnidades;
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
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblAyuda = new System.Windows.Forms.Label();
            this.lblEstado = new System.Windows.Forms.Label();
            this.lblReposicion = new System.Windows.Forms.Label();
            this.lblDestino = new System.Windows.Forms.Label();
            this.lblTipo = new System.Windows.Forms.Label();
            this.lblUnidades = new System.Windows.Forms.Label();
            this.btnIngresar = new System.Windows.Forms.Button();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnMovimiento = new System.Windows.Forms.Button();
            this.grpMovimientos = new System.Windows.Forms.GroupBox();
            this.cmbTipo = new System.Windows.Forms.ComboBox();
            this.txtUnidades = new System.Windows.Forms.TextBox();
            this.txtBusqueda = new System.Windows.Forms.TextBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.btnMostrarTodos = new System.Windows.Forms.Button();
            this.dgvProductos = new System.Windows.Forms.DataGridView();
            this.grpMovimientos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(45)))), ((int)(((byte)(72)))));
            this.lblTitulo.Location = new System.Drawing.Point(28, 22);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(800, 54);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Inventario";
            // 
            // lblAyuda
            // 
            this.lblAyuda.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAyuda.Location = new System.Drawing.Point(32, 86);
            this.lblAyuda.Name = "lblAyuda";
            this.lblAyuda.Size = new System.Drawing.Size(976, 32);
            this.lblAyuda.TabIndex = 1;
            this.lblAyuda.Text = "Busque por código o nombre. Cierre esta ventana para volver al menú.";
            // 
            // lblEstado
            // 
            this.lblEstado.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblEstado.Location = new System.Drawing.Point(32, 689);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(976, 48);
            this.lblEstado.TabIndex = 4;
            // 
            // lblReposicion
            // 
            this.lblReposicion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblReposicion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(75)))), ((int)(((byte)(0)))));
            this.lblReposicion.Location = new System.Drawing.Point(32, 633);
            this.lblReposicion.Name = "lblReposicion";
            this.lblReposicion.Size = new System.Drawing.Size(976, 48);
            this.lblReposicion.TabIndex = 5;
            this.lblReposicion.Text = "Sin productos para reposición en los resultados";
            // 
            // lblDestino
            // 
            this.lblDestino.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDestino.Location = new System.Drawing.Point(16, 26);
            this.lblDestino.Name = "lblDestino";
            this.lblDestino.Size = new System.Drawing.Size(944, 28);
            this.lblDestino.TabIndex = 0;
            this.lblDestino.Text = "Destino: seleccione un producto en la tabla";
            // 
            // lblTipo
            // 
            this.lblTipo.Location = new System.Drawing.Point(16, 66);
            this.lblTipo.Name = "lblTipo";
            this.lblTipo.Size = new System.Drawing.Size(46, 28);
            this.lblTipo.TabIndex = 1;
            this.lblTipo.Text = "Tipo";
            // 
            // lblUnidades
            // 
            this.lblUnidades.Location = new System.Drawing.Point(232, 66);
            this.lblUnidades.Name = "lblUnidades";
            this.lblUnidades.Size = new System.Drawing.Size(88, 28);
            this.lblUnidades.TabIndex = 2;
            this.lblUnidades.Text = "Unidades";
            // 
            // btnIngresar
            // 
            this.btnIngresar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnIngresar.Location = new System.Drawing.Point(32, 449);
            this.btnIngresar.Name = "btnIngresar";
            this.btnIngresar.Size = new System.Drawing.Size(160, 42);
            this.btnIngresar.TabIndex = 4;
            this.btnIngresar.Text = "Ingresar";
            this.btnIngresar.Click += new System.EventHandler(this.Ingresar_Click);
            // 
            // btnActualizar
            // 
            this.btnActualizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnActualizar.Location = new System.Drawing.Point(208, 449);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(160, 42);
            this.btnActualizar.TabIndex = 5;
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.Click += new System.EventHandler(this.Actualizar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEliminar.ForeColor = System.Drawing.Color.Firebrick;
            this.btnEliminar.Location = new System.Drawing.Point(384, 449);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(160, 42);
            this.btnEliminar.TabIndex = 6;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.Click += new System.EventHandler(this.Eliminar_Click);
            // 
            // btnMovimiento
            // 
            this.btnMovimiento.Location = new System.Drawing.Point(498, 58);
            this.btnMovimiento.Name = "btnMovimiento";
            this.btnMovimiento.Size = new System.Drawing.Size(250, 40);
            this.btnMovimiento.TabIndex = 2;
            this.btnMovimiento.Text = "Registrar movimiento";
            this.btnMovimiento.Click += new System.EventHandler(this.Movimiento_Click);
            // 
            // grpMovimientos
            // 
            this.grpMovimientos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpMovimientos.Controls.Add(this.lblDestino);
            this.grpMovimientos.Controls.Add(this.lblTipo);
            this.grpMovimientos.Controls.Add(this.cmbTipo);
            this.grpMovimientos.Controls.Add(this.lblUnidades);
            this.grpMovimientos.Controls.Add(this.txtUnidades);
            this.grpMovimientos.Controls.Add(this.btnMovimiento);
            this.grpMovimientos.Location = new System.Drawing.Point(32, 507);
            this.grpMovimientos.Name = "grpMovimientos";
            this.grpMovimientos.Size = new System.Drawing.Size(976, 112);
            this.grpMovimientos.TabIndex = 7;
            this.grpMovimientos.TabStop = false;
            this.grpMovimientos.Text = "Movimientos de stock";
            // 
            // cmbTipo
            // 
            this.cmbTipo.AccessibleName = "Tipo de movimiento";
            this.cmbTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipo.Items.AddRange(new object[] {
            "Entrada",
            "Salida"});
            this.cmbTipo.Location = new System.Drawing.Point(70, 62);
            this.cmbTipo.Name = "cmbTipo";
            this.cmbTipo.Size = new System.Drawing.Size(140, 29);
            this.cmbTipo.TabIndex = 0;
            // 
            // txtUnidades
            // 
            this.txtUnidades.AccessibleName = "Unidades del movimiento";
            this.txtUnidades.Location = new System.Drawing.Point(324, 62);
            this.txtUnidades.Name = "txtUnidades";
            this.txtUnidades.Size = new System.Drawing.Size(150, 29);
            this.txtUnidades.TabIndex = 1;
            // 
            // txtBusqueda
            // 
            this.txtBusqueda.AccessibleName = "Buscar por código o nombre";
            this.txtBusqueda.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBusqueda.Location = new System.Drawing.Point(32, 134);
            this.txtBusqueda.Name = "txtBusqueda";
            this.txtBusqueda.Size = new System.Drawing.Size(614, 29);
            this.txtBusqueda.TabIndex = 0;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(83)))), ((int)(((byte)(148)))));
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.ForeColor = System.Drawing.Color.White;
            this.btnBuscar.Location = new System.Drawing.Point(662, 128);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(140, 42);
            this.btnBuscar.TabIndex = 1;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.Buscar_Click);
            // 
            // btnMostrarTodos
            // 
            this.btnMostrarTodos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMostrarTodos.Location = new System.Drawing.Point(818, 128);
            this.btnMostrarTodos.Name = "btnMostrarTodos";
            this.btnMostrarTodos.Size = new System.Drawing.Size(190, 42);
            this.btnMostrarTodos.TabIndex = 2;
            this.btnMostrarTodos.Text = "Mostrar todos";
            this.btnMostrarTodos.Click += new System.EventHandler(this.MostrarTodos_Click);
            // 
            // dgvProductos
            // 
            this.dgvProductos.AccessibleName = "Productos encontrados";
            this.dgvProductos.AllowUserToAddRows = false;
            this.dgvProductos.AllowUserToDeleteRows = false;
            this.dgvProductos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvProductos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProductos.BackgroundColor = System.Drawing.Color.White;
            this.dgvProductos.ColumnHeadersHeight = 40;
            this.dgvProductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvProductos.Location = new System.Drawing.Point(32, 194);
            this.dgvProductos.MultiSelect = false;
            this.dgvProductos.Name = "dgvProductos";
            this.dgvProductos.ReadOnly = true;
            this.dgvProductos.RowHeadersVisible = false;
            this.dgvProductos.RowTemplate.Height = 32;
            this.dgvProductos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProductos.Size = new System.Drawing.Size(976, 239);
            this.dgvProductos.TabIndex = 3;
            this.dgvProductos.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.DatosEnlazados);
            this.dgvProductos.SelectionChanged += new System.EventHandler(this.SeleccionCambiada);
            // 
            // frmInventario
            // 
            this.AcceptButton = this.btnBuscar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(251)))));
            this.ClientSize = new System.Drawing.Size(1040, 749);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.lblAyuda);
            this.Controls.Add(this.txtBusqueda);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.btnMostrarTodos);
            this.Controls.Add(this.dgvProductos);
            this.Controls.Add(this.lblEstado);
            this.Controls.Add(this.lblReposicion);
            this.Controls.Add(this.btnIngresar);
            this.Controls.Add(this.btnActualizar);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.grpMovimientos);
            this.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.MinimumSize = new System.Drawing.Size(1056, 718);
            this.Name = "frmInventario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Administración del inventario";
            this.grpMovimientos.ResumeLayout(false);
            this.grpMovimientos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
