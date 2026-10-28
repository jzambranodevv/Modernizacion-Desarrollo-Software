using System;
using System.Globalization;
using System.Windows.Forms;
using SistemaInventario.Controlador;

namespace SistemaInventario.Formularios
{
    public partial class frmProductos : Form
    {
        private readonly TLista lista;
        private readonly string codigoOriginal;
        public string MensajeResultado { get; private set; }

        // Constructor para el diseñador; los accesos de la aplicación inyectan TLista.
        public frmProductos() { InitializeComponent(); }

        public frmProductos(TLista lista, string codigo = null) : this()
        {
            this.lista = lista ?? throw new ArgumentNullException(nameof(lista));
            codigoOriginal = codigo;
            bool actualizar = codigo != null;
            txtCodigo.ReadOnly = actualizar;
            btnGuardar.Text = actualizar ? "Actualizar" : "Ingresar";
            lblTitulo.Text = actualizar ? "Actualizar producto" : "Ingresar producto";
            lblAyuda.Text = actualizar ? "Actualice los datos. El código identifica al producto y no cambia."
                : "Complete los datos del nuevo producto.";
            lblEstado.Text = "";
            if (!actualizar) return;
            var producto = lista.ObtenerPorCodigo(codigo);
            if (producto == null)
            {
                Mostrar(false, "El producto no existe.");
                btnGuardar.Enabled = false;
                return;
            }
            txtCodigo.Text = producto.Codigo;
            txtNombre.Text = producto.Nombre;
            txtPrecio.Text = producto.Precio.ToString(CultureInfo.CurrentCulture);
            txtCantidad.Text = producto.Cantidad.ToString(CultureInfo.InvariantCulture);
            txtMinimo.Text = producto.StockMinimo.ToString(CultureInfo.InvariantCulture);
        }

        private bool Convertir(out decimal precio, out int cantidad, out int minimo)
        {
            precio = 0; cantidad = 0; minimo = 0;
            var estilo = NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint;
            if (!decimal.TryParse(txtPrecio.Text.Trim().Replace(',', '.'), estilo,
                CultureInfo.InvariantCulture, out precio))
            {
                Mostrar(false, "Introduzca un precio numérico con coma o punto decimal, sin separadores de miles.");
                txtPrecio.Focus(); return false;
            }
            if (!int.TryParse(txtCantidad.Text.Trim(), NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out cantidad))
            {
                Mostrar(false, "La cantidad debe ser un número entero."); txtCantidad.Focus(); return false;
            }
            if (!int.TryParse(txtMinimo.Text.Trim(), NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out minimo))
            {
                Mostrar(false, "El stock mínimo debe ser un número entero."); txtMinimo.Focus(); return false;
            }
            return true;
        }

        private void Mostrar(bool exito, string mensaje)
        {
            lblEstado.ForeColor = exito ? System.Drawing.Color.DarkGreen : System.Drawing.Color.Firebrick;
            lblEstado.Text = mensaje;
            lblEstado.AccessibleName = mensaje;
        }

        private void Guardar_Click(object sender, EventArgs e)
        {
            decimal precio; int cantidad, minimo; string mensaje;
            if (!Convertir(out precio, out cantidad, out minimo)) return;
            bool exito = codigoOriginal == null
                ? lista.Registrar(txtCodigo.Text, txtNombre.Text, precio, cantidad, minimo, out mensaje)
                : lista.Modificar(codigoOriginal, txtNombre.Text, precio, cantidad, minimo, out mensaje);
            if (!exito) { Mostrar(false, mensaje); return; }
            MensajeResultado = mensaje;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}