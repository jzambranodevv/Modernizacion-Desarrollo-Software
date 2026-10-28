# Especificación de comportamiento — v1.0

**Fuente:** `requirements.md` · **Estado:** pendiente de implementar y probar

Esta especificación describe el comportamiento esperado; no prescribe todavía clases, formularios ni código

## Datos

Producto: **código** (texto), **nombre** (texto), **precio** (decimal), **cantidad** (entero), **stock mínimo** (entero). Se almacenan temporalmente en memoria. Se ignoran espacios iniciales/finales de código y nombre; el código no distingue mayúsculas/minúsculas

## EF01 — Registrar producto (RF01)

* **Entrada:** código, nombre, precio, cantidad inicial y stock mínimo
* **Condiciones:** código y nombre con contenido; código no repetido; precio numérico >= 0; cantidad y stock mínimo enteros >= 0
* **Éxito:** guardar exactamente un producto y confirmar el registro
* **Error:** explicar el dato inválido o duplicado; no agregar ningún producto

## EF02 — Modificar producto (RF02)

* **Entrada:** código del producto existente y nuevos valores de nombre, precio, cantidad y stock mínimo
* **Condiciones:** el código existe y los nuevos datos cumplen las validaciones de EF01. El código no cambia en esta versión
* **Éxito:** actualizar solo el producto indicado y confirmar
* **Error:** si no existe o algún dato es inválido, no cambiar ningún producto

## EF03 — Eliminar producto (RF03)

* **Entrada:** código del producto
* **Éxito:** si existe, eliminar únicamente ese producto y confirmar
* **Error:** si no existe, informar y no modificar los datos

## Criterios de aceptación (comprobables)

|ID|Prueba|Resultado esperado|
|-|-|-|
|CA01|Registrar P001 / Teclado / 15,50 / 10 / 3\|Se guarda el producto con precio decimal 15,50 (equivalente a 15.50), cantidad 10 y stock mínimo 3.\||Se guarda el producto con esos datos.|
|CA02|Registrar p001 después de P001|Se rechaza por duplicado.|
|CA03|Registrar código vacío o nombre en blanco|Se rechaza; no cambia el total.|
|CA04|Registrar precio -1 o cantidad -1 o mínimo -1|Se rechaza; no cambia el total.|
|CA05|Modificar P001 con nombre Mouse, precio 12, cantidad 8, mínimo 2|Se actualizan esos cuatro datos, manteniendo P001.|
|CA06|Modificar P999|Se informa que no existe; sin cambios.|
|CA07|Modificar P001 con cantidad -2|Se rechaza; conserva los datos anteriores.|
|CA08|Eliminar P001|Se elimina únicamente P001.|
|CA09|Eliminar P999|Se informa que no existe; sin cambios.|

## Restricciones para la implementación

* Implementar **solo v1.0**. No agregar búsqueda, consultas generales, movimientos ni panel de resumen todavía
* La forma de seleccionar un producto para modificar/eliminar puede ser básica, sin constituir un módulo de consulta avanzado
* No afirmar cumplimiento hasta ejecutar y registrar las pruebas

## Trazabilidad

RF01 → EF01 → CA01–CA04; RF02 → EF02 → CA05–CA07; RF03 → EF03 → CA08–CA09

