using System;
using System.Collections.Generic;
using SistemaInventario.Entidades;

namespace SistemaInventario.Controlador
{
    public sealed class TLista
    {
        private readonly List<Producto> productos = new List<Producto>();

        // Instantánea de consulta; los productos son inmutables desde fuera del controlador.
        public IReadOnlyList<Producto> Productos { get { return Array.AsReadOnly(productos.ToArray()); } }

        public Producto ObtenerPorCodigo(string codigo)
        {
            int indice = Indice(codigo);
            return indice < 0 ? null : productos[indice];
        }

        private static bool RequiereReposicion(Producto producto)
        {
            return producto.Cantidad <= producto.StockMinimo;
        }

        public IReadOnlyList<Producto> StockBajo
        {
            get { return productos.FindAll(RequiereReposicion).AsReadOnly(); }
        }

        public void ObtenerResumen(out int diferentes, out long unidades, out int bajos)
        {
            diferentes = productos.Count;
            unidades = 0;
            bajos = 0;
            foreach (var producto in productos)
            {
                unidades += producto.Cantidad;
                if (RequiereReposicion(producto)) bajos++;
            }
        }

        public bool RegistrarMovimiento(string codigo, string tipo, int unidades, out string mensaje)
        {
            int indice = Indice(codigo);
            if (indice < 0) { mensaje = "El producto no existe."; return false; }
            if (tipo != "Entrada" && tipo != "Salida")
            { mensaje = "Seleccione Entrada o Salida."; return false; }
            if (unidades <= 0) { mensaje = "Las unidades deben ser positivas."; return false; }
            var producto = productos[indice];
            if (tipo == "Salida" && unidades > producto.Cantidad)
            { mensaje = "Stock insuficiente para la salida."; return false; }
            if (tipo == "Entrada" && unidades > int.MaxValue - producto.Cantidad)
            { mensaje = "La entrada supera la cantidad máxima permitida."; return false; }
            int cantidad = tipo == "Entrada" ? producto.Cantidad + unidades : producto.Cantidad - unidades;
            productos[indice] = new Producto(producto.Codigo, producto.Nombre, producto.Precio,
                cantidad, producto.StockMinimo);
            mensaje = string.Format("{0} de {1} unidades registrada para {2} / {3}.",
                tipo, unidades, producto.Codigo, producto.Nombre);
            return true;
        }

        public IReadOnlyList<Producto> Buscar(string texto)
        {
            string termino = (texto ?? "").Trim();
            if (termino.Length == 0) return Productos;
            return productos.FindAll(p =>
                p.Codigo.IndexOf(termino, StringComparison.OrdinalIgnoreCase) >= 0 ||
                p.Nombre.IndexOf(termino, StringComparison.OrdinalIgnoreCase) >= 0).AsReadOnly();
        }

        private int Indice(string codigo)
        {
            return productos.FindIndex(p => string.Equals(p.Codigo, (codigo ?? "").Trim(),
                StringComparison.OrdinalIgnoreCase));
        }

        private static string Validar(string codigo, string nombre, decimal precio, int cantidad, int minimo)
        {
            if (string.IsNullOrWhiteSpace(codigo)) return "El código es obligatorio.";
            if (string.IsNullOrWhiteSpace(nombre)) return "El nombre es obligatorio.";
            if (precio < 0) return "El precio no puede ser negativo.";
            if (cantidad < 0) return "La cantidad no puede ser negativa.";
            if (minimo < 0) return "El stock mínimo no puede ser negativo.";
            return null;
        }

        public bool Registrar(string codigo, string nombre, decimal precio, int cantidad, int minimo, out string mensaje)
        {
            mensaje = Validar(codigo, nombre, precio, cantidad, minimo);
            if (mensaje != null) return false;
            if (Indice(codigo) >= 0) { mensaje = "Ya existe un producto con ese código."; return false; }
            productos.Add(new Producto(codigo.Trim(), nombre.Trim(), precio, cantidad, minimo));
            mensaje = "Producto registrado correctamente.";
            return true;
        }

        public bool Modificar(string codigo, string nombre, decimal precio, int cantidad, int minimo, out string mensaje)
        {
            int indice = Indice(codigo);
            if (indice < 0) { mensaje = "El producto no existe."; return false; }
            mensaje = Validar(codigo, nombre, precio, cantidad, minimo);
            if (mensaje != null) return false;
            productos[indice] = new Producto(productos[indice].Codigo, nombre.Trim(), precio, cantidad, minimo);
            mensaje = "Producto modificado correctamente.";
            return true;
        }

        public bool Eliminar(string codigo, out string mensaje)
        {
            int indice = Indice(codigo);
            if (indice < 0) { mensaje = "El producto no existe."; return false; }
            productos.RemoveAt(indice);
            mensaje = "Producto eliminado correctamente.";
            return true;
        }
    }
}
