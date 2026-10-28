# Tareas — evolución a v1.1

**Regla:** conservar v1.0 e incorporar únicamente RF04–RF05 de v1.1 según spec.md. No anticipar v1.2. La implementación de v1.1 está autorizada; no realizar commits, push ni Pull Requests.

## Historial de v1.0

Las tareas T01–T09 siguientes describen el alcance y el último estado documentado de v1.0; sus exclusiones de consulta y búsqueda no limitan la nueva etapa v1.1. Las tareas de integración siguen sujetas a autorización posterior.

* [x] **T01 — Revisar documentación:** comprobar consistencia entre `requirements.md`, `spec.md` y `plan.md`; señalar cualquier ambigüedad antes de programar.
* [x] **T02 — Preparar solución:** crear la solución para Visual Studio y la estructura Entidades–Controlador–Formularios (.NET Framework 4.7.2).
* [x] **T03 — Generar entidad:** crear `Producto` con los cinco datos establecidos.
* [x] **T04 — Generar controlador:** desarrollar `TLista` con registro, modificación y eliminación, validaciones y resultados claros (EF01–EF03).
* [x] **T05 — Generar formulario:** diseñar `frmProductos` sencillo y funcional. Convertir las entradas según `spec.md` y delegar las reglas de negocio en `TLista`. Permitir solo selección por código para modificar/eliminar, sin consultas generales, filtros ni búsquedas avanzadas.
* [x] **T06 — Crear pruebas automáticas:** usar MSTest V2 según `plan.md` para comprobar CA01–CA12 y CA15 sobre el controlador, sin abrir el formulario. Restaurar el estado inicial de cada caso y variante; comprobar que los demás productos se conservan y registrar resultados verificables.
* [ ] **T07 — Compilar y verificar manualmente:** comprobar el targeting pack 4.7.2 y compilar la solución con MSBuild. Abrir y ejecutar la aplicación en Visual Studio; verificar CA13–CA14, los mensajes, el precio con coma o punto de CA01 y la selección por código. Registrar resultados esperados y obtenidos, y corregir errores.
* [ ] **T08 — Automatizar:** crear `.github/workflows/build.yml` en `windows-2022` según `plan.md`: comprobar o instalar el Developer Pack 4.7.2, restaurar paquetes, compilar con MSBuild y ejecutar las pruebas con VSTest y el adaptador MSTest V2. Conservar resultados TRX y fallar ante errores o ausencia de pruebas. Comprobar que el pipeline finalice correctamente.
* [ ] **T09 — Integrar v1.0:** registrar commits y crear un Pull Request obligatorio con revisión registrada. Etiquetar `v1.0` solo después de integrar el PR, con compilación, pruebas automáticas, revisión manual y pipeline aprobados.

## Verificación de la versión 1.0

* Distinguir las pruebas ejecutadas, con su resultado y evidencia, de las pendientes por limitaciones del entorno, indicando la limitación concreta. No considerar aprobada una prueba pendiente.
* Ejecutar las pruebas automatizadas del controlador para CA01–CA12 y CA15, y las pruebas manuales del formulario indicadas en T07; registrar la cobertura de CA01–CA15.
* Corregir los errores encontrados sin modificar los requerimientos establecidos.
* Comprobar que el proyecto compile correctamente en Visual Studio.
* Verificar la ejecución del pipeline de GitHub Actions.
* Registrar e integrar los cambios mediante un Pull Request.


## Estado de implementación y verificación

T01–T06 completadas. T07 parcial: solución compilada con MSBuild de Visual Studio Insiders y formulario abierto; pruebas interactivas pendientes por fallo del control de ventanas. T08 parcial: workflow configurado, pendiente de ejecución en GitHub Actions. T09 pendiente por instrucción del usuario: sin commits, push, Pull Request ni etiqueta.

Los resultados ejecutados y pendientes se detallan en [VERIFICACION-v1.0.md](VERIFICACION-v1.0.md).
## Tareas de v1.1

* [x] **T10 — Especificar v1.1:** incorporar RF04–RF05, EF04–EF05 y CA16–CA29; documentar navegación y sesión compartida, y revisar trazabilidad con plan y tareas sin alterar CA01–CA15.
* [x] **T11 — Establecer la regresión:** ejecutar las pruebas automatizadas existentes de v1.0 y registrar las verificaciones manuales pendientes, sin asumirlas aprobadas.
* [x] **T12 — Ampliar TLista:** implementar consulta y búsqueda de EF04–EF05, reutilizando el acceso existente y conservando las operaciones y validaciones de v1.0.
* [x] **T13 — Organizar formularios y navegación:** crear frmPrincipal como inicio con Gestión de productos y Consultas. Crear frmConsultas con tabla de solo lectura, cinco columnas, búsqueda y Mostrar todos. Conservar frmProductos exclusivamente para v1.0; entregar desde frmPrincipal la misma TLista a ambos secundarios, sin controladores ni inventarios duplicados. Mantener controles nativos y los tres Designer funcionales.
* [x] **T14 — Refrescar al consultar:** cargar todos en la primera apertura de frmConsultas; al regresar desde el menú, consultar TLista y reaplicar la búsqueda vigente. Buscar y Mostrar todos también leen datos actuales. Conservar resultados ante rechazos; mantener confirmaciones y errores en frmProductos. Implementar el retorno al menú y el ciclo de vida de los secundarios según plan.md.
* [x] **T15 — Ampliar pruebas automatizadas:** verificar la parte de controlador de CA16–CA27, con estados independientes, coincidencias parciales, espacios, mayúsculas, duplicados, orden e integridad de datos. Reejecutar CA01–CA12 y CA15 como regresión.
* [ ] **T16 — Compilar y verificar los formularios:** compilar ambos proyectos en Release y revisar manualmente CA16–CA29, incluidos actualización al regresar a Consultas, búsqueda vigente, mensajes y restricciones del DataGridView. Verificar menú inicial, retorno de secundarios y la misma instancia de TLista sin pérdidas ni duplicados. Repetir CA13–CA14, las conversiones de CA01 y los mensajes de v1.0 en frmProductos; comprobar los tres Designer.
* [ ] **T17 — Verificar automatización:** conservar MSTest V2, MSBuild y VSTest en Windows, targeting pack 4.7.2 y evidencia TRX. Comprobar la asignación Release y TargetPath; asegurar fallo ante pruebas fallidas o ninguna descubierta. La ejecución remota queda pendiente hasta autorizar la publicación de cambios.
* [ ] **T18 — Revisar resultados de v1.1:** registrar cobertura de CA01–CA29 y separar pruebas ejecutadas, fallidas y pendientes con sus limitaciones. Corregir defectos sin cambiar contratos para justificar errores. No declarar finalizada v1.1 mientras existan verificaciones requeridas pendientes.

## Estado de la evolución

T10–T15 completadas. T16 parcial: Debug y Release compilados; verificaciones manuales de Windows Forms y Designer pendientes. T17 parcial: workflow adaptado a feature/version-1.1, con comprobaciones locales; ejecución remota pendiente. T18 parcial: cobertura y pendientes registrados en [VERIFICACION-v1.1.md](VERIFICACION-v1.1.md), sin declarar finalizada la verificación visual. No se realizaron commits, push, Pull Requests ni etiquetas.
