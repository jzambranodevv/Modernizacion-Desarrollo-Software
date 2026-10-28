namespace SistemaInventario.Entidades
{
    public sealed class Producto
    {
        public string Codigo { get; private set; }
        public string Nombre { get; private set; }
        public decimal Precio { get; private set; }
        public int Cantidad { get; private set; }
        public int StockMinimo { get; private set; }

        internal Producto(string codigo, string nombre, decimal precio, int cantidad, int stockMinimo)
        {
            Codigo = codigo;
            Nombre = nombre;
            Precio = precio;
            Cantidad = cantidad;
            StockMinimo = stockMinimo;
        }
    }
}