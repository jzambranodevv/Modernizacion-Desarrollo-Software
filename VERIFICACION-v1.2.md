# Verificación de v1.2

**Fecha:** 28-10-2026  
**Rama:** `feature/version-1.2`  
**Alcance:** implementación según `requirements.md`, `spec.md`, `plan.md` y `tasks.md`. Sin cambios en los contratos SDD.

## Resultado automatizado

- Compilación de `SistemaInventario.sln`: Debug y Release, ambas correctas con MSBuild de Visual Studio Insiders y targeting pack .NET Framework 4.7.2 disponible.
- Regresión y reglas de controlador: 93/93 pruebas aprobadas en Debug y 93/93 en Release; 0 fallidas. Se conservan las 52 pruebas previas y se añadieron 41 casos/variantes en `PruebasInventario/InventarioTests.cs`.
- La ejecución local del paso MSTest del workflow obtuvo el ensamblado mediante `TargetPath`, encontró el adaptador MSTest V2 y verificó 93/93 pruebas con TRX. No se ejecutó GitHub Actions remoto.
- Evidencias: `TestResults/v1.2-final-debug.trx`, `TestResults/v1.2-final-release.trx` y `TestResults/inventario.trx`.

### Criterios cubiertos automáticamente

- CA01–CA12 y CA15: validaciones y operaciones existentes de v1.0.
- CA16–CA27: consulta, búsqueda, resultados y conservación de datos de v1.1. CA28, protección de controles del DataGridView, requiere interfaz.
- CA31 y CA33–CA34: acceso del controlador por código y conservación del destino seleccionado en resultados filtrados; esto no valida la presentación del editor ni el diálogo.
- CA36–CA41: destino inexistente, entradas/salidas, positividad, stock insuficiente, saldo cero, atomicidad y desbordamiento. La conversión de texto del formulario de CA39 no se considera cubierta por estas pruebas de controlador.
- CA42–CA47: umbral inclusivo, recálculo de alertas, resumen global, cambios, independencia del filtro y suma de unidades en 64 bits.
- CA48 y la parte de datos de CA50: reaplicación de consulta y actualización del resumen/inventario tras operaciones.

## Comprobaciones manuales realizadas

En la aplicación Release de Windows se verificó:

- Desde el menú, «Registrar producto» abrió el editor vacío con Ingresar/Cancelar. Cancelar volvió al menú sin cambiar el resumen; un ingreso válido volvió al menú y actualizó el resumen.
- «Inventario» abrió la tabla de cinco columnas. Con la búsqueda parcial `mOu` se seleccionó P003; Actualizar abrió sus datos (código P003, nombre Mouse, precio 12, cantidad 8 y mínimo 2). El código apareció bloqueado y Cancelar conservó el filtro y los datos.
- Con esa búsqueda activa, Salida 6 cambió la cantidad de 8 a 2, mostró la señal de reposición para P003 y mantuvo el filtro. Entrada 1 cambió la cantidad a 3, retiró la alerta y mantuvo la búsqueda. El reenlace limpió la selección.
- Eliminar mostró «¿Eliminar P003 / Mouse?» y el foco predeterminado quedó en No. Al responder No, el producto permaneció sin cambios.
- Al volver al menú, el resumen mostró 1 producto, 3 unidades y 0 con stock bajo.
- Se envió una búsqueda sin coincidencias, se volvió al menú y se abrió frmInventario otra vez: el texto enviado y el aviso de ausencia de coincidencias se conservaron.
- Los tres formularios abrieron en el diseñador visual de Visual Studio Insiders: `frmPrincipal`, `frmInventario` y `frmProductos`. Se cargaron sus controles; no se guardaron cambios de diseño. Se corrigió el texto de diseño inicial de `frmProductos` para que represente el modo de ingreso.

## Criterios que siguen pendientes de comprobación manual

- CA13–CA14 y CA28–CA30: cobertura manual parcial. Falta completar conversiones/mensajes, probar límites de solo lectura y el ingreso desde frmInventario; CA30 sí se comprobó desde el acceso directo del menú.
- CA32: se comprobó Actualizar sin selección; faltan Eliminar y movimiento sin selección y el caso de búsqueda sin coincidencias.
- CA34: falta confirmar Sí y verificar eliminación desde una tabla filtrada con varios productos; se comprobó No y que el producto no cambia.
- CA35: falta repetir cancelación al cerrar con X y cubrir todas las combinaciones de modos/cancelación.
- CA39: faltan en el formulario entradas vacías, texto no numérico, cero, negativo, fraccionario y fuera de rango para movimientos; sus reglas de controlador sí tienen pruebas automatizadas.
- CA43: falta repetir en la interfaz los cambios de umbral por modificación y eliminación; la transición observada fue mediante salida y entrada.
- CA48–CA50: falta completar las variantes de actualización/eliminación con búsqueda que queda vacía tras operar, y el recorrido integral de CA49 con ingreso y actualización desde frmInventario.
- Falta efectuar modificaciones y guardarlas en cada diseñador; se comprobó que las tres superficies y sus controles cargan. La verificación visual no sustituye la ejecución completa de aceptación en Windows.
- Falta la ejecución remota del workflow en GitHub Actions.

## Defecto corregido durante la verificación

El texto estático de diseño de `frmProductos` todavía indicaba que el formulario permitía eliminar y modificar introduciendo un código, aunque v1.2 lo usa como editor por modos y elimina desde `frmInventario`. Se ajustaron únicamente el título y la ayuda de diseño al modo de ingreso. El comportamiento de ejecución ya cambiaba ambos textos por modo.

## Archivos y control de cambios

Además de los archivos de implementación y pruebas de v1.2, esta verificación actualiza `tasks.md` y añade este informe. Visual Studio generó un `.resx` temporal al abrir frmInventario; como el formulario no usa recursos y el archivo no estaba incluido en el proyecto, se retiró ese artefacto. Se mantuvo la rama `feature/version-1.2`; no se creó commit, push ni Pull Request.
