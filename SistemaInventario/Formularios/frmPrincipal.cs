using System;
using System.Windows.Forms;
using SistemaInventario.Controlador;

namespace SistemaInventario.Formularios
{
    public partial class frmPrincipal : Form
    {
        private readonly TLista lista = new TLista();
        private readonly frmProductos productos;
        private readonly frmConsultas consultas;

        public frmPrincipal()
        {
            InitializeComponent();
            productos = new frmProductos(lista);
            consultas = new frmConsultas(lista);
            components.Add(productos);
            components.Add(consultas);
        }

        private void Gestion_Click(object sender, EventArgs e)
        {
            productos.ShowDialog(this);
        }

        private void Consultas_Click(object sender, EventArgs e)
        {
            consultas.ShowDialog(this);
        }
    }
}