using System.Drawing;
using System.Windows.Forms;

namespace SistemaInventario.Formularios
{
    partial class frmPrincipal
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitulo;
        private Label lblDescripcion;
        private Label lblNota;
        private Button btnGestion;
        private Button btnConsultas;

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
            this.btnGestion = new Button();
            this.btnConsultas = new Button();
            this.SuspendLayout();
            this.lblTitulo.Text = "Gestión de inventario";
            this.lblTitulo.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            this.lblTitulo.ForeColor = Color.FromArgb(25, 45, 72);
            this.lblTitulo.Location = new Point(32, 28);
            this.lblTitulo.Size = new Size(610, 56);
            this.lblDescripcion.Text = "Seleccione una opción para continuar.";
            this.lblDescripcion.Location = new Point(36, 98);
            this.lblDescripcion.Size = new Size(600, 36);
            this.btnGestion.Text = "Gestión de productos";
            this.btnGestion.Location = new Point(36, 166);
            this.btnGestion.Size = new Size(290, 72);
            this.btnGestion.BackColor = Color.FromArgb(30, 83, 148);
            this.btnGestion.ForeColor = Color.White;
            this.btnGestion.FlatStyle = FlatStyle.Flat;
            this.btnGestion.TabIndex = 0;
            this.btnGestion.Click += new System.EventHandler(this.Gestion_Click);
            this.btnConsultas.Text = "Consultas";
            this.btnConsultas.Location = new Point(346, 166);
            this.btnConsultas.Size = new Size(290, 72);
            this.btnConsultas.TabIndex = 1;
            this.btnConsultas.Click += new System.EventHandler(this.Consultas_Click);
            this.lblNota.Text = "Versión 1.1 · Los datos se conservan durante esta sesión.";
            this.lblNota.ForeColor = Color.DimGray;
            this.lblNota.Location = new Point(36, 284);
            this.lblNota.Size = new Size(600, 48);
            this.AutoScaleDimensions = new SizeF(9F, 23F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Font = new Font("Segoe UI", 12F);
            this.BackColor = Color.FromArgb(245, 247, 251);
            this.ClientSize = new Size(676, 352);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Inventario · Versión 1.1";
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.lblDescripcion);
            this.Controls.Add(this.btnGestion);
            this.Controls.Add(this.btnConsultas);
            this.Controls.Add(this.lblNota);
            this.ResumeLayout(false);
        }
    }
}