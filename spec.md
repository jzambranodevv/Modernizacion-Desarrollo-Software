# Especificación de comportamiento — v1.0

**Fuente:** `requirements.md` · **Estado:** pendiente de implementar y probar

Esta especificación describe el comportamiento esperado; no prescribe todavía clases, formularios ni código

## Datos

Producto: **código** (texto), **nombre** (texto), **precio** (decimal), **cantidad** (entero), **stock mínimo** (entero). Se almacenan temporalmente en memoria. Se ignoran espacios iniciales/finales de código y nombre; el código no distingue mayúsculas/minúsculas

El formulario convierte las entradas a los tipos correspondientes antes de invocar al controlador. El precio admite coma o punto decimal, sin separadores de miles; cantidad y stock mínimo admiten solo enteros. Una entrada no convertible se rechaza con un mensaje y sin cambiar los datos.

## EF01 — Registrar producto (RF01)

* **Entrada:** código, nombre, precio, cantidad inicial y stock mínimo
* **Condiciones:** código y nombre con contenido; código no repetido; precio numérico >= 0; cantidad y stock mínimo enteros >= 0
* **Éxito:** guardar exactamente un producto y confirmar el registro
* **Error:** explicar el dato inválido o duplicado; no agregar ningún producto

## EF02 — Modificar producto (RF02)

La modificación directa de cantidad pertenece a v1.0. El registro de movimientos de entrada y salida se incorporará en v1.2.

* **Entrada:** código del producto existente y nuevos valores de nombre, precio, cantidad y stock mínimo
* **Condiciones:** el código existe y los nuevos datos cumplen las validaciones de EF01. El código no cambia en esta versión
* **Éxito:** actualizar solo el producto indicado y confirmar
* **Error:** si no existe o algún dato es inválido, no cambiar ningún producto

## EF03 — Eliminar producto (RF03)

* **Entrada:** código del producto
* **Éxito:** si existe, eliminar únicamente ese producto y confirmar
* **Error:** si no existe, informar y no modificar los datos

## Criterios de aceptación (comprobables)

Cada caso y cada variante se ejecutan de forma independiente, restaurando su estado inicial. **Base** contiene P001 / Teclado / 15,50 / 10 / 3 y P002 / Monitor / 100 / 5 / 1. **Vacío** no contiene productos. En entradas parciales se usan los restantes datos válidos de P001; para nuevos registros se usa P003. Todo rechazo conserva todos los productos y sus valores; toda operación exitosa conserva los demás productos.

|ID|Estado inicial|Prueba|Resultado esperado|
|-|-|-|-|
|CA01|Vacío|Registrar P001 / Teclado / 15,50 / 10 / 3; repetir desde Vacío con 15.50|Se guarda exactamente un producto con precio decimal 15,50, cantidad 10 y mínimo 3; se confirma.|
|CA02|Base|Registrar p001 con los demás datos válidos|Se rechaza por duplicado; sin cambios.|
|CA03|Base|Registrar con código vacío o nombre en blanco, por separado|Se rechaza cada variante; sin cambios.|
|CA04|Base|Registrar con precio -1, cantidad -1 o mínimo -1, por separado|Se rechaza cada variante; sin cambios.|
|CA05|Base|Modificar P001 con nombre Mouse, precio 12, cantidad 8, mínimo 2|Se actualizan esos cuatro datos, manteniendo P001 y P002 intacto; se confirma.|
|CA06|Base|Modificar P999 con datos válidos|Se informa que no existe; sin cambios.|
|CA07|Base|Modificar P001 con nombre en blanco, precio -1, cantidad -2 o mínimo -1, por separado|Se rechaza cada variante; conserva todos los datos anteriores.|
|CA08|Base|Eliminar P001|Se elimina únicamente P001, se conserva P002 intacto y se confirma.|
|CA09|Base|Eliminar P999|Se informa que no existe; sin cambios.|
|CA10|Base|Registrar código « P003 » y nombre « Ratón » con números válidos; intentar luego « p003 »|Se guardan P003 y Ratón sin espacios exteriores; el segundo registro se rechaza por duplicado.|
|CA11|Base|Registrar con código compuesto solo por espacios; repetir con nombre compuesto solo por espacios|Se rechaza cada variante; sin cambios.|
|CA12|Base|Registrar P003 con precio, cantidad y mínimo en cero; por separado, modificar P001 con esos tres valores en cero|Ambas operaciones admiten cero y conservan los demás productos.|
|CA13|Base|En el formulario, registrar P003 o modificar P001 introduciendo «abc» en precio, cantidad o mínimo, un campo por variante|Se explica el dato no numérico; sin cambios.|
|CA14|Base|En el formulario, registrar P003 o modificar P001 con cantidad o mínimo 1,5 o 1.5, por separado|Se rechaza la cantidad fraccionaria, sin redondear ni alterar los datos.|
|CA15|Base|Modificar « p001 » con nombre « Mouse » y números válidos; por separado, eliminar « p001 »|Se identifica P001 ignorando espacios y mayúsculas; al modificar se guarda Mouse y se conserva el código P001; P002 permanece intacto.|

## Restricciones para la implementación

* Implementar **solo v1.0**. No agregar búsqueda, consultas generales, movimientos ni panel de resumen todavía
* Para modificar o eliminar, solo se permite seleccionar un producto por su código mediante un selector sencillo de códigos existentes o la entrada del código. No se permiten listados generales de productos, filtros ni búsquedas avanzadas.
* No afirmar cumplimiento hasta ejecutar y registrar las pruebas

## Trazabilidad

RF01 → EF01 → CA01–CA04, CA10–CA14; RF02 → EF02 → CA05–CA07, CA12–CA15; RF03 → EF03 → CA08–CA09, CA15.

