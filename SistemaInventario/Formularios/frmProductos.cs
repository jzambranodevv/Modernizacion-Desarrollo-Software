using System;
using System.Globalization;
using System.Windows.Forms;
using SistemaInventario.Controlador;

namespace SistemaInventario.Formularios
{
    public partial class frmProductos : Form
    {
        private readonly TLista lista;

        public frmProductos() { InitializeComponent(); }

        public frmProductos(TLista lista) : this()
        {
            this.lista = lista ?? throw new ArgumentNullException(nameof(lista));
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

        private void Registrar_Click(object sender, EventArgs e)
        {
            decimal precio; int cantidad, minimo; string mensaje;
            if (!Convertir(out precio, out cantidad, out minimo)) return;
            bool exito = lista.Registrar(txtCodigo.Text, txtNombre.Text, precio, cantidad, minimo, out mensaje);
            Mostrar(exito, mensaje);
        }

        private void Modificar_Click(object sender, EventArgs e)
        {
            decimal precio; int cantidad, minimo; string mensaje;
            if (!Convertir(out precio, out cantidad, out minimo)) return;
            bool exito = lista.Modificar(txtCodigo.Text, txtNombre.Text, precio, cantidad, minimo, out mensaje);
            Mostrar(exito, mensaje);
        }

        private void Eliminar_Click(object sender, EventArgs e)
        {
            string mensaje;
            bool exito = lista.Eliminar(txtCodigo.Text, out mensaje);
            Mostrar(exito, mensaje);
        }
    }
}
