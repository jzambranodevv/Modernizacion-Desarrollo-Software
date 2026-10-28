using System.Drawing;
using System.Windows.Forms;

namespace SistemaInventario.Formularios
{
    partial class frmProductos
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitulo, lblAyuda, lblEstado, lblNota;
        private TextBox txtCodigo, txtNombre, txtPrecio, txtCantidad, txtMinimo;
        private Label lblCodigo, lblNombre, lblPrecio, lblCantidad, lblMinimo;
        private Button btnRegistrar, btnModificar, btnEliminar;

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
            this.lblNota = new Label();
            this.lblCodigo = new Label();
            this.txtCodigo = new TextBox();
            this.lblNombre = new Label();
            this.txtNombre = new TextBox();
            this.lblPrecio = new Label();
            this.txtPrecio = new TextBox();
            this.lblCantidad = new Label();
            this.txtCantidad = new TextBox();
            this.lblMinimo = new Label();
            this.txtMinimo = new TextBox();
            this.btnRegistrar = new Button();
            this.btnModificar = new Button();
            this.btnEliminar = new Button();
            this.SuspendLayout();
            this.lblTitulo.Text = "Gestión de productos";
            this.lblTitulo.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            this.lblTitulo.ForeColor = Color.FromArgb(25, 45, 72);
            this.lblTitulo.Location = new Point(32, 24);
            this.lblTitulo.Size = new Size(650, 50);
            this.lblAyuda.Text = "Registre un producto o indique su código para modificarlo o eliminarlo.";
            this.lblAyuda.Location = new Point(36, 82);
            this.lblAyuda.Size = new Size(640, 44);
            this.lblCodigo.Text = "Código del producto";
            this.lblCodigo.Location = new Point(36, 144);
            this.lblCodigo.Size = new Size(175, 28);
            this.txtCodigo.Location = new Point(220, 144);
            this.txtCodigo.Size = new Size(440, 30);
            this.txtCodigo.TabIndex = 0;
            this.txtCodigo.AccessibleName = "Código del producto";
            this.Controls.Add(this.lblCodigo);
            this.Controls.Add(this.txtCodigo);
            this.lblNombre.Text = "Nombre";
            this.lblNombre.Location = new Point(36, 208);
            this.lblNombre.Size = new Size(175, 28);
            this.txtNombre.Location = new Point(220, 208);
            this.txtNombre.Size = new Size(440, 30);
            this.txtNombre.TabIndex = 1;
            this.txtNombre.AccessibleName = "Nombre";
            this.Controls.Add(this.lblNombre);
            this.Controls.Add(this.txtNombre);
            this.lblPrecio.Text = "Precio";
            this.lblPrecio.Location = new Point(36, 272);
            this.lblPrecio.Size = new Size(175, 28);
            this.txtPrecio.Location = new Point(220, 272);
            this.txtPrecio.Size = new Size(440, 30);
            this.txtPrecio.TabIndex = 2;
            this.txtPrecio.AccessibleName = "Precio";
            this.Controls.Add(this.lblPrecio);
            this.Controls.Add(this.txtPrecio);
            this.lblCantidad.Text = "Cantidad";
            this.lblCantidad.Location = new Point(36, 336);
            this.lblCantidad.Size = new Size(175, 28);
            this.txtCantidad.Location = new Point(220, 336);
            this.txtCantidad.Size = new Size(440, 30);
            this.txtCantidad.TabIndex = 3;
            this.txtCantidad.AccessibleName = "Cantidad";
            this.Controls.Add(this.lblCantidad);
            this.Controls.Add(this.txtCantidad);
            this.lblMinimo.Text = "Stock mínimo";
            this.lblMinimo.Location = new Point(36, 400);
            this.lblMinimo.Size = new Size(175, 28);
            this.txtMinimo.Location = new Point(220, 400);
            this.txtMinimo.Size = new Size(440, 30);
            this.txtMinimo.TabIndex = 4;
            this.txtMinimo.AccessibleName = "Stock mínimo";
            this.Controls.Add(this.lblMinimo);
            this.Controls.Add(this.txtMinimo);
            this.btnRegistrar.Text = "Registrar";
            this.btnRegistrar.Location = new Point(36, 478);
            this.btnRegistrar.Size = new Size(196, 44);
            this.btnRegistrar.BackColor = Color.FromArgb(30, 83, 148);
            this.btnRegistrar.ForeColor = Color.White;
            this.btnRegistrar.FlatStyle = FlatStyle.Flat;
            this.btnRegistrar.TabIndex = 5;
            this.btnRegistrar.Click += new System.EventHandler(this.Registrar_Click);
            this.btnModificar.Text = "Modificar";
            this.btnModificar.Location = new Point(250, 478);
            this.btnModificar.Size = new Size(196, 44);
            this.btnModificar.TabIndex = 6;
            this.btnModificar.Click += new System.EventHandler(this.Modificar_Click);
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.Location = new Point(464, 478);
            this.btnEliminar.Size = new Size(196, 44);
            this.btnEliminar.ForeColor = Color.Firebrick;
            this.btnEliminar.TabIndex = 7;
            this.btnEliminar.Click += new System.EventHandler(this.Eliminar_Click);
            this.lblEstado.Location = new Point(36, 542);
            this.lblEstado.Size = new Size(624, 60);
            this.lblEstado.Text = "Listo para registrar un producto.";
            this.lblNota.Location = new Point(36, 614);
            this.lblNota.Size = new Size(624, 42);
            this.lblNota.ForeColor = Color.DimGray;
            this.lblNota.Text = "Datos temporales: se pierden al cerrar la aplicación. Cierre esta ventana para volver al menú.";
            this.AutoScaleDimensions = new SizeF(9F, 23F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Font = new Font("Segoe UI", 12F);
            this.BackColor = Color.FromArgb(245, 247, 251);
            this.ClientSize = new Size(704, 680);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Gestión de productos · Inventario";
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.lblAyuda);
            this.Controls.Add(this.btnRegistrar);
            this.Controls.Add(this.btnModificar);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.lblEstado);
            this.Controls.Add(this.lblNota);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
