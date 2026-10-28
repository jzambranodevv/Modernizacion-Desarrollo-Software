# Plan técnico — Sistema de Inventario v1.2

**Base:** requirements.md y spec.md, incluida la evolución explícita de contratos históricos. **Estado:** diseño documental; no implementar todavía.

## Tecnología y alcance

Conservar C#, Windows Forms, .NET Framework 4.7.2, archivos estándar de Visual Studio Insiders, controles nativos y Designer editable. Mantener Entidades–Controlador–Formularios, los dos proyectos existentes y datos solo en memoria. Sin dependencias visuales externas, bases de datos ni historial de movimientos.

## Distribución y navegación

* **frmPrincipal:** formulario inicial y propietario de una única TLista durante la sesión. Sustituir los accesos de v1.1 por «Registrar producto», que abre frmProductos en modo ingreso vacío, e «Inventario», que abre frmInventario. Mostrar tres indicadores globales de EF09. Refrescarlos al iniciar y al regresar de frmInventario o del ingreso directo; no duplicar contadores ni calcularlos sobre filas filtradas.
* **frmInventario (renombrado desde frmConsultas, no un formulario adicional):** conservar tabla de cinco columnas, búsqueda, Mostrar todos y búsqueda vigente. Añadir Ingresar, Actualizar y Eliminar; un grupo «Movimientos» con destino seleccionado de solo lectura, tipo Entrada/Salida, unidades y acción de registrar; y señalización de reposición de EF08. Esta distribución evita otros formularios y rutas duplicadas.
* **frmProductos:** editor modal abierto desde frmPrincipal para ingreso o desde frmInventario para ingreso/actualización, con la misma TLista, modo y código original cuando corresponde. Ingreso: campos vacíos, código editable y acciones Ingresar y Cancelar. Actualización: cargar datos actuales del controlador, código bloqueado y acciones Actualizar y Cancelar. Adaptar los botones existentes al modo; Cancelar cierra sin guardar. No ofrecer Eliminar en el editor; la eliminación se realiza exclusivamente desde frmInventario. Conservar conversiones y mensajes de v1.0.
* **Ciclo de vida:** conservar frmInventario durante la sesión para mantener la búsqueda. Crear una sesión de editor por apertura, reiniciando modo/campos y liberándola al cerrar; no conservar una selección anterior. Un guardado exitoso cierra el editor; un error lo mantiene abierto; Cancelar o cerrar sin guardar cancela. frmInventario se abre modal desde el menú y el editor modal desde el menú o frmInventario; al cerrar vuelve al formulario que lo abrió; solo cerrar el menú termina la sesión.
* **Propiedad de datos:** frmPrincipal crea TLista y la entrega tanto al editor de ingreso directo como a frmInventario, que pasa esa misma referencia a sus editores. Ningún secundario crea otra lista. Las instantáneas para presentación no son inventarios independientes. Al cerrar la aplicación, liberar formularios y perder los datos como en versiones anteriores.

## Entidades y controlador

* **Producto:** conservar los cinco datos y código inmutable. No añadir campos de auditoría ni entidades de movimiento o resumen; no se requiere historial.
* **TLista:** conservar registro, modificación, eliminación y búsqueda, con validaciones y resultados claros. Reutilizar la consulta existente y añadir solo operaciones necesarias para recuperar el producto por código, aplicar entradas/salidas, identificar stock bajo y obtener los tres totales. No crear controladores paralelos ni duplicar reglas en formularios.
* Resolver el destino mediante código normalizado y comparación ordinal sin distinguir mayúsculas; comprobar existencia al cargar y al operar. Una entrada/salida toma la cantidad actual de TLista, nunca la de una instantánea.
* Validar unidades positivas, tipo válido, stock suficiente y suma dentro de int antes de cambiar el producto. Aplicar un único cambio atómico conservando los demás datos y productos. Mantener el orden de registro y las instantáneas de solo lectura.
* TLista define cantidad <= mínimo para alertas y calcula el resumen global; sumar unidades en long. Conservar modificación directa de cantidad como ajuste absoluto de EF02, sin ejecutar otro movimiento implícito.
* El formulario convierte texto a decimal/int como antes y muestra mensajes; TLista decide todas las reglas del negocio. No introducir una arquitectura adicional para representar modos o resultados sencillos.

## Selección, confirmación y refresco

* Usar selección explícita de una sola fila y obtener el código del Producto enlazado. Nunca traducir el índice de una fila filtrada a un índice de TLista, ni usar el texto de búsqueda como código de destino.
* Actualizar, Eliminar y registrar movimiento exigen fila seleccionada; informar si falta. Revalidar existencia y mostrar código/nombre del destino. El código original del editor no se toma de un campo editable.
* Eliminar se ejecuta solo desde frmInventario, con confirmación código/nombre y No predeterminado. No o cerrar cancela; Sí llama TLista. No permitir borrado mediante tecla o fila del DataGridView.
* Tras cerrar el editor, eliminar o mover, releer TLista, reaplicar la última búsqueda enviada y recalcular alertas visibles. Limpiar selección y CurrentCell tras reenlazar; no seleccionar automáticamente otro producto.
* Mantener tabla sin edición directa, altas/bajas de filas ni ordenación que cambie el orden de registro. No añadir columnas: alertas mediante estilo y texto «Reposición:» con códigos visibles; el resumen global permanece en el menú.
* Mantener Mostrar todos y búsqueda vacía como en EF05. Cancelaciones/rechazos conservan datos y resultados; un éxito que salga del filtro muestra confirmación y aviso sin coincidencias. El resumen se refresca al retornar al menú desde Inventario o el ingreso directo, antes de habilitarlo. Tras un ingreso desde el menú, frmInventario relee TLista al abrirse y reaplica la búsqueda vigente, sin reiniciarla.

## Verificación prevista

* **Regresión automática:** conservar pruebas del controlador de CA01–CA12, CA15 y CA16–CA27. Mantener los contratos de datos y documentar las adaptaciones de interfaz de CA01–CA29 conforme a la tabla de evolución; no borrar pruebas históricas para ocultar regresiones.
* **Pruebas automatizadas nuevas:** cubrir la parte del controlador de CA30–CA48: identidad por código, atomicidad, movimientos positivos, stock cero/insuficiente, inexistencia, desbordamiento, umbral inclusivo, alertas tras cambios, resumen global e independiente del filtro, suma long y conservación de otros productos. La lógica de selección/confirmación no se certifica con pruebas del controlador.
* **Comprobaciones manuales:** modos, precarga, código bloqueado, cancelación, confirmación de eliminación, selección bajo filtro y limpieza posterior (CA30–CA36, CA48); entradas inválidas y mensajes (CA39), actualización visible (CA37–CA46), navegación/sesión compartida e ingreso desde el menú (CA49–CA50). Ejecutar las variantes de CA13–CA14 y conversión de CA01, y la regresión visual histórica adaptada, incluida la protección de celdas de CA28.
* Comprobar los tres Designer, legibilidad y señalización accesible sin depender solo del color. Registrar por criterio y variante resultados, evidencias y pendientes con su limitación; no afirmar verificación visual a partir de pruebas automáticas.
* En la implementación futura, compilar Debug y Release y ampliar MSTest V2 con MSTest.TestFramework/MSTest.TestAdapter 2.2.10 sobre .NET Framework 4.7.2. Conservar GitHub Actions Windows, restauración NuGet, targeting pack 4.7.2 (Developer Pack si falta), MSBuild, TargetPath y adaptador explícito de VSTest.
* Preparar en esa etapa el workflow para feature/version-1.2 y conservar TRX; fallar ante compilación incorrecta, pruebas fallidas o ninguna descubierta. No ejecutar publicación ni acciones remotas en esta etapa.

## Control de esta etapa

Solo actualizar requirements.md, spec.md, plan.md y tasks.md en feature/version-1.2. No modificar C#, proyectos, pruebas ni workflow; no compilar ni declarar pruebas nuevas aprobadas porque todavía no hay implementación. Sin commits, push ni Pull Requests.