# Verificación — Inventario v1.1

## Alcance implementado

Rama feature/version-1.1. RF04–RF05 según EF04–EF05, sin funcionalidades de v1.2. requirements.md, spec.md y plan.md no se modificaron.

- frmPrincipal es el inicio y conserva una única TLista. Abre frmProductos y frmConsultas como ventanas modales reutilizadas; su contenedor libera ambos formularios al terminar la sesión.
- frmProductos recibe ese controlador y conserva las operaciones, conversiones y validaciones de v1.0. No contiene tabla general ni búsqueda.
- frmConsultas presenta cinco columnas de solo lectura y consulta el controlador al mostrarse, Buscar y Mostrar todos. Mantiene el último texto enviado; Mostrar todos lo limpia. Al volver desde gestión, recalcula los resultados de la búsqueda vigente.
- TLista reutiliza Productos para consultar y añade Buscar: coincidencia parcial ordinal sin distinguir mayúsculas, espacios exteriores ignorados, código o nombre, orden de registro y sin duplicados. Los resultados son instantáneas de solo lectura, no inventarios independientes.

## Compilación y pruebas ejecutadas

Herramientas locales: MSBuild y VSTest 18.11 de Visual Studio Insiders; targeting pack .NET Framework 4.7.2 y MSTest V2 2.2.10 disponibles.

| Comprobación | Resultado |
|---|---|
| Regresión previa a los cambios | 22 pruebas de v1.0 aprobadas |
| Compilación Debug | Correcta |
| Compilación Release | Correcta |
| Pruebas Debug | 52 aprobadas, 0 fallidas |
| Pruebas Release | 52 aprobadas, 0 fallidas |

Las 52 ejecuciones incluyen las 22 existentes, sin modificar TListaTests.cs, y 30 nuevas en ConsultaTests.cs. Evidencia local en TestResults/regresion-previa-v1.1.trx, TestResults/v1.1-debug.trx y TestResults/v1.1-release.trx. Los resultados de ejecución están ignorados por Git.

## Cobertura y límites

| Criterios | Verificación automática | Verificación manual pendiente |
|---|---|---|
| CA01–CA12 y CA15 | Regresión de entidad/controlador aprobada | Conversiones de precio con coma/punto de CA01 y mensajes en frmProductos |
| CA13–CA14 | No sustituidos por pruebas del controlador | Entradas no numéricas y fraccionarias, en todas sus variantes |
| CA16–CA17 | Datos completos, orden e inventario vacío | Columnas, filas y mensaje de inventario vacío |
| CA18 | Consulta nueva refleja registro, modificación y eliminación; otros datos intactos | Actualización al regresar desde gestión a Consultas |
| CA19–CA24 | Búsquedas parciales por código/nombre, mayúsculas, espacios, sin coincidencias, sustitución de resultados, vacío y sin duplicados | Búsqueda mediante controles, Mostrar todos, limpieza del campo y mensajes |
| CA25–CA27 | Repetir la búsqueda tras operaciones exitosas/rechazadas refleja correctamente los datos | Búsqueda vigente conservada al navegar, confirmaciones/errores y tabla actualizada |
| CA28 | Resultados del controlador inmutables comprobados; protección de la tabla revisada en código | Intentar editar, agregar y eliminar filas en DataGridView |
| CA29 | Una sola creación de TLista revisada en código y misma instancia entregada a ambos secundarios | Inicio, apertura/cierre repetidos, retorno al menú y datos sin pérdidas ni duplicados |

La protección de resultados tiene dos pruebas adicionales. Las comprobaciones del controlador no certifican por sí solas el comportamiento visual ni el ciclo de vida de las ventanas.

## Comprobación visual parcial ejecutada

Se abrió la aplicación real en Windows con la habilidad de control de Windows:

- Menú inicial con los dos botones y apertura de frmConsultas.
- Cinco columnas visibles y mensaje «No hay productos registrados» en Vacío.
- Búsqueda «inexistente» con tabla vacía y «No se encontraron coincidencias».
- Retorno al menú al cerrar Consultas, y apertura de frmProductos sin tabla ni búsqueda.
- Registro de P001 / Teclado / 15,50 / 10 / 3 con confirmación.
- Retorno al menú y nueva entrada en Consultas: una única fila P001, con sus cinco datos exactos, sin perder el registro.
- Búsqueda «tEc» desde los controles: se mantiene únicamente la fila de Teclado.
- Se corrigió el recorte inicial de encabezados aumentando su altura; la captura de la compilación final muestra los cinco encabezados completos.

Estas son comprobaciones parciales con un producto, no la aprobación de todas las variantes de CA16–CA29. Siguen pendientes Base completa, modificación y eliminación desde la interfaz con búsqueda vigente, protección interactiva de filas, todas las variantes de errores/conversiones y revisión del Designer. Los errores iniciales del control de ventanas («coordinate input geometry is unavailable») se recuperaron actualizando la captura y la selección de ventanas; no se presentan como fallos del sistema.

## GitHub Actions

Se mantuvieron Windows, MSTest V2 2.2.10, MSBuild, VSTest, comprobación del targeting pack y TargetPath, adaptador explícito y resultados TRX. Se añadió feature/version-1.1 al evento push, conservando feature/version-1.0. El control sigue fallando ante errores, resultados ausentes, ninguna prueba o cualquier prueba no aprobada.

Se ejecutaron localmente los bloques de compilación Release y pruebas del workflow actualizado: 52 pruebas aprobadas, con adaptador y TargetPath resueltos correctamente; evidencia adicional en TestResults/inventario.trx. La ejecución remota permanece pendiente: no se realizó push ni se inició un pipeline remoto.

## Reproducción y revisión pendiente

1. Abrir SistemaInventario.sln en Visual Studio Insiders y compilar Debug y Release.
2. Ejecutar las pruebas con VSTest y el adaptador packages/mstest.testadapter/2.2.10/build/_common; comprobar 52 ejecuciones aprobadas.
3. Iniciar el sistema, revisar el menú y entrar en Gestión de productos para registrar Base según spec.md.
4. Volver al menú y entrar en Consultas. Ejecutar CA16–CA29 con estados iniciales y variantes independientes; cerrar solo los secundarios para conservar la sesión. Verificar en especial que escribir sin Buscar no cambie la búsqueda vigente y que volver a Consultas la reaplique.
5. Repetir CA13–CA14 y la parte de formulario de CA01. Comprobar que al cerrar frmPrincipal termine la sesión.
6. Abrir y guardar los tres formularios en el Designer; revisar legibilidad, anclajes y comportamiento con la escala de pantalla del equipo.
7. Registrar resultados manuales y evidencia del pipeline cuando se autorice publicar cambios.

T10–T15 completadas; T16–T18 parciales por las verificaciones pendientes. No se crearon commits, push, Pull Requests ni etiquetas.