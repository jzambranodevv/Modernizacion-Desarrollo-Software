using System.Drawing;
using System.Windows.Forms;

namespace SistemaInventario.Formularios
{
    partial class frmPrincipal
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitulo;
        private Label lblDescripcion;
        private Label lblNota, lblDiferentes, lblUnidades, lblBajos, lblEstado;
        private Button btnRegistrar;
        private Button btnInventario;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblTitulo = new Label();
            this.lblDescripcion = new Label();
            this.lblNota = new Label();
            this.lblDiferentes = new Label();
            this.lblUnidades = new Label();
            this.lblBajos = new Label();
            this.lblEstado = new Label();
            this.btnRegistrar = new Button();
            this.btnInventario = new Button();
            this.SuspendLayout();
            this.lblTitulo.Text = "Gestión de inventario";
            this.lblTitulo.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            this.lblTitulo.ForeColor = Color.FromArgb(25, 45, 72);
            this.lblTitulo.Location = new Point(32, 28);
            this.lblTitulo.Size = new Size(610, 56);
            this.lblDescripcion.Text = "Seleccione una opción para continuar.";
            this.lblDescripcion.Location = new Point(36, 98);
            this.lblDescripcion.Size = new Size(600, 36);
            this.btnRegistrar.Text = "Registrar producto";
            this.btnRegistrar.Location = new Point(36, 166);
            this.btnRegistrar.Size = new Size(290, 72);
            this.btnRegistrar.BackColor = Color.FromArgb(30, 83, 148);
            this.btnRegistrar.ForeColor = Color.White;
            this.btnRegistrar.FlatStyle = FlatStyle.Flat;
            this.btnRegistrar.TabIndex = 0;
            this.btnRegistrar.Click += new System.EventHandler(this.Registrar_Click);
            this.btnInventario.Text = "Inventario";
            this.btnInventario.Location = new Point(346, 166);
            this.btnInventario.Size = new Size(290, 72);
            this.btnInventario.TabIndex = 1;
            this.btnInventario.Click += new System.EventHandler(this.Inventario_Click);
            this.lblNota.Text = "Versión 1.2 · Datos en memoria durante esta sesión.";
            this.lblNota.ForeColor = Color.DimGray;
            this.lblNota.Location = new Point(36, 492);
            this.lblNota.Size = new Size(600, 48);
            this.lblDiferentes.Text = "Productos diferentes: 0";
            this.lblDiferentes.Location = new Point(36, 272);
            this.lblDiferentes.Size = new Size(600, 38);
            this.lblUnidades.Text = "Unidades disponibles: 0";
            this.lblUnidades.Location = new Point(36, 316);
            this.lblUnidades.Size = new Size(600, 38);
            this.lblBajos.Text = "Productos con stock bajo: 0";
            this.lblBajos.Location = new Point(36, 360);
            this.lblBajos.Size = new Size(600, 38);
            this.lblBajos.ForeColor = Color.FromArgb(120, 75, 0);
            this.lblEstado.Location = new Point(36, 418);
            this.lblEstado.Size = new Size(600, 56);
            this.lblEstado.ForeColor = Color.DarkGreen;
            this.AutoScaleDimensions = new SizeF(9F, 23F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Font = new Font("Segoe UI", 12F);
            this.BackColor = Color.FromArgb(245, 247, 251);
            this.ClientSize = new Size(676, 560);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Inventario · Versión 1.2";
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.lblDescripcion);
            this.Controls.Add(this.btnRegistrar);
            this.Controls.Add(this.btnInventario);
            this.Controls.Add(this.lblNota);
            this.Controls.Add(this.lblDiferentes);
            this.Controls.Add(this.lblUnidades);
            this.Controls.Add(this.lblBajos);
            this.Controls.Add(this.lblEstado);
            this.ResumeLayout(false);
        }
    }
}