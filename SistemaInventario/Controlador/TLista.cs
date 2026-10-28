using System;
using System.Collections.Generic;
using SistemaInventario.Entidades;

namespace SistemaInventario.Controlador
{
    public sealed class TLista
    {
        private readonly List<Producto> productos = new List<Producto>();

        // Instantánea de solo lectura para comprobar el estado sin permitir mutaciones externas.
        public IReadOnlyList<Producto> Productos { get { return productos.ToArray(); } }

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