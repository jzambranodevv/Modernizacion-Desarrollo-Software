# Requerimientos — Sistema de Gestión de Inventario

**Estado:** especificación de la evolución a v1.1, pendiente de implementar · **Método:** Spec-Driven Development (SDD)

## Contexto

Una empresa de software necesita evitar pérdida de versiones, desorganización, errores y falta de documentación y automatización. Para demostrar una forma de trabajo basada en SDD e IA se desarrollará un sistema pequeño de inventario en C#

## Necesidades del usuario

* **RF01.** Registrar productos con código, nombre, precio, cantidad inicial y stock mínimo.
* **RF02.** Modificar el nombre, precio, cantidad y stock mínimo de un producto existente, excepto su código, que no cambia en v1.0.
* **RF03.** Eliminar un producto existente.
* **RF04.** Consultar todos los productos registrados en un DataGridView con código, nombre, precio, cantidad y stock mínimo. Actualizar la tabla después de registrar, modificar o eliminar productos.
* **RF05.** Buscar coincidencias parciales por código o nombre, sin distinguir mayúsculas/minúsculas, y mostrar los resultados en el mismo DataGridView. Informar cuando no existan coincidencias.

## Reglas de negocio

* **RN01.** Cada código identifica a un solo producto (sin distinguir mayúsculas/minúsculas).
* **RN02.** Código y nombre son obligatorios.
* **RN03.** Precio, cantidad y stock mínimo no pueden ser negativos; las cantidades son enteras.
* **RN04.** No se modifica ni elimina un producto inexistente.
* **RN05.** Consultar y buscar no alteran los productos ni sus datos. La tabla es de solo lectura; las modificaciones y eliminaciones conservan las operaciones existentes por código.
* **RN06.** La búsqueda usa un único texto, sin espacios exteriores, y encuentra productos cuyo código o nombre contenga ese texto. Un texto vacío o solo de espacios muestra todos los productos.
* **RN07.** Al volver a Consultas después de registrar, modificar o eliminar, actualizar la tabla y reaplicar la búsqueda vigente, si existe. Una operación rechazada conserva los datos y los resultados de la consulta.

## Navegación de v1.1

El sistema inicia en frmPrincipal con los botones «Gestión de productos» y «Consultas». El primero abre frmProductos, que conserva exclusivamente RF01–RF03; el segundo abre frmConsultas, que reúne RF04–RF05. Ambos comparten el mismo inventario durante la sesión. Volver al menú o reabrir un formulario no pierde ni duplica los productos.

## Alcance del sistema

El sistema se desarrollará en tres versiones:

* **v1.0:** registro, modificación y eliminación de productos.
* **v1.1:** consulta y búsqueda por código o nombre.
* **v1.2:** entradas y salidas de inventario, alertas de existencias bajas y panel de resumen.

En esta etapa se documenta la versión 1.1, que conserva RF01–RF03 de v1.0 e incorpora únicamente RF04–RF05. El código sigue siendo inmutable. No se incorporarán movimientos, alertas ni panel de resumen de v1.2.

No se incluirán funciones de facturación, proveedores, autenticación de usuarios ni conexión a bases de datos.

