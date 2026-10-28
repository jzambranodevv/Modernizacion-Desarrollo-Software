# Requerimientos — Sistema de Gestión de Inventario

**Estado:** diseño de v1.2, pendiente de implementar y verificar · **Método:** Spec-Driven Development (SDD)

## Contexto

Una empresa de software necesita evitar pérdida de versiones, desorganización, errores y falta de documentación y automatización. Para demostrar una forma de trabajo basada en SDD e IA se desarrollará un sistema pequeño de inventario en C#

## Necesidades del usuario

* **RF01.** Registrar productos con código, nombre, precio, cantidad inicial y stock mínimo.
* **RF02.** Modificar el nombre, precio, cantidad y stock mínimo de un producto existente, excepto su código, que no cambia en v1.0.
* **RF03.** Eliminar un producto existente.
* **RF04.** Consultar todos los productos registrados en un DataGridView con código, nombre, precio, cantidad y stock mínimo. Actualizar la tabla después de registrar, modificar o eliminar productos.
* **RF05.** Buscar coincidencias parciales por código o nombre, sin distinguir mayúsculas/minúsculas, y mostrar los resultados en el mismo DataGridView. Informar cuando no existan coincidencias.
* **RF06.** Registrar entradas y salidas de unidades de productos existentes; exigir enteros positivos, impedir stock negativo y actualizar las existencias inmediatamente sin modificar el código.
* **RF07.** Identificar y mostrar claramente los productos cuya cantidad sea menor o igual a su stock mínimo, incluidos los de cantidad y mínimo cero.
* **RF08.** Mostrar el total de productos diferentes, la suma de unidades disponibles y el número de productos con stock bajo, calculados sobre todo el inventario.
* **RF09.** Centralizar la administración en frmInventario (nombre que reemplaza frmConsultas en v1.2): Ingresar abre campos vacíos, Actualizar exige una fila y precarga sus datos, Eliminar exige fila y confirmación. Permitir también el ingreso directo desde frmPrincipal. Reutilizar frmProductos con Ingresar o Actualizar según el modo y Cancelar en ambos; eliminar solo desde frmInventario y conservar una sola TLista compartida.

## Reglas de negocio

* **RN01.** Cada código identifica a un solo producto (sin distinguir mayúsculas/minúsculas).
* **RN02.** Código y nombre son obligatorios.
* **RN03.** Precio, cantidad y stock mínimo no pueden ser negativos; las cantidades son enteras.
* **RN04.** No se modifica ni elimina un producto inexistente.
* **RN05.** Consultar y buscar no alteran los productos ni sus datos. La tabla es de solo lectura; las modificaciones y eliminaciones conservan las operaciones existentes por código.
* **RN06.** La búsqueda usa un único texto, sin espacios exteriores, y encuentra productos cuyo código o nombre contenga ese texto. Un texto vacío o solo de espacios muestra todos los productos.
* **RN07.** Al volver a frmInventario después de registrar, modificar o eliminar, actualizar la tabla y reaplicar la búsqueda vigente, si existe. Una operación rechazada conserva los datos y los resultados de la consulta.
* **RN08.** En v1.2, identificar el producto por el código asociado a la fila seleccionada, nunca por su posición en la tabla o en la lista completa. Después de reenlazar resultados, limpiar la selección; no elegir automáticamente otro producto para una operación posterior.
* **RN09.** Cada movimiento modifica solo la cantidad del producto indicado. Rechazar cantidades inválidas, producto inexistente, stock insuficiente o desbordamiento, sin cambios parciales. No se requiere un historial de movimientos en esta versión.
* **RN10.** Recalcular resultados, alertas y resumen después de cada cambio exitoso; respetar la última búsqueda enviada. Cancelaciones y errores no alteran el inventario. El resumen es global y no depende de la búsqueda.
* **RN11.** Conservar la modificación directa de cantidad de RF02 como ajuste absoluto; no convertirla implícitamente en entrada o salida. El código permanece inmutable en todos los modos y movimientos.

## Navegación de v1.1 (histórica)

El sistema inicia en frmPrincipal con los botones «Gestión de productos» y «Consultas». El primero abre frmProductos, que conserva exclusivamente RF01–RF03; el segundo abre frmConsultas, que reúne RF04–RF05. Ambos comparten el mismo inventario durante la sesión. Volver al menú o reabrir un formulario no pierde ni duplica los productos.

En v1.2 esta navegación evoluciona: frmPrincipal es el inicio, muestra el resumen global y dos botones: «Registrar producto», que abre frmProductos en modo ingreso con campos vacíos, e «Inventario», que abre frmInventario, nombre que reemplaza frmConsultas. frmInventario reúne tabla, búsqueda, administración, movimientos y alertas; abre frmProductos solo para ingresar o actualizar, con Cancelar disponible en ambos modos. La eliminación se realiza desde frmInventario. Se mantienen tres formularios. Cerrar o cancelar frmProductos vuelve al formulario que lo abrió (frmPrincipal o frmInventario), que refleja los datos actuales; cerrar frmInventario vuelve al menú; solo cerrar frmPrincipal termina la sesión.

## Alcance del sistema

El sistema se desarrollará en tres versiones:

* **v1.0:** registro, modificación y eliminación de productos.
* **v1.1:** consulta y búsqueda por código o nombre.
* **v1.2:** entradas y salidas de inventario, alertas de existencias bajas y panel de resumen.

En esta etapa se documenta v1.2: conservar RF01–RF05 e incorporar RF06–RF09. Solo se actualizan requirements.md, spec.md, plan.md y tasks.md; no se implementa código ni se realizan commits, push o Pull Requests.

No se incluirán funciones de facturación, proveedores, autenticación de usuarios ni conexión a bases de datos.

