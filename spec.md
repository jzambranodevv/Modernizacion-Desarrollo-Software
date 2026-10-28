# Especificación de comportamiento — v1.2

**Fuente:** `requirements.md` · **Estado:** diseño de v1.2 pendiente de implementar y probar. EF01–EF05 y CA01–CA29 se conservan como base histórica; la sección Evolución a v1.2 identifica expresamente sus cambios de interfaz.

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

## Restricciones de v1.1 (históricas)

* Implementar solo v1.0 y la evolución RF04–RF05 de v1.1. No agregar movimientos, alertas ni panel de resumen de v1.2, ni búsquedas distintas de EF05.
* Mantener la identificación por código para modificar o eliminar; el DataGridView incorpora la consulta y búsqueda, sin cambiar los contratos EF01–EF03.
* No afirmar cumplimiento hasta ejecutar y registrar las pruebas

## Trazabilidad

RF01 → EF01 → CA01–CA04, CA10–CA14; RF02 → EF02 → CA05–CA07, CA12–CA15; RF03 → EF03 → CA08–CA09, CA15.

RF04 → EF04 → CA16–CA18, CA23, CA27–CA28; RF05 → EF05 → CA19–CA27. CA18 y CA25–CA27 comprueban también la integración con RF01–RF03. Navegación de RF01–RF05 → Navegación y sesión compartida → CA29.


## Evolución a v1.2 y compatibilidad

Esta sección sustituye las restricciones de alcance y navegación históricas de v1.1, sin borrar EF01–EF05 ni CA01–CA29. En v1.2, frmConsultas se renombra a frmInventario: es el mismo formulario evolucionado, no un cuarto formulario. Las referencias anteriores a frmConsultas/Consultas se conservan solo como historia; su regresión de interfaz se realiza sobre frmInventario. Las reglas de datos, validaciones, búsquedas y atomicidad siguen vigentes.

|Base anterior|Evolución explícita en v1.2|Verificación|
|-|-|-|
|EF01–EF03 y CA01–CA15|Mismas operaciones del controlador. La interfaz usa Ingresar/Actualizar por modos; la eliminación se solicita desde frmInventario con confirmación. Los casos de código inexistente se conservan en pruebas del controlador.|Regresión y CA30–CA36|
|EF02, CA05, CA07, CA12–CA15|Se conserva la edición directa de cantidad como ajuste absoluto; los movimientos son operaciones adicionales, sin doble aplicación del cambio.|CA31, CA37–CA41|
|EF04–EF05 y CA16–CA27|Mismas cinco columnas y búsqueda. La gestión se abre desde frmInventario y se refresca al cerrar el editor o finalizar una eliminación/movimiento; ya no exige pasar por el menú. La confirmación de eliminar corresponde a frmInventario.|CA33–CA35, CA48|
|CA28 (protección de tabla)|Sigue prohibida la edición directa de celdas y alta/baja mediante filas; los nuevos botones sí permiten gestionar el producto seleccionado.|CA32–CA35|
|Navegación de v1.1 y CA29|El menú ofrece «Registrar producto» e «Inventario» y un resumen; reemplaza los accesos históricos «Gestión de productos» y «Consultas». El editor retorna al formulario que lo abrió y conserva el inventario compartido.|CA49–CA50|

Los criterios históricos se ejecutan como regresión del negocio; sus pasos de interfaz incompatibles se adaptan mediante esta tabla y se registran como evolucionados, no como aprobaciones literales de la interfaz antigua. Movimientos significa aplicar y confirmar entradas/salidas en la sesión: no incluye historial, fechas, auditoría ni persistencia adicional.

## EF06 — Administrar desde frmInventario y editar por modos (RF09)

* **Navegación:** frmPrincipal es el inicio, muestra el resumen global y conserva una única TLista. Sus dos botones son «Registrar producto», que abre frmProductos modal en ingreso con campos vacíos, e «Inventario», que abre frmInventario modal. frmInventario también abre frmProductos modal para ingresar o actualizar, entregando esa misma TLista y el modo. Al cerrar el editor, devolver a quien lo abrió (frmPrincipal o frmInventario); la búsqueda vigente se conserva al ir al menú. No duplicar inventarios.
* **Selección:** una sola fila completa, inicialmente ninguna. Actualizar, Eliminar y movimientos requieren selección explícita; si falta, informar «Seleccione un producto» sin operar. Capturar el código del objeto enlazado, no un índice ni el texto de búsqueda. Revalidar su existencia en TLista antes de cargar/confirmar; ante inexistencia, informar, refrescar sin afectar otro producto.
* **Modo ingreso:** campos vacíos, código editable y solo Ingresar y Cancelar disponibles. Aplicar EF01. No copiar la selección ni datos de aperturas anteriores.
* **Modo actualización:** cargar los cinco datos actuales del código seleccionado; código visible y de solo lectura, solo Actualizar y Cancelar disponibles. Aplicar EF02 al código original aunque el contenido visible intentara cambiarse. Conservar el ajuste absoluto de cantidad y las validaciones anteriores.
* **Botones de frmProductos:** mostrar Ingresar o Actualizar según el modo, además de Cancelar en ambos. Cancelar cierra sin guardar; no mostrar ni habilitar Eliminar en el editor, pues la eliminación corresponde exclusivamente a frmInventario. No añadir otro modo ni una ruta duplicada.
* **Guardado/cancelación:** un éxito guarda una sola vez, confirma y cierra el editor. Un error mantiene el editor abierto y sus entradas, sin alterar datos. Cancelar o cerrar sin guardar cancela sin cambios.
* **Eliminar desde frmInventario:** confirmar mostrando código y nombre capturados, con No como opción predeterminada. No o cerrar la confirmación cancela; Sí invoca EF03 únicamente para ese código y confirma el resultado.
* **Refresco:** al regresar del editor a frmInventario y tras eliminar o mover, volver a leer TLista y reaplicar el último texto enviado mediante Buscar. Mostrar todos sigue limpiándolo; texto escrito sin enviar no lo reemplaza. Un error o cancelación conserva resultados si no hubo cambios. Después de cada reenlace, quitar selección y fila actual para impedir acciones accidentales sobre otro producto. Si un éxito deja la búsqueda sin filas, mostrar tanto la confirmación como «No se encontraron coincidencias».

## EF07 — Registrar entrada o salida (RF06)

* **Entrada:** producto seleccionado en frmInventario, tipo Entrada o Salida y unidades. Mostrar código y nombre de destino, sin permitir editar el código. TLista recibe la identidad y decide la validez de la operación.
* **Condiciones:** producto existente; unidades enteras de 1 a int.MaxValue. El formulario rechaza texto vacío, no numérico, fraccionario o fuera de int; TLista rechaza unidades no positivas y tipo no válido. Una salida no puede superar la cantidad actual; una entrada no puede superar int.MaxValue como cantidad resultante.
* **Éxito:** sumar o restar exactamente una vez sobre la cantidad actual del producto, sin modificar código, nombre, precio, mínimo ni otros productos. Aplicar atómicamente, confirmar tipo/unidades/destino y refrescar tabla y alertas antes de la siguiente interacción.
* **Error:** informar dato inválido, inexistencia, stock insuficiente o desbordamiento, sin cambios parciales. No reutilizar la cantidad precargada en una fila como saldo para calcular el movimiento.
* No se exige confirmación adicional para movimientos. El ajuste absoluto de EF02 no registra ni ejecuta automáticamente una entrada/salida.

## EF08 — Alertas de stock bajo (RF07)

* **Regla:** stock bajo si cantidad <= stock mínimo, incluidos 0 y 0. TLista calcula la condición; consultar alertas no modifica el inventario.
* **Presentación:** en frmInventario, destacar las filas coincidentes con un estilo y mostrar además el texto «Reposición:» con sus códigos visibles, o «Sin productos para reposición en los resultados». La señal no depende solo del color. Mantener exactamente las cinco columnas y la búsqueda de EF05.
* **Actualización:** recalcular después de ingreso, actualización (incluidos cantidad/mínimo), eliminación y movimientos, y al consultar/buscar/reabrir. La señalización corresponde a las filas visibles; el número global aparece en el resumen, aunque la búsqueda oculte productos con stock bajo.

## EF09 — Resumen global (RF08)

* En frmPrincipal mostrar: productos diferentes = número de productos; unidades disponibles = suma de cantidades; productos con stock bajo = número que cumple EF08. En Vacío mostrar 0 / 0 / 0.
* TLista calcula sobre todos los productos, sin depender de búsqueda o selección. Sumar unidades en entero de 64 bits para no desbordar int; no sumar precios ni contar resultados filtrados.
* Actualizar al iniciar y al volver de frmInventario o del editor abierto mediante «Registrar producto», antes de habilitar el menú. Un ingreso directo también se refleja al abrir o reabrir frmInventario, reaplicando su búsqueda vigente. Mientras el menú esté bloqueado por una ventana modal no se exige actualización simultánea; al reabrir, usar los datos actuales. No mantener contadores independientes del inventario.

## Criterios de aceptación de v1.2

Cada caso y variante restaura Base o Vacío de CA01–CA29, salvo el estado indicado. Todo rechazo/cancelación conserva todos los datos; todo éxito afecta únicamente al destino. La búsqueda se ejecuta antes de seleccionar la fila.

|ID|Estado inicial|Prueba|Resultado esperado|
|-|-|-|-|
|CA30|Base|Seleccionar P001 y pulsar Ingresar; ingresar P003 / Mouse / 12 / 8 / 2|Editor vacío con código editable y solo Ingresar y Cancelar disponibles; se registra P003 una vez, confirma, cierra y actualiza tres filas.|
|CA31|Base|Seleccionar P001 y Actualizar; cambiar a Mouse / 12 / 8 / 2|Cinco datos precargados, código P001 bloqueado y solo Actualizar y Cancelar disponibles; conserva P001, aplica los cuatro valores y no afecta P002 ni duplica el ajuste de cantidad.|
|CA32|Base sin selección; repetir desde búsqueda sin coincidencias|Intentar Actualizar, Eliminar o movimiento, por separado|No se abre editor/confirmación ni se opera; se solicita selección.|
|CA33|Base, Buscar «Monitor», seleccionar su única fila|Actualizar nombre a Pantalla con los demás datos iguales|Solo P002 cambia aunque ocupe la fila 0 filtrada; P001 intacto; búsqueda Monitor queda vacía, muestra confirmación y sin coincidencias.|
|CA34|Base, Buscar «Monitor», seleccionar P002|Eliminar; responder No, cerrar confirmación o responder Sí, por separado|Confirmación identifica P002/Monitor y predetermina No. Dos primeras variantes sin cambios; Sí elimina solo P002, confirma y reaplica la búsqueda.|
|CA35|Base|Abrir actualización, cambiar datos y pulsar Cancelar; repetir cerrando sin guardar; luego abrir Ingresar|Sin cambios en inventario; nuevo editor vacío, sin datos ni acciones del modo previo; Cancelar disponible y ninguna acción Eliminar en ambos modos.|
|CA36|Base; destino P999 inexistente|Solicitar actualización, eliminación, entrada o salida por ese código al controlador, por separado|Rechaza por inexistencia; no opera sobre otro producto. La interfaz comprueba también el destino antes de abrir/confirmar.|
|CA37|Base, P001 seleccionado|Registrar Entrada de 5|P001 queda en 15; demás datos y P002 intactos; confirmación y tabla actualizada inmediatamente.|
|CA38|Base, P001 seleccionado|Registrar Salida de 4; por separado Salida de 10|P001 queda en 6 o 0, respectivamente; cero permitido, sin modificar otros datos ni productos.|
|CA39|Base, P001 seleccionado|Entrada y Salida con 0, -1, vacío, «abc», 1,5, 1.5 o 2147483648, por separado; tipo inválido al controlador|Cada variante se rechaza sin redondear ni cambiar datos; errores de texto/rango en formulario y de valores/tipo en TLista.|
|CA40|Base, P001 seleccionado|Registrar Salida de 11|Se informa stock insuficiente; cantidad sigue en 10 y demás datos intactos.|
|CA41|Un producto P001 con cantidad int.MaxValue|Registrar Entrada de 1|Se rechaza por desbordamiento sin cambios; la aplicación no falla.|
|CA42|Vacío; registrar A / A / 1 / 2 / 3, B / B / 1 / 3 / 3, C / C / 1 / 4 / 3 y D / D / 0 / 0 / 0|Consultar alertas|A, B y D requieren reposición; C no. Se muestran estilo y texto con esos códigos; las cinco columnas permanecen.|
|CA43|Base|Salida de 7 de P001; por separado actualizar mínimo de P001 a 10|P001 pasa a stock bajo. Después, Entrada de 1 en cada variante lo saca de alerta; al eliminarlo desaparece su señal. Consultar no altera datos.|
|CA44|Base; por separado Vacío|Abrir resumen|Base: 2 productos / 15 unidades / 0 con stock bajo. Vacío: 0 / 0 / 0.|
|CA45|Base|Entrada de 5 en P001; Salida de 7; ingreso de P003 / Mouse / 1 / 0 / 0; ajuste de P001 a cantidad 2; eliminación de P001, por separado|Al volver al menú: 2/20/0, 2/8/1, 3/15/1, 2/7/1 y 1/5/0, respectivamente. Cada variante parte de Base.|
|CA46|Base con P001 ajustado a cantidad 3|Buscar «Monitor» y volver al resumen|Tabla solo P002, sin reposición visible; resumen global 2/8/1, sin depender del filtro.|
|CA47|Dos productos distintos con cantidad int.MaxValue y mínimo 0|Consultar resumen|2 productos, 4294967294 unidades y 0 con stock bajo; sin desbordamiento ni redondeo.|
|CA48|Base|Buscar «Mouse» e ingresar P003 / Mouse / 12 / 8 / 2; por separado buscar «Teclado» y modificar nombre a Mouse o eliminar P001; por separado mover P001 con búsqueda «P001»|Se reaplica el último término enviado: P003 aparece en primera variante, tabla vacía con confirmación/aviso en las siguientes, cantidad actualizada en movimiento. Tras reenlazar no hay selección; otro intento de operar exige seleccionarla.|
|CA49|Vacío, nueva sesión|Menú → Inventario → Ingresar Base → cerrar frmInventario → reabrir; alternar ingreso y actualización; al final cerrar el menú|Datos sin pérdidas ni duplicados y misma TLista compartida; editor retorna a frmInventario y esta al menú, resumen actualizado. El código nunca cambia; solo cerrar frmPrincipal descarta la sesión.|
|CA50|Vacío, nueva sesión; repetir desde Base, también tras buscar «Mouse» en Inventario y volver al menú|Pulsar «Registrar producto», ingresar P003 / Mouse / 12 / 8 / 2 y regresar al menú; abrir Inventario. Por separado, abrir el editor desde el menú y Cancelar o cerrar sin guardar|Editor vacío con Ingresar/Cancelar. Al guardar, menú actualizado: 1/8/0 desde Vacío o 3/23/0 desde Base; Inventario muestra P003 una vez. Si había búsqueda vigente, se reaplica al reabrir. Cancelar/cerrar no cambia datos ni resumen y retorna al menú.|

## Trazabilidad y alcance vigentes de v1.2

RF09 → EF06 → CA30–CA36, CA48–CA50; RF06 → EF07 → CA32, CA36–CA41, CA48; RF07 → EF08 → CA42–CA43, CA46; RF08 → EF09 → CA44–CA47, CA49–CA50. RF01–RF05 mantienen su trazabilidad anterior y participan en CA30–CA36 y CA48–CA50.

Solo C#, Windows Forms y .NET Framework 4.7.2; Entidades–Controlador–Formularios, lógica en TLista y memoria compartida. No bases de datos, historial de movimientos, proveedores, facturación, autenticación ni funcionalidades adicionales. Esta etapa modifica solo los cuatro documentos; la implementación y sus pruebas siguen pendientes.
