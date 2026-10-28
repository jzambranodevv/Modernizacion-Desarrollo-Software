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
        private Button btnGuardar, btnCancelar;

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
            this.btnGuardar = new Button();
            this.btnCancelar = new Button();
            this.SuspendLayout();
            this.lblTitulo.Text = "Ingresar producto";
            this.lblTitulo.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            this.lblTitulo.ForeColor = Color.FromArgb(25, 45, 72);
            this.lblTitulo.Location = new Point(32, 24);
            this.lblTitulo.Size = new Size(650, 50);
            this.lblAyuda.Text = "Complete los datos del nuevo producto.";
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
            this.btnGuardar.Text = "Ingresar";
            this.btnGuardar.Location = new Point(36, 478);
            this.btnGuardar.Size = new Size(196, 44);
            this.btnGuardar.BackColor = Color.FromArgb(30, 83, 148);
            this.btnGuardar.ForeColor = Color.White;
            this.btnGuardar.FlatStyle = FlatStyle.Flat;
            this.btnGuardar.TabIndex = 5;
            this.btnGuardar.Click += new System.EventHandler(this.Guardar_Click);
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Location = new Point(464, 478);
            this.btnCancelar.Size = new Size(196, 44);
            this.btnCancelar.ForeColor = Color.Firebrick;
            this.btnCancelar.TabIndex = 7;
            this.btnCancelar.DialogResult = DialogResult.Cancel;
            this.lblEstado.Location = new Point(36, 542);
            this.lblEstado.Size = new Size(624, 60);
            this.lblEstado.Text = "Listo para registrar un producto.";
            this.lblNota.Location = new Point(36, 614);
            this.lblNota.Size = new Size(624, 42);
            this.lblNota.ForeColor = Color.DimGray;
            this.lblNota.Text = "Datos temporales: se pierden al cerrar la aplicación. Cancelar cierra sin guardar.";
            this.AutoScaleDimensions = new SizeF(9F, 23F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Font = new Font("Segoe UI", 12F);
            this.BackColor = Color.FromArgb(245, 247, 251);
            this.ClientSize = new Size(704, 680);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Productos · Inventario";
            this.AcceptButton = this.btnGuardar;
            this.CancelButton = this.btnCancelar;
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.lblAyuda);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.lblEstado);
            this.Controls.Add(this.lblNota);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
