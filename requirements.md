# Requerimientos — Sistema de Gestión de Inventario

**Estado:** propuesta aprobable para v1.0 · **Método:** Spec-Driven Development (SDD)

## Contexto

Una empresa de software necesita evitar pérdida de versiones, desorganización, errores y falta de documentación y automatización. Para demostrar una forma de trabajo basada en SDD e IA se desarrollará un sistema pequeño de inventario en C#

## Necesidades del usuario

* **RF01.** Registrar productos con código, nombre, precio, cantidad inicial y stock mínimo.
* **RF02.** Modificar los datos de un producto existente.
* **RF03.** Eliminar un producto existente.

## Reglas de negocio

* **RN01.** Cada código identifica a un solo producto (sin distinguir mayúsculas/minúsculas).
* **RN02.** Código y nombre son obligatorios.
* **RN03.** Precio, cantidad y stock mínimo no pueden ser negativos; las cantidades son enteras.
* **RN04.** No se modifica ni elimina un producto inexistente.

## Alcance del sistema

El sistema se desarrollará en tres versiones:

* **v1.0:** registro, modificación y eliminación de productos.
* **v1.1:** consulta y búsqueda por código o nombre.
* **v1.2:** entradas y salidas de inventario, alertas de existencias bajas y panel de resumen.

En esta etapa se implementará únicamente la versión 1.0.

No se incluirán funciones de facturación, proveedores, autenticación de usuarios ni conexión a bases de datos.

