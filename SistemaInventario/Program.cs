using System;
using System.Windows.Forms;
using SistemaInventario.Formularios;

namespace SistemaInventario
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmProductos());
        }
    }
}