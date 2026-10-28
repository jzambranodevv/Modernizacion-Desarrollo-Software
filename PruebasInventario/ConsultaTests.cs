using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SistemaInventario.Controlador;
using SistemaInventario.Entidades;

namespace PruebasInventario
{
    [TestClass]
    public class ConsultaTests
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

        private static string Estado(IEnumerable<Producto> productos)
        {
            return string.Join(";", productos.Select(p => string.Join("|",
                p.Codigo, p.Nombre, p.Precio, p.Cantidad, p.StockMinimo)));
        }

        private void MonitorIntacto()
        {
            var monitor = lista.Productos.Single(p => p.Codigo == "P002");
            Assert.AreEqual("Monitor", monitor.Nombre);
            Assert.AreEqual(100m, monitor.Precio);
            Assert.AreEqual(5, monitor.Cantidad);
            Assert.AreEqual(1, monitor.StockMinimo);
        }

        [TestMethod]
        public void CA16_ConsultaCompletaConservaDatosYOrden()
        {
            var antes = Estado(lista.Productos);
            var resultado = lista.Productos;
            CollectionAssert.AreEqual(new[] { "P001", "P002" }, resultado.Select(p => p.Codigo).ToArray());
            Assert.AreEqual("Teclado", resultado[0].Nombre);
            Assert.AreEqual(15.50m, resultado[0].Precio);
            Assert.AreEqual(10, resultado[0].Cantidad);
            Assert.AreEqual(3, resultado[0].StockMinimo);
            MonitorIntacto();
            Assert.AreEqual(antes, Estado(resultado));
            Assert.AreEqual(antes, Estado(lista.Productos));
        }

        [DataTestMethod]
        [DataRow("")]
        [DataRow("   ")]
        [DataRow(null)]
        [DataRow("P001")]
        public void CA17_CA22_CA23_InventarioVacio(string texto)
        {
            var vacia = new TLista();
            Assert.AreEqual(0, vacia.Productos.Count);
            Assert.AreEqual(0, vacia.Buscar(texto).Count);
            Assert.AreEqual(0, vacia.Productos.Count);
        }

        [DataTestMethod]
        [DataRow("registrar")]
        [DataRow("modificar")]
        [DataRow("eliminar")]
        public void CA18_ConsultaNuevaReflejaGestion(string operacion)
        {
            var anterior = lista.Productos;
            string estadoAnterior = Estado(anterior);
            if (operacion == "registrar")
            {
                Assert.IsTrue(lista.Registrar("P003", "Mouse", 12, 8, 2, out mensaje));
                CollectionAssert.AreEqual(new[] { "P001", "P002", "P003" },
                    lista.Productos.Select(p => p.Codigo).ToArray());
                Assert.AreEqual(12m, lista.Productos[2].Precio);
                Assert.AreEqual(8, lista.Productos[2].Cantidad);
                Assert.AreEqual(2, lista.Productos[2].StockMinimo);
                Assert.AreSame(anterior[0], lista.Productos[0]);
            }
            else if (operacion == "modificar")
            {
                Assert.IsTrue(lista.Modificar("P001", "Mouse", 12, 8, 2, out mensaje));
                Assert.AreEqual(2, lista.Productos.Count);
                Assert.AreEqual("P001", lista.Productos[0].Codigo);
                Assert.AreEqual("Mouse", lista.Productos[0].Nombre);
                Assert.AreEqual(12m, lista.Productos[0].Precio);
                Assert.AreEqual(8, lista.Productos[0].Cantidad);
                Assert.AreEqual(2, lista.Productos[0].StockMinimo);
            }
            else
            {
                Assert.IsTrue(lista.Eliminar("P001", out mensaje));
                Assert.AreEqual("P002", lista.Productos.Single().Codigo);
            }
            MonitorIntacto();
            // Los resultados anteriores son instantáneas, no otro inventario mutable.
            Assert.AreEqual(estadoAnterior, Estado(anterior));
        }

        [DataTestMethod]
        [DataRow("p00", "P001,P002")]
        [DataRow("001", "P001")]
        [DataRow("tEc", "P001")]
        [DataRow("TOR", "P002")]
        [DataRow(" P001 ", "P001")]
        [DataRow(" monitor ", "P002")]
        [DataRow("inexistente", "")]
        [DataRow("o", "P001,P002")]
        public void CA19_CA24_BusquedaParcialOrdinalSinAlterarDatos(string texto, string esperado)
        {
            string antes = Estado(lista.Productos);
            var resultado = lista.Buscar(texto);
            Assert.AreEqual(esperado, string.Join(",", resultado.Select(p => p.Codigo)));
            foreach (var producto in resultado)
                Assert.AreEqual(Estado(new[] { lista.Productos.Single(p => p.Codigo == producto.Codigo) }),
                    Estado(new[] { producto }));
            Assert.AreEqual(antes, Estado(lista.Productos));
        }

        [TestMethod]
        public void CA22_BusquedasSucesivasNoAcumulanResultados()
        {
            var primero = lista.Buscar("P001");
            Assert.AreEqual("P001", primero.Single().Codigo);
            var segundo = lista.Buscar("Monitor");
            Assert.AreEqual("P002", segundo.Single().Codigo);
            Assert.AreEqual("P001", primero.Single().Codigo);
            Assert.AreEqual(2, lista.Productos.Count);
        }

        [DataTestMethod]
        [DataRow("")]
        [DataRow("   ")]
        [DataRow(null)]
        public void CA23_TextoVacioConsultaTodos(string texto)
        {
            Assert.AreEqual(1, lista.Buscar("P001").Count);
            Assert.AreEqual(Estado(lista.Productos), Estado(lista.Buscar(texto)));
        }

        [TestMethod]
        public void CA24_CoincidenciaEnAmbosCamposNoDuplica()
        {
            Assert.IsTrue(lista.Registrar("P003", "P003", 1, 1, 0, out mensaje));
            string antes = Estado(lista.Productos);
            Assert.AreEqual("P003", lista.Buscar("P003").Single().Codigo);
            Assert.AreEqual(antes, Estado(lista.Productos));
        }

        [DataTestMethod]
        [DataRow(false)]
        [DataRow(true)]
        public void CA25_ReaplicarBusquedaIncluyeRegistroOModificacion(bool modificar)
        {
            Assert.AreEqual(0, lista.Buscar("Mouse").Count);
            if (modificar)
                Assert.IsTrue(lista.Modificar("P001", "Mouse", 12, 8, 2, out mensaje));
            else
                Assert.IsTrue(lista.Registrar("P003", "Mouse", 12, 8, 2, out mensaje));
            var encontrado = lista.Buscar("Mouse").Single();
            Assert.AreEqual(modificar ? "P001" : "P003", encontrado.Codigo);
            Assert.AreEqual("Mouse", encontrado.Nombre);
            Assert.AreEqual(12m, encontrado.Precio);
            Assert.AreEqual(8, encontrado.Cantidad);
            Assert.AreEqual(2, encontrado.StockMinimo);
            Assert.IsFalse(string.IsNullOrWhiteSpace(mensaje));
            MonitorIntacto();
        }

        [DataTestMethod]
        [DataRow(false)]
        [DataRow(true)]
        public void CA26_ReaplicarBusquedaExcluyeModificadoOEliminado(bool eliminar)
        {
            Assert.AreEqual("P001", lista.Buscar("Teclado").Single().Codigo);
            if (eliminar) Assert.IsTrue(lista.Eliminar("P001", out mensaje));
            else Assert.IsTrue(lista.Modificar("P001", "Mouse", 12, 8, 2, out mensaje));
            Assert.AreEqual(0, lista.Buscar("Teclado").Count);
            Assert.IsFalse(string.IsNullOrWhiteSpace(mensaje));
            MonitorIntacto();
        }

        [DataTestMethod]
        [DataRow("registrar")]
        [DataRow("modificar")]
        [DataRow("eliminar")]
        public void CA27_RechazoConservaConsultaEInventario(string operacion)
        {
            string antes = Estado(lista.Productos);
            string consulta = Estado(lista.Buscar("P001"));
            bool exito;
            if (operacion == "registrar") exito = lista.Registrar("p001", "Otro", 12, 8, 2, out mensaje);
            else if (operacion == "modificar") exito = lista.Modificar("P001", "Mouse", 12, -2, 2, out mensaje);
            else exito = lista.Eliminar("P999", out mensaje);
            Assert.IsFalse(exito);
            Assert.IsFalse(string.IsNullOrWhiteSpace(mensaje));
            Assert.AreEqual(antes, Estado(lista.Productos));
            Assert.AreEqual(consulta, Estado(lista.Buscar("P001")));
        }

        [DataTestMethod]
        [DataRow("")]
        [DataRow("P001")]
        public void ConsultasNoPermitenModificarElInventarioDesdeSusResultados(string texto)
        {
            string antes = Estado(lista.Productos);
            var resultado = (IList<Producto>)lista.Buscar(texto);
            Assert.IsTrue(resultado.IsReadOnly);
            Assert.ThrowsException<NotSupportedException>(() => resultado.Clear());
            Assert.ThrowsException<NotSupportedException>(() => resultado[0] = resultado[0]);
            Assert.AreEqual(antes, Estado(lista.Productos));
        }
    }
}