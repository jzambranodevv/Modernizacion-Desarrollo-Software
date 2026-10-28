# Plan técnico — Sistema de Inventario v1.1

**Base:** requirements.md y spec.md. **Estado:** diseño de v1.1, pendiente de implementar y verificar.

## Tecnología y alcance

* Mantener C#, Windows Forms, .NET Framework 4.7.2 y archivos estándar compatibles con Visual Studio Insiders.
* Conservar productos en memoria, sin base de datos ni paquetes visuales externos.
* Mantener Entidades–Controlador–Formularios y las funcionalidades de v1.0. Incorporar solo consulta y búsqueda de RF04–RF05; excluir movimientos, alertas y panel de resumen de v1.2.

## Estructura y responsabilidades

* **Entidades/Producto.cs:** conservar los cinco datos y la inmutabilidad del código, sin ampliar la entidad.
* **Controlador/TLista.cs:** conservar registro, modificación, eliminación y sus validaciones. Proporcionar consulta de todos los productos y búsqueda parcial por código o nombre según EF04–EF05. Centralizar normalización del texto, comparación ordinal sin distinguir mayúsculas, combinación sin duplicados y orden de registro. Devolver resultados de solo lectura o instantáneas que impidan modificar el inventario desde la vista; reutilizar el acceso existente cuando corresponda.
* **Formularios/frmPrincipal.cs y Designer:** nuevo formulario inicial con dos botones, «Gestión de productos» y «Consultas». Crea y conserva una única instancia de TLista durante la sesión y la entrega a ambos formularios secundarios mediante sus constructores.
* **Formularios/frmProductos.cs y Designer:** conservar exclusivamente los campos, botones, conversiones numéricas y mensajes de v1.0. Recibir TLista desde frmPrincipal en lugar de crear un controlador propio; no añadir tabla general ni búsqueda.
* **Formularios/frmConsultas.cs y Designer:** nuevo formulario dedicado a RF04–RF05, con DataGridView de solo lectura, campo de búsqueda y botones Buscar y Mostrar todos. Recibir la misma TLista; mostrar sus resultados sin filtrar ni duplicar reglas de negocio en la interfaz.
* **Program.cs y solución:** iniciar frmPrincipal e incluir los dos nuevos formularios con sus Designer, conservando ambos proyectos existentes. No añadir otras clases, formularios ni bibliotecas innecesarias.
* **PruebasInventario/:** conservar MSTest V2, MSTest.TestFramework y MSTest.TestAdapter 2.2.10 para .NET Framework 4.7.2; ampliar el proyecto existente.

## Presentación y actualización

* frmPrincipal abre los secundarios como ventanas modales, uno a la vez. Conservar una instancia de cada secundario durante la sesión para mantener la búsqueda vigente; cerrarlos devuelve al menú sin destruir el inventario. Cerrar frmPrincipal termina la aplicación y libera los formularios. No crear otra TLista al navegar.
* La única fuente del inventario es TLista. Las filas enlazadas al DataGridView son resultados de presentación, no otra lista de inventario editable ni una copia usada como fuente de las operaciones.
* Mantener controles nativos y Designer funcional. El DataGridView muestra únicamente código, nombre, precio, cantidad y stock mínimo; es de solo lectura, sin alta ni eliminación directa de filas y sin ordenación adicional que cambie el orden de registro.
* Al mostrar frmConsultas por primera vez, consultar todos los productos. En cada apertura posterior, volver a leer TLista y reaplicar la búsqueda vigente, o mostrar todos si no existe. Mostrar todos limpia el campo y la búsqueda vigente.
* Buscar envía el texto a TLista y sustituye las filas. La búsqueda vigente cambia solo al ejecutar Buscar o Mostrar todos; escribir sin enviar no cambia los resultados.
* Tras registrar, modificar o eliminar en frmProductos, la siguiente entrada en frmConsultas refresca la tabla con los datos compartidos. Buscar y Mostrar todos también consultan TLista. No modificar resultados locales para simular cambios ni añadir sincronización simultánea entre ventanas.
* frmProductos muestra la confirmación o el error de su operación. Una operación rechazada no cambia los datos ni los resultados de la consulta. frmConsultas diferencia inventario vacío de búsqueda sin coincidencias, incluido cuando una operación previa deja vacía la búsqueda vigente.
* Mantener identificación por código para modificar/eliminar; la consulta no introduce edición desde la tabla. No añadir filtros avanzados ni movimientos de inventario.

## Verificación y automatización

* **Regresión automática v1.0:** conservar y ejecutar CA01–CA12 y CA15 con datos tipados, estados independientes y conservación de los demás productos.
* **Nuevas pruebas del controlador:** verificar las reglas de consulta y búsqueda de CA16–CA27 sin abrir formularios: cinco datos, vacíos, coincidencias por ambos campos, espacios, mayúsculas, orden, ausencia de duplicados y conservación del inventario. Para CA18 y CA25–CA27, ejecutar la operación y volver a consultar/buscar; esto no sustituye comprobar la actualización visual.
* **Pruebas manuales:** mantener CA13–CA14, las entradas con coma/punto de CA01 y los mensajes de v1.0 en frmProductos. Verificar en CA16–CA28 las columnas, filas, actualización al regresar a Consultas, búsqueda vigente, mensajes y protección de la tabla. CA29 comprueba el menú inicial, retorno desde secundarios y ausencia de pérdidas o duplicados al navegar; revisar además que ambos reciben la misma instancia de TLista. Comprobar apertura y guardado de los tres formularios en el Designer.
* Registrar por criterio y variante el resultado, evidencia o limitación concreta. Distinguir ejecutadas, fallidas y pendientes; no declarar aprobadas las pendientes ni inferir el comportamiento del formulario solo de pruebas del controlador.
* Mantener GitHub Actions en Windows con MSTest V2, MSBuild y VSTest; restaurar NuGet y comprobar el targeting pack 4.7.2, instalando el Developer Pack de Microsoft si falta. El runtime no sustituye los ensamblados de referencia.
* Compilar ambos proyectos en Release: comprobar la asignación Release de la solución y obtener TargetPath de MSBuild para localizar la DLL de pruebas. Verificar el adaptador restaurado, conservar resultados TRX y fallar ante errores de compilación, pruebas fallidas o ausencia de pruebas.
* La integración futura exige regresión, nuevas pruebas, revisión manual y pipeline aprobados. Commits, push, Pull Request y etiqueta requieren autorización posterior; esta etapa solo actualiza documentación.
