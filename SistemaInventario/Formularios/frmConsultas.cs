using System;
using System.Windows.Forms;
using SistemaInventario.Controlador;

namespace SistemaInventario.Formularios
{
    public partial class frmConsultas : Form
    {
        private readonly TLista lista;
        private string busquedaVigente = "";

        // El constructor sin parámetros permite abrir el diseñador sin crear otro inventario.
        public frmConsultas() { InitializeComponent(); }

        public frmConsultas(TLista lista) : this()
        {
            this.lista = lista ?? throw new ArgumentNullException(nameof(lista));
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (Visible && lista != null) Actualizar();
        }

        private void Actualizar()
        {
            var resultados = lista.Buscar(busquedaVigente);
            dgvProductos.DataSource = resultados;
            lblEstado.Text = resultados.Count > 0
                ? string.Format("{0} producto(s).", resultados.Count)
                : string.IsNullOrWhiteSpace(busquedaVigente)
                    ? "No hay productos registrados"
                    : "No se encontraron coincidencias";
        }

        private void Buscar_Click(object sender, EventArgs e)
        {
            busquedaVigente = txtBusqueda.Text;
            Actualizar();
        }

        private void MostrarTodos_Click(object sender, EventArgs e)
        {
            txtBusqueda.Clear();
            busquedaVigente = "";
            Actualizar();
        }
    }
}