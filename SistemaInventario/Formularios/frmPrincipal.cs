using System;
using System.Windows.Forms;
using SistemaInventario.Controlador;

namespace SistemaInventario.Formularios
{
    public partial class frmPrincipal : Form
    {
        private readonly TLista lista = new TLista();
        private readonly frmInventario inventario;

        public frmPrincipal()
        {
            InitializeComponent();
            inventario = new frmInventario(lista);
            components.Add(inventario);
            ActualizarResumen();
        }

        private void ActualizarResumen()
        {
            int diferentes, bajos; long unidades;
            lista.ObtenerResumen(out diferentes, out unidades, out bajos);
            lblDiferentes.Text = "Productos diferentes: " + diferentes;
            lblUnidades.Text = "Unidades disponibles: " + unidades;
            lblBajos.Text = "Productos con stock bajo: " + bajos;
        }

        private void Registrar_Click(object sender, EventArgs e)
        {
            using (var editor = new frmProductos(lista))
            {
                if (editor.ShowDialog(this) == DialogResult.OK)
                    lblEstado.Text = editor.MensajeResultado;
            }
            ActualizarResumen();
        }

        private void Inventario_Click(object sender, EventArgs e)
        {
            inventario.ShowDialog(this);
            ActualizarResumen();
        }
    }
}