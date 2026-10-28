using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SistemaInventario.Controlador;

namespace PruebasInventario
{
    [TestClass]
    public class InventarioTests
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
                string.Join("|", p.Codigo, p.Nombre, p.Precio, p.Cantidad, p.StockMinimo)));
        }

        private void Resumen(int diferentes, long unidades, int bajos)
        {
            int actualDiferentes, actualBajos; long actualUnidades;
            lista.ObtenerResumen(out actualDiferentes, out actualUnidades, out actualBajos);
            Assert.AreEqual(diferentes, actualDiferentes);
            Assert.AreEqual(unidades, actualUnidades);
            Assert.AreEqual(bajos, actualBajos);
        }

        [DataTestMethod]
        [DataRow("P001")]
        [DataRow(" p001 ")]
        public void CA31_IdentidadYDatosParaPrecarga(string codigo)
        {
            var p = lista.ObtenerPorCodigo(codigo);
            Assert.AreEqual("P001", p.Codigo);
            Assert.AreEqual("Teclado", p.Nombre);
            Assert.AreEqual(15.50m, p.Precio);
            Assert.AreEqual(10, p.Cantidad);
            Assert.AreEqual(3, p.StockMinimo);
        }

        [DataTestMethod]
        [DataRow("actualizar")]
        [DataRow("eliminar")]
        [DataRow("mover")]
        public void CA33_CA34_IdentidadEnPrimeraFilaFiltrada(string accion)
        {
            var teclado = lista.ObtenerPorCodigo("P001");
            var destino = lista.Buscar("Monitor").Single();
            Assert.AreEqual("P002", destino.Codigo);
            if (accion == "actualizar")
            {
                Assert.IsTrue(lista.Modificar(destino.Codigo, "Pantalla", 100, 5, 1, out mensaje));
                Assert.AreEqual(0, lista.Buscar("Monitor").Count);
                Assert.AreEqual("Pantalla", lista.ObtenerPorCodigo("P002").Nombre);
            }
            else if (accion == "eliminar")
            {
                Assert.IsTrue(lista.Eliminar(destino.Codigo, out mensaje));
                Assert.IsNull(lista.ObtenerPorCodigo("P002"));
                Assert.AreEqual(0, lista.Buscar("Monitor").Count);
            }
            else
            {
                Assert.IsTrue(lista.RegistrarMovimiento(destino.Codigo, "Entrada", 2, out mensaje));
                Assert.AreEqual(7, lista.ObtenerPorCodigo("P002").Cantidad);
            }
            Assert.AreSame(teclado, lista.ObtenerPorCodigo("P001"));
        }

        [DataTestMethod]
        [DataRow("actualizar")]
        [DataRow("eliminar")]
        [DataRow("Entrada")]
        [DataRow("Salida")]
        public void CA36_DestinoInexistenteSinCambios(string accion)
        {
            string antes = Estado();
            Assert.IsNull(lista.ObtenerPorCodigo("P999"));
            bool exito = accion == "actualizar" ? lista.Modificar("P999", "Otro", 1, 1, 1, out mensaje)
                : accion == "eliminar" ? lista.Eliminar("P999", out mensaje)
                : lista.RegistrarMovimiento("P999", accion, 1, out mensaje);
            Assert.IsFalse(exito);
            StringAssert.Contains(mensaje, "no existe");
            Assert.AreEqual(antes, Estado());
        }

        [TestMethod]
        public void CA37_EntradaActualizaSoloCantidadYUsaSaldoActual()
        {
            var anterior = lista.ObtenerPorCodigo("P001");
            var otro = lista.ObtenerPorCodigo("P002");
            Assert.IsTrue(lista.RegistrarMovimiento(" p001 ", "Entrada", 5, out mensaje));
            var actual = lista.ObtenerPorCodigo("P001");
            Assert.AreEqual(15, actual.Cantidad);
            Assert.AreEqual(anterior.Codigo, actual.Codigo);
            Assert.AreEqual(anterior.Nombre, actual.Nombre);
            Assert.AreEqual(anterior.Precio, actual.Precio);
            Assert.AreEqual(anterior.StockMinimo, actual.StockMinimo);
            Assert.AreSame(otro, lista.ObtenerPorCodigo("P002"));
            StringAssert.Contains(mensaje, "Entrada");
            StringAssert.Contains(mensaje, "5");
            StringAssert.Contains(mensaje, "P001");
            StringAssert.Contains(mensaje, "Teclado");
            Assert.IsTrue(lista.RegistrarMovimiento(anterior.Codigo, "Salida", 1, out mensaje));
            Assert.AreEqual(14, lista.ObtenerPorCodigo("P001").Cantidad);
            Assert.AreEqual(10, anterior.Cantidad);
        }

        [DataTestMethod]
        [DataRow(4, 6)]
        [DataRow(10, 0)]
        public void CA38_SalidaAdmiteStockFinalCero(int unidades, int esperado)
        {
            var anterior = lista.ObtenerPorCodigo("P001");
            var otro = lista.ObtenerPorCodigo("P002");
            Assert.IsTrue(lista.RegistrarMovimiento("P001", "Salida", unidades, out mensaje));
            var actual = lista.ObtenerPorCodigo("P001");
            Assert.AreEqual(esperado, actual.Cantidad);
            Assert.AreEqual(anterior.Codigo, actual.Codigo);
            Assert.AreEqual(anterior.Nombre, actual.Nombre);
            Assert.AreEqual(anterior.Precio, actual.Precio);
            Assert.AreEqual(anterior.StockMinimo, actual.StockMinimo);
            Assert.AreSame(otro, lista.ObtenerPorCodigo("P002"));
        }

        [DataTestMethod]
        [DataRow("Entrada", 0)]
        [DataRow("Entrada", -1)]
        [DataRow("Salida", 0)]
        [DataRow("Salida", -1)]
        public void CA39_UnidadesNoPositivas(string tipo, int unidades)
        {
            string antes = Estado();
            Assert.IsFalse(lista.RegistrarMovimiento("P001", tipo, unidades, out mensaje));
            Assert.IsFalse(string.IsNullOrWhiteSpace(mensaje));
            Assert.AreEqual(antes, Estado());
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("Transferencia")]
        public void CA39_TipoInvalido(string tipo)
        {
            string antes = Estado();
            Assert.IsFalse(lista.RegistrarMovimiento("P001", tipo, 1, out mensaje));
            StringAssert.Contains(mensaje, "Entrada o Salida");
            Assert.AreEqual(antes, Estado());
        }

        [TestMethod]
        public void CA40_SalidaInsuficienteEsAtomica()
        {
            string antes = Estado();
            Assert.IsFalse(lista.RegistrarMovimiento("P001", "Salida", 11, out mensaje));
            StringAssert.Contains(mensaje, "insuficiente");
            Assert.AreEqual(antes, Estado());
        }

        [TestMethod]
        public void CA41_DesbordamientoEsAtomico()
        {
            Assert.IsTrue(lista.Modificar("P001", "Teclado", 15.50m, int.MaxValue, 3, out mensaje));
            string antes = Estado();
            Assert.IsFalse(lista.RegistrarMovimiento("P001", "Entrada", 1, out mensaje));
            StringAssert.Contains(mensaje, "máxima");
            Assert.AreEqual(antes, Estado());
        }

        [DataTestMethod]
        [DataRow(0, int.MaxValue)]
        [DataRow(int.MaxValue - 1, 1)]
        public void LimiteSuperiorDeEntradaEsValido(int saldo, int entrada)
        {
            Assert.IsTrue(lista.Modificar("P001", "Teclado", 15.50m, saldo, 3, out mensaje));
            Assert.IsTrue(lista.RegistrarMovimiento("P001", "Entrada", entrada, out mensaje));
            Assert.AreEqual(int.MaxValue, lista.ObtenerPorCodigo("P001").Cantidad);
        }

        [TestMethod]
        public void CA42_UmbralInclusivoYCero()
        {
            lista = new TLista();
            Assert.IsTrue(lista.Registrar("A", "A", 1, 2, 3, out mensaje));
            Assert.IsTrue(lista.Registrar("B", "B", 1, 3, 3, out mensaje));
            Assert.IsTrue(lista.Registrar("C", "C", 1, 4, 3, out mensaje));
            Assert.IsTrue(lista.Registrar("D", "D", 0, 0, 0, out mensaje));
            string antes = Estado();
            CollectionAssert.AreEqual(new[] { "A", "B", "D" }, lista.StockBajo.Select(p => p.Codigo).ToArray());
            Resumen(4, 9, 3);
            Assert.AreEqual(antes, Estado());
        }

        [DataTestMethod]
        [DataRow(false)]
        [DataRow(true)]
        public void CA43_AlertasSeRecalculanTrasCadaCambio(bool cambiarMinimo)
        {
            if (cambiarMinimo)
                Assert.IsTrue(lista.Modificar("P001", "Teclado", 15.50m, 10, 10, out mensaje));
            else Assert.IsTrue(lista.RegistrarMovimiento("P001", "Salida", 7, out mensaje));
            Assert.AreEqual("P001", lista.StockBajo.Single().Codigo);
            Assert.IsTrue(lista.RegistrarMovimiento("P001", "Entrada", 1, out mensaje));
            Assert.AreEqual(0, lista.StockBajo.Count);
            Assert.IsTrue(lista.Modificar("P001", "Teclado", 15.50m, 0, 3, out mensaje));
            Assert.AreEqual(1, lista.StockBajo.Count);
            Assert.IsTrue(lista.Eliminar("P001", out mensaje));
            Assert.AreEqual(0, lista.StockBajo.Count);
        }

        [DataTestMethod]
        [DataRow(false)]
        [DataRow(true)]
        public void CA44_ResumenBaseYVacio(bool vacio)
        {
            if (vacio) lista = new TLista();
            string antes = Estado();
            Resumen(vacio ? 0 : 2, vacio ? 0 : 15, 0);
            Assert.AreEqual(antes, Estado());
        }

        [DataTestMethod]
        [DataRow("entrada", 2, 20L, 0)]
        [DataRow("salida", 2, 8L, 1)]
        [DataRow("ingreso", 3, 15L, 1)]
        [DataRow("ajuste", 2, 7L, 1)]
        [DataRow("eliminar", 1, 5L, 0)]
        public void CA45_ResumenTrasCambios(string operacion, int diferentes, long unidades, int bajos)
        {
            bool exito;
            switch (operacion)
            {
                case "entrada": exito = lista.RegistrarMovimiento("P001", "Entrada", 5, out mensaje); break;
                case "salida": exito = lista.RegistrarMovimiento("P001", "Salida", 7, out mensaje); break;
                case "ingreso": exito = lista.Registrar("P003", "Mouse", 1, 0, 0, out mensaje); break;
                case "ajuste": exito = lista.Modificar("P001", "Teclado", 15.50m, 2, 3, out mensaje); break;
                default: exito = lista.Eliminar("P001", out mensaje); break;
            }
            Assert.IsTrue(exito);
            Resumen(diferentes, unidades, bajos);
        }

        [TestMethod]
        public void CA46_ResumenGlobalIndependienteDelFiltro()
        {
            Assert.IsTrue(lista.Modificar("P001", "Teclado", 15.50m, 3, 3, out mensaje));
            Assert.AreEqual("P002", lista.Buscar("Monitor").Single().Codigo);
            Assert.AreEqual("P001", lista.StockBajo.Single().Codigo);
            Resumen(2, 8, 1);
        }

        [TestMethod]
        public void CA47_SumaUnidadesEn64Bits()
        {
            Assert.IsTrue(lista.Modificar("P001", "Teclado", 15.50m, int.MaxValue, 0, out mensaje));
            Assert.IsTrue(lista.Modificar("P002", "Monitor", 100, int.MaxValue, 0, out mensaje));
            Resumen(2, 4294967294L, 0);
        }

        [DataTestMethod]
        [DataRow("ingreso")]
        [DataRow("actualizar")]
        [DataRow("eliminar")]
        [DataRow("movimiento")]
        public void CA48_ReconsultaRespetaTextoEnviado(string operacion)
        {
            string filtro = operacion == "ingreso" ? "Mouse" : operacion == "movimiento" ? "P001" : "Teclado";
            lista.Buscar(filtro);
            bool exito;
            if (operacion == "ingreso") exito = lista.Registrar("P003", "Mouse", 12, 8, 2, out mensaje);
            else if (operacion == "actualizar") exito = lista.Modificar("P001", "Mouse", 12, 8, 2, out mensaje);
            else if (operacion == "eliminar") exito = lista.Eliminar("P001", out mensaje);
            else exito = lista.RegistrarMovimiento("P001", "Entrada", 1, out mensaje);
            Assert.IsTrue(exito);
            var resultado = lista.Buscar(filtro);
            if (operacion == "ingreso") Assert.AreEqual("P003", resultado.Single().Codigo);
            else if (operacion == "movimiento") Assert.AreEqual(11, resultado.Single().Cantidad);
            else Assert.AreEqual(0, resultado.Count);
            Assert.AreEqual("Monitor", lista.ObtenerPorCodigo("P002").Nombre);
        }

        [DataTestMethod]
        [DataRow(false)]
        [DataRow(true)]
        public void CA50_ResumenTrasIngresoUnico(bool desdeBase)
        {
            if (!desdeBase) lista = new TLista();
            Assert.IsTrue(lista.Registrar("P003", "Mouse", 12, 8, 2, out mensaje));
            Resumen(desdeBase ? 3 : 1, desdeBase ? 23 : 8, 0);
            Assert.AreEqual("P003", lista.Buscar("Mouse").Single().Codigo);
        }
    }
}