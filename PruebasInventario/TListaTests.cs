using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SistemaInventario.Controlador;

namespace PruebasInventario
{
    [TestClass]
    public class TListaTests
    {
        private TLista lista;
        private string mensaje;

        [TestInitialize]
        public void Preparar()
        {
            lista = new TLista();
            Assert.IsTrue(lista.Registrar("P001", "Teclado", 15.50m, 10, 3, out mensaje));
            Assert.IsTrue(lista.Registrar("P002", "Monitor", 100m, 5, 1, out mensaje));
        }

        private string Estado()
        {
            return string.Join(";", lista.Productos.Select(p =>
                p.Codigo + "|" + p.Nombre + "|" + p.Precio + "|" + p.Cantidad + "|" + p.StockMinimo));
        }

        private void MonitorIntacto()
        {
            var p = lista.Productos.Single(x => x.Codigo == "P002");
            Assert.AreEqual("Monitor", p.Nombre);
            Assert.AreEqual(100m, p.Precio);
            Assert.AreEqual(5, p.Cantidad);
            Assert.AreEqual(1, p.StockMinimo);
        }

        [TestMethod]
        public void CA01_Registro()
        {
            lista = new TLista();
            Assert.IsTrue(lista.Registrar("P001", "Teclado", 15.50m, 10, 3, out mensaje));
            var p = lista.Productos.Single();
            Assert.AreEqual("P001", p.Codigo); Assert.AreEqual("Teclado", p.Nombre);
            Assert.AreEqual(15.50m, p.Precio); Assert.AreEqual(10, p.Cantidad);
            Assert.AreEqual(3, p.StockMinimo); Assert.IsFalse(string.IsNullOrWhiteSpace(mensaje));
        }

        [TestMethod]
        public void CA02_Duplicado()
        {
            string antes = Estado();
            Assert.IsFalse(lista.Registrar("p001", "Otro", 1, 1, 1, out mensaje));
            Assert.AreEqual(antes, Estado()); StringAssert.Contains(mensaje, "código");
        }

        [DataTestMethod]
        [DataRow("", "Teclado", 15, 10, 3)]
        [DataRow("P003", "", 15, 10, 3)]
        [DataRow("P003", "Teclado", -1, 10, 3)]
        [DataRow("P003", "Teclado", 15, -1, 3)]
        [DataRow("P003", "Teclado", 15, 10, -1)]
        [DataRow("   ", "Teclado", 15, 10, 3)]
        [DataRow("P003", "   ", 15, 10, 3)]
        public void CA03_CA04_CA11_RegistroInvalido(string codigo, string nombre, int precio, int cantidad, int minimo)
        {
            string antes = Estado();
            Assert.IsFalse(lista.Registrar(codigo, nombre, precio, cantidad, minimo, out mensaje));
            Assert.AreEqual(antes, Estado()); Assert.IsFalse(string.IsNullOrWhiteSpace(mensaje));
        }

        [TestMethod]
        public void CA05_Modificar()
        {
            Assert.IsTrue(lista.Modificar("P001", "Mouse", 12, 8, 2, out mensaje));
            var p = lista.Productos.Single(x => x.Codigo == "P001");
            Assert.AreEqual("Mouse", p.Nombre); Assert.AreEqual(12m, p.Precio);
            Assert.AreEqual(8, p.Cantidad); Assert.AreEqual(2, p.StockMinimo);
            Assert.AreEqual(2, lista.Productos.Count); MonitorIntacto();
        }

        [TestMethod]
        public void CA06_ModificarInexistente()
        {
            string antes = Estado();
            Assert.IsFalse(lista.Modificar("P999", "Mouse", 12, 8, 2, out mensaje));
            StringAssert.Contains(mensaje, "no existe"); Assert.AreEqual(antes, Estado());
        }

        [DataTestMethod]
        [DataRow(" ", 12, 8, 2)]
        [DataRow("Mouse", -1, 8, 2)]
        [DataRow("Mouse", 12, -2, 2)]
        [DataRow("Mouse", 12, 8, -1)]
        public void CA07_ModificacionInvalida(string nombre, int precio, int cantidad, int minimo)
        {
            string antes = Estado();
            Assert.IsFalse(lista.Modificar("P001", nombre, precio, cantidad, minimo, out mensaje));
            Assert.AreEqual(antes, Estado()); Assert.IsFalse(string.IsNullOrWhiteSpace(mensaje));
        }

        [TestMethod]
        public void CA08_Eliminar()
        {
            Assert.IsTrue(lista.Eliminar("P001", out mensaje));
            Assert.AreEqual(1, lista.Productos.Count); MonitorIntacto();
        }

        [TestMethod]
        public void CA09_EliminarInexistente()
        {
            string antes = Estado();
            Assert.IsFalse(lista.Eliminar("P999", out mensaje));
            StringAssert.Contains(mensaje, "no existe"); Assert.AreEqual(antes, Estado());
        }

        [TestMethod]
        public void CA10_EspaciosYDuplicado()
        {
            var teclado = lista.Productos.Single(x => x.Codigo == "P001");
            Assert.IsTrue(lista.Registrar(" P003 ", " Ratón ", 15.50m, 10, 3, out mensaje));
            var p = lista.Productos.Single(x => x.Codigo == "P003");
            Assert.AreEqual("Ratón", p.Nombre);
            string antes = Estado();
            Assert.IsFalse(lista.Registrar(" p003 ", "Otro", 1, 1, 1, out mensaje));
            Assert.AreEqual(antes, Estado()); MonitorIntacto();
            Assert.AreSame(teclado, lista.Productos.Single(x => x.Codigo == "P001"));
            Assert.AreEqual(3, lista.Productos.Count);
        }

        [DataTestMethod]
        [DataRow(false)]
        [DataRow(true)]
        public void CA12_Ceros(bool modificar)
        {
            var teclado = lista.Productos.Single(x => x.Codigo == "P001");
            if (modificar) Assert.IsTrue(lista.Modificar("P001", "Teclado", 0, 0, 0, out mensaje));
            else Assert.IsTrue(lista.Registrar("P003", "Teclado", 0, 0, 0, out mensaje));
            var p = lista.Productos.Single(x => x.Codigo == (modificar ? "P001" : "P003"));
            Assert.AreEqual(0m, p.Precio); Assert.AreEqual(0, p.Cantidad); Assert.AreEqual(0, p.StockMinimo);
            Assert.AreEqual(modificar ? 2 : 3, lista.Productos.Count); MonitorIntacto();
            if (!modificar) Assert.AreSame(teclado, lista.Productos.Single(x => x.Codigo == "P001"));
        }

        [DataTestMethod]
        [DataRow(false)]
        [DataRow(true)]
        public void CA15_IdentificarPorCodigoNormalizado(bool eliminar)
        {
            if (eliminar)
            {
                Assert.IsTrue(lista.Eliminar(" p001 ", out mensaje));
                Assert.AreEqual(1, lista.Productos.Count);
            }
            else
            {
                Assert.IsTrue(lista.Modificar(" p001 ", " Mouse ", 12, 8, 2, out mensaje));
                var p = lista.Productos.Single(x => x.Codigo == "P001");
                Assert.AreEqual("Mouse", p.Nombre); Assert.AreEqual(12m, p.Precio);
                Assert.AreEqual(8, p.Cantidad); Assert.AreEqual(2, p.StockMinimo);
                Assert.AreEqual(2, lista.Productos.Count);
            }
            MonitorIntacto();
        }
    }
}
