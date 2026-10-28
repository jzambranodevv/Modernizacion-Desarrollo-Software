# Especificación de comportamiento — v1.1

**Fuente:** `requirements.md` · **Estado:** evolución v1.1 pendiente de implementar y probar; se conservan EF01–EF03 y CA01–CA15 de v1.0.

Esta especificación describe el comportamiento esperado y la distribución de formularios acordada; no genera código.

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

## EF04 — Consultar productos (RF04)

* **Entrada:** abrir frmConsultas desde frmPrincipal o solicitar Mostrar todos en frmConsultas. La primera apertura muestra todos; al volver se conserva y reaplica la búsqueda vigente, si existe.
* **Éxito:** mostrar todos los productos en el mismo DataGridView, con columnas código, nombre, precio, cantidad y stock mínimo, una fila por producto y en orden de registro. Mostrar todos limpia la búsqueda vigente.
* **Sin productos:** mostrar la tabla sin filas de datos e informar «No hay productos registrados».
* **Reglas:** tabla de solo lectura, sin alta, edición ni eliminación directa de filas. Consultar no modifica el inventario. Las operaciones EF01–EF03 mantienen sus campos, validaciones e identificación por código.
* **Actualización:** al volver a frmConsultas después de gestionar productos en frmProductos, consultar de nuevo el inventario compartido. Si hay búsqueda vigente, reaplicarla según EF05. Una operación rechazada no altera los resultados; su error se muestra en frmProductos. Buscar y Mostrar todos también consultan siempre los datos actuales.

## EF05 — Buscar productos (RF05)

* **Entrada:** un texto y la acción Buscar en frmConsultas, sin selector adicional de campo.
* **Reglas:** ignorar espacios exteriores; buscar el texto como subcadena del código o del nombre, sin distinguir mayúsculas/minúsculas (comparación ordinal). Combinar ambas coincidencias sin duplicar productos; conservar el orden de registro y no alterar el inventario.
* **Éxito:** sustituir las filas del DataGridView por todos los productos coincidentes, con las cinco columnas de EF04; no acumular resultados de búsquedas anteriores.
* **Sin coincidencias:** tabla sin filas de datos e informar «No se encontraron coincidencias».
* **Texto vacío o solo espacios:** quitar la búsqueda vigente y aplicar EF04, incluido su mensaje si no hay productos.
* **Búsqueda vigente:** es el último texto enviado mediante Buscar, no el texto todavía sin enviar. Se conserva al volver al menú. Al regresar a frmConsultas tras EF01–EF03 se recalculan sus resultados; si quedan vacíos, informar sin coincidencias. La confirmación de la operación se muestra en frmProductos.

## Navegación y sesión compartida

frmPrincipal es el formulario inicial y presenta «Gestión de productos» y «Consultas». Abre frmProductos para EF01–EF03 y frmConsultas para EF04–EF05; cerrar un formulario secundario devuelve al menú. frmProductos no contiene tabla general ni herramientas de búsqueda. Ambos reciben la misma instancia de TLista, conservada durante toda la sesión, sin listas de inventario independientes. La consulta se refresca cada vez que se vuelve a ella; no se exige actualización simultánea de ventanas. Solo cerrar frmPrincipal finaliza la sesión y descarta los datos en memoria.

## Criterios de aceptación de v1.1

Se conservan sin renumerar CA01–CA15. Cada caso y variante siguiente restaura Base o Vacío, definidos arriba. Las consultas y búsquedas se realizan en frmConsultas y las operaciones EF01–EF03 en frmProductos, accediendo desde frmPrincipal. Tras gestionar productos, volver al menú y entrar de nuevo en Consultas para comprobar los resultados. Todas las consultas y búsquedas conservan íntegros los datos; los cambios de inventario solo ocurren mediante EF01–EF03.

|ID|Estado inicial|Prueba|Resultado esperado|
|-|-|-|-|
|CA16|Base, registrada mediante EF01|Consultar Mostrar todos|Se muestran P001 y P002, en ese orden, con sus cinco datos exactos y sin filas adicionales de alta.|
|CA17|Vacío|Abrir frmConsultas o Mostrar todos, por separado|Tabla vacía y mensaje «No hay productos registrados».|
|CA18|Base, sin búsqueda|Registrar P003 / Mouse / 12 / 8 / 2; por separado, modificar P001 con esos cuatro valores; por separado, eliminar P001|La tabla se actualiza: tres filas al registrar, dos con los nuevos datos al modificar, solo P002 al eliminar. Los demás productos se conservan.|
|CA19|Base|Buscar «p00»; por separado, «001»|Se muestran P001 y P002 en la primera variante, solo P001 en la segunda, con las cinco columnas.|
|CA20|Base|Buscar «tEc»; por separado, «TOR»|Se muestra solo P001 por Teclado en la primera variante y solo P002 por Monitor en la segunda.|
|CA21|Base|Buscar « P001 »; por separado, « monitor »|Se ignoran espacios exteriores y mayúsculas: solo P001 o solo P002, respectivamente.|
|CA22|Base|Buscar «inexistente»; por separado, buscar «P001» y luego «Monitor»|Sin coincidencias: tabla vacía y mensaje correspondiente. En la segunda variante, la tabla sustituye P001 por P002.|
|CA23|Base, después de buscar «P001»|Buscar texto vacío; buscar solo espacios; pulsar Mostrar todos, por separado|Se limpia la búsqueda vigente y se muestran ambos productos. Desde Vacío, las tres variantes muestran el estado de CA17.|
|CA24|Base|Buscar «o»; por separado, registrar P003 / P003 / 1 / 1 / 0 y buscar «P003»|Primera variante: P001 y P002 una vez cada uno. Segunda: P003 aparece una sola vez aunque coincidan código y nombre. Consultar y buscar no alteran ningún dato.|
|CA25|Base, después de buscar «Mouse»|Registrar P003 / Mouse / 12 / 8 / 2; por separado, modificar P001 con nombre Mouse y números válidos|frmProductos confirma la operación. Al volver a frmConsultas se mantiene la búsqueda: aparece únicamente el producto registrado o modificado, con datos actualizados.|
|CA26|Base, después de buscar «Teclado»|Modificar P001 con nombre Mouse y números válidos; por separado, eliminar P001|frmProductos confirma la operación. Al volver a frmConsultas se reaplica la búsqueda: tabla vacía y aviso sin coincidencias. P002 permanece intacto.|
|CA27|Base, después de buscar «P001»|Intentar registrar p001; modificar P001 con cantidad -2; eliminar P999, por separado|Cada operación se rechaza con su mensaje; inventario y filas mostradas permanecen iguales.|
|CA28|Base|En frmConsultas, intentar editar, agregar o eliminar filas directamente en el DataGridView|La tabla impide las tres acciones. EF01–EF03 siguen operando en frmProductos y el código no cambia.|
|CA29|Vacío, nueva sesión|Comprobar el menú inicial; entrar en Gestión de productos, registrar Base, cerrar al menú y abrir Consultas. Repetir la navegación sin cerrar frmPrincipal|Los dos botones abren el formulario correspondiente; frmProductos no tiene tabla general ni búsqueda. Consultas muestra Base sin pérdidas ni duplicados y ambos formularios usan la misma instancia de TLista. Cerrar un secundario permite volver al menú.|

## Restricciones para la implementación

* Implementar solo v1.0 y la evolución RF04–RF05 de v1.1. No agregar movimientos, alertas ni panel de resumen de v1.2, ni búsquedas distintas de EF05.
* Mantener la identificación por código para modificar o eliminar; el DataGridView incorpora la consulta y búsqueda, sin cambiar los contratos EF01–EF03.
* No afirmar cumplimiento hasta ejecutar y registrar las pruebas

## Trazabilidad

RF01 → EF01 → CA01–CA04, CA10–CA14; RF02 → EF02 → CA05–CA07, CA12–CA15; RF03 → EF03 → CA08–CA09, CA15.

RF04 → EF04 → CA16–CA18, CA23, CA27–CA28; RF05 → EF05 → CA19–CA27. CA18 y CA25–CA27 comprueban también la integración con RF01–RF03. Navegación de RF01–RF05 → Navegación y sesión compartida → CA29.

