using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using SistemaInventario.Controlador;
using SistemaInventario.Entidades;

namespace SistemaInventario.Formularios
{
    public partial class frmInventario : Form
    {
        private readonly TLista lista;
        private string busquedaVigente = "";

        public frmInventario() { InitializeComponent(); }

        public frmInventario(TLista lista) : this()
        {
            this.lista = lista ?? throw new ArgumentNullException(nameof(lista));
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (Visible && lista != null) Actualizar();
        }

        private void Actualizar(string mensaje = "")
        {
            var resultados = lista.Buscar(busquedaVigente);
            dgvProductos.DataSource = resultados;
            var bajos = lista.StockBajo.Select(p => p.Codigo).ToArray();
            foreach (DataGridViewRow fila in dgvProductos.Rows)
            {
                var producto = fila.DataBoundItem as Producto;
                bool bajo = producto != null && bajos.Contains(producto.Codigo);
                fila.DefaultCellStyle.BackColor = bajo ? Color.LemonChiffon : Color.White;
                fila.DefaultCellStyle.ForeColor = Color.FromArgb(25, 45, 72);
            }
            var visiblesBajos = resultados.Where(p => bajos.Contains(p.Codigo)).Select(p => p.Codigo).ToArray();
            lblReposicion.Text = visiblesBajos.Length == 0
                ? "Sin productos para reposición en los resultados"
                : "Reposición: " + string.Join(", ", visiblesBajos);
            string estado = resultados.Count > 0 ? string.Format("{0} producto(s).", resultados.Count)
                : string.IsNullOrWhiteSpace(busquedaVigente)
                    ? "No hay productos registrados" : "No se encontraron coincidencias";
            lblEstado.Text = string.IsNullOrEmpty(mensaje) ? estado : mensaje + " " + estado;
            LimpiarSeleccion();
        }

        private void LimpiarSeleccion()
        {
            dgvProductos.ClearSelection();
            dgvProductos.CurrentCell = null;
            lblDestino.Text = "Destino: seleccione un producto en la tabla";
        }

        private void DatosEnlazados(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            LimpiarSeleccion();
        }

        private void SeleccionCambiada(object sender, EventArgs e)
        {
            var producto = dgvProductos.SelectedRows.Count == 1
                ? dgvProductos.SelectedRows[0].DataBoundItem as Producto : null;
            lblDestino.Text = producto == null ? "Destino: seleccione un producto en la tabla"
                : "Destino: " + producto.Codigo + " / " + producto.Nombre;
        }

        private Producto Seleccionado()
        {
            if (dgvProductos.SelectedRows.Count != 1 ||
                !(dgvProductos.SelectedRows[0].DataBoundItem is Producto))
            {
                lblEstado.Text = "Seleccione un producto";
                return null;
            }
            var fila = (Producto)dgvProductos.SelectedRows[0].DataBoundItem;
            var actual = lista.ObtenerPorCodigo(fila.Codigo);
            if (actual == null) Actualizar("El producto no existe.");
            return actual;
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

        private void Ingresar_Click(object sender, EventArgs e)
        {
            using (var editor = new frmProductos(lista))
            {
                var resultado = editor.ShowDialog(this);
                Actualizar(resultado == DialogResult.OK ? editor.MensajeResultado : "");
            }
        }

        private void Actualizar_Click(object sender, EventArgs e)
        {
            var producto = Seleccionado();
            if (producto == null) return;
            using (var editor = new frmProductos(lista, producto.Codigo))
            {
                var resultado = editor.ShowDialog(this);
                Actualizar(resultado == DialogResult.OK ? editor.MensajeResultado : "");
            }
        }

        private void Eliminar_Click(object sender, EventArgs e)
        {
            var producto = Seleccionado();
            if (producto == null) return;
            var respuesta = MessageBox.Show(this,
                "¿Eliminar " + producto.Codigo + " / " + producto.Nombre + "?",
                "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);
            if (respuesta != DialogResult.Yes) return;
            string mensaje;
            lista.Eliminar(producto.Codigo, out mensaje);
            Actualizar(mensaje);
        }

        private void Movimiento_Click(object sender, EventArgs e)
        {
            var producto = Seleccionado();
            if (producto == null) return;
            int unidades;
            if (!int.TryParse(txtUnidades.Text.Trim(), NumberStyles.AllowLeadingSign,
                CultureInfo.InvariantCulture, out unidades))
            {
                lblEstado.Text = "Las unidades deben ser un número entero dentro del rango permitido.";
                txtUnidades.Focus();
                return;
            }
            string mensaje;
            if (lista.RegistrarMovimiento(producto.Codigo, cmbTipo.Text, unidades, out mensaje))
                Actualizar(mensaje);
            else lblEstado.Text = mensaje;
        }
    }
}
